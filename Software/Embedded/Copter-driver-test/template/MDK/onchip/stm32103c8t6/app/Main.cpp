/*
测试PMU6050、电机、
*/

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
//#include "IMU.h"
#include "LED.h"


#define IMU_UPDATE_CONTROL_TIME  0.008
#define RCV_DATE_TIME            0.02
#define SEND_DATE_TIME           0.1
#define HINT_TIME                0.5


//Timer T1(TIM1,1,2,3); //使用定时器计，溢出时间:1S+2毫秒+3微秒
USART com(1,115200,false);
USART com2(1,115200,false);
//Communication COM433(com); 

PWM pwm4(TIM4,1,1,1,1,24000);

//ADC pressure(1); //PA1读取AD值
//flash InfoStore(0x08000000+60*MEMORY_PAGE_SIZE,true);     //flash
//0页 存磁力计校准值  1页存PID

//GPIO ledRedGPIO(GPIOB,5,GPIO_Mode_Out_PP,GPIO_Speed_50MHz);//LED GPIO
//GPIO ledGreGPIO(GPIOA,12,GPIO_Mode_Out_PP,GPIO_Speed_50MHz);//LED GPIO
//LED Red(ledRedGPIO);

I2C i2c(2);
mpu6050 mpu6050(i2c);
HMC5883L mag(i2c);

int main()
{
	mpu6050.Init();
	tskmgr.DelayMs(500);
	mag.Init();
	tskmgr.DelayS(1);
	
	double Updata_posture=0; //计算欧拉角 10ms
	double Receive_date=0;  //接收数据  20ms
	double Updata_hint=0; //更新提示信息状态 500ms
	
//	float Vol = 0;//电压
//	ledRedGPIO.SetLevel(1);//用于表示是否处于上锁状态
//	ledGreGPIO.SetLevel(1);//表示系统是否忙于做其他事
	
	pwm4.SetDuty(0,0,0,0);


	while(1)
	{			
		if(tskmgr.TimeSlice(Updata_posture,0.1) ) 
		{
				//更新、获得欧拉角
			//imu.UpdateIMU();
			//mpu6050.Update();
			if(mag.Update())
			{
			   //com<<"OK\n";
			}
					

		}
		

		
		if(tskmgr.TimeSlice(Updata_hint,1) ) //提示更新
		{
		// com<<MPU6050.GetAccRaw().x<<"\t"<<MPU6050.GetAccRaw().y<<"\t"<<MPU6050.GetAccRaw().z<<"\t"<<MPU6050.GetGyrRaw().x<<"\t"<<MPU6050.GetGyrRaw().y<<"\t"<<MPU6050.GetGyrRaw().z<<"\n";
		//	com<<mpu6050.GetAccRaw().x<<"\t"<<mpu6050.GetAccRaw().y<<"\t"<<mpu6050.GetAccRaw().z<<"\t"<<mpu6050.GetGyrRaw().x<<"\t"<<mpu6050.GetGyrRaw().y<<"\t"<<mpu6050.GetGyrRaw().z<<"\t"<<mag.GetDataRaw().x<<"\t"<<mag.GetDataRaw().y<<"\t"<<mag.GetDataRaw().z<<"\n";
       com<<"heading:"<<mag.GetDataRaw().x<<"\t"<<mag.GetDataRaw().y<<"\t"<<mag.GetDataRaw().x<<"\n";
			

		//	Vol=pressure.Voltage_I(1,1,3,4.2)*100;//电压检测
		}
		
	}
}


//USART usart1(1,115200,true);
//I2C iic(2);
//mpu6050 mpu6050_(iic);
//HMC5883L mag(iic);

//int main()
//{

////初始化mpu6050
//	if(!iic.IsHealth())//发生了错误
//		com<<"iic error\n";
//	if(!mpu6050_.Init(&iic))//iic总线错误
//	{
//		Delay::Ms(10);
//		com<<"init error1\n";
//		if(!mpu6050_.Init(&iic))//iic总线错误
//		{
//			com<<"init error2\n";
//		}
//		Delay::Ms(5);
//	}
////测试磁力计是否存在
//	if(mag.TestConnection(false))
//	{
//		com<<"success\n";
//	}
//	else
//	{
//		com<<"failed\n";
//	}	
//	
////初始化磁力计
//	if(mag.GetHealth())
//		mag.Init();
//	

//	while(1)
//	{
//		//更新mpu6050，函数自带错误检查，如果发现总线正常，但是mpu6050未初始化，则会进行初始化
//		if(!mpu6050_.Update())
//		{
//			com<<"error\n\n\n";
//		}	
//		else
//		{
//			com<<"mpu6050:"<<mpu6050_.GetAccRaw().z<<"\t";
//		}
//		
//		//更新磁力计相关数据
//		if(!mag.Update())
//		{
//			com<<"error_mag\n";
//			if(!mpu6050_.Init(true))//设置mpu6050为bypass模式
//				mpu6050_.Init(true);
//		}
//		else
//		{
//			com<<"heading:"<<mag.GetDataRaw().x<<"\t"<<mag.GetDataRaw().y<<"\t"<<mag.GetDataRaw().x<<"\n";
//		}
//		
//		//延时、串口显示、指示灯显示
//		
//		Delay::Ms(200);
//		Delay::Ms(2);
//	}
//}


