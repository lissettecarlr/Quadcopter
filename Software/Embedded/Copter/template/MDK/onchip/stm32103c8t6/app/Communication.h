#ifndef _COMMUNICATION_H_
#define _COMMUNICATION_H_

#include "stm32f10x.h"
#include "USART.h"
#include "CRC.h"

class Communication{
	
	private:
		USART &usart;
		u8 FrameClock[5];
		
		u8* ProtocolClock(bool flag);//上锁协议
		u8* ProtocolControl();
	
		bool Calibration(u8 *data,int lenth,u8 check);
	
	public:
		
//接收的飞行器数据
	 float RcvYaw;
	 float RcvRoll;
	 float RcvPitch;
	 float RcvPower;
	 int RcvTime;
 	 u8 RcvHight;
	 u8 RcvThr;
	 bool ClockState; //1为锁定，0为解锁
		
	
	
	 Communication(USART &com);
	 bool DataListening();//数据接收监听
	
	//上锁与解锁
		bool FlightLockControl(bool flag);
	
	
};



#endif
