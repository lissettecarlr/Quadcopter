#include "UranusIMU.h"



UarnusIMU::UarnusIMU(USART &com):mCom(com)
{
		acc.x =0;
		acc.y =0;
		acc.z =0;
		
		gyr.x = 0;
		gyr.y = 0;
		gyr.z = 0;

		mag.x = 0;
		mag.y = 0;
		mag.z = 0;		

		angle.x = 0;
		angle.y = 0;
		angle.z = 0;	
}


Vector3<int> UarnusIMU::GetAcc()
{
  return acc;
}

Vector3<int> UarnusIMU::GetGyr()
{
	return gyr;
}


Vector3<int> UarnusIMU::GetMag()
{
	return mag;
}

Vector3f UarnusIMU::GetAngle()
{
 return angle;
}


/*
5A A5  //包头
1C 00  //帧长度
90 D4  //CRC校验
A0 E4 FF 30 00 E4 03 
B0 00 00 00 00 00 00 
C0 7A 00 32 00 27 FF 
D0 5D FF E9 FE DB 05
*/
bool UarnusIMU::Update()
{
	u8 data[34];
	u8 ch[2]={0};
	u16 check1=0,check2=0;
	int num=mCom.ReceiveBufferSize();
	if(num >=34)
	{
		mCom.GetReceivedData(ch,1);
		if(ch[0] == 0x5a)
		{
		   mCom.GetReceivedData(ch,1);
			 if(ch[0] == 0xa5)
			 {
			    data[0] = 0x5a;
				  data[1] = 0xa5;
				  mCom.GetReceivedData(data+2,2); //取出长度
				  mCom.GetReceivedData(ch,2); //CRC校验
				  check1=ch[1]*256+ch[0];
				  mCom.GetReceivedData(data+4,28); //取出4组
				  crc16_update(&check2,data,32);
				 if(check2 == check1) //校验正确
				 {
					  short temp = 0;
					  
					  temp = (((short)(temp ^ data[6]) )<<8)^data[5] ;
				    acc.x = temp;
					  temp = 0;
					 
						temp = (((short)(temp ^ data[8]) )<<8)^data[7] ;
				    acc.y = temp;
					  temp = 0;
					 
					 	temp = (((short)(temp ^ data[10]) )<<8)^data[9] ;
				    acc.z = temp;
					  temp = 0;
					 
					  
					 	temp = (((short)(temp ^ data[13]) )<<8)^data[12] ;
				    gyr.x = temp;
					  temp = 0;
					 
						temp = (((short)(temp ^ data[15]) )<<8)^data[14] ;
				    gyr.y = temp;
					  temp = 0;
					 
					 	temp = (((short)(temp ^ data[17]) )<<8)^data[16] ;
				    gyr.z = temp;
					  temp = 0;
					 
  
	
	         	temp = (((short)(temp ^ data[20]) )<<8)^data[19] ;
				    mag.x = temp;
					  temp = 0;
					 
						temp = (((short)(temp ^ data[22]) )<<8)^data[21] ;
				    mag.y = temp;
					  temp = 0;
					 
					 	temp = (((short)(temp ^ data[24]) )<<8)^data[23] ;
				    mag.z = temp;
					  temp = 0;
						
						temp = (((short)(temp ^ data[27]) )<<8)^data[26] ;
						angle.x = (float)temp/100;
					  temp = 0;
						temp = (((short)(temp ^ data[29]) )<<8)^data[28] ;
						angle.y = (float)temp/100;;
						temp = 0;
						temp = (((short)(temp ^ data[31]) )<<8)^data[30] ;
						angle.z = (float)temp/10;;	
					 
					 mCom.ClearReceiveBuffer();
					 return true;
					 
				 }
				 else //校验错误
					 return false;
				  
			 }
			 else //包头错误
				 return false;
		} 
		else
			return false;
	}
	else
		return false;
	

}


void UarnusIMU::crc16_update(uint16_t *currectCrc, const uint8_t *src, uint32_t lengthInBytes)
{
    uint32_t crc = *currectCrc;
    uint32_t j;
    for (j=0; j < lengthInBytes; ++j)
    {
        uint32_t i;
        uint32_t byte = src[j];
        crc ^= byte << 8;
        for (i = 0; i < 8; ++i)
        {
            uint32_t temp = crc << 1;
            if (crc & 0x8000)
            {
                temp ^= 0x1021;
            }
            crc = temp;
        }
    } 
    *currectCrc = crc;
}
