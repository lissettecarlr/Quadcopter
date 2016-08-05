#ifndef __CONTROL_H_
#define __CONTROL_H_

#include "stm32f10x.h"
#include "flash.h"
#include "Vector3.h"
#include "TaskManager.h"
#include "PWM.h"



// PID结构体
typedef struct
{
    float P;
    float I;
    float D;
    float Desired; //期望
    float Error; //误差
    float CumulativeError;//积分累计误差
		float Proportion; //比例项
    float Integral;//积分项
    float Differential;//微分项
		float iLimit;//积分限制
    float Output;//最后输出
		float OLimit;//输出限幅
 
}PID_Typedef;



class Control{
	private:
		
		PWM &mMoto;
		double TimeInterval;
		double OldTime;
	
		PID_Typedef pitch_angle_PID;	//pitch角度环的PID
		PID_Typedef pitch_rate_PID;		//pitch角速率环的PID

		PID_Typedef roll_angle_PID;   //roll角度环的PID
		PID_Typedef roll_rate_PID;    //roll角速率环的PID

		PID_Typedef yaw_angle_PID;    //yaw角度环的PID 
		PID_Typedef yaw_rate_PID;     //yaw角速率环的PID

	
	
	public:
		Control(PWM &Moto);
		bool ReadPID(flash info,u16 Page,u16 position);
		bool SavePID(flash info,u16 Page,u16 position);
	
	//SET------------------------------------------------
		bool SetPID_ROL(float P,float I,float D)
		{
			roll_angle_PID.P=P;
			roll_angle_PID.I=I;
			roll_angle_PID.D=D;
			return true;
		}
		
		bool SetPID_PIT(float P,float I,float D)
		{
			pitch_angle_PID.P=P;
			pitch_angle_PID.I=I;
			pitch_angle_PID.D=D;
			return true;
		}
		
		bool SetPID_YAW(float P,float I,float D)
		{
			yaw_angle_PID.P=P;
			yaw_angle_PID.I=I;
			yaw_angle_PID.D=D;
			return true;
		}
		
	//GET-----------------------------------------------
		PID_Typedef GetPID_ROL()
		{
				return roll_angle_PID;
		}
		
		PID_Typedef GetPID_YAW()
		{
			return yaw_angle_PID;
		}
		
		PID_Typedef GetPID_PIT()
		{
			return pitch_angle_PID;
		}
		
		//传入： 当前角度/陀螺仪的角速度/遥控器量
		bool PIDControl(Vector3f angle,Vector3<float> gyr,u16 RcThr,u16 RcPit,u16 RcRol,u16 PcYaw);
		//串级PID
		bool SeriesPIDComtrol(Vector3f angle,Vector3<float> gyr,u16 RcThr,u16 RcPit,u16 RcRol,u16 PcYaw);
		
		
};


#endif
