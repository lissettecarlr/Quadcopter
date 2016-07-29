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
	u8 data[300];
	u8 num = com433.ReceiveBufferSize();
	if(num>0)
	{
			com433.GetReceivedData(data,num);
			usart.SendData(data,num);
			return true;
	}
	return false;
	
	
	
	
//		u8 ch=0;
//		u8 data[30]={0};
//		u8 num = com433.ReceiveBufferSize();
//		if(num>0)
//		{
//				com433.GetReceivedData(&ch,1);
//				if(ch == 0xaa)
//				{
//					com433.GetReceivedData(&ch,1);
//					if(ch == 0xaa)
//					{
//						data[0] = 0xaa;
//						data[1] = 0xaa;
//						while(com433.ReceiveBufferSize()<2);//等待数据
//						com433.GetReceivedData(&ch,1);
//						data[2] = ch;
//						com433.GetReceivedData(&ch,1);
//						data[3] = ch;
//						while(com433.ReceiveBufferSize()<ch+1);//等待数据
//						com433.GetReceivedData(data+4,ch+1);
//						if( Calibration(data,data[3]+4,data[ch+4]))
//						{
//							usart.SendData(data,ch+5);
//							return true;
//						}
//						else
//							return false ;//校验错误
//					}
//					else
//						return false;
//				}
//				else
//					return false;				
//		}
//		else
//			return false;
		
}

bool Communication::SendData2Copter(uint16_t Yaw,uint16_t Thr,uint16_t Roll,uint16_t Pitch,bool Dir)
{
	u8 _cnt=0;
	u8 data_to_send[30];
	
	data_to_send[_cnt++]=0xAA;
	
	if(Dir)
	data_to_send[_cnt++]=0xAf;
	else
	data_to_send[_cnt++]=0xAa;	
	
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
	
	if(Dir)
	com433.SendData(data_to_send, _cnt);
	else
	usart.SendData(data_to_send, _cnt);	
	
	return true;
}
