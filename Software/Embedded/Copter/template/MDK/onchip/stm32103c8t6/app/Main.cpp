//#include "stdlib.h"
//#include "stdio.h"

#include "stm32f10x.h"
#include "Configuration.h"
#include "TaskManager.h"
#include "USART.h"
#include "I2C.h"
#include "Timer.h"
#include "ADC.h"
#include "PWM.h"
#include "flash.h"
#include "mpu6050.h"
#include "HMC5883L.h"
#include "Moto.h"
#include "Control.h"
#include "Communication.h"
#include "IMU.h"
#include "LED.h"



//Timer T1(TIM1,1,2,3); //使用定时器计，溢出时间:1S+2毫秒+3微秒
//USART com(1,115200,false);
USART com(2,115200,false);
Communication COM433(com); 


PWM pwm4(TIM4,1,1,1,1,24000);
ADC pressure(1); //PA1读取AD值
flash InfoStore(0x08000000+60*MEMORY_PAGE_SIZE,true);     //flash
//0页 存磁力计校准值  1页存PID

GPIO ledRedGPIO(GPIOA,11,GPIO_Mode_Out_PP,GPIO_Speed_50MHz);//LED GPIO
GPIO ledYewGPIO(GPIOA,12,GPIO_Mode_Out_PP,GPIO_Speed_50MHz);//LED GPIO
LED Red(ledRedGPIO);

I2C i2c(2);
Control control(pwm4);


