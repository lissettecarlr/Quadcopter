#include "Control.h"

Control::Control(PWM &Moto):mMoto(Moto)
{
	OldTime=0;
	
	//积分限幅
	pitch_angle_PID.iLimit =5;
	roll_angle_PID.iLimit =5;
	//总输出限幅
	pitch_angle_PID.OLimit = 50;
	roll_angle_PID.OLimit = 50;
	
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
	data[0] = pitch_angle_PID.P *1000;
	data[1] = pitch_angle_PID.I *1000;
	data[2] = pitch_angle_PID.D *1000;
	
	data[3] = roll_angle_PID.P *1000;
	data[4] = roll_angle_PID.I *1000;
	data[5] = roll_angle_PID.D *1000;
	
	data[6] = yaw_angle_PID.P *1000;
	data[7] = yaw_angle_PID.I *1000;
	data[8] = yaw_angle_PID.D *1000;
	
	info.Clear(Page);
	if(!info.Write(Page,position,data,9))
		return false;
	return true;
}

bool Control::PIDControl(Vector3f angle,Vector3<float> gyr,u16 RcThr,u16 RcPit,u16 RcRol,u16 PcYaw)
{
	
		float MOTO1=0,MOTO2=0,MOTO3=0,MOTO4=0;
		float Thr=(RcThr - 1000)/10; //将接收到的油门量转化为百分比
	
		//计算时间间隔
		if(OldTime ==0)
			TimeInterval = 0.008;
		else
			TimeInterval = tskmgr.Time() - OldTime;
		OldTime = tskmgr.Time();
		
//PITCH轴		
		//比例
		pitch_angle_PID.Error = (RcPit -1500)*0.04- angle.y; //期望角度减去当前角度,这里将遥控器范围规定在+-20°
		pitch_angle_PID.Proportion = pitch_angle_PID.P * pitch_angle_PID.Error; // 区间在 P*20
		//积分
		pitch_angle_PID.CumulativeError += pitch_angle_PID.Error *TimeInterval;
		pitch_angle_PID.Integral = pitch_angle_PID.I * pitch_angle_PID.CumulativeError;
		//积分限幅5%的油门量
		if(pitch_angle_PID.Integral > pitch_angle_PID.iLimit)
			pitch_angle_PID.Integral = pitch_angle_PID.iLimit;
		if(pitch_angle_PID.Integral < -pitch_angle_PID.iLimit)
			pitch_angle_PID.Integral = -pitch_angle_PID.iLimit;		
		//微分
		pitch_angle_PID.Differential = -pitch_angle_PID.D * gyr.y;//  下偏陀螺仪为正，上偏为负
		pitch_angle_PID.Output = pitch_angle_PID.Proportion + pitch_angle_PID.Integral+pitch_angle_PID.Differential;
		
		//PID总和限幅
		if(pitch_angle_PID.Output >pitch_angle_PID.OLimit)
			pitch_angle_PID.Output=pitch_angle_PID.OLimit;
		if(pitch_angle_PID.Output<-pitch_angle_PID.OLimit)
			pitch_angle_PID.Output=-pitch_angle_PID.OLimit;
		
		MOTO1 = Thr - pitch_angle_PID.Output;
		MOTO3 = Thr + pitch_angle_PID.Output;
		
//ROLL轴
//思考例子：当前-20度，也就是飞机向左偏了，目标是0度，误差就是20，由于向0度运动时陀螺仪是正数，于是微分项添加一个负号
//要想回到0，MOTO2要减速,MOTO4要加速		
		//比例
		roll_angle_PID.Error = (RcRol -1500)*0.04- angle.x; //期望角度减去当前角度,这里将遥控器范围规定在+-20°
		roll_angle_PID.Proportion = roll_angle_PID.P *roll_angle_PID.Error; // 区间在 P*20
		//积分
		roll_angle_PID.CumulativeError += roll_angle_PID.Error *TimeInterval;
		roll_angle_PID.Integral = roll_angle_PID.I * roll_angle_PID.CumulativeError;
		//积分限幅5%的油门量
		if(roll_angle_PID.Integral > roll_angle_PID.iLimit)
			roll_angle_PID.Integral = roll_angle_PID.iLimit;
		if(roll_angle_PID.Integral < -roll_angle_PID.iLimit)
			roll_angle_PID.Integral = -roll_angle_PID.iLimit;		
		//微分
		roll_angle_PID.Differential = -roll_angle_PID.D * gyr.x*0.01;//微分  左偏为负 右偏为正
		
		roll_angle_PID.Output = roll_angle_PID.Proportion + roll_angle_PID.Integral+roll_angle_PID.Differential;
		
		//PID总和限幅
		if(roll_angle_PID.Output >roll_angle_PID.OLimit)
			roll_angle_PID.Output=roll_angle_PID.OLimit;
		if(roll_angle_PID.Output<-roll_angle_PID.OLimit)
			roll_angle_PID.Output=-roll_angle_PID.OLimit;
		
		MOTO2 = Thr - roll_angle_PID.Output;
		MOTO4 = Thr + roll_angle_PID.Output;
		
		
		
//输出		
		if(RcThr<1200)
			mMoto.SetDuty(0,Thr,0,Thr);
		else
			mMoto.SetDuty(0,MOTO2,0,MOTO4);
		
		return true;
}

bool Control::SeriesPIDComtrol(Vector3f angle,Vector3<float> gyr,u16 RcThr,u16 RcPit,u16 RcRol,u16 PcYaw)
{
//	float O_Roll_error_angle=0;
//	float O_Roll_PIDout=0,I_Roll_PIDout=0;
//	float MOTO2=0,MOTO4=0;
//	float Thr=(RcThr - 1000)/10; //将接收到的油门量转化为百分比
//	
//		//计算时间间隔
//	if(OldTime ==0)
//		TimeInterval = 0.008;
//	else
//		TimeInterval = tskmgr.Time() - OldTime;
//	OldTime = tskmgr.Time();

//	
//	//roll外环（PI）
//	O_Roll_error_angle = (RcRol -1500)*0.08- angle.y;   //遥控器 范围+-40°
//	Rol_i += O_Roll_error_angle *TimeInterval;
//	
//	//积分限幅5%的油门量
//	if(Rol_i>5)
//		Rol_i=5;
//	if(Rol_i<-5)
//		Rol_i=-5;
//	
//	
//	//-------------Roll 外环计算输出-------------------//
//	O_Roll_PIDout = PID_ROL[0] * O_Roll_error_angle + PID_ROL[1]* Rol_i;
//	
//	
//	//roll内环积分
//	Rol_error+=gyr.x-O_Roll_PIDout;
//		if(Rol_error>5)
//		Rol_error=5;
//	if(Rol_error<-5)
//		Rol_error=-5;
//	
//	I_Roll_PIDout= (O_Roll_PIDout+gyr.x)*PID_YAW[0]+Rol_error*PID_YAW[1]+(gyr.x - lastAngle_gx)*PID_YAW[2];
//	
//		//PID总和限幅
//	if(I_Roll_PIDout >50)
//		I_Roll_PIDout=50;
//	if(I_Roll_PIDout<-50)
//		I_Roll_PIDout=-50;
//	
//	lastAngle_gx = gyr.x;
//	
//	//电机输出
//		MOTO2 = Thr - I_Roll_PIDout;
//		MOTO4 = Thr + I_Roll_PIDout;
//	
//	mMoto.SetDuty(0,MOTO2,0,MOTO4);
//	return true;

}

