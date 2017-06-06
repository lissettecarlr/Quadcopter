#include "stdlib.h"
#include "GPIO.h"
#include "USART.h"
#include "Interrupt.h"
#include "TaskManager.h"
#include "ADC.h"
#include "UranusIMU.h"
#include "PWM.h"


#define DOING 1


USART com1(1,9600,false);   //USART1
USART com2(2,9600,false);
USART com3(3,9600,false);
   
PWM pwm4(TIM4,1,1,1,1,24000);
//GPIO Beep(GPIOB,12,GPIO_Mode_Out_PP,GPIO_Speed_50MHz);

//HMI out(com);  //ÏÔÊ¾Æ÷
//PA1  Í¨µÀ1
//ADC adc(2); 


int main(){

/******************************************************************************************************/
#if DOING == 1	
	pwm4.SetDuty(10,10,10,10);
	while(1)
	{

	}
#endif
/******************************************************************************************************/
	

/******************************************************************************************************/	
#if DOING ==2

			
		while(1)
		{	
			
		}
		
#endif
/******************************************************************************************************/
		

/******************************************************************************************************/		
#if DOING==3

  double doing3=0;
	while(1)
	{
	
		if(tskmgr.ClockTool(doing3,2))
		{

		}
	}
#endif
	

#if DOING==4
	
double doing4=0;
while(1)
{
	 if(tskmgr.ClockTool(doing4,2))
	 {
	 }
}
	
#endif

#if DOING==5

double doing5=0;
while(1)
{
  if(tskmgr.ClockTool(doing5,2))  
	{
	}
}
#endif
	


#if DOING==6

  double doing6=0;
	while(1)
	{
		if(tskmgr.ClockTool(doing6,2))
		{
		}	
	}


#endif
/******************************************************************************************************/	
	
}

