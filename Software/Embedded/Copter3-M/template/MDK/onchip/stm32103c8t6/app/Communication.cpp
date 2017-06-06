#include "communication.h"



Communication::Communication(USART &com):usart(com)
{
	 mRcvTargetYaw=0;
	 mRcvTargetRoll=0;
	 mRcvTargetPitch=0;
	 mRcvTargetThr=0;
 	 mRcvTargetHight=0;
	 mClockState = true; //1为锁定，0为解锁
		
	 mAcc_Calibrate = false;
	 mGyro_Calibrate = false;
	 mMag_Calibrate = false;
	 mPidUpdata = false;
	 mGetPid = false;
}


bool Communication::Calibration(u8 *data,int lenth,u8 check)
{
	u8 num=0; 
	for(int i=0;i<lenth;i++)
	{
		num+=data[i];
	}
	if(num == check)
		return true;
	else
		return false;
}

bool Communication::DataListening()
{
		u8 ch=0;
		u8 data[30]={0};
		u8 check=0;
		u8 num = usart.ReceiveBufferSize();
		if(num>0)
		{
				usart.GetReceivedData(&ch,1);
				if(ch == 0xaa)
				{
					usart.GetReceivedData(&ch,1);
					if(ch == 0xaf)
					{
						//功能字判断
						data[0] = 0xaa;
						data[1] = 0xaf;
						usart.GetReceivedData(&ch,1);
						if(ch == 0x01)//命令集1
						{
							data[2]= 0x01;
							while(usart.ReceiveBufferSize()<2);//等待数据
							usart.GetReceivedData(data+3,3);							
							check=data[5];							
							if( Calibration(data,data[3]+4,check )) //如果校验正确
							{
								if(data[4] == 0x01) //ACC校准
								{
										mAcc_Calibrate = true;
										return true;
								}
								else if(data[4] == 0x02)//GYRO校准
								{
										mGyro_Calibrate = true;
										return true;
								}
								else if(data[4] == 0x04)//MAG校准
								{
									  mMag_Calibrate = true;
									  return true;
								}
								else if(data[4] == 0xa0)//飞机锁定
								{
									mClockState = true;
									reply(data[2],check);
									return true;
								}
								else if(data[4] == 0xa1)//飞机解锁
								{
									mClockState = false;
									reply(data[2],check);
									return true;
								}
								else
								{
									//未知命令
									return false;
								}
							}
							else
								return false; //校准错误
						}
						else if(ch == 0x02)//命令集2
						{
							data[2]= ch;
							while(usart.ReceiveBufferSize()<3);//等待数据
							usart.GetReceivedData(data+3,3);
							check=data[5];
							if( Calibration(data,data[3]+4,check )) //如果校验正确
							{
								if(data[4] == 0x01) //PID请求
								{
										mGetPid=true;
										return true;
								}
								else if(data[4]==0XA0)//读取下位机版本信息
								{
								   return true;
								}
								else 
								{
									//未知命令
									return false;
								}
								
							}
							else //校验错误
								return false;
						
						}
						else if(ch == 0x03)//控制信息
						{
							data[2]= ch;
							while(usart.ReceiveBufferSize()<22);//等待数据
							usart.GetReceivedData(data+3,22);							
							check=data[24];							
							if( Calibration(data,data[3]+4,check )) //如果校验正确
							{								
								mRcvTargetThr =(u16)data[4]*256+ data[5];
								mRcvTargetYaw =(u16)data[6]*256+ data[7];
								mRcvTargetRoll =(u16)data[8]*256+ data[9];
								mRcvTargetPitch =(u16)data[10]*256+ data[11];
								return true;
							}
							return false;
						}
						else if(ch ==0x10 ||ch ==0x11 ||ch ==0x12) //PID更新
						{
							static u8 PIDnumber;
							PIDnumber = ch -0x10;
							if(PIDnumber>2) //防止PID数值越界
							{
								return false;
							}
							data[2]= ch;
							while(usart.ReceiveBufferSize()<20);//等待数据
							usart.GetReceivedData(data+3,20);
							check=data[22];
							if( Calibration(data,data[3]+4,check )) //如果校验正确
							{
					
									PID[PIDnumber][0]=((u16)data[4]*256+data[5])/1000.0;
									PID[PIDnumber][1]=((u16)data[6]*256+data[7])/1000.0;
									PID[PIDnumber][2]=((u16)data[8]*256+data[9])/1000.0;
								
									PID[PIDnumber][3]=((u16)data[10]*256+data[11])/1000.0;
									PID[PIDnumber][4]=((u16)data[12]*256+data[13])/1000.0;
									PID[PIDnumber][5]=((u16)data[14]*256+data[15])/1000.0;
								
									PID[PIDnumber][6]=((u16)data[16]*256+data[17])/1000.0;
									PID[PIDnumber][7]=((u16)data[18]*256+data[19])/1000.0;
									PID[PIDnumber][8]=((u16)data[20]*256+data[21])/1000.0;
									reply(ch,check);
									mPidUpdata = true;
									return true;
							}
							else
								return false;
						
						}
						else
						{
								//未知功能字
							return false;
						}
						
					}
					else
						return false; //不是帧头
				}
				else
					return false;		//不是帧头		
		}
		else
			return false;//没接收到数据
		
}

