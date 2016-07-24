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


