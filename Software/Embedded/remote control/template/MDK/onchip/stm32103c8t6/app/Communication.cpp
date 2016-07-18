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
		u8 data[14]={0};
		u8 check=0;
		u8 num = usart.ReceiveBufferSize();
		if(num>3)
		{
				usart.GetReceivedData(&ch,1);
				if(ch == 0xbb)
				{
					usart.GetReceivedData(&ch,1);
					if(ch == 0xbb)
					{
						//命令字判断
						usart.GetReceivedData(&ch,1);
						if(ch == 0xff)//状态数据
						{
							while(usart.ReceiveBufferSize()<14);//等待数据
							usart.GetReceivedData(data,14);
							check=data[13];							
							if( Calibration(data,13,check )) //如果校验正确
							{
								 RcvYaw=(float)data[0]+(float)data[1]/100;
								 RcvRoll=(float)data[2]+(float)data[3]/100;
								 RcvPitch=(float)data[4]+(float)data[5]/100;
								 RcvPower=(float)data[6]+(float)data[7]/100;
								 RcvThr=data[8];
								 RcvHight=data[9];
								 if(data[10]==0)
										ClockState=false;
								 else
										ClockState=true;
								 
								 RcvTime =(int)data[11]<<8+data[12];
								 
								 return true;
																 
							}
							else
								return false;
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
					return false;				
		}
		else
			return false;
		
}

u8* Communication::ProtocolClock(bool flag)//上锁协议
{
		FrameClock[0]=0xaa;
		FrameClock[1]=0XAA;
		FrameClock[2]=0X03;
		FrameClock[3]=(u8)flag;
		//和校验
	  FrameClock[4]=FrameClock[2]+FrameClock[3];
		return FrameClock;
}

//上锁与解锁
bool Communication::FlightLockControl(bool flag)
{
		if(flag == false)//解锁
		{
			ClockState = false;
			usart.SendData(ProtocolClock(false),5);
		}
		else
		{
			ClockState = true;
			usart.SendData(ProtocolClock(true),5);
		}
		return true;
}


