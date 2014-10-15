Aj_ZRLC_Tweezers
================

USB connected TI TMS320F28027 based ZRLC Tweezers with CCS5.5 C-Code and VS2013 VB.Net-Code

Most Digital-Multi-meters measure Resistance and Capacitance and LC-meters can measure Inductance and Capacitance

I present here a ZRLC meter which can measure Resistance, Capacitance and Inductance.

This application is based on the principle of measuring the voltage (amplitude and phase) across an unknown Impedance
when fed a sin-wave of known frequency through a known resistance.

The hardware is built around a TMS320F28027 micro-controller an 8-port-analogue-switch AD817 from analogue devices and a Microchip
rail-to-rail dual operational-amplifier MCP6022.

A High-Frequency PWM waveform is generated in the micro-controller and modulated by a 64-Bit sin-wave sequence. This 
when filtered and buffered form a sin-wave source ( selectable 300Hz, 3kHz, 30kHz). The sin-wave source is connected
to one of 4-resistors (100, 1k, 10k, 100k) using the analogue-switch. The selected resistor being connected to the unknown 
Impedance whose second terminal is connected to ground.

Once a frequency and resistance have been set the sin-wave is generated on command and 128 10-Bit samples of the 
Vout(signal-source) and Vin(Voltage at unknown Impedance) are A/D converted and stored in internal memory.

This data is transferred to a PC through an USB port which also powers the hardware.

Suitable digital-signal-processing on the PC using the VS2103 GUI software displays the real and imaginary part of the 
unknown Impedance. This is interpreted as Resistance, Capacitance or Impedance.

This hardware will form an invaluable tool for electronics students and hobbyists. 