int main()
{
	Red.On();
	tskmgr.DelayMs(500);
	mpu6050 MPU6050(i2c);
	tskmgr.DelayMs(500);
	HMC5883L mag(i2c);
	tskmgr.DelayS(1);
	IMU imu(MPU6050,mag);
	tskmgr.DelayS(1);
	
	
	
	double Updata_posture=0; //计算欧拉角 10ms
//	double Updata_Control=0; //控制 500Hz 2ms
	double Receive_data=0;  //接收数据  20ms
	double Send_data=0; //发送数据  100ms
	double Updata_hint=0; //更新提示信息状态 500ms
	
	float Vol = 0;//电压
	ledRedGPIO.SetLevel(0);//用于表示是否处于上锁状态
	ledYewGPIO.SetLevel(0);//表示系统是否忙于做其他事
	
	pwm4.SetDuty(0,0,0,0);

		
//	if(InfoStore.Read(0,0))
//如下方式写将会使IIC出错	
//		imu.init(InfoStore.Read(0,0),InfoStore.Read(0,2),InfoStore.Read(0,4),InfoStore.Read(0,6),InfoStore.Read(0,8),InfoStore.Read(0,10));

	imu.init();
	
	if(InfoStore.Read(0,0))
		mag.SetCalibrateRatioBias(InfoStore.Read(0,0),InfoStore.Read(0,2),InfoStore.Read(0,4),InfoStore.Read(0,6),InfoStore.Read(0,8),InfoStore.Read(0,10));
	

	
	while(1)
	{			
		if(tskmgr.TimeSlice(Updata_posture,0.008) ) 
		{
				//更新、获得欧拉角
				imu.UpdateIMU();
			if(!COM433.mClockState) //当解锁
				control.PIDControl(imu.mAngle,MPU6050.GetGyrDegree(),COM433.mRcvTargetThr,COM433.mRcvTargetPitch,COM433.mRcvTargetRoll,COM433.mRcvTargetYaw);		
		}

		
		if(tskmgr.TimeSlice(Receive_data,0.02) ) 
		{
			//接收
			COM433.DataListening();
			if(COM433.mMag_Calibrate == true) //磁力计校准
			{
				LOG("mag Calibrating - - - - -");
				if(imu.MagCalibrate(10)) //10s的磁力计校准
				{
					tskmgr.DelayMs(1000);//不延时的话IIC就出错了
					LOG("mag Calibrate succeed - - - - -");	
						InfoStore.Write(0,0,mag.mRatioX);
						InfoStore.Write(0,2,mag.mRatioY);
						InfoStore.Write(0,4,mag.mRatioZ);
						InfoStore.Write(0,6,mag.mBiasX);
						InfoStore.Write(0,8,mag.mBiasY);
						InfoStore.Write(0,10,mag.mBiasZ);
				}
				else
				 LOG("mag Calibrate error - - - - -");
				COM433.mMag_Calibrate = false;
			}
		}
		
		if(COM433.mGyro_Calibrate) //角速度计校准
		{
			imu.GyroCalibrate();
			COM433.mGyro_Calibrate = false;
		}
		
		
		if(tskmgr.TimeSlice(Send_data,0.1) )
		{
			//发送
			//
			if(imu.GyroIsCalibrated())
			{
				//com<<imu.mAngle.x<<"\t"<<imu.mAngle.y<<"\t"<<imu.mAngle.z<<"\n";
				//com<<MPU6050.GetAccRaw().x<<"\t"<<MPU6050.GetAccRaw().y<<"\t"<<MPU6050.GetAccRaw().z<<"\t"<<MPU6050.GetGyrRaw().x<<"\t"<<MPU6050.GetGyrRaw().y<<"\t"<<MPU6050.GetGyrRaw().z<<"\t"<<mag.GetDataRaw().x<<"\t"<<mag.GetDataRaw().y<<"\t"<<mag.GetDataRaw().z<<"\n";
				//COM433.SendCopterState(imu.mAngle.y,imu.mAngle.x,imu.mAngle.z,(u32)Vol,0,(u8)COM433.mClockState);
				COM433.SendCopterState(imu.mAngle.x,control.GetPID_ROL().Differential,control.GetPID_ROL().Proportion,(u32)Vol,0,(u8)COM433.mClockState);
				COM433.SendSensorOriginalData(MPU6050.GetAccRaw(),MPU6050.GetGyrRaw(),mag.GetNoCalibrateDataRaw());
				//COM433.SendRcvControlQuantity();//发送接收到的舵量
								
				//com<<COM433.mRcvTargetYaw<<"\t"<<COM433.mRcvTargetRoll<<"\t"<<COM433.mRcvTargetPitch<<"\t"<<COM433.mRcvTargetThr<<"\n";
			}
			if(COM433.mGetPid)//如果请求了获取PID
			{
				COM433.mGetPid=false;
				COM433.SendPID(control.GetPID_PIT().P,control.GetPID_PIT().I,control.GetPID_PIT().D,control.GetPID_ROL().P,control.GetPID_ROL().I,control.GetPID_ROL().D,control.GetPID_YAW().P,control.GetPID_YAW().I,control.GetPID_YAW().D);
			}
			
		}
		
		if(tskmgr.TimeSlice(Updata_hint,0.5) ) //提示更新
		{
			if(COM433.mClockState)
				ledYewGPIO.SetLevel(0);
			else
				ledYewGPIO.SetLevel(1);
			
			if(COM433.mMag_Calibrate)
				ledRedGPIO.SetLevel(0);
			else
				ledRedGPIO.SetLevel(1);
			
			if(!imu.GyroIsCalibrated())
				ledRedGPIO.SetLevel(0);
			else 
				ledRedGPIO.SetLevel(1);		

			if(COM433.mPidUpdata) //更新PID
			{
				COM433.mPidUpdata=false;
				control.SetPID_PIT(COM433.PID[0],COM433.PID[1],COM433.PID[2]);
				control.SetPID_ROL(COM433.PID[3],COM433.PID[4],COM433.PID[5]);
				control.SetPID_YAW(COM433.PID[6],COM433.PID[7],COM433.PID[8]);
				Red.Blink(4,100,1);
			}
			Vol=pressure.Voltage_I(1,1,3,4.2)*100;
		}
		
	}
}

