//##################################################################################################
//  Started 17 Sept 2014
//  File:   Aj_ZRLC.c
//          Using code based on Aj_Scope5.c
//			Started Development of C2000 based ZRLC meter
//			Functions to be used are:
//				UART, EPWM 1A/2A,Simultaneous ADC B1/A1, OSC1 Compensation, SCI, SPI
//			New Commands
//				T=Test, A= CH1 5kHz 0x41, B= Ch2 5kHz 0x42, C= CH1 1Hz 0x43, D= Ch2 1kHz 0x44, 
//				E= CH1 100Hz 0x45, F= Ch2 100Hz 0x46,
//				O= Output (Read Command) 0x4F
//				G= 100 Ohms, H= 1kOhm, J= 10kOhm, K 100 kOhm
//				Added L= All Switches Off
//
//
//		
// ---------------------------------------------------------------------------------------------------
//			Using TMS320F28027 
//
//			AIM is to create ZRLC meter 
//			Based on AJ_Scope4.c
//			There is no COMP2 and The ADC's are different
//			Using ADCINA4 and ADCINB4
//			COMP1A input changed to Channel 2
//			ADCINA4 comes on the old COMP2A Pin
//			

//######################################################################################################
//  Uses portions of example code
//  (C) Copyright 2012, Texas Instruments, Inc.
//#############################################################################
// $TI Release: f2802x Support Library v210 $
// $Release Date: Mon Sep 17 09:13:31 CDT 2012 $
//#############################################################################

#include "DSP28x_Project.h"     // Device Header file and Examples Include File

#include "f2802x_common/include/adc.h"
#include "f2802x_common/include/clk.h"
#include "f2802x_common/include/flash.h"
#include "f2802x_common/include/gpio.h"
#include "f2802x_common/include/pie.h"
#include "f2802x_common/include/pll.h"
#include "f2802x_common/include/pwm.h"
#include "f2802x_common/include/wdog.h"

#include "f2802x_common/include/sci.h"  //added to adc_example for sci
#include "f2802x_common/include/comp.h" //added to adc_example for comp 
#include "f2802x_common/include/osc.h" //added for oscillator compensation 

//#include "stdint.h"
#include "handles.h" //handles required for APIs
#include "rs232_CCS.c" //Aj sci include file
#include "pie.c" //pie functions were being declared as implicit ?

//Critical Functions to be run in RAM


#pragma CODE_SECTION(Output_And_Acquire_30K, "ramfuncs");
#pragma CODE_SECTION(Output_And_Acquire_3K, "ramfuncs");
#pragma CODE_SECTION(Output_And_Acquire_300, "ramfuncs");
// Prototype for ISR XINT1
interrupt void xint1_isr(void);

// Prototype statements for functions found within this file.


void Initialize_Handles(void);////ADC,CLK,CPU,FLASH,GPIO,PIE,PLL,PWM,WDOG,SCI,COMP
void Initialize_System(void);//Disable Watchdog, Select Clock Source, Setup PLL
void Initialize_GPIO(void); //TX RX COMP1 COMP2
void Setup_ADC(void);//Clock, Calibration, Ref, power On,ADCINA1/B1 Simultaneous
void Setup_Comp(void);//Comp1 and Comp2 
void Setup_EPWM(void);//EPWM 1A/2A 75%/25%
void Setup_Interrupt(void);


void Output_And_Acquire_30K();
void Output_And_Acquire_3K();
void Output_And_Acquire_300();
void Output();
void setswitch(uint8_t sdata);


uint8_t SIN_MAP[128] = {//64 Step Table  8 Bit ADC Mode   24/32 0.75 gain
0X20,0X22,0X24,0X27,0X29,0X2B,0X2D,0X2F,0X31,0X32,0X34,0X35,0X36,0X37,0X37,0X37,
0X37,0X37,0X37,0X36,0X35,0X34,0X33,0X31,0X30,0X2E,0X2C,0X2A,0X28,0X25,0X23,0X21,
0X1E,0X1C,0X1A,0X17,0X15,0X13,0X11,0X0F,0X0E,0X0C,0X0B,0X0A,0X09,0X08,0X08,0X08,
0X08,0X08,0X08,0X09,0X0A,0X0B,0X0D,0X0E,0X10,0X12,0X14,0X16,0X18,0X1B,0X1D,0X1F,
0X20,0X22,0X24,0X27,0X29,0X2B,0X2D,0X2F,0X31,0X32,0X34,0X35,0X36,0X37,0X37,0X37,
0X37,0X37,0X37,0X36,0X35,0X34,0X33,0X31,0X30,0X2E,0X2C,0X2A,0X28,0X25,0X23,0X21,
0X1E,0X1C,0X1A,0X17,0X15,0X13,0X11,0X0F,0X0E,0X0C,0X0B,0X0A,0X09,0X08,0X08,0X08,
0X08,0X08,0X08,0X09,0X0A,0X0B,0X0D,0X0E,0X10,0X12,0X14,0X16,0X18,0X1B,0X1D,0X1F};