bool Communication::SendCopterState(float angle_rol, float angle_pit, float angle_yaw, s32 Hight, u8 fly_model, u8 armed)
{
	u8 _cnt=0;
	vs16 _temp;
	vs32 _temp2 = Hight;
	u8 data_to_send[30];
	
	data_to_send[_cnt++]=0xAA;
	data_to_send[_cnt++]=0xAA;
	data_to_send[_cnt++]=0x01;
	data_to_send[_cnt++]=0;
	
	_temp = (int)(angle_rol*100);
	data_to_send[_cnt++]=BYTE1(_temp);
	data_to_send[_cnt++]=BYTE0(_temp);
	_temp = (int)(angle_pit*100);
	data_to_send[_cnt++]=BYTE1(_temp);
	data_to_send[_cnt++]=BYTE0(_temp);
	_temp = (int)(angle_yaw*100);
	data_to_send[_cnt++]=BYTE1(_temp);
	data_to_send[_cnt++]=BYTE0(_temp);
	
	data_to_send[_cnt++]=BYTE3(_temp2);
	data_to_send[_cnt++]=BYTE2(_temp2);
	data_to_send[_cnt++]=BYTE1(_temp2);
	data_to_send[_cnt++]=BYTE0(_temp2);
	
	data_to_send[_cnt++] = fly_model;
	
	data_to_send[_cnt++] = armed;
	
	data_to_send[3] = _cnt-4;
	
	u8 sum = 0;
	for(u8 i=0;i<_cnt;i++)
		sum += data_to_send[i];
	data_to_send[_cnt++]=sum;
	
	//发送
	usart.SendData(data_to_send, _cnt);
	return true;
}

bool Communication::SendSensorOriginalData(Vector3<int> acc, Vector3<int> gyro,Vector3<int> mag)
{
	u8 _cnt=0;
	vs16 _temp;
	u8 data_to_send[30];
	

	data_to_send[_cnt++]=0xAA;
	data_to_send[_cnt++]=0xAA;
	data_to_send[_cnt++]=0x02;
	data_to_send[_cnt++]=0;
	
	_temp = acc.x;
	data_to_send[_cnt++]=BYTE1(_temp);
	data_to_send[_cnt++]=BYTE0(_temp);
	
	_temp = acc.y;
	data_to_send[_cnt++]=BYTE1(_temp);
	data_to_send[_cnt++]=BYTE0(_temp);
	
	_temp = acc.z;
	data_to_send[_cnt++]=BYTE1(_temp);
	data_to_send[_cnt++]=BYTE0(_temp);
	
	_temp = gyro.x;
	data_to_send[_cnt++]=BYTE1(_temp);
	data_to_send[_cnt++]=BYTE0(_temp);
	
	_temp = gyro.y;
	data_to_send[_cnt++]=BYTE1(_temp);
	data_to_send[_cnt++]=BYTE0(_temp);
	
	_temp = gyro.z;
	data_to_send[_cnt++]=BYTE1(_temp);
	data_to_send[_cnt++]=BYTE0(_temp);
	
	
	_temp = mag.x;
	data_to_send[_cnt++]=BYTE1(_temp);
	data_to_send[_cnt++]=BYTE0(_temp);
	
	_temp = mag.y;
	data_to_send[_cnt++]=BYTE1(_temp);
	data_to_send[_cnt++]=BYTE0(_temp);
	
	_temp = mag.z;
	data_to_send[_cnt++]=BYTE1(_temp);
	data_to_send[_cnt++]=BYTE0(_temp);
	
	data_to_send[3] = _cnt-4;
	u8 sum = 0;
	for(u8 i=0;i<_cnt;i++)
		sum += data_to_send[i];
	data_to_send[_cnt++] = sum;
	
	usart.SendData(data_to_send, _cnt);
	return true;
}

