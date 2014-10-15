//Extracted from SCI Loopback example
//-------------------------------------------------
//Initialises SCIA
//defines getChar(),putChar(),printf(),putInt16()
//------------------------------------------------


//#include "DSP28x_Project.h"   
#include "f2802x_common/include/sci.h"


// Prototype statements for functions found within this file.
void scia_init(void);
void scia_fifo_init(void);
void putChar(uint8_t a);//Transmits a character
uint8_t  getChar(void); //Waits and gets a character from SCI
void putInt16(int16_t X); //msb - lsb of 12 bit data
void putInt8(int16_t Y);//8 msb of 12 bit data
void printf(uint8_t *msg);////Prints a character string till '\0

//Handles 
CLK_Handle myClk;
SCI_Handle mySci;


// Setup ,SCIA  DLB, 8-bit word, baud rate 0x000F, default, 1 STOP bit, no parity
void scia_init()
{

    CLK_enableSciaClock(myClk);

    // 1 stop bit,  No loopback
    // No parity,8 char bits,
    // async mode, idle-line protocol
    SCI_disableParity(mySci);
    SCI_setNumStopBits(mySci, SCI_NumStopBits_One);
    SCI_setCharLength(mySci, SCI_CharLength_8_Bits);
    
    SCI_enableTx(mySci);
    SCI_enableRx(mySci);
    SCI_enableTxInt(mySci);
    SCI_enableRxInt(mySci);

    // SCI BRR = LSPCLK/(SCI BAUDx8) - 1
#if (CPU_FRQ_60MHZ)
    //SCI_setBaudRate(mySci, SCI_BaudRate_115_2_kBaud); 
	SCI_setBaudRate(mySci,  (SCI_BaudRate_e)15);//115.2Kbaud
#elif (CPU_FRQ_50MHZ)
	SCI_setBaudRate(mySci, 12);//LSPCLK SYSCLK/4 default 50 Mhz selected in F2802x_Examples.h
	//C:\ti\controlSUITE\device_support\f2802x\v200\f2802x_common\include\F2802x_Examples.h
    //SCI_setBaudRate(mySci, (SCI_BaudRate_e)12);
#elif (CPU_FRQ_40MHZ)
    SCI_setBaudRate(mySci, (SCI_BaudRate_e)129);
#endif

    SCI_enable(mySci);
    
    return;
}

// Transmit a character from the SCI
void putChar(uint8_t a){
    while(SCI_getTxFifoStatus(mySci) != SCI_FifoStatus_Empty) continue;//Checks for one empty location in Buffer then transmits
    SCI_putDataBlocking(mySci, a);
}
void putInt16(int16_t X){
uint8_t lsb,msb;
	lsb = X & 0x00ff;       // lsb of 16 bit Integer
    msb =(X & 0xff00)>>8;  //  msb of 16 bit Integer
	putChar(msb);
	putChar(lsb);
}

void putInt8(int16_t Y){
uint8_t msb8;
	msb8 =(Y & 0x0ff0)>>4;  //  msb of 12 bit Integer
	putChar(msb8);
}
// Get a character from the SCI
uint8_t getChar(void){
		while(SCI_getRxFifoStatus(mySci) < SCI_FifoStatus_1_Word) continue; // Wait for one character
               
        return(SCI_getData(mySci)); // return character
}
//Print 
void printf(uint8_t * msg){//Limited to 25 characters or till '\0'
    int i;
	i = 0;
    while(msg[i] != '\0'){
        putChar(msg[i]);
        i++;
    }
	
	/*	
    for (i=0;i<25;i++){ 
		putChar(msg[i]);
	}*/
}

// Initalize the SCI FIFO
void scia_fifo_init()
{
    
    SCI_enableFifoEnh(mySci);
    SCI_resetTxFifo(mySci);
    SCI_clearTxFifoInt(mySci);
    SCI_resetChannels(mySci);
    SCI_setTxFifoIntLevel(mySci, SCI_FifoLevel_Empty);

    SCI_resetRxFifo(mySci);
    SCI_clearRxFifoInt(mySci);
    SCI_setRxFifoIntLevel(mySci, SCI_FifoLevel_4_Words);

    return;
}

//===========================================================================
// No more.
//===========================================================================
