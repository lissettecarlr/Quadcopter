#include "Control.h"

Control::Control(PWM &Moto):mMoto(Moto)
{
	OldTime=0;
	
	
	//积分限幅
	pitch_angle_PID.iLimit =5;
	roll_angle_PID.iLimit =5;
	
	pitch_rate_PID.iLimit =30;
	roll_rate_PID.iLimit =30;
	//总输出限幅
	pitch_angle_PID.OLimit = 60;
	roll_angle_PID.OLimit = 60;
	
	FlyThr = 20;
	
//	SetPID_ROL(0,0,0);
//	SetPID_YAW(0,0,0);
//	SetPID_PIT(0,0,0);
//	
//	SetPID_ROL_rate(0,0,0);
//	SetPID_YAW_rate(0,0,0);
//	SetPID_PIT_rate(0,0,0);
	
}


//读取PID
bool Control::ReadPID(flash info,u16 Page,u16 position)
{
	u16 data[18];
	if(!info.Read(Page,position,data,18))
		return false;
	
	SetPID_PIT(data[0]/1000.0,data[1]/1000.0,data[2]/1000.0);
	SetPID_ROL(data[3]/1000.0,data[5]/1000.0,data[5]/1000.0);	
	SetPID_YAW(data[6]/1000.0,data[7]/1000.0,data[8]/1000.0);
	
	SetPID_PIT_rate(data[9]/1000.0,data[10]/1000.0,data[11]/1000.0);
	SetPID_ROL_rate(data[12]/1000.0,data[13]/1000.0,data[14]/1000.0);
	SetPID_YAW_rate(data[15]/1000.0,data[16]/1000.0,data[17]/1000.0);
	
	return true;
	
}
	
//保存PID
bool Control::SavePID(flash info,u16 Page,u16 position)
{
	u16 data[18];
	data[0] = pitch_angle_PID.P *1000;
	data[1] = pitch_angle_PID.I *1000;
	data[2] = pitch_angle_PID.D *1000;
	
	data[3] = roll_angle_PID.P *1000;
	data[4] = roll_angle_PID.I *1000;
	data[5] = roll_angle_PID.D *1000;
	
	data[6] = yaw_angle_PID.P *1000;
	data[7] = yaw_angle_PID.I *1000;
	data[8] = yaw_angle_PID.D *1000;
	
	data[9] = pitch_rate_PID.P *1000;
	data[10] = pitch_rate_PID.I *1000;
	data[11] = pitch_rate_PID.D *1000;
	
	data[12] = roll_rate_PID.P *1000;
	data[13] = roll_rate_PID.I *1000;
	data[14] = roll_rate_PID.D *1000;
	
	data[15] = yaw_rate_PID.P *1000;
	data[16] = yaw_rate_PID.I *1000;
	data[17] = yaw_rate_PID.D *1000;
	
	info.Clear(Page);
	if(!info.Write(Page,position,data,18))
		return false;
	return true;
}

