#ifndef __IMU_H_
#define __IMU_H_

#include "stm32f10x.h"
#include "attitude.h"
#include "mpu6050.h"
#include "Configuration.h"
#include "TaskManager.h"
#include "HMC5883L.h"

class IMU{
	private:
		mpu6050 &mIns;
		HMC5883L &mMag;
		AttitudeCalculation mAHRS_Algorithm;
		bool mIsCalibrating;
	
	public:
		Vector3f mAngle;
		IMU(mpu6050 &Ins,HMC5883L &Mag);
		bool init();
		bool UpdateIMU(); 
	
};



#endif