bool Communication::SendRcvControlQuantity(float aux1,float aux2,float aux3,float aux4,float aux5,float aux6)
{
	u8 _cnt=0;
	u8 data_to_send[30];
	vs16 _temp;
	
	data_to_send[_cnt++]=0xAA;
	data_to_send[_cnt++]=0xAA;
	data_to_send[_cnt++]=0x03;
	data_to_send[_cnt++]=0;
	
	data_to_send[_cnt++]=BYTE1(mRcvTargetThr);
	data_to_send[_cnt++]=BYTE0(mRcvTargetThr);
	
	data_to_send[_cnt++]=BYTE1(mRcvTargetYaw);
	data_to_send[_cnt++]=BYTE0(mRcvTargetYaw);
	
	data_to_send[_cnt++]=BYTE1(mRcvTargetRoll);
	data_to_send[_cnt++]=BYTE0(mRcvTargetRoll);
	
	data_to_send[_cnt++]=BYTE1(mRcvTargetPitch);
	data_to_send[_cnt++]=BYTE0(mRcvTargetPitch);
	
	
	_temp = (int)(aux1*100);
	data_to_send[_cnt++]=BYTE1(_temp);
	data_to_send[_cnt++]=BYTE0(_temp);
	
	_temp = (int)(aux2*100);
	data_to_send[_cnt++]=BYTE1(_temp);
	data_to_send[_cnt++]=BYTE0(_temp);
	
	_temp = (int)(aux3*100);
	data_to_send[_cnt++]=BYTE1(_temp);
	data_to_send[_cnt++]=BYTE0(_temp);
	
	_temp = (int)(aux4*100);
	data_to_send[_cnt++]=BYTE1(_temp);
	data_to_send[_cnt++]=BYTE0(_temp);
	
	_temp = (int)(aux5*100);
	data_to_send[_cnt++]=BYTE1(_temp);
	data_to_send[_cnt++]=BYTE0(_temp);
	
	_temp = (int)(aux6*100);
	data_to_send[_cnt++]=BYTE1(_temp);
	data_to_send[_cnt++]=BYTE0(_temp);
	


	data_to_send[3] = _cnt-4;
	u8 sum = 0;
	for(u8 i=0;i<_cnt;i++)
		sum += data_to_send[i];
	data_to_send[_cnt++] = sum;
	
	usart.SendData(data_to_send, _cnt);
	return true;
}

bool Communication::reply(u8 difference,u8 sum)
{
	u8 data_to_send[10];
	
	data_to_send[0]=0xAA;
	data_to_send[1]=0xAA;
	data_to_send[2]=0xEF;
	data_to_send[3]=2;
	data_to_send[4]=difference;
	data_to_send[5]=sum;
	
	u8 _sum = 0;
	for(u8 i=0;i<6;i++)
		_sum += data_to_send[i];
	data_to_send[6]=_sum;
	
	usart.SendData(data_to_send,7);
	return true;
}
bool Communication::SendPID(u8 PIDnumber,float p1_p,float p1_i,float p1_d,float p2_p,float p2_i,float p2_d,float p3_p,float p3_i,float p3_d)
{
	u8 data_to_send[30]={0};
	//u8 _cnt=0;
	vs16 _temp=0;
	
	data_to_send[0]=0xAA;
	data_to_send[1]=0xAA;
	data_to_send[2]=PIDnumber;
	data_to_send[3]=18;
	
	_temp = (int)(p1_p*1000);
	data_to_send[4]=BYTE1(_temp);
	data_to_send[5]=BYTE0(_temp);
	
	_temp = (int)(p1_i*1000);
	data_to_send[6]=BYTE1(_temp);
	data_to_send[7]=BYTE0(_temp);
	
	_temp = (int)(p1_d*1000);
	data_to_send[8]=BYTE1(_temp);
	data_to_send[9]=BYTE0(_temp);
	
	_temp = (int)(p2_p*1000);
	data_to_send[10]=BYTE1(_temp);
	data_to_send[11]=BYTE0(_temp);
	
	_temp = (int)(p2_i*1000);
	data_to_send[12]=BYTE1(_temp);
	data_to_send[13]=BYTE0(_temp);
	
	_temp = (int)(p2_d*1000);
	data_to_send[14]=BYTE1(_temp);
	data_to_send[15]=BYTE0(_temp);
	
	_temp = (int)(p3_p*1000);
	data_to_send[16]=BYTE1(_temp);
	data_to_send[17]=BYTE0(_temp);
	
	_temp = (int)(p3_i*1000);	
	data_to_send[18]=BYTE1(_temp);
	data_to_send[19]=BYTE0(_temp);
	
	_temp = (int)(p3_d*1000);
	data_to_send[20]=BYTE1(_temp);
	data_to_send[21]=BYTE0(_temp);
	
	//data_to_send[3] = _cnt-4;
	u8 sum = 0;
	for(u8 i=0;i<=21;i++)
		sum += data_to_send[i];
	
	data_to_send[22]=sum;
	usart.SendData(data_to_send, 23);
	
	
	return true;
}