bool Control::PIDControl(Vector3f angle,Vector3<float> gyr,u16 RcThr,u16 RcPit,u16 RcRol,u16 RcYaw)
{
	
		//规范化接收的遥控器值  1000 - 2000  平衡位置度量在50内
		if(RcThr<1000) RcThr=1000;
		if(RcThr>2000) RcThr=2000;
		if(RcPit<1000)	 RcPit=1000;
		if(RcPit>2000)	 RcPit=2000;
		if(RcPit>1450 && RcPit<1550) RcPit=1500;
//		if(RcRol<1000)	 RcRol=1000;
//		if(RcRol>2000)	 RcRol=2000;
//		if(RcRol>1450 && RcRol<1550) RcRol=1500;
//		if(RcYaw<1000)	 RcYaw=1000;
//		if(RcYaw>2000)	 RcYaw=2000;
//		if(RcYaw>1450 && RcYaw<1550) RcYaw=1500;

		float Thr=(RcThr - 1000)/10; //将接收到的油门量转化为百分比
		//float TargetRoll = (RcRol -1500)*0.08; //遥控器控制在+-40°
		float TargetPitch = (RcPit -1500)*0.08;
	
	
		//计算时间间隔
		if(OldTime ==0)
			TimeInterval = 0.008;
		else
			TimeInterval = tskmgr.Time() - OldTime;
		OldTime = tskmgr.Time();

		
//PITCH轴		
		//比例
		pitch_angle_PID.Error = TargetPitch- angle.x; //期望角度减去当前角度,这里将遥控器范围规定在+-20°
		pitch_angle_PID.Proportion = pitch_angle_PID.P * pitch_angle_PID.Error; // 区间在 P*20
		//积分
		if(Thr>FlyThr) //大于起飞油门时才开始积分
		pitch_angle_PID.CumulativeError += pitch_angle_PID.Error *TimeInterval;
		pitch_angle_PID.Integral = pitch_angle_PID.I * pitch_angle_PID.CumulativeError;
		//积分限幅  的油门量
		if(pitch_angle_PID.Integral > pitch_angle_PID.iLimit)
			pitch_angle_PID.Integral = pitch_angle_PID.iLimit;
		if(pitch_angle_PID.Integral < -pitch_angle_PID.iLimit)
			pitch_angle_PID.Integral = -pitch_angle_PID.iLimit;		
		//微分
		//pitch_angle_PID.Differential = -pitch_angle_PID.D * gyr.x;//  上偏陀螺仪为正
		pitch_angle_PID.Differential = -(pitch_angle_PID.Error -pitch_angle_PID.LastError)/TimeInterval * pitch_angle_PID.D;
		
		pitch_angle_PID.Output = pitch_angle_PID.Proportion + pitch_angle_PID.Integral+pitch_angle_PID.Differential;
		
	  pitch_angle_PID.LastError = pitch_angle_PID.Error;
	  
		
		//角速度环
		pitch_rate_PID.Error = pitch_angle_PID.Output - gyr.y;  //为正 减少14电机
		pitch_rate_PID.Proportion = pitch_rate_PID.P * pitch_rate_PID.Error;
		
		if(Thr>FlyThr) //大于起飞油门时才开始积分
		pitch_rate_PID.CumulativeError +=pitch_rate_PID.Error *TimeInterval;
		pitch_rate_PID.Integral = pitch_rate_PID.CumulativeError *pitch_rate_PID.I;
		
//		//积分限幅  的油门量
		if(pitch_rate_PID.Integral > pitch_rate_PID.iLimit)
			pitch_rate_PID.Integral = pitch_rate_PID.iLimit;
		if(pitch_rate_PID.Integral < -pitch_rate_PID.iLimit)
			pitch_rate_PID.Integral = -pitch_rate_PID.iLimit;	
		
		pitch_rate_PID.Differential = -(pitch_rate_PID.Error -pitch_rate_PID.LastError)/TimeInterval * pitch_rate_PID.D;
		
		pitch_rate_PID.Output = pitch_rate_PID.Proportion - pitch_rate_PID.Integral+pitch_rate_PID.Differential;
		
		pitch_rate_PID.LastError = pitch_rate_PID.Error;
		
	  MOTO1 = Thr - pitch_rate_PID.Output;
	  MOTO2 = Thr + pitch_rate_PID.Output;
	  MOTO3 = Thr + pitch_rate_PID.Output;	
	  MOTO4 = Thr - pitch_rate_PID.Output;
		

		
//输出
		if(MOTO1<0)
			MOTO1=0;			
		if(MOTO2<0)
			MOTO2=0;	
		if(MOTO4<0)
			MOTO4=0;			
		if(MOTO3<0)
			MOTO3=0;	

		if(Thr<FlyThr)
			mMoto.SetDuty(Thr,Thr,Thr,Thr);
		else
			mMoto.SetDuty(MOTO1,MOTO2,MOTO3,MOTO4);
		return true;
}