// Global variables used 
uint16_t input_string[3], AN[256], data,adc_index, sliding_delay;
uint8_t data_index, busy, error=0;
uint8_t capture_mode, sampling_mode, fft, sampling_rate, degC;
uint8_t gain, pga;//PGA Gain 1/2/5, PGA1/PGA2

uint32_t delay,loop;
uint32_t sliding_delay_cycles;
uint16_t counter, temperature;
uint8_t *msg, sdata= 0x00;


#define RPT_NOP(n)  __asm(" RPT #(" #n ") || NOP")


void main(void) {

 

uint8_t d =0x20;
delay=1;
	
	//restart:
	busy=1;//dummy line
    //uint8_t *msg;

	   
//Initialization and Setup 
	Initialize_Handles(); //Initialize all Handles //ADC,CLK,CPU,FLASH,GPIO,PIE,PLL,PWM,WDOG,SCI,COMP
  	Initialize_System();  //Disable Watchdog, Select Clock Source, Setup PLL
  	Setup_ADC(); //Clock, Calibration, Ref, power On,ADCINA1/B1 Simultaneous
  	Setup_Comp();//Comp1 and Comp2 
	Initialize_GPIO();//TX RX COMP1 COMP2
	Setup_EPWM();//EPWM 1A/2A 75%/25%
   
	scia_init();  		// Initialize SCI
    scia_fifo_init();  	// Initialize the SCI FIFO
	
	//setswitch(sdata);
       
// If running from flash copy RAM only functions to RAM   
#ifdef _FLASH
    memcpy(&RamfuncsRunStart, &RamfuncsLoadStart, (size_t)&RamfuncsLoadSize);
#endif 
  
	//Setup_Interrupt();//PIE, Interrupts 
	
	
	
	busy=1;
		//Aj
		putChar(65);putChar(106);
		//msg = "Aj-Scope Start";
		//printf(msg);
	
		DSP28x_usDelay( 1000); //Initial delay
	busy=0;
	

	// Wait for SCI Commands
    while(1){ // Get command byte
		d=getChar();
	     	if (d==0X54){ //T for test
				//Aj_ZRLC Ready \r"
				putChar(65);putChar(106);putChar(95);putChar(90);putChar(82);putChar(76);putChar(67);putChar(32);
				putChar(82);putChar(101); putChar(97 );putChar(100);putChar(121);putChar(32);putChar(13);
			//printf(uint8_t * msg){//Limited to 25 characters or till '\0'			
        	}
			else if (d==0X41){ //A for Acquire CH-A/CH-B  30K (32.54kHz)
				Output_And_Acquire_30K();
				putChar(68);putChar(111);putChar(110);putChar(101);putChar(65);putChar(13);
				sdata=0x00;
				setswitch(sdata);
			}
			else if (d==0X42){ //B for Acquire CH-A/CH-B  3K 
				Output_And_Acquire_3K();
				putChar(68);putChar(111);putChar(110);putChar(101);putChar(66);putChar(13);
				sdata=0x00;
				setswitch(sdata);
			}
			else if (d==0X43){ //C for Acquire CH-A/CH-B  300 
				Output_And_Acquire_300();
				putChar(68);putChar(111);putChar(110);putChar(101);putChar(67);putChar(13);
				sdata=0x00;
				setswitch(sdata);
			}
			else if (d==0X4F){ //O for Output Data Values
				//Printf("Output \r");
				Output();
			}
			else if (d==0X63){ //"c" Compensate OSC1
				//Force start of conversion on SOC0 and SOC1
				ADC_forceConversion(myAdc, ADC_SocNumber_2);
				ADC_forceConversion(myAdc, ADC_SocNumber_3);
				DSP28x_usDelay( 10);// delay for conversion complete minimum 35 cycles
				// Get temp sensor sample result from SOC1
				temperature = ADC_readResult(myAdc, ADC_ResultNumber_3);
				degC = ADC_getTemperatureC(myAdc, temperature);
				putChar(degC);
				OSC_runCompensation(myOSC, OSC_Number_1, temperature);
			}
			else if (d==0X47){ //G Set 100 Ohms
			// Transmit data
				sdata=0xC0;
				setswitch(sdata);
				putChar(82);putChar(49);putChar(48);putChar(48);putChar(13);
			}
			else if (d==0X48){ //H Set 1K Ohms
				sdata=0x30;
				setswitch(sdata);
				putChar(82);putChar(49);putChar(75);putChar(13);
			}
			else if (d==0X4A){ //J Set 10K Ohms
				sdata=0x0C;
				setswitch(sdata);
				putChar(82);putChar(49);putChar(48);putChar(75);putChar(13);
			}
			else if (d==0X4B){ //K Set 100K Ohms
				sdata=0x03;
				setswitch(sdata);
				putChar(82);putChar(49);putChar(48);putChar(48);putChar(75);putChar(13);
			}
			else if (d==0X4C){ //L Set All OFF
			sdata=0x00;
			setswitch(sdata);
			putChar(82);putChar(79);putChar(70);putChar(70);
			}
			
	}
	//loop */

}//end of main
//-----------------------Functions-------------------------------------------
	
