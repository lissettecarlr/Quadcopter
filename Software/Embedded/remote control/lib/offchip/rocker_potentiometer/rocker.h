/**
  ******************************************************************************
  * @file    
  * @author  lissettecarlr(creat)
  * @version V0.1
  * @date    7/18/2016          
  * @brief   (V0.1)飞行器遥控器驱动，不仅监测方向，还得到搬动的度量，没进行校准，所以使用
						 宏定义来区分区间，由于遥控器硬件问题，未进行测试。
  ******************************************************************************
*/

#ifndef __ROCKER_H_
#define __ROCKER_H_

#include "stm32f10x.h"
#include "ADC.h"



#define LX_NIGHT_THRESHOLDS 2
#define LX_LEFT_THRESHOLDS 3
#define LX_UP_THRESHOLDS 1
#define LX_DOWN_THRESHOLDS 0.5


#define NX_NIGHT_THRESHOLDS 1
#define NX_LEFT_THRESHOLDS 0.7
#define NX_UP_THRESHOLDS 2
#define NX_DOWN_THRESHOLDS 3


//在没写遥控器校准的时候就这么凑合着  用于求出摇杆的百分比
#define THRMAX 		3
#define ROLLMAX 	3
#define YAWMAX 		3
#define PITCHMAX	3


class rocker{

	private:
		ADC &mAdc;
		u8 mLeftDirectionState;
		u8 mNightDirectionState;
		u8 mThrCH,mYawCH,mRollCH,mPitchCH; //保存通道
		float mThrVal,mYawVal,mRollVal,mPitchVal; //用于保存摇杆的搬动量  百分百
	
		bool UpdataDirctionState();
		
	
	public:
		rocker(ADC &adc,u8 LX,u8 LY,u8 NX,u8 NY); //yaw Thr Roll pitch
		bool Updata();
		u8 getLeftState(); //左上向右数的九宫格
		u8 getNightState();
		float getThrVal();
		float getYawVal();
		float getRollVal();
		float getPitch();
};







#endif
