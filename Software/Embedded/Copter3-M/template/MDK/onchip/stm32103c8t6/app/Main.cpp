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
#include "UranusIMU.h"

#define IMU_UPDATE_CONTROL_TIME  0.008
#define RCV_DATE_TIME            0.02
#define SEND_DATE_TIME           0.1
#define HINT_TIME                0.5


//Timer T1(TIM1,1,2,3); //使用定时器计，溢出时间:1S+2毫秒+3微秒
USART com(1,115200,false);
USART com2(2,115200,false);
USART com3(3,115200,false);

Communication WIFI(com2); 
UarnusIMU uar(com3);
AttitudeCalculation att;

PWM pwm4(TIM4,1,1,1,1,24000);
ADC pressure(1); //PA1读取AD值
flash InfoStore(0x08000000+60*MEMORY_PAGE_SIZE,true);     //flash
//0页 存磁力计校准值  1页存PID

GPIO ledRedGPIO(GPIOB,5,GPIO_Mode_Out_PP,GPIO_Speed_50MHz);//LED GPIO
GPIO ledYewGPIO(GPIOA,12,GPIO_Mode_Out_PP,GPIO_Speed_50MHz);//LED GPIO
LED Red(ledRedGPIO);

//Control control(pwm4);
int main()
{
	Red.On();
//	tskmgr.DelayMs(500);
//	mpu6050 MPU6050(i2c,500);
//	tskmgr.DelayMs(500);
//	HMC5883L mag(i2c,500);
//	tskmgr.DelayS(1);
//	IMU imu(MPU6050,mag);
//	tskmgr.DelayS(1);
	
	Control control(pwm4);
	

	control.ReadPID(InfoStore,0,0);
	
	double Updata_posture=0; //计算欧拉角 10ms
//	double Updata_Control=0; //控制 500Hz 2ms
	double Receive_date=0;  //接收数据  20ms
	double Send_date=0; //发送数据  100ms
	double Updata_hint=0; //更新提示信息状态 500ms
	
	float Vol = 0;//电压
//	float magCV[6]={0};//用于保存磁力计数据
	
	ledRedGPIO.SetLevel(0);//用于表示是否处于上锁状态
	ledYewGPIO.SetLevel(0);//表示系统是否忙于做其他事
	
	pwm4.SetDuty(0,0,0,0);

//imu.init();
	
	//if(InfoStore.Read(0,0))
	//	mag.SetCalibrateRatioBias(InfoStore.Read(0,0),InfoStore.Read(0,2),InfoStore.Read(0,4),InfoStore.Read(0,6),InfoStore.Read(0,8),InfoStore.Read(0,10));
//	mag.SetOffsetBias(231,-482,-446.5);
//	mag.SetOffsetRatio(1,0.921,0.615);

//  com3<<"AT+MODE=0\r\n";
  Vector3f ang;
  Vector3f gyr;
	while(1)
	{			
		uar.Update();
		if(tskmgr.TimeSlice(Updata_posture,0.008) ) 
		{
//			gyr.x = (uar.GetGyr().x*10) * AtR;
//			gyr.y = (uar.GetGyr().y*10) * AtR;
//			gyr.z = (uar.GetGyr().z*10) * AtR;
//			
//			ang=att.GetAngle(uar.GetAcc(), gyr ,uar.GetMag(),0.008);
			
  		if(!WIFI.mClockState) //当解锁
			{
				 // control.SeriesPIDComtrol(imu.mAngle,MPU6050.GetGyrDegree(),WIFI.mRcvTargetThr,WIFI.mRcvTargetPitch,WIFI.mRcvTargetRoll,WIFI.mRcvTargetYaw);	
           control.PIDControl(uar.GetAngle(),WIFI.mRcvTargetThr,WIFI.mRcvTargetPitch,WIFI.mRcvTargetRoll,WIFI.mRcvTargetYaw);				
				
			}
			else{
			  pwm4.SetDuty(0,0,0,0);
			}
		}

		
		if(tskmgr.TimeSlice(Receive_date,0.02) ) 
		{
			//接收
			WIFI.DataListening();
			
			if(WIFI.mAcc_Calibrate)//加速度校准
			{
			  WIFI.mAcc_Calibrate=false;
			}
			if( WIFI.mGyro_Calibrate)//角速度校准
			{
			  WIFI.mGyro_Calibrate=false;
				ledRedGPIO.SetLevel(0);
//				imu.GyroCalibrate();
			}
			if(WIFI.mMag_Calibrate)//磁力计校准
			{
			  WIFI.mMag_Calibrate=false;
				ledRedGPIO.SetLevel(0);
//				mag.StartCalibrate();
			}
		}
			
		if(tskmgr.TimeSlice(Send_date,0.1) )
		{
			//发送
			//				
				//com<<MPU6050.GetAccRaw().x<<"\t"<<MPU6050.GetAccRaw().y<<"\t"<<MPU6050.GetAccRaw().z<<"\t"<<MPU6050.GetGyrRaw().x<<"\t"<<MPU6050.GetGyrRaw().y<<"\t"<<MPU6050.GetGyrRaw().z<<"\t"<<mag.GetDataRaw().x<<"\t"<<mag.GetDataRaw().y<<"\t"<<mag.GetDataRaw().z<<"\n";
				WIFI.SendCopterState(uar.GetAngle().y,uar.GetAngle().x,uar.GetAngle().z,(u32)Vol,0,(u8)WIFI.mClockState);								
			//	WIFI.SendCopterState(ang.x,ang.y,ang.z,(u32)Vol,0,(u8)WIFI.mClockState);								
			
				WIFI.SendSensorOriginalData(uar.GetAcc(),uar.GetGyr(),uar.GetMag());
				//WIFI.SendRcvControlQuantity(control.MOTO1,control.MOTO2,control.MOTO3,control.MOTO4,control.pitch_angle_PID.Output,control.pitch_rate_PID.Output);//发送接收到的舵量
				WIFI.SendRcvControlQuantity(control.MOTO1,control.MOTO4,control.pitch_angle_PID.Proportion,control.pitch_angle_PID.Integral,control.pitch_angle_PID.Differential,control.pitch_angle_PID.Output);//发送接收到的舵量
								
								
			if(WIFI.mGetPid)//如果请求了获取PID
			{
				WIFI.mGetPid=false;
				
//				WIFI.SendPID(0x10,
//				control.GetPID_ROL().P,control.GetPID_ROL().I,control.GetPID_ROL().D,
//				control.GetPID_YAW().P,control.GetPID_YAW().I,control.GetPID_YAW().D,
//				control.GetPID_PIT().P,control.GetPID_PIT().I,control.GetPID_PIT().D
//				);
				
					WIFI.SendPID(0x10,
					control.roll_angle_PID.P,control.roll_angle_PID.I,control.roll_angle_PID.D	,
					control.yaw_angle_PID.P,control.yaw_angle_PID.I,control.yaw_angle_PID.D	,
					control.pitch_angle_PID.P,control.pitch_angle_PID.I,control.pitch_angle_PID.D				
					);	
				
//				WIFI.SendPID(0x11,
//				control.GetPID_ROL_rate().P,control.GetPID_ROL_rate().I,control.GetPID_ROL_rate().D,
//				control.GetPID_YAW_rate().P,control.GetPID_YAW_rate().I,control.GetPID_YAW_rate().D,
//				control.GetPID_PIT_rate().P,control.GetPID_PIT_rate().I,control.GetPID_PIT_rate().D
//				);
				
				
				  WIFI.SendPID(0x11,
					control.roll_rate_PID.P,control.roll_rate_PID.I,control.roll_rate_PID.D	,
					control.yaw_rate_PID.P,control.yaw_rate_PID.I,control.yaw_rate_PID.D	,
					control.pitch_rate_PID.P,control.pitch_rate_PID.I,control.pitch_rate_PID.D				
					);	
				
			}
			
		}
		
		if(tskmgr.TimeSlice(Updata_hint,0.5) ) //提示更新
		{
			com<<WIFI.mRcvTargetPitch<<"\t"<<control.pitch_angle_PID.Proportion<<"\t"<<control.pitch_angle_PID.Error<<"\t"<<control.pitch_angle_PID.P<<"\n";
			ledRedGPIO.SetLevel(1);
			
			if(WIFI.mClockState) //1锁定
				ledYewGPIO.SetLevel(0);
			else
				ledYewGPIO.SetLevel(1);
			
//			if(mag.IsCalibrated())
//				ledRedGPIO.SetLevel(1);

//			if(imu.GyroIsCalibrated())
//			{
//				//InfoStore
//				ledRedGPIO.SetLevel(1);
//			}
	
			if(WIFI.mPidUpdata) //更新PID
			{
				WIFI.mPidUpdata=false;	
				
				control.roll_angle_PID.P=WIFI.PID[0][0];
				control.roll_angle_PID.I=WIFI.PID[0][1];
				control.roll_angle_PID.D=WIFI.PID[0][2];
				
				control.yaw_angle_PID.P = WIFI.PID[0][3];
				control.yaw_angle_PID.I = WIFI.PID[0][4];
				control.yaw_angle_PID.D = WIFI.PID[0][5];
				
				control.pitch_angle_PID.P = WIFI.PID[0][6];
				control.pitch_angle_PID.I = WIFI.PID[0][7];
				control.pitch_angle_PID.D = WIFI.PID[0][8];
				
//				control.SetPID_ROL(WIFI.PID[0][0],WIFI.PID[0][1],WIFI.PID[0][2]);
//				control.SetPID_YAW(WIFI.PID[0][3],WIFI.PID[0][4],WIFI.PID[0][5]);
//				control.SetPID_PIT(WIFI.PID[0][6],WIFI.PID[0][7],WIFI.PID[0][8]);
								
				control.roll_rate_PID.P=WIFI.PID[1][0];
				control.roll_rate_PID.I=WIFI.PID[1][1];
				control.roll_rate_PID.D=WIFI.PID[1][2];
				
				control.yaw_rate_PID.P = WIFI.PID[1][3];
				control.yaw_rate_PID.I = WIFI.PID[1][4];
				control.yaw_rate_PID.D = WIFI.PID[1][5];
				
				control.pitch_rate_PID.P = WIFI.PID[1][6];
				control.pitch_rate_PID.I = WIFI.PID[1][7];
				control.pitch_rate_PID.D = WIFI.PID[1][8];
				
				
//				control.SetPID_ROL_rate(WIFI.PID[1][0],WIFI.PID[1][1],WIFI.PID[1][2]);
//				control.SetPID_YAW_rate(WIFI.PID[1][3],WIFI.PID[1][4],WIFI.PID[1][5]);
//				control.SetPID_PIT_rate(WIFI.PID[1][6],WIFI.PID[1][7],WIFI.PID[1][8]);
				
				
				control.SavePID(InfoStore,0,0);
				
				Red.Blink(4,100,1);
			}
			Vol=pressure.Voltage_I(1,1,3,4.2)*100;//电压检测
		}
		
	}
}

