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
		mpu6050 &mIns; //加速度计和陀螺仪
		HMC5883L &mMag;//磁力计
		AttitudeCalculation mAHRS_Algorithm; //姿态解算
		bool mGyroIsCalibrating;
		bool mMagIsCalibrated; //true 为校准完成
	  
	public:
		Vector3f mAngle;
		IMU(mpu6050 &Ins,HMC5883L &Mag);
		bool GyroCalibrate();
	  //参数: 留给校准多长时间
		bool MagCalibrate(double SpendTime);
		bool init();
	  //参数是磁力计校准参数
		bool init(float RatioX,float RatioY,float RatioZ,float BiasX,float BiasY,float BiasZ);
		bool UpdateIMU(); 
		bool GyroIsCalibrated();
		bool MagIsCalibrated();
	
	
};



#endif
