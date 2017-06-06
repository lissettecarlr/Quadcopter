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
		while(TaskManager::Time()-time<1.5) //给磁力计初始化空出1.5s时间，主要是留给IIC通信
		{}
		LOG("calibrating ... don't move!!!\n");	
		mIns.StartGyroCalibrate();//启动陀螺仪校准
		//这里不进行磁力计校准，主要是因为 校准磁力计需要拿在手中画八字
		mGyroIsCalibrating = true;
		return true;
}


bool IMU::init(float RatioX,float RatioY,float RatioZ,float BiasX,float BiasY,float BiasZ)
{
		float time = TaskManager::Time();
		mIns.Init();
	  //mIns.StartGyroCalibrate();
		if(!mMag.TestConnection(false))
			LOG("mag connection error\n");
		//mMag.Init(RatioX,RatioY,RatioZ,BiasX,BiasY,BiasZ);
		mMag.Init();
		while(TaskManager::Time()-time<1.5)
		{}
		LOG("calibrating ... don't move!!!\n");	
		mIns.StartGyroCalibrate();//启动陀螺仪校准
		mGyroIsCalibrating = true;
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
	if(mGyroIsCalibrating&&!mIns.IsGyroCalibrating())//角速度校准结束
	{
		mGyroIsCalibrating = false;
		LOG("\ncalibrate complete\n");
	}
	if(mIns.IsGyroCalibrated())//角速度已经校准了	
	{
		//Vector3<int> temp(0,0,0);
		
		mAngle = mAHRS_Algorithm.GetAngle(mIns.GetAccRaw(),mIns.GetGyr(),mMag.GetDataRaw(),mIns.GetUpdateInterval());
		//mAngle = mAHRS_Algorithm.GetAngle(mIns.GetAccRaw(),mIns.GetGyr(),temp,mIns.GetUpdateInterval());
	}
	return true;
}

bool IMU::GyroIsCalibrated()
{
	return mIns.IsGyroCalibrated();
}

bool IMU::GyroCalibrate()
{
	mIns.StartGyroCalibrate();//启动校准
	return true;
}

bool IMU::MagCalibrate(double SpendTime)
{
	mMag.StartCalibrate();
	mMagIsCalibrated = true;
	return true;
}


bool IMU::MagIsCalibrated()
{
	return mMagIsCalibrated;
}
