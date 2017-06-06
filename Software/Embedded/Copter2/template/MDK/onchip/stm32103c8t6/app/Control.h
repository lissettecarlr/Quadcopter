#ifndef __CONTROL_H_
#define __CONTROL_H_

#include "stm32f10x.h"
#include "flash.h"
#include "Vector3.h"
#include "TaskManager.h"
#include "PWM.h"
#include "math.h"


//#define DUBUG_PITCH
//#define DUBUG_ROLL
#define NORMAL

// PID结构体
typedef struct
{
    float P;
    float I;
    float D;
    float Desired; //期望
    float Error; //误差
	  float LastError; //上一次的误差
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
		float FlyThr; //起飞油门量 经验值，用于区分使用PID的时间
	


		//增量式PID工具
		//参数：需要更新的PID结构体，目标位置，当前位置，时间间隔
		bool TOOL_PID_Postion_Cal(PID_Typedef * PID,float target,float measure,float Thr,double dertT);
	public:
		Control(PWM &Moto);
	
		float MOTO1,MOTO2,MOTO3,MOTO4;
		PID_Typedef pitch_angle_PID;	//pitch角度环的PID
		PID_Typedef pitch_rate_PID;		//pitch角速率环的PID

		PID_Typedef roll_angle_PID;   //roll角度环的PID
		PID_Typedef roll_rate_PID;    //roll角速率环的PID

		PID_Typedef yaw_angle_PID;    //yaw角度环的PID 
		PID_Typedef yaw_rate_PID;     //yaw角速率环的PID
	
	
	
	
		bool ReadPID(flash info,u16 Page,u16 position);
		bool SavePID(flash info,u16 Page,u16 position);
	
	//SET------------------------------------------------
		bool SetPID_ROL(float P,float I,float D);

		
		bool SetPID_PIT(float P,float I,float D);

		
		bool SetPID_YAW(float P,float I,float D);

		//角速度环
		bool SetPID_ROL_rate(float P,float I,float D);

		
		bool SetPID_PIT_rate(float P,float I,float D);

		
		bool SetPID_YAW_rate(float P,float I,float D);

		
	//GET-----------------------------------------------
		PID_Typedef GetPID_ROL();
		
		PID_Typedef GetPID_YAW();

		
		PID_Typedef GetPID_PIT();

		
		PID_Typedef GetPID_ROL_rate();

		
		PID_Typedef GetPID_YAW_rate();

		
		PID_Typedef GetPID_PIT_rate();

		//传入： 当前角度/陀螺仪的角速度/遥控器量
		bool PIDControl(Vector3f angle,Vector3<float> gyr,u16 RcThr,u16 RcPit,u16 RcRol,u16 RcYaw);
		//串级PID
		bool SeriesPIDComtrol(Vector3f angle,Vector3<float> gyr,u16 RcThr,u16 RcPit,u16 RcRol,u16 RcYaw);
		
};


#endif
