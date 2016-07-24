#include "communication.h"



Communication::Communication(USART &com):usart(com)
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
							}
							else
								return false;
						}
						else//其他命令字
						{}
						
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




