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
#include "attitude.h"
#include "Control.h"
#include "Communication.h"





//Timer T1(TIM1,1,2,3); //使用定时器计，溢出时间:1S+2毫秒+3微秒
USART com2(2,115200,false);
Communication COM433(com2); 
I2C imu(2); 
mpu6050 IMU(imu);
PWM pwm4(TIM4,1,1,1,1,24000);
ADC pressure(1); //PA1读取AD值
//flash InfoStore(0x08000000+100*MEMORY_PAGE_SIZE,true);     //flash

GPIO ledRedGPIO(GPIOA,11,GPIO_Mode_Out_PP,GPIO_Speed_50MHz);//LED GPIO
GPIO ledGREGPIO(GPIOA,12,GPIO_Mode_Out_PP,GPIO_Speed_50MHz);//LED GPIO

int main()
{
	bool clock = true;  //锁定
	
	double Updata_posture_control=0; //计算欧拉角和自稳时间片 10ms
	double Receive_data=0;  //接收数据  20ms
	double Send_data=0; //发送数据  100ms
	double Updata_hint=0; //更新提示信息状态 500ms
	
	ledRedGPIO.SetLevel(0);
	ledGREGPIO.SetLevel(0);
	
	Vector3<int> acc;
	Vector3<int> gyro;
	
	pwm4.SetDuty(0,30,70,100);
	
	float v=0;
	
	while(1)
	{			
			ledRedGPIO.SetLevel(0);
			ledGREGPIO.SetLevel(0);
			tskmgr.DelayMs(500);
			ledRedGPIO.SetLevel(1);
			ledGREGPIO.SetLevel(1);
			tskmgr.DelayMs(500);
		
		if(tskmgr.TimeSlice(Updata_posture_control,0.01) )
		{
				IMU.Update(false,&acc,&gyro);
		}
		if(tskmgr.TimeSlice(Receive_data,0.02) )
		{
		
		}
		if(tskmgr.TimeSlice(Send_data,0.1) )
		{
				com2<<"test!!\n";
				com2<<acc.x<<acc.y<<acc.z;
		}
		if(tskmgr.TimeSlice(Updata_hint,0.5) )
		{
			
			if(clock == true)
			{}
			else
			{}
			
			v=pressure.Voltage_I(1,1,3,4.2);
			com2<<v;
			
		}
		
	}
}