bool Control::SeriesPIDComtrol(Vector3f angle,Vector3<float> gyr,u16 RcThr,u16 RcPit,u16 RcRol,u16 RcYaw)
{
	
		//规范化接收的遥控器值  1000 - 2000  平衡位置度量在50内
	if(RcThr<1000) RcThr=1000;
	if(RcThr>2000) RcThr=2000;
	if(RcPit<1000)	 RcPit=1000;
	if(RcPit>2000)	 RcPit=2000;
	if(RcPit>1450 && RcPit<1550) RcPit=1500;
	if(RcRol<1000)	 RcRol=1000;
	if(RcRol>2000)	 RcRol=2000;
	if(RcRol>1450 && RcRol<1550) RcPit=1500;
	if(RcYaw<1000)	 RcYaw=1000;
	if(RcYaw>2000)	 RcYaw=2000;
	if(RcYaw>1450 && RcYaw<1550) RcYaw=1500;
	
	
	//角度环（外环）
	float TargetRcThr = (RcThr - 1000)/10;
	float TargetPitch = (RcPit-1500)*0.08; //期望角度控制在+-40°
	float TargetRoll = (RcRol -1500)*0.08;
	float TargetYaw = (RcYaw -1500)*0.08;
	
	//计算时间间隔
	if(OldTime ==0)
		TimeInterval = 0.008;
	else
		TimeInterval = tskmgr.Time() - OldTime;
	OldTime = tskmgr.Time();
	
	//角度外环
	TOOL_PID_Postion_Cal(&pitch_angle_PID,TargetPitch,angle.y,TargetRcThr,TimeInterval);
	TOOL_PID_Postion_Cal(&roll_angle_PID,TargetRoll,angle.x,TargetRcThr,TimeInterval);
	TOOL_PID_Postion_Cal(&yaw_angle_PID,TargetYaw,angle.z,TargetRcThr,TimeInterval);
	
	
	//角速度环（内环） 只是用PI , yaw只使用I
	TOOL_PID_Postion_Cal(&pitch_rate_PID,pitch_angle_PID.Output,gyr.y,TargetRcThr,TimeInterval);	
	TOOL_PID_Postion_Cal(&roll_rate_PID,roll_angle_PID.Output,gyr.x,TargetRcThr,TimeInterval);
	TOOL_PID_Postion_Cal(&yaw_rate_PID,yaw_angle_PID.Output,gyr.z,TargetRcThr,TimeInterval);
	
	
	/*
       头
   m2     m3
     \   /
      \ /
      / \
     /   \
   m1     m4
 
*/
	
	//电机控制
	
	MOTO1 = TargetRcThr - pitch_rate_PID.Output + roll_rate_PID.Output + yaw_rate_PID.Output;
	MOTO2 = TargetRcThr + pitch_rate_PID.Output + roll_rate_PID.Output - yaw_rate_PID.Output;
	MOTO3 = TargetRcThr + pitch_rate_PID.Output - roll_rate_PID.Output + yaw_rate_PID.Output;	
	MOTO4 = TargetRcThr - pitch_rate_PID.Output - roll_rate_PID.Output - yaw_rate_PID.Output;
	
	if(MOTO1 >100)
		MOTO1=100;
	if(MOTO2 >100)
		MOTO2=100;
	if(MOTO3 >100)
		MOTO3=100;
	if(MOTO4 >100)
		MOTO4=100;
	
	if(MOTO1 <0)
		MOTO1=0;
	if(MOTO2 <0)
		MOTO2=0;
	if(MOTO3 <0)
		MOTO3=0;
	if(MOTO4 <0)
		MOTO4=0;
//#ifdef DUBUG_PITCH
//	if(RcThr<FlyThr)
//		mMoto.SetDuty(RcThr,0,RcThr,0);
//	else
//		mMoto.SetDuty(MOTO1,0,MOTO3,0);
//	#endif
//	
//	#ifdef DUBUG_ROLL
//	if(RcThr<FlyThr)
//		mMoto.SetDuty(0,RcThr,0,RcThr);
//	else
//		mMoto.SetDuty(0,MOTO2,0,MOTO4);
//	#endif
	
	#ifdef NORMAL
	if(TargetRcThr<FlyThr)
		mMoto.SetDuty(TargetRcThr,TargetRcThr,TargetRcThr,TargetRcThr);
	else
		mMoto.SetDuty(MOTO1,MOTO2,MOTO3,MOTO4);
	#endif
	

//	if(TargetRcThr >10)
//		mMoto.SetDuty(0,TargetRcThr-roll_rate_PID.Output,0,TargetRcThr+roll_rate_PID.Output);
//	else
//		mMoto.SetDuty(0,TargetRcThr,0,TargetRcThr);
	
	return true;
}