void Communication::SendMotoMsg(u16 m_1,u16 m_2,u16 m_3,u16 m_4,u16 m_5,u16 m_6,u16 m_7,u16 m_8)
{
	u8 data_to_send[30];
	u8 _cnt=0;
	vs16 _temp;
	
	
	data_to_send[_cnt++]=0xAA;
	data_to_send[_cnt++]=0xAA;
	data_to_send[_cnt++]=0x06;
	data_to_send[_cnt++]=0;
	
	data_to_send[_cnt++]=BYTE1(m_1);
	data_to_send[_cnt++]=BYTE0(m_1);
	data_to_send[_cnt++]=BYTE1(m_2);
	data_to_send[_cnt++]=BYTE0(m_2);
	data_to_send[_cnt++]=BYTE1(m_3);
	data_to_send[_cnt++]=BYTE0(m_3);
	data_to_send[_cnt++]=BYTE1(m_4);
	data_to_send[_cnt++]=BYTE0(m_4);
	data_to_send[_cnt++]=BYTE1(m_5);
	data_to_send[_cnt++]=BYTE0(m_5);
	data_to_send[_cnt++]=BYTE1(m_6);
	data_to_send[_cnt++]=BYTE0(m_6);
	data_to_send[_cnt++]=BYTE1(m_7);
	data_to_send[_cnt++]=BYTE0(m_7);
	data_to_send[_cnt++]=BYTE1(m_8);
	data_to_send[_cnt++]=BYTE0(m_8);
	
	data_to_send[3] = _cnt-4;
	
	u8 sum = 0;
	for(u8 i=0;i<_cnt;i++)
		sum += data_to_send[i];
	
	data_to_send[_cnt++]=sum;
	
	usart.SendData(data_to_send, _cnt);
}	

void Communication::test(float a,float b,float c,float d,float e,float f,float g,float h,float i)
{
	u8 _cnt=0;
	vs16 _temp;
	u8 data_to_send[30];
	

	data_to_send[_cnt++]=0xAA;
	data_to_send[_cnt++]=0xAA;
	data_to_send[_cnt++]=0x02;
	data_to_send[_cnt++]=0;
	
	
	_temp = (int)(a*100);
	data_to_send[_cnt++]=BYTE1(_temp);
	data_to_send[_cnt++]=BYTE0(_temp);
	
	_temp = (int)(b*100);
	data_to_send[_cnt++]=BYTE1(_temp);
	data_to_send[_cnt++]=BYTE0(_temp);
	
	_temp = (int)(c*100);
	data_to_send[_cnt++]=BYTE1(_temp);
	data_to_send[_cnt++]=BYTE0(_temp);
	
	_temp = (int)(d*100);
	data_to_send[_cnt++]=BYTE1(_temp);
	data_to_send[_cnt++]=BYTE0(_temp);
	
	_temp = (int)(e*100);
	data_to_send[_cnt++]=BYTE1(_temp);
	data_to_send[_cnt++]=BYTE0(_temp);
	
	_temp = (int)(f*100);
	data_to_send[_cnt++]=BYTE1(_temp);
	data_to_send[_cnt++]=BYTE0(_temp);
	
	
	_temp = (int)(g*100);
	data_to_send[_cnt++]=BYTE1(_temp);
	data_to_send[_cnt++]=BYTE0(_temp);
	
	_temp = (int)(h*100);
	data_to_send[_cnt++]=BYTE1(_temp);
	data_to_send[_cnt++]=BYTE0(_temp);
	
	_temp = (int)(i*100);
	data_to_send[_cnt++]=BYTE1(_temp);
	data_to_send[_cnt++]=BYTE0(_temp);
	
	data_to_send[3] = _cnt-4;
	u8 sum = 0;
	for(u8 i=0;i<_cnt;i++)
		sum += data_to_send[i];
	data_to_send[_cnt++] = sum;
	
	usart.SendData(data_to_send, _cnt);

}
