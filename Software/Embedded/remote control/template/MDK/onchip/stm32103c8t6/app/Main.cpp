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
#include "HMI.h"
#include "Communication.h"
#include "rocker.h"

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

HMI MessageShow(com3);
rocker RC(pressure,5,6,8,9);//yaw Thr Roll pitch
Communication Rc2Copter(com2);

int main()
{
	double record_UI_Updata=0; //UI更新时间片
	double record_UI_power=0;//电量更新
	double record_UI_time_hight=0;//时间更新
	double record_UI_YRPT_CLOCK=0;//时间更新
	double record_JoystickUpdata=0;
	double test=0;
	


	
	ledRedGPIO.SetLevel(1);
	ledGREGPIO.SetLevel(1);
	
	
	
	while(1)
	{
		Rc2Copter.DataListening();
		
		
		if(tskmgr.TimeSlice(record_UI_Updata,0.1))  //0.1秒更新一次UI
		{
				//MessageShow.setTextBox("t21",num);
			if(tskmgr.TimeSlice(record_UI_power,5))//每5秒更新一次电量值
			{
				MessageShow.setTextBox(UI_RcPower,pressure.Voltage_I(1,1,2,4.2),1);
				MessageShow.setTextBox(UI_CopterPower,Rc2Copter.RcvPower,1);
				
			}
			if(tskmgr.TimeSlice(record_UI_time_hight,1))//每秒更新一次高度和时间
			{
				MessageShow.setTextBox(UI_Hight,Rc2Copter.RcvHight,0);
				MessageShow.setTextBox(UI_AliveTime,Rc2Copter.RcvTime,0);
			}
			if(tskmgr.TimeSlice(record_UI_YRPT_CLOCK,0.5)) //没0.5秒更新一次显示
			{
				MessageShow.setTextBox(UI_YAW,Rc2Copter.RcvYaw,0);
				MessageShow.setTextBox(UI_ROOL,Rc2Copter.RcvRoll,0);
				MessageShow.setTextBox(UI_PITCH,Rc2Copter.RcvPitch,0);
				MessageShow.setTextBox(UI_THR,Rc2Copter.RcvThr,0);	
				
				if(Rc2Copter.ClockState == 1)
					MessageShow.vis(UI_FlyClock,1);//显示  为橙色
				else
					MessageShow.vis(UI_FlyClock,0);//隐藏  为蓝色
				
			}
			
			//更新摇杆状态显示
			MessageShow.outputDirection(UI_DirectionL,RC.getLeftState());
			MessageShow.outputDirection(UI_DirectionR,RC.getNightState());
			
		}
		
		if(tskmgr.TimeSlice(record_JoystickUpdata,0.1))  //0.1秒更新一次摇杆状态
		{
  				if(RC.Updata())
					{
					//遥控器状态判断  外八解锁，内八上锁
						if(RC.getLeftState() == 7 && RC.getNightState() == 9) //解
						{
								Rc2Copter.FlightLockControl(false);
						}
						else if(RC.getLeftState() == 9 && RC.getNightState() == 7)
						{
								Rc2Copter.FlightLockControl(true);
						}
						else if(RC.getLeftState() == 1 && RC.getNightState() == 3 && Rc2Copter.ClockState == 1) //外八且上锁
						{
								
						}
						else//如果处于解锁状态则发送数据
						{
								if(Rc2Copter.ClockState == 0)
								{
									//发送数据
								}
						}
															
					}			
		}
		if(tskmgr.TimeSlice(test,0.1))  
			com2<<pressure[5]<<"\t"<<pressure[6]<<"\t"<<pressure[8]<<"\t"<<pressure[9]<<"\n";
		
		
	}
}





//X1（5）: 中间值：        				最小：（右移）0.008   最大（左移） 3.299
//Y1（6）:         0.87              （下移）0.16		   （上移） 2.98
//X2（8）:         0.83              （左移）0.002       （右移） 2.89
//Y2（9）:         2.27              （上移）0.010       （下移） 3.299  
