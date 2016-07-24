# include"IMU.h"

IMU::IMU(mpu6050 &Ins,HMC5883L &Mag):mIns(Ins),mMag(Mag)
{}
	
bool IMU::init()
{
		float time = TaskManager::Time();
		mIns.Init();
	  //测试磁力计是否存在
		if(!mMag.TestConnection(false))
			LOG("mag connection error\n");
		mMag.Init();
		while(TaskManager::Time()-time<1.5)
		{}
		mIns.StartGyroCalibrate();//启动校准
		mIsCalibrating = true;
		LOG("calibrating ... don't move!!!\n");
		return true;
}
	

bool IMU::UpdateIMU()
{
	if(MOD_ERROR== mIns.Update())
	{
		LOG("mpu6050 error\n\n\n");
		return false;
	}
	if(&mMag!=0)
	{
		if(MOD_ERROR == mMag.Update())
		{
			LOG("MAG error\n\n\n");
			return false;
		}
	}
	if(mIsCalibrating&&!mIns.IsGyroCalibrating())//角速度校准结束
	{
		mIsCalibrating = false;
		LOG("\ncalibrate complete\n");
	}
	if(mIns.IsGyroCalibrated())//角速度已经校准了	
	{
		mAngle = mAHRS_Algorithm.GetAngle(mIns.GetAccRaw(),mIns.GetGyr(),mMag.GetDataRaw(),mIns.GetUpdateInterval());
	}
	return true;
}

bool IMU::IsCalibrated()
{
	return mIns.IsGyroCalibrated();
}