void Initialize_Handles(void){//ADC,CLK,CPU,FLASH,GPIO,PIE,PLL,PWM,WDOG,SCI,COMP
// Initialize all the handles needed for this application    
    myAdc = ADC_init((void *)ADC_BASE_ADDR, sizeof(ADC_Obj));
    myClk = CLK_init((void *)CLK_BASE_ADDR, sizeof(CLK_Obj));
    myCpu = CPU_init((void *)NULL, sizeof(CPU_Obj));
    myFlash = FLASH_init((void *)FLASH_BASE_ADDR, sizeof(FLASH_Obj));
    myGpio = GPIO_init((void *)GPIO_BASE_ADDR, sizeof(GPIO_Obj));
    myPie = PIE_init((void *)PIE_BASE_ADDR, sizeof(PIE_Obj));
    myPll = PLL_init((void *)PLL_BASE_ADDR, sizeof(PLL_Obj));
    myPwm1 = PWM_init((void *)PWM_ePWM1_BASE_ADDR, sizeof(PWM_Obj));
    myWDog = WDOG_init((void *)WDOG_BASE_ADDR, sizeof(WDOG_Obj));
	mySci = SCI_init((void *)SCIA_BASE_ADDR, sizeof(SCI_Obj));   //added for SCI
	myComp1 = COMP_init((void *)COMP1_BASE_ADDR, sizeof(COMP_Obj));//added for Comp
	myComp2 = COMP_init((void *)COMP2_BASE_ADDR, sizeof(COMP_Obj));//added for Comp
	myOSC  = OSC_init((void *)OSC_BASE_ADDR, sizeof(OSC_Obj));//for osc compensation
}
void Initialize_System(void){
 // Perform basic system initialization    
    WDOG_disable(myWDog);
 //Select the internal oscillator 1 as the clock source
    CLK_setOscSrc(myClk, CLK_OscSrc_Internal);
 // Setup the PLL for x12 /2 which will yield 60Mhz = 10Mhz * 12 / 2
    //PLL_setup(myPll, PLL_Multiplier_12, PLL_DivideSelect_ClkIn_by_2);
// Setup the PLL for x10 /2 which will yield 50Mhz = 10Mhz * 12 / 2
    PLL_setup(myPll, PLL_Multiplier_10, PLL_DivideSelect_ClkIn_by_2);
	
//LSPCLK Prescaler div by 2
	//SysCtrlReg.bits.LOSPCP	=2;//this is the default div by 4
}
void Initialize_GPIO(void){
// Initialize RX/TX Digital I/O
    GPIO_setPullUp(myGpio, GPIO_Number_28, GPIO_PullUp_Enable);
    GPIO_setPullUp(myGpio, GPIO_Number_29, GPIO_PullUp_Disable);
    GPIO_setQualification(myGpio, GPIO_Number_28, GPIO_Qual_ASync);
    GPIO_setMode(myGpio, GPIO_Number_28, GPIO_28_Mode_SCIRXDA);
    GPIO_setMode(myGpio, GPIO_Number_29, GPIO_29_Mode_SCITXDA);
	
	
// Initialize COMP1/COMP2 Digital IO
    EALLOW;
	GpioCtrlRegs.GPAPUD.bit.GPIO1  = 1;    // Disable pull-up for GPIO1 (CMP1OUT)
	//GpioCtrlRegs.GPAPUD.bit.GPIO3  = 1;    // Disable pull-up for GPIO3 (CMP2OUT)
	GpioCtrlRegs.GPAMUX1.bit.GPIO1 = 3;   // Configure GPIO1 for CMP1OUT operation
	//GpioCtrlRegs.GPAMUX1.bit.GPIO3 = 3;   // Configure GPIO3 for CMP2OUT operation
	GpioCtrlRegs.AIOMUX1.bit.AIO2  = 2;   // Configure AIO2 as Analog Input COMP1A IN
	//GpioCtrlRegs.AIOMUX1.bit.AIO4  = 2;   // Configure AIO4 as Analog Input COMP2A IN
	EDIS;
	
	//GPIO EPWM1A and EPWM2A
	EALLOW;
	GpioCtrlRegs.GPAPUD.bit.GPIO0  = 1;    // Disable pull-up for GPIO0 (EPWM1A)
	GpioCtrlRegs.GPAPUD.bit.GPIO2  = 1;    // Disable pull-up for GPIO2 (EPWM1B)
	GpioCtrlRegs.GPAMUX1.bit.GPIO0 = 1;   // Configure GPIO0 for EPWM1A operation
	GpioCtrlRegs.GPAMUX1.bit.GPIO2 = 1;   // Configure GPIO2 for EPWM1B operation
	EDIS;

// GPC pins configured for use as External Interrupts
// GPIO12 input Button input with pull down
	GPIO_setMode(myGpio, GPIO_Number_12, GPIO_12_Mode_GeneralPurpose);
	GPIO_setDirection(myGpio, GPIO_Number_12, GPIO_Direction_Input);
	GPIO_setPullUp(myGpio, GPIO_Number_12, GPIO_PullUp_Disable); // important !!
	
// GPIO4 input Connected to GPIO1 COMP1 OUT CH1 Trigger
	GPIO_setMode(myGpio, GPIO_Number_4, GPIO_4_Mode_GeneralPurpose);
	GPIO_setDirection(myGpio, GPIO_Number_4, GPIO_Direction_Input);
	GPIO_setPullUp(myGpio, GPIO_Number_4, GPIO_PullUp_Disable); // important !!

// GPIO5 input Connected to GPIO3 COMP2 OUT Ch2 Trigger
	//GPIO_setMode(myGpio, GPIO_Number_5, GPIO_5_Mode_GeneralPurpose);
	//GPIO_setDirection(myGpio, GPIO_Number_5, GPIO_Direction_Input);
	//GPIO_setPullUp(myGpio, GPIO_Number_5, GPIO_PullUp_Disable); // important !!
	
//For SPI my way of doing a simple SPI
	// GPIO16 SPI DATA OUT with pull UP  J2-6
	GPIO_setPullUp(myGpio, GPIO_Number_16, GPIO_PullUp_Enable);
	GPIO_setLow(myGpio, GPIO_Number_16);//Start Data with Low
	GPIO_setMode(myGpio, GPIO_Number_16, GPIO_16_Mode_GeneralPurpose);
	GPIO_setDirection(myGpio, GPIO_Number_16, GPIO_Direction_Output);
	// GPIO17 SPI CLOCK OUT with pull UP J2-7
	GPIO_setPullUp(myGpio, GPIO_Number_17, GPIO_PullUp_Enable);
	GPIO_setLow(myGpio, GPIO_Number_17);//CLock Starts with Low SPI 00 Mode
	GPIO_setMode(myGpio, GPIO_Number_17, GPIO_17_Mode_GeneralPurpose);
	GPIO_setDirection(myGpio, GPIO_Number_17, GPIO_Direction_Output);
	// GPIO6 CHIP SELECT BAR For Switch with pull UP  J2-8
	GPIO_setPullUp(myGpio, GPIO_Number_6, GPIO_PullUp_Enable);
	GPIO_setHigh(myGpio, GPIO_Number_6);//CS BAR High
	GPIO_setMode(myGpio, GPIO_Number_6, GPIO_6_Mode_GeneralPurpose);
	GPIO_setDirection(myGpio, GPIO_Number_6, GPIO_Direction_Output);
	
	/* / GPIO7 CHIP SELECT BAR CH2 OUT with pull UP
	GPIO_setPullUp(myGpio, GPIO_Number_7, GPIO_PullUp_Enable);
	GPIO_setHigh(myGpio, GPIO_Number_7);//CS BAR High
	GPIO_setMode(myGpio, GPIO_Number_7, GPIO_7_Mode_GeneralPurpose);
	GPIO_setDirection(myGpio, GPIO_Number_7, GPIO_Direction_Output);
	*/
	
}
void Setup_ADC(void){
	CLK_enableAdcClock(myClk);
    (*Device_cal)();

// Initialize the ADC
    ADC_enableBandGap(myAdc);
    ADC_enableRefBuffers(myAdc);
    ADC_powerUp(myAdc);
    ADC_enable(myAdc);
    ADC_setVoltRefSrc(myAdc, ADC_VoltageRefSrc_Int);
// Configure ADC
	//This portion for reading CH1/CH2 inputs on ADCINB1/ADCINA1 
    //Note: ADC clock will be set as div/2 to workaround the ADC 1st sample issue for rev0 silicon errata 
	ADC_setSampleMode(myAdc, ADC_SampleMode_SOC0_and_SOC1_Together);//Simultaneous mode SIMUL0 bit to be SET?
    ADC_setSocChanNumber (myAdc, ADC_SocNumber_0, ADC_SocChanNumber_A4);    //set SOC0 channel select to ADCINA4
    ADC_setSocChanNumber (myAdc, ADC_SocNumber_1, ADC_SocChanNumber_B4);    //set SOC1 channel select to ADCINB4
    ADC_setSocTrigSrc(myAdc, ADC_SocNumber_0, ADC_SocTrigSrc_Sw);   //set SOC0 start trigger on SW ?
  			  													    //in simultaneous mode this will trigger both A and B
    ADC_setSocSampleWindow(myAdc, ADC_SocNumber_0, ADC_SocSampleWindow_7_cycles);   //set SOC0 S/H Window to 7 ADC Clock Cycles, (6 ACQPS plus 1)
    ADC_setSocSampleWindow(myAdc, ADC_SocNumber_1, ADC_SocSampleWindow_7_cycles);   //set SOC1 S/H Window to 7 ADC Clock Cycles, (6 ACQPS plus 1)	

	//This portion for reading the temperature sensor to be used for OSC1 Compensation
	ADC_enableTempSensor(myAdc);                                            //Connect channel A5 internally to the temperature sensor
	//ADC_setSampleMode(myAdc, ADC_SampleMode_SOC2_and_SOC3_Together);//Simultaneous mode SIMUL0 bit to be SET?
    ADC_setSocChanNumber (myAdc, ADC_SocNumber_2, ADC_SocChanNumber_A5);    //Set SOC3 channel select to ADCINA5
    ADC_setSocChanNumber (myAdc, ADC_SocNumber_3, ADC_SocChanNumber_A5);    //Set SOC4 channel select to ADCINA5
    ADC_setSocSampleWindow(myAdc, ADC_SocNumber_2, ADC_SocSampleWindow_7_cycles);   //Set SOC3 acquisition period to 7 ADCCLK
    ADC_setSocSampleWindow(myAdc, ADC_SocNumber_3, ADC_SocSampleWindow_7_cycles);   //Set SOC4 acquisition period to 7 ADCCLK
	
	
	
	
	
	
}
void Setup_Comp(void){
// Configure Comparator 
	//API Commands from comp.h not used
	//Direct Commands using Comp.h
	EALLOW;
		SysCtrlRegs.PCLKCR3.bit.COMP1ENCLK=1; // Enable SYSCLKOUT to COMP1  Clock Required for DAC
		//SysCtrlRegs.PCLKCR3.bit.COMP2ENCLK=1; // from DSP2802x_SysCtrl.h
	
		Comp1Regs.COMPCTL.bit.COMPDACEN=1;//Comparator/DAC logic is powered up.
		Comp1Regs.COMPCTL.bit.COMPSOURCE=0; //Inverting input of comparator connected to internal DAC
		Comp1Regs.COMPCTL.bit.CMPINV=0; //normal output
		Comp1Regs.COMPCTL.bit.SYNCSEL=0; //async value is passed
		//Comp1Regs.DACCTL.bit.DACSOURCE=0;//DAC Controlled by DACVAL
		
		//Comp2Regs.COMPCTL.bit.COMPDACEN=1;//Comparator/DAC logic is powered up.	
		//Comp2Regs.COMPCTL.bit.COMPSOURCE=0; //Inverting input of comparator connected to internal DAC		
		//Comp2Regs.COMPCTL.bit.CMPINV=0; //normal output
		//Comp2Regs.COMPCTL.bit.SYNCSEL=0; //async value is passed
		//Comp2Regs.DACCTL.bit.DACSOURCE=0;//DAC Controlled by DACVAL
	EDIS;
	
	//Disable the Hysteresis
	EALLOW;
		AdcRegs.COMPHYSTCTL.bit.COMP1_HYST_DISABLE =0;//1; //Comp1 Hysteresis disable =1 , Enable=0   180 ohms/ 100kohms 5V on 0-5V transition
		//AdcRegs.COMPHYSTCTL.bit.COMP2_HYST_DISABLE =0;//1; //Comp1 Hysteresis disable =1 , Enable=0
	EDIS; 
	
	//Set the default values
	Comp1Regs.DACVAL.all = 512; // Set DAC1 to vref/2
	//Comp2Regs.DACVAL.all = 512; // Set DAC1 to vref/2
	
}
void Setup_EPWM(void){//Setup EPWM 1A and 2A
//CLOCK
	EALLOW;
	SysCtrlRegs.PCLKCR1.bit.EPWM1ENCLK=1; //Enable SYSCLKOUT to EPWM1
	SysCtrlRegs.PCLKCR1.bit.EPWM2ENCLK=1; //Enable SYSCLKOUT to EPWM2
	SysCtrlRegs.PCLKCR0.bit.TBCLKSYNC = 1;   // Enable TBCLK
	EDIS;
//EPWM1A
	EPwm1Regs.TBPRD =64;//128;//1023; // Period = 1024 TBCLK counts
	EPwm1Regs.CMPA.half.CMPA = 32;//64;//512; // Compare A = 512 TBCLK counts 50%
	//EPwm1Regs.CMPB = 256; // Compare B = 256 TBCLK counts  25%
	EPwm1Regs.TBPHS.all = 0; // Set Phase register to zero 
	EPwm1Regs.TBCTR = 0; // clear TB counter
	EPwm1Regs.TBCTL.bit.CTRMODE = TB_COUNT_UP;
	EPwm1Regs.TBCTL.bit.PHSEN = TB_DISABLE; // Phase loading disabled
	EPwm1Regs.TBCTL.bit.PRDLD = TB_SHADOW;
	EPwm1Regs.TBCTL.bit.SYNCOSEL = TB_SYNC_DISABLE;
	EPwm1Regs.TBCTL.bit.HSPCLKDIV = TB_DIV1; // TBCLK = SYSCLK
	EPwm1Regs.TBCTL.bit.CLKDIV = TB_DIV1;//TB_DIV2;//TB_DIV1;
	EPwm1Regs.CMPCTL.bit.SHDWAMODE = CC_SHADOW;
	EPwm1Regs.CMPCTL.bit.SHDWBMODE = CC_SHADOW;
	EPwm1Regs.CMPCTL.bit.LOADAMODE = CC_CTR_ZERO; // load on CTR = Zero
	EPwm1Regs.CMPCTL.bit.LOADBMODE = CC_CTR_ZERO; // load on CTR = Zero
	EPwm1Regs.AQCTLA.bit.ZRO = AQ_SET;
	EPwm1Regs.AQCTLA.bit.CAU = AQ_CLEAR;
	//EPwm1Regs.AQCTLB.bit.ZRO = AQ_SET;
	//EPwm1Regs.AQCTLB.bit.CBU = AQ_CLEAR;
//EPWM2A
	EPwm2Regs.TBPRD = 1023; // Period = 1024 TBCLK counts
	EPwm2Regs.CMPA.half.CMPA = 512; // Compare A = 512 TBCLK counts 50%
	//EPwm2Regs.CMPB = 256; // Compare B = 256 TBCLK counts  25%
	EPwm2Regs.TBPHS.all = 0; // Set Phase register to zero 
	EPwm2Regs.TBCTR = 0; // clear TB counter
	EPwm2Regs.TBCTL.bit.CTRMODE = TB_COUNT_UP;
	EPwm2Regs.TBCTL.bit.PHSEN = TB_DISABLE; // Phase loading disabled
	EPwm2Regs.TBCTL.bit.PRDLD = TB_SHADOW;
	EPwm2Regs.TBCTL.bit.SYNCOSEL = TB_SYNC_DISABLE;// 
	EPwm2Regs.TBCTL.bit.HSPCLKDIV = TB_DIV1; // TBCLK = SYSCLK
	EPwm2Regs.TBCTL.bit.CLKDIV = TB_DIV2;//TB_DIV1;
	EPwm2Regs.CMPCTL.bit.SHDWAMODE = CC_SHADOW;
	EPwm2Regs.CMPCTL.bit.SHDWBMODE = CC_SHADOW;
	EPwm2Regs.CMPCTL.bit.LOADAMODE = CC_CTR_ZERO; // load on CTR = Zero
	EPwm2Regs.CMPCTL.bit.LOADBMODE = CC_CTR_ZERO; // load on CTR = Zero
	EPwm2Regs.AQCTLA.bit.ZRO = AQ_SET;
	EPwm2Regs.AQCTLA.bit.CAU = AQ_CLEAR;
	//EPwm2Regs.AQCTLB.bit.ZRO = AQ_SET;
	//EPwm2Regs.AQCTLB.bit.CBU = AQ_CLEAR;
}
void Setup_Interrupt(void){
// Disable the PIE and all interrupts
	PIE_disable(myPie);
	PIE_disableAllInts(myPie);
	CPU_disableGlobalInts(myCpu);
	CPU_clearIntFlags(myCpu);

// Setup a debug vector table and enable the PIE
	PIE_setDebugIntVectorTable(myPie);
	PIE_enable(myPie);

// Register interrupt handlers in the PIE vector table
	PIE_registerPieIntHandler(myPie, PIE_GroupNumber_1, PIE_SubGroupNumber_4, (intVec_t)&xint1_isr);

// Enable XINT1 in the PIE: Group 1 interrupt 4
// Enable INT1 which is connected to WAKEINT
	PIE_enableInt(myPie, PIE_GroupNumber_1, PIE_InterruptSource_XINT_1);
	CPU_enableInt(myCpu, CPU_IntNumber_1);

// Enable Global Interrupts
	CPU_enableGlobalInts(myCpu);

// Disable ExtInt_1 this will be enables ad required in the capture_data()
	PIE_disableExtInt(myPie, CPU_ExtIntNumber_1);//disable Interrupt EXT1

// Select Inputs as required	

// GPIO12 is XINT1 Interrupt by Button
	//GPIO_setExtInt(myGpio, GPIO_Number_12, CPU_ExtIntNumber_1);
	
// GPIO4 is XINT1  Interrupt by CH1 Trigger   
	//GPIO_setExtInt(myGpio, GPIO_Number_4, CPU_ExtIntNumber_1);
	
// GPIO5 is XINT1 Interrupt by CH2 Trigger
	GPIO_setExtInt(myGpio, GPIO_Number_4, CPU_ExtIntNumber_1);  //Default
	
// Configure XINT1
	PIE_setExtIntPolarity(myPie, CPU_ExtIntNumber_1, PIE_ExtIntPolarity_RisingEdge);
	//PIE_setExtIntPolarity(myPie, CPU_ExtIntNumber_1, PIE_ExtIntPolarity_FallingEdge);

// Enable XINT1
	//PIE_enableExtInt(myPie, CPU_ExtIntNumber_1);
	counter=0;
}