bool Control::TOOL_PID_Postion_Cal(PID_Typedef * PID,float target,float measure,float Thr,double dertT)
{
//	float tempI=0; //积分项暂存
//	
//	PID->Error = target - measure; //计算误差
//	PID->Differential = (PID->Error - PID->LastError)/dertT; //计算微分值
//	PID->Output=(PID->P * PID->Error) + (PID->I * PID->Integral) + (PID->D * PID->Differential);  //PID:比例环节+积分环节+微分环节
//	PID->LastError=PID->Error;//保存误差
//	
//	
//	if( fabs(PID->Output) <Thr) //比油门还大时不积分
//	{
//		tempI=(PID->Integral) + (PID->Error) * dertT;     //积分环节
//		if(tempI>-PID->iLimit && tempI<PID->iLimit &&PID->Output > - PID->iLimit && PID->Output < PID->iLimit)//在输出小于30才累计
//			PID->Integral = tempI;
//	}
	
	
	//P
  	PID->Error = target - measure;
	  PID->Proportion = PID->P * PID->Error;
	//I
	  if(Thr>FlyThr) //油门大于起飞油门踩进行积分
	    PID->CumulativeError += PID->Error * dertT;
	  PID->Integral = PID->I * PID->CumulativeError;
	  
		if(PID->Integral > PID->iLimit)
			PID->Integral = PID->iLimit;
		if(PID->Integral < -PID->iLimit)
			PID->Integral = -PID->iLimit;	
	//D
    PID->Differential = (PID->Error -PID->LastError)/dertT * PID->D;
	
		PID->Output = PID->Proportion + PID->Integral + PID->Differential;
		
		//控制量上限
		if(PID->Output > PID->OLimit)
			PID->Output = PID->OLimit;
		if(PID->Output < -PID->OLimit)
		  PID->Output = -PID->OLimit;
		
		PID->LastError = PID->Error;
	return true;
	
}

	//SET------------------------------------------------
		bool Control::SetPID_ROL(float P,float I,float D)
		{
			roll_angle_PID.P=P;
			roll_angle_PID.I=I;
			roll_angle_PID.D=D;
			return true;
		}
		
		bool Control::SetPID_PIT(float P,float I,float D)
		{
			pitch_angle_PID.P=P;
			pitch_angle_PID.I=I;
			pitch_angle_PID.D=D;
			return true;
		}
		
		bool Control::SetPID_YAW(float P,float I,float D)
		{
			yaw_angle_PID.P=P;
			yaw_angle_PID.I=I;
			yaw_angle_PID.D=D;
			return true;
		}
		//角速度环
		bool Control::SetPID_ROL_rate(float P,float I,float D)
		{
			roll_rate_PID.P=P;
			roll_rate_PID.I=I;
			roll_rate_PID.D=D;
			return true;
		}
		
		bool Control::SetPID_PIT_rate(float P,float I,float D)
		{
			pitch_rate_PID.P=P;
			pitch_rate_PID.I=I;
			pitch_rate_PID.D=D;
			return true;
		}
		
		bool Control::SetPID_YAW_rate(float P,float I,float D)
		{
			yaw_rate_PID.P=P;
			yaw_rate_PID.I=I;
			yaw_rate_PID.D=D;
			return true;
		}
		
	//GET-----------------------------------------------
		PID_Typedef Control::GetPID_ROL()
		{
				return roll_angle_PID;
		}
		
		PID_Typedef Control::GetPID_YAW()
		{
			return yaw_angle_PID;
		}
		
		PID_Typedef Control::GetPID_PIT()
		{
			return pitch_angle_PID;
		}
		
		PID_Typedef Control::GetPID_ROL_rate()
		{
				return roll_rate_PID;
		}
		
		PID_Typedef Control::GetPID_YAW_rate()
		{
			return yaw_rate_PID;
		}
		
		PID_Typedef Control::GetPID_PIT_rate()
		{
			return pitch_rate_PID;
		}
