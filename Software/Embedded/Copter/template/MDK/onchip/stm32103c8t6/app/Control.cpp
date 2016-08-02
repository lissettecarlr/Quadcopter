#include "Control.h"

Control::Control(PWM &Moto):mMoto(Moto)
{
	OldTime=0;
	Pit_i=0;
}


//读取PID
bool Control::ReadPID(flash info,u16 Page,u16 position)
{
	u16 data[9];
	if(!info.Read(Page,position,data,9))
		return false;
	
	SetPID_PIT(data[0]/1000.0,data[1]/1000.0,data[2]/1000.0);
	SetPID_ROL(data[3]/1000.0,data[2]/1000.0,data[5]/1000.0);	
	SetPID_YAW(data[6]/1000.0,data[7]/1000.0,data[8]/1000.0);
	
	return true;
	
}
	
//保存PID
bool Control::SavePID(flash info,u16 Page,u16 position)
{
	u16 data[9];
	data[0] = PID_PIT[0] *1000;
	data[1] = PID_PIT[1] *1000;
	data[2] = PID_PIT[2] *1000;
	
	data[3] = PID_ROL[0] *1000;
	data[4] = PID_ROL[1] *1000;
	data[5] = PID_ROL[2] *1000;
	
	data[6] = PID_YAW[0] *1000;
	data[7] = PID_YAW[1] *1000;
	data[8] = PID_YAW[2] *1000;
	
	info.Clear(Page);
	if(!info.Write(Page,position,data,9))
		return false;
	return true;
}

bool Control::PIDControl(Vector3f angle,Vector3<float> gyr,u16 RcThr,u16 RcPit,u16 RcRol,u16 PcYaw)
{
		float Pitch_angle_error=0,Roll_angle_error=0,Yaw_angle=0;
		float Pitch_P_out=0,Pitch_I_out=0,Pitch_D_out=0;
		float Pitch_PID=0;
		float MOTO1=0,MOTO2=0,MOTO3=0,MOTO4=0;
		//计算时间间隔
		if(OldTime ==0)
			TimeInterval = 0.01;
		else
			TimeInterval = tskmgr.Time() - OldTime;
		OldTime = tskmgr.Time();
		
//PITCH轴
		Pitch_angle_error = -(RcPit -1500)*0.04- angle.y; //期望角度减去当前角度,这里将遥控器范围规定在+-20°
		Pitch_P_out = PID_PIT[0] * Pitch_angle_error; // 区间在 P*20
		Pit_i += Pitch_angle_error *TimeInterval; //积分
		//积分限幅5%的油门量
		if(Pit_i>5)
			Pit_i=5;
		if(Pit_i<-5)
			Pit_i=-5;
		
		Pitch_I_out = PID_PIT[1] * Pit_i;
		Pitch_D_out = -PID_PIT[2] * gyr.y;//微分 
		
		Pitch_PID = Pitch_P_out + Pitch_I_out +Pitch_D_out;
		
		//PID总和限幅
		if(Pitch_PID >50)
			Pitch_PID=50;
		if(Pitch_PID<-50)
			Pitch_PID=-50;
		
		MOTO1 = (RcThr - 1000)/10 - Pitch_PID;
		MOTO3 = (RcThr - 1000)/10 + Pitch_PID;
		
//ROLL轴
		
		mMoto.SetDuty(MOTO1,MOTO2,MOTO3,MOTO4);
		return true;
}

