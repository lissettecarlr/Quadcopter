#include "communication.h"



Communication::Communication(USART &com):usart(com)
{
	 RcvTargetYaw=0;
	 RcvTargetRoll=0;
	 RcvTargetPitch=0;
	 RcvTargetThr=0;
 	 RcvTargetHight=0;
	 ClockState = true; //1为锁定，0为解锁
		
	 Acc_Calibrate = false;
	 Gyro_Calibrate = false;
	 Mag_Calibrate = false;
	 PidUpdata = false;
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
										Acc_Calibrate = true;
										return true;
								}
								else if(data[4] == 0x02)//GYRO校准
								{
										Gyro_Calibrate = true;
										return true;
								}
								else if(data[4] == 0x04)//MAG校准
								{
									  Mag_Calibrate = true;
									  return true;
								}
								else if(data[4] == 0xa0)//飞机锁定
								{
									ClockState = true;
									return true;
								}
								else if(data[4] == 0xa1)//飞机解锁
								{
									ClockState = false;
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
						return true;
						}
						else if(ch == 0x03)//控制信息
						{
						return true;
						}
						else if(ch ==0x10) //PID更新
						{
						return true;
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
	
	usart.SendData(data_to_send, _cnt);
	return true;
}


