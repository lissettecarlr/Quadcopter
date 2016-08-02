#ifndef __CONTROL_H_
#define __CONTROL_H_

#include "stm32f10x.h"
#include "flash.h"
#include "Vector3.h"
#include "TaskManager.h"
#include "PWM.h"

class Control{
	private:
		
		PWM &mMoto;
		float PID_ROL[3],PID_PIT[3],PID_YAW[3];
		double TimeInterval;
		double OldTime;
		float Pit_i;
	public:
		Control(PWM &Moto);
		bool ReadPID(flash info,u16 Page,u16 position);
		bool SavePID(flash info,u16 Page,u16 position);
	
	//SET------------------------------------------------
		bool SetPID_ROL(float P,float I,float D)
		{
			PID_ROL[0]=P;PID_ROL[1]=I;PID_ROL[2]=D;
			return true;
		}
		
		bool SetPID_PIT(float P,float I,float D)
		{
			PID_PIT[0]=P;PID_PIT[1]=I;PID_PIT[2]=D;
			return true;
		}
		
		bool SetPID_YAW(float P,float I,float D)
		{
			PID_YAW[0]=P;PID_YAW[1]=I;PID_YAW[2]=D;
			return true;
		}
		
	//GET-----------------------------------------------
		float GetPID_ROL(u8 PID)
		{
			if(PID ==0 || PID ==1 || PID==2)
				return PID_ROL[PID];
			else
				return 0;
		}
		float GetPID_YAW(u8 PID)
		{
			if(PID ==0 || PID ==1 || PID==2)
				return PID_YAW[PID];
			else
				return 0;
		}
		
		float GetPID_PIT(u8 PID)
		{
			if(PID ==0 || PID ==1 || PID==2)
				return PID_PIT[PID];
			else
				return 0;
		}
		
		//传入： 当前角度/陀螺仪的角速度/遥控器量
		bool PIDControl(Vector3f angle,Vector3<float> gyr,u16 RcThr,u16 RcPit,u16 RcRol,u16 PcYaw);
		
		
};


#endif
