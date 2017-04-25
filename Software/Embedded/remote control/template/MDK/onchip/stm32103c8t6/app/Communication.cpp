#include "communication.h"



Communication::Communication(USART &com):usart(com)
{
		RcvYaw =0;
		RcvRoll =0;
		RcvPitch = 0;
		RcvPower=0;
		RcvTime=0;
		RcvHight =0;
		RcvThr=0;
		ClockState =1; //1为锁定，0为解锁
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
		u8 data[20]={0};
		//u8 check=0;
		u8 num = usart.ReceiveBufferSize();
		if(num>3)
		{
				usart.GetReceivedData(&ch,1);
				if(ch == 0xaa)
				{
					usart.GetReceivedData(&ch,1);
					if(ch == 0xaa)
					{
						//命令字判断
						usart.GetReceivedData(&ch,1);
						if(ch == 0x01)//状态数据
						{
							while(usart.ReceiveBufferSize()<14);//等待数据
							usart.GetReceivedData(data,14);
//							check=data[13];							
//							if( Calibration(data,13,check )) //如果校验正确
//							{
							RcvRoll=(float)(((u16)data[1])<<8+data[2])/100;
							RcvPitch=(float)(((u16)data[3])<<8+data[4])/100;
							RcvYaw=(float)(((u16)data[5])<<8+data[6])/100;
							
							RcvPower=((u32)data[7])<<24 + ((u32)data[8])<<16 + ((u16)data[9])<<8 + data[10];
							ClockState=data[12];
							
							usart.ClearReceiveBuffer();
							
							return true;
																 
//							}
//							else
//								return false;
						}
						else//其他命令字
						{
								return true;
						}
						
					}
					else
						return false;
				}
				else
					return false;			//帧头错误	
		}
		else
			return false;
		
}



//上锁与解锁
bool Communication::FlightLockControl(bool flag)
{
		if(flag == false)//解锁
		{
			ClockState = false;
			//usart.SendData(ProtocolClock(false),5);
		}
		else
		{
			ClockState = true;
			//usart.SendData(ProtocolClock(true),5);
		}
		return true;
}

bool Communication::SendData2Copter(float Yaw,float Thr,float Roll,float Pitch)
{
	u8 _cnt=0;
	u8 data_to_send[30];
	
	data_to_send[_cnt++]=0xAA;
	
	data_to_send[_cnt++]=0xAF;	
	
	data_to_send[_cnt++]=0x03;
	data_to_send[_cnt++]=0;
	
	data_to_send[_cnt++]=BYTE1(Thr);
	data_to_send[_cnt++]=BYTE0(Thr);
	
	data_to_send[_cnt++]=BYTE1(Yaw);
	data_to_send[_cnt++]=BYTE0(Yaw);
	
	data_to_send[_cnt++]=BYTE1(Roll);
	data_to_send[_cnt++]=BYTE0(Roll);
	
	data_to_send[_cnt++]=BYTE1(Pitch);
	data_to_send[_cnt++]=BYTE0(Pitch);
	
	u8 i=12;
	while(i--)
		data_to_send[_cnt++]=0;
	
	data_to_send[3] = _cnt-4;
	u8 sum = 0;
	for(u8 i=0;i<_cnt;i++)
		sum += data_to_send[i];
	data_to_send[_cnt++] = sum;
	
	usart.SendData(data_to_send, _cnt);	
	
	return true;
}

