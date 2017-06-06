#include "stdlib.h"
#include "GPIO.h"
#include "USART.h"
#include "Interrupt.h"
#include "TaskManager.h"
#include "UranusIMU.h"





USART com1(1,9600,false);   //USART1
USART com2(2,115200,false);
USART com3(3,9600,false);
   
UarnusIMU imu(com2);

//GPIO Beep(GPIOB,12,GPIO_Mode_Out_PP,GPIO_Speed_50MHz);

//HMI out(com);  //ÏÔÊ¾Æ÷
//PA1  Í¨µÀ1
//ADC adc(2); 


u8 data[32]={0x5A,0xA5,
0x1C,0x00, 
//0x90,0xD4,
0xA0,0xE4,0xFF,0x30,0x00,0xE4,0x03,
0xB0,0x00,0x00,0,0,0,0, 
0xC0,0x7A,0x00,0x32,0x00,0x27,0xFF, 
0xD0,0x5D,0xFF,0xE9,0xFE,0xDB,0x05};

uint16_t che=0;
u8 data1[10]={1,2,3,4,5,6,7,8,9,0};

int main(){

  double doing=0;
	double doing2=0;
	
//	while(1)
//	{
//		if(tskmgr.ClockTool(doing,2))
//		{
//			imu.crc16_update(&che,data,32);
//			com1<<(u8)che<<"\t"<<(che>>8)<<"\n";
//			
//		}	
//	}
	
	while(1)
	{
		
//	  if(tskmgr.ClockTool(doing,0.02))
//		{
			//imu.Update();
//		}
		if(tskmgr.ClockTool(doing2,3))
		{
		  //com1<<imu.GetAcc().x<<"\t"<<imu.GetAcc().y<<"\t"<<imu.GetAcc().z<<"\n";
		  // com1<<imu.GetGyr().x<<"\t"<<imu.GetGyr().y<<"\t"<<imu.GetGyr().z<<"\n";
		//	com1<<imu.GetMag().x<<"\t"<<imu.GetMag().y<<"\t"<<imu.GetMag().z<<"\n";
			//com1<<imu.GetAngle().x<<"\t"<<imu.GetAngle().y<<"\t"<<imu.GetAngle().z<<"\n";
					
			com1<<"1234567890";//1
			com1<<"1234567890";//2
			com1<<"1234567890";//3
			com1<<"1234567890";//4
			com1<<"1234567890";//5
			com1<<"1234567890";//6
			com1<<"1234567890";//7
			com1<<"1234567890";//8
			com1<<"1234567890";//9
			com1<<"1234567890";//0
			
			
		}
		
	}

	
}