//--------------ZRLC Functions-------------------------------------------
void Output_And_Acquire_30K(){ //Outputs Sine Wave and Acquires Z Voltage
uint16_t table_index;
uint16_t repeat;
	for(repeat=1;repeat<500; repeat++){//32.54kHz
		for (table_index=0;table_index<256;table_index++){
			//Output
			EPwm1Regs.CMPA.half.CMPA = (SIN_MAP[table_index/2]); 
			//64 step table  32.62kHz no-delay line and 15.62 with delay=1 both channels
			//Acquire	
			AdcRegs.ADCSOCFRC1.bit.SOC0=1; //ADC_forceConversion(myAdc, ADC_SocNumber_0);
			//DSP28x_usDelay( delay);// delay=1  5*1 +9 = 14 cycles
			//RPT_NOP(0);//29kHz
			//asm("NOP");//31.24khz
			//nothing 32.54kHz
			// Get result from SOC0 and SOC1
			AN[table_index]=  AdcResult.ADCRESULT0;
				table_index++;
			AN[table_index]=  AdcResult.ADCRESULT1;
			
		}
	}
}
void Output_And_Acquire_3K(){ //Outputs Sine Wave and Acquires Z Voltage
uint16_t table_index;
uint16_t repeat;
	for(repeat=1;repeat<100;repeat++){//3006 Hz
		for (table_index=0;table_index<256;table_index++){
			//Output
			EPwm1Regs.CMPA.half.CMPA = (SIN_MAP[table_index/2]); 
			//Acquire	
			AdcRegs.ADCSOCFRC1.bit.SOC0=1; //ADC_forceConversion(myAdc, ADC_SocNumber_0);
			DSP28x_usDelay(41);// delay=1  5*1 +9 = 14 cycles
			RPT_NOP(9);
			// Get result from SOC0 and SOC1
			AN[table_index]=  AdcResult.ADCRESULT0;
				table_index++;
			AN[table_index]=  AdcResult.ADCRESULT1;
			
		}
	}
}
void Output_And_Acquire_300(){ //Outputs Sine Wave and Acquires Z Voltage
uint16_t table_index;
uint16_t repeat;
	for(repeat=1;repeat<50;repeat++){//300.3Hz
		for (table_index=0;table_index<256;table_index++){
			//Output
			EPwm1Regs.CMPA.half.CMPA = (SIN_MAP[table_index/2]); 
			//Acquire	
			AdcRegs.ADCSOCFRC1.bit.SOC0=1; //ADC_forceConversion(myAdc, ADC_SocNumber_0);
			DSP28x_usDelay( 512);// delay=1  5*1 +9 = 14 cycles
			// Get result from SOC0 and SOC1
			AN[table_index]=  AdcResult.ADCRESULT0;
				table_index++;
			AN[table_index]=  AdcResult.ADCRESULT1;
			
		}
	}
}
void Output(){ //Outputs captured Z Voltage
uint16_t table_index;
	
		for (table_index=0;table_index<256;table_index++){
			putInt16(AN[table_index]);
			DSP28x_usDelay(100);
			}
	
}
void setswitch(uint8_t sdata){
	
	#define spid   GPIO_Number_16  	//  SPI Data   J2-6
	#define spic   GPIO_Number_17 	//  SPI Clock  J2-7
	#define cs1    GPIO_Number_6  	//  CS Bar for Switch J2-8
	

	// spic,spid,cs1
	uint8_t count;
	uint8_t write_cmd_string, bit_out;
	
	//initial values
	GPIO_setHigh(myGpio, cs1);//CS1 BAR High
	//GPIO_setLow (myGpio, spic);//SPI Clock Starts with Low 00 Mode
	GPIO_setHigh (myGpio, spic);//SPI Clock Starts with High
	//start the instruction cycle
	//Write Command D8-D15 010101000
	write_cmd_string=sdata;
	
	//Enable Switch
	GPIO_setLow(myGpio, cs1);//cs1=0;
		
	DSP28x_usDelay(20); //0.1usec
	//output the write_command_string	LSB first
	for (count=0; count<=7;count++){
		bit_out=write_cmd_string&0x01; 
		if (bit_out==1){
		GPIO_setHigh(myGpio, spid);//spid=1;
		DSP28x_usDelay(20); //0.1usec
		GPIO_setLow(myGpio, spic);//spic=0; data transferred on H-L
		DSP28x_usDelay(20);
		GPIO_setHigh(myGpio, spic);//spic=1;
		}
		else {
		GPIO_setLow(myGpio, spid);//spid=0;
		DSP28x_usDelay(20);
		GPIO_setLow(myGpio, spic);//spic=0; data transferred on H-L
		DSP28x_usDelay(20);
		GPIO_setHigh(myGpio, spic);//spic=1;
		}
		write_cmd_string=write_cmd_string>>1;
	}
		
	DSP28x_usDelay(10);
	//deselect both Switch
	GPIO_setHigh(myGpio, cs1);//cs1=1;
	
}


