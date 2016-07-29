#include "communication.h"



Communication::Communication(USART &com1,USART &com2):usart(com1),com433(com2)
{
	
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

bool Communication::DataListening_SendPC()
{
		u8 ch=0;
		u8 data[30]={0};
		u8 num = com433.ReceiveBufferSize();
		if(num>0)
		{
				com433.GetReceivedData(&ch,1);
				if(ch == 0xaa)
				{
					com433.GetReceivedData(&ch,1);
					if(ch == 0xaa)
					{
						data[0] = 0xaa;
						data[1] = 0xaa;
						while(com433.ReceiveBufferSize()<2);//等待数据
						com433.GetReceivedData(&ch,1);
						data[2] = ch;
						com433.GetReceivedData(&ch,1);
						data[3] = ch;
						while(com433.ReceiveBufferSize()<ch+1);//等待数据
						com433.GetReceivedData(data+4,ch+1);
						if( Calibration(data,data[3]+4,data[ch+4]))
						{
							usart.SendData(data,ch+5);
							return true;
						}
						else
							return false ;//校验错误
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


