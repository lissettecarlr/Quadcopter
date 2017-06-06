#ifndef __MAGNETOMETER_H		
#define __MAGNETOMETER_H
#include "Vector3.h"

class Magnetometer
{
public:
	/////////////////////
	///Initialization
	/////////////////////
	virtual bool Init(bool wait=false)=0;

	//////////////////////
	///Update data from sensor to memory
	///@param wait If wait until the commang execute complete
	///@param mag The adress of data save to
	///@return if wait set to true,MOD_READY:update succed MOD_ERROR:update fail  MOD_BUSY:Update interval is too short
	///        if wait set to false,MOD_ERROR:发送更新数据失败 MOD_READY:命令将会发送（具体的发送时间取决于队列中的排队的命令的数量）MOD_BUSY:Update interval is too short
	/////////////////////
	virtual unsigned char Update(bool wait=false,Vector3<int> *mag=0)=0;
	
	
	///////////////////////
	///Get magnetometer's raw data from memory 
	///@retval magnetometer's raw data
	///////////////////////
	virtual Vector3<int> GetDataRaw() = 0;
		
	////////////////////////////////
	///获取两次更新值之间的时间间隔
	////////////////////////////////
	virtual double GetUpdateInterval() = 0;
	
	//三轴校准函数，调用之后，拿着你的小飞机绕八字，传入的参数是当你每个轴都到达峰值而不在更新，这个时间之后就退出校准
	virtual bool StartCalibrate()=0;
	
	virtual bool IsCalibrated()=0;
		
	//获取磁力计校准的偏置
	virtual Vector3f GetOffsetBias() = 0;
	
	//获取磁力计校准的比例
	virtual Vector3f GetOffsetRatio() = 0;
	
		//获取磁力计校准的偏置
	virtual void SetOffsetBias(float x,float y,float z) = 0;
	
	//获取磁力计校准的比例
	virtual void SetOffsetRatio(float x,float y,float z) = 0;
};
		
		
#endif


