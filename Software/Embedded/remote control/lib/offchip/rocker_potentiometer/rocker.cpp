#include "rocker.h"



rocker::rocker(ADC &adc,u8 LX,u8 LY,u8 NX,u8 NY):mAdc(adc)
{
	mThrCH=LY;
	mYawCH=LX;
	mRollCH=NX;
	mPitchCH=NY;
}
	

bool rocker::UpdataDirctionState()
{
	u8 Xtemp,Ytemp;
	
	//左右判断
	if(mYawVal ==0)
		return 0;
	else if(mYawVal<LX_NIGHT_THRESHOLDS) //向右搬动了
		Xtemp=6;
	else if(mYawVal>LX_LEFT_THRESHOLDS) //向左搬动了
		Xtemp=4;
	else
		Xtemp=5;
	
  //上下判断
	if(mThrVal ==0)
		return 0;
  else if(mThrVal>LX_UP_THRESHOLDS)//向上搬动了
		Ytemp=2;
	else if(mThrVal<LX_DOWN_THRESHOLDS)//向下搬动了
		Ytemp=8;
	else
		Ytemp=5;
	
	if(Xtemp==4 && Ytemp==2) //左上
		mLeftDirectionState=1;
	else if(Xtemp==5 && Ytemp==2) //上
		mLeftDirectionState=2;
	else if(Xtemp==6 && Ytemp==2) //右上
		mLeftDirectionState=3;
	else if(Xtemp==4 && Ytemp==5) //左
		mLeftDirectionState=4;
	else if(Xtemp==5 && Ytemp==5) //中
		mLeftDirectionState=5;			
	else if(Xtemp==6 && Ytemp==5) //右
		mLeftDirectionState=6;
	else if(Xtemp==4 && Ytemp==8) //左下
		mLeftDirectionState=7;
	else if(Xtemp==5 && Ytemp==8) //下
		mLeftDirectionState=8;
	else if(Xtemp==6 && Ytemp==8) //右下
		mLeftDirectionState=9;
	else 
		return false;
	
	
//右摇杆状态
	
	if(mRollVal ==0)
		return 0;
	else if(mRollVal<LX_NIGHT_THRESHOLDS) //向右搬动了
		Xtemp=6;
	else if(mRollVal>LX_LEFT_THRESHOLDS) //向左搬动了
		Xtemp=4;
	else
		Xtemp=5;
	
  //上下判断
	if(mPitchVal ==0)
		return 0;
  else if(mPitchVal>LX_UP_THRESHOLDS)//向上搬动了
		Ytemp=2;
	else if(mPitchVal<LX_DOWN_THRESHOLDS)//向下搬动了
		Ytemp=8;
	else
		Ytemp=5;
	
	if(Xtemp==4 && Ytemp==2) //左上
		mNightDirectionState=1;
	else if(Xtemp==5 && Ytemp==2) //上
		mNightDirectionState=2;
	else if(Xtemp==6 && Ytemp==2) //右上
		mNightDirectionState=3;
	else if(Xtemp==4 && Ytemp==5) //左
		mNightDirectionState=4;
	else if(Xtemp==5 && Ytemp==5) //中
		mNightDirectionState=5;			
	else if(Xtemp==6 && Ytemp==5) //右
		mNightDirectionState=6;
	else if(Xtemp==4 && Ytemp==8) //左下
		mNightDirectionState=7;
	else if(Xtemp==5 && Ytemp==8) //下
		mNightDirectionState=8;
	else if(Xtemp==6 && Ytemp==8) //右下
		mNightDirectionState=9;
	else 
		return false;
	
	return true;
	
}

bool rocker::Updata()
{
		
		mThrVal=mAdc[mThrCH]/THRMAX*100;
		mYawVal=mAdc[mYawCH]/YAWMAX*100;
		mRollVal=mAdc[mRollCH]/ROLLMAX*100;
		mPitchVal=mAdc[mPitchCH]/PITCHMAX*100;
	
		if(mThrVal>100)
			mThrVal=100;
		if(mYawVal>100)
			mYawVal=100;
		if(mRollVal>100)
			mRollVal=100;
		if(mPitchVal>100)
			mPitchVal=100;
		
			
		if(mThrVal>0 && mYawVal>0 && mRollVal>0 && mPitchVal>0)
		{
			UpdataDirctionState();//摇杆方向更新
			return true;
		}
		else
			return false;
}



u8 rocker::getLeftState()
{
		return mLeftDirectionState;
}

u8 rocker::getNightState()
{
		return mNightDirectionState;
}
	
	
float rocker::getThrVal()
{
	return mThrVal;
}


float rocker::getYawVal()
{
	return mYawVal;
}
float rocker::getRollVal()
{
	return mRollVal;
}

float rocker::getPitch()
{
	return mPitchVal;
}
