#ifndef _COMMUNICATION_H_
#define _COMMUNICATION_H_

#include "stm32f10x.h"
#include "USART.h"
#include "CRC.h"
#include "Vector3.h"


#define BYTE0(dwTemp)       ( *( (char *)(&dwTemp)		) )
#define BYTE1(dwTemp)       ( *( (char *)(&dwTemp) + 1) )
#define BYTE2(dwTemp)       ( *( (char *)(&dwTemp) + 2) )
#define BYTE3(dwTemp)       ( *( (char *)(&dwTemp) + 3) )


class Communication{
	
	private:
		USART &usart;
		bool Calibration(u8 *data,int lenth,u8 check);
	
	public:
		
//接收
	 vs16 mRcvTargetYaw;
	 vs16 mRcvTargetRoll;
	 vs16 mRcvTargetPitch;
	 vs16 mRcvTargetThr;
 	 u32 mRcvTargetHight;
	
	 bool mClockState; //1为锁定，0为解锁
		
	 bool mAcc_Calibrate;
	 bool mGyro_Calibrate;
	 bool mMag_Calibrate;
	 bool mPidUpdata;
	
	 Communication(USART &com);
	 bool DataListening();//数据接收监听
	
//	//上锁与解锁
//	 bool FlightLockControl(bool flag);
	
	
//发送
		//状态数据
	bool SendCopterState(float angle_rol, float angle_pit, float angle_yaw, s32 Hight, u8 fly_model, u8 armed);
		//传感器原始数据
	bool SendSensorOriginalData(Vector3<int> acc, Vector3<int> gyro,Vector3<int> mag);
		//接收到的控制量
	bool SendRcvControlQuantity();
		//发送PID数据
	bool SendPID(float p1_p,float p1_i,float p1_d,float p2_p,float p2_i,float p2_d,float p3_p,float p3_i,float p3_d);
		//接收应答 需要应答的功能字和校验和
	bool reply(u8 difference,u8 sum);
	
};



#endif
