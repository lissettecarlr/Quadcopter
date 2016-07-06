//#include "stdlib.h"
//#include "stdio.h"

#include "stm32f10x.h"
#include "Configuration.h"
#include "TaskManager.h"
#include "USART.h"
#include "I2C.h"
#include "Timer.h"
#include "ADC.h"
#include "PWM.h"
#include "flash.h"
#include "InputCapture_TIM.h"
#include "InputCapture_EXIT.h"


//Timer T1(TIM1,1,2,3); //使用定时器计，溢出时间:1S+2毫秒+3微秒
USART com2(2,115200,false);
//USART com1(1,115200,false);
USART com3(3,9600,false);
//I2C abc(2); 
//PWM pwm2(TIM2,1,1,1,1,20000);  //开启时钟2的4个通道，频率2Whz
//PWM pwm3(TIM3,1,1,0,0,20000);  //开启时钟3的2个通道，频率2Whz
//PWM pwm4(TIM4,1,1,1,0,20000);  //开启时钟4的3个通道，频率2Whz
//InputCapture_TIM t4(TIM4, 400, true, true, true, true);
//InputCapture_EXIT ch1(GPIOB,6);
ADC pressure(1,5,6,8,9); //PA1读取AD值
//flash InfoStore(0x08000000+100*MEMORY_PAGE_SIZE,true);     //flash

GPIO ledRedGPIO(GPIOA,11,GPIO_Mode_Out_PP,GPIO_Speed_50MHz);//LED GPIO
GPIO ledGREGPIO(GPIOA,12,GPIO_Mode_Out_PP,GPIO_Speed_50MHz);//LED GPIO

int main()
{
	double record_UI_Updata=0; //UI更新时间片
	
	ledRedGPIO.SetLevel(1);
	ledGREGPIO.SetLevel(1);
	
	
	while(1)
	{			
		if(tskmgr.TimeSlice(record_UI_Updata,0.1))  //0.1秒更新一次UI
		{

		}
			
	}
}





//X1（5）: 中间值：2.56        最小：（右移）0.008   最大（左移） 3.299
//Y1（6）:         0.87              （下移）0.005		   （上移） 2.98
//X2（8）:         0.83              （左移）0.002       （右移） 2.89
//Y2（9）:         2.27              （上移）0.010       （下移） 3.299  
