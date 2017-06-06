#ifndef __UARNUSIMU_H
#define __UARNUSIMU_H

#include "stm32f10x.h"
#include "USART.h"
#include "Vector3.h"

class UarnusIMU{
	
	private:
		USART &mCom;
		Vector3<int> acc;
	  Vector3<int> gyr;
	  Vector3<int> mag;
	  Vector3f angle; //X横滚,Y俯仰
	
	 
	public:
		//currectCrc: 用于保存校验值的16位比变量地址。协议是低位在前高位在后校验值
	  //src：用于校验的数组
	  //lengthInBytes ：数组长度
	   void crc16_update(uint16_t *currectCrc, const uint8_t *src, uint32_t lengthInBytes);	
	
	  UarnusIMU(USART &com);
	  bool Update();
	
	  Vector3<int> GetAcc();
	//  Vector3<int> GetOrgGyr();
  	  Vector3<int> GetGyr();
//	  Vector3f GetGyrDegree();
	  Vector3<int> GetMag();
	  Vector3f GetAngle();
	
};



#endif
