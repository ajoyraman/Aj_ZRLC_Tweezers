Imports System
Imports System.IO.Ports
Imports System.Globalization
Imports ZedGraph
Imports System.Math
'Imports System.Numerics
Imports System.Collections
Imports KarlsTools


Public Class Aj_ZRLC_Form1
#Region "Definations"
#Region "Definitions for Connect to COM Port "
    'Definitions for COM Port Connection
    'Define  serial port instance
    Dim mPort As New SerialPort
    Dim myBuffer As Integer = 256 + 32
    'myBuffer = mPort.ReadBufferSize SET in Sub Test_232
#End Region
#Region "Definations for comPort_Setup and test"
    Public Shared selected_com_port As String
    'Gets possible serial port names and puts in listbox
    Dim myTestReadString As String 'Used by Test 232 & other readbacks
#End Region
#Region "Definations for Command String"
    Dim test_cmd() As Byte = {&H54} 'T
    Dim acquire_cmd_Ch12() As Byte = {&H41} 'A    41
    'Dim acquire_cmd_Ch2() As Byte = {&H42} 'B   42
    Dim read_cmd() As Byte = {&H4F} 'O
    Dim resistance_mode_cmd() As Byte = {&H52}  'R
    Dim capacitance_mode_cmd() As Byte = {&H43} 'L
    Dim inductance_mode_cmd() As Byte = {&H4C}  'C
    Dim set_switch_cmd() As Byte = {&H48} 'H 1K default
    Dim compensate_freq_cmd() As Byte = {&H63} 'c Compensate Oscillator for temperature
    Dim ch12, rset As Byte 'CH12 data and resistance to be set
#End Region
#Region "Other variables"
    Dim length As Byte
    Dim freq As String = 5350
    Dim resistance As String = 10000
    Dim Ch1_dataArray(512) As Byte '0-256=257
    Dim Time_array(400), CH1_DFT_Array(400), CH2_DFT_Array(400) As Byte
    Dim DataArray(257, 4) As String
    Dim Data_Length As Integer = 512

    Dim Multiplier As String = 1
    Dim Max_DFT As Double
    Dim Read_Mode As Byte
    Dim Max_Frequency As Integer = 5350 / 256.0
    Dim maxout, minout, amp1, amp2, Capacitance, Resistor, Inductance, R, L, C As Double
    Dim F_mode, R_Mode As Byte
    Dim Zmag, Zmag1, theta, phi, sign, deg, A, B As Double
    Dim Ch1_data(256), Ch2_data(256) As Double

    Dim Z0, Z1, Z2, Z3 As Complex '= New Complex(3.0, 4.0)

    Dim ZC5L0, ZC4, ZC0, ZL1 As Complex 'ZC1R4,
    Dim R100 As Double = 100 '105
    Dim F30K As Double = 32540

    Dim gain As Double
    Dim mystep As Integer
    Dim keep_running As Boolean

#End Region
#Region "Definitions For Plot data"
    'Dim myPane As GraphPane = Aj_ZG1.GraphPane
    Dim xcolumn, ycolumn, y1column As Integer ', Max_Frequency As Integer
    Dim display_x, display_y, display_y1 As Boolean
    Dim loc As New Point(0, 140)
    Dim Plot_Error, Sq_Display, DFT_Display As Boolean

#End Region
#End Region
#Region "Main Form Load"
    Private Sub Aj_LCR_Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Connect_Panel.Visible = True
        Display_Panel.Visible = False
        Result_Panel.Visible = False
        Z_Panel.Visible = False
        RLC_Mode_Panel.Visible = True
        Block_Diagram_Panel.Visible = True
        'BackColor = Color.MediumSpringGreen
        'O_Button.BackColor = Color.LightGray
        'D_Button.BackColor = Color.LightGray
        'Mode_Select()

        'Deafult calib values
        ZC5L0 = New Complex(22373, -84980) 'Open 30kHz 180K
        ZC4 = New Complex(112937, -919805) 'Open 3kHz 180K
        ZL1 = New Complex(8830, -67074) 'Open 30kHz 15K 
        'ZC1R4 = New Complex(0.87, -1.07) 'Short 3kHz 100 Ohms
        ZC0 = New Complex(0.311, -6.477) 'Short 300Hz 100 Ohms


    End Sub
#End Region
#Region "Connect To COM Port"
    Sub GetSerialPortNames()
        ' Show all available COM ports.
        'Get SP data and write to ListBox
        For Each sp As String In My.Computer.Ports.SerialPortNames
            Com_Ports.Items.Add(sp)
        Next
    End Sub
    'Sub called to get serial port names
    Private Sub Get_Ports_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Get_Ports_Button.Click
        'Calls GetSerialPortnames()
        GetSerialPortNames()
        Click_to_Proceed_Button.Text = "Select Com Port"
    End Sub
    'shows available serial ports
    'on select removes other names from list
    'calls initialise of mPort with selected name
    Private Sub Available_Ports_ListBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Com_Ports.SelectedIndexChanged
        RemoveOtherItems()
        Initialize_232()
        Test_232()
        ''which_port_TextBox.Text = selected_com_port
    End Sub
    'Removes Other than selected Item from Available_Ports_ListBox
    Private Sub RemoveOtherItems()
        Dim selected_index, item_count, x As Integer
        ' Determine index value of selected item
        selected_index = Com_Ports.SelectedIndex
        item_count = Com_Ports.Items.Count

        selected_com_port = Com_Ports.Items(selected_index)

        ' Clear all items below selected
        For x = (item_count - 1) To (selected_index + 1) Step -1
            Com_Ports.Items.RemoveAt(x)
        Next x

        selected_index = Com_Ports.SelectedIndex
        item_count = Com_Ports.Items.Count

        ' Remove all items above selected item in the ListBox.
        For x = (selected_index - 1) To 0 Step -1
            Com_Ports.Items.RemoveAt(x)
        Next x
        '' Clear all selections in the ListBox.
        'Available_Ports_ListBox.ClearSelected()
        ' Remove all items below now top item in the ListBox.
    End Sub
    'Initializes selected port
    Private Sub Initialize_232()
        'Close the port before defining parameters
        If mPort.IsOpen Then
            mPort.Close()
        End If
        'COM port number selected
        mPort.PortName = selected_com_port
        'Speed of your link. 
        mPort.BaudRate = 115200 '9600
        'Data Terminal Ready signal. 
        'It's better to set this enable.
        mPort.DtrEnable = True
        'Data Ready to Send signal. 
        'It's better to set this enable.
        mPort.RtsEnable = True
        'Optional, If you want to send AT commands.
        mPort.NewLine = Chr(13)
        'Open the port if not already open
        If mPort.IsOpen Then
            'do nothing
        Else
            mPort.Open()
        End If
        'By Default Bits=8, Stop Bits=1, Parity=None
    End Sub

    'Test Communication with SigGen Hardware
    Private Sub Test_232()

        'Dim myTestReadString As String 'defined earlier

        myBuffer = mPort.ReadBufferSize
        mPort.DiscardInBuffer() 'Clear Input Buffer
        mPort.Write(test_cmd, 0, 1)  ' Write Test Command

        Try
            mPort.ReadTimeout = 1000
            myTestReadString = mPort.ReadLine
            If myTestReadString.Contains("Aj") Then
                Click_to_Proceed()
                'Click_to_Proceed_Button.Text = "Click to Proceed"
            Else
                Throw New Exception()
            End If

        Catch Exception As TimeoutException
            Click_to_Proceed_Button.Text = "Port Time Out"
            mPort.Close()
            'Finally
            'If mPort IsNot Nothing Then mPort.Close()
        End Try

    End Sub

#End Region

#Region "Button Commands"
    'Private Sub A_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles A_Button.Click
    

    'Private Sub Click_to_Proceed_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Click_to_Proceed_Button.Click
    'If Click_to_Proceed_Button.Text = "Click to Proceed" Then
    Private Sub Click_to_Proceed()
        Connect_Panel.Visible = False
        Display_Panel.Visible = True
        Result_Panel.Visible = True
        Block_Diagram_Panel.Visible = False
        'End If

    End Sub
    'Private Sub D_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles D_Button.Click
    Private Sub Display()
        Plot()
    End Sub
    Private Sub Z_RadioButton_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Label3.Text = "Impedence"
    End Sub

    Private Sub R_RadioButton_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles R_RadioButton.CheckedChanged
        Label3.Text = "Resistance"
    End Sub

    Private Sub L_RadioButton_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles L_RadioButton.CheckedChanged
        Label3.Text = "Inductance"
    End Sub

    Private Sub C_RadioButton_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C_RadioButton.CheckedChanged
        Label3.Text = "Capacitance"
    End Sub

    Private Sub Measure_Z_Button_Click(sender As Object, e As EventArgs) Handles Measure_Z_Button.Click
        Z_Panel.Visible = True
        RLC_Mode_Panel.Visible = False
    End Sub

    Private Sub Measure_RLC_Button_Click(sender As Object, e As EventArgs) Handles Measure_RLC_Button.Click
        Z_Panel.Visible = False
        RLC_Mode_Panel.Visible = True
    End Sub

    Private Sub Exit_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Exit_Button.Click
        'Run_Button.Text = "STOP"
        'Application.Exit()
        Me.Close()
    End Sub

#End Region


#Region "Acquire"
    Private Sub Acquire(ByVal ch12 As Byte, ByVal rset As Byte)
        Dim R As Integer
        Dim read_temp_msb, read_temp_lsb, read_temp1 As Double
        Dim rowno As Integer
        Dim MaxFreq_by_DataLength As String


        Application.DoEvents()
        set_switch_cmd(0) = rset 'resistance set
        mPort.Write(set_switch_cmd, 0, 1) 'write the Command String
        'Mode_Select()
        R = resistance

        System.Threading.Thread.Sleep(50) 'sleep 30mSec

        mPort.Write(compensate_freq_cmd, 0, 1) 'write the Compensate Command String
        System.Threading.Thread.Sleep(50) 'sleep 30mSec
        mPort.DiscardInBuffer() 'Clear Input Buffer



        'Acquire and Read CH1/CH2 Data

        acquire_cmd_Ch12(0) = ch12


        mPort.Write(acquire_cmd_Ch12, 0, 1) 'write the Command String
        While (mPort.BytesToRead = 0)  'wait for Done
        End While

        'Read Data CH1/CH2
        mPort.DiscardInBuffer() 'Clear Input Buffer
        'Issue Read Command for CH1 data
        mPort.Write(read_cmd, 0, 1) 'write the Read Command String
        'Sleep a bit
        System.Threading.Thread.Sleep(100) 'sleep 30mSec
        'Wait for data and read it 
        'While (mPort.BytesToRead = 0)
        'End While
        mPort.Read(Ch1_dataArray, 0, Data_Length) ' Read Serial data
        '-----------------
        ''repeat read
        'System.Threading.Thread.Sleep(50) 'sleep 30mSec
        'mPort.DiscardInBuffer() 'Clear Input Buffer
        ''Issue Read Command for CH1 data
        'mPort.Write(read_cmd, 0, 1) 'write the Read Command String
        ''Sleep a bit
        'System.Threading.Thread.Sleep(100) 'sleep 30mSec
        ''Wait for data and read it 
        ''While (mPort.BytesToRead = 0)
        ''End While
        'mPort.Read(Ch1_dataArray, 0, Data_Length) ' Read Serial data 512 Values 256 msb-lsb

        '---------------------------------------------
        'Switch Off the SWITCHES
        rset = &H4C 'L OFF
        set_switch_cmd(0) = rset 'resistance set
        mPort.Write(set_switch_cmd, 0, 1) 'write the Command String

        '----------------------------------------------
        'Scale and Move data to  Dim DataArray(401, 4) As String
        'rowno (0) = headers 
        'CH1 16 Bit all data msb-lsb 0-1,4-5,8-9,......
        For rowno = 1 To Data_Length / 4 '1 to 256
            read_temp_msb = Ch1_dataArray(rowno * 4 - 4) ' 0,4,8
            read_temp_lsb = Ch1_dataArray(rowno * 4 - 3) '1,5,9
            read_temp1 = read_temp_msb * 256 + read_temp_lsb
            read_temp1 = read_temp1 * 3 / 4096
            'For 12 Bit data 0 to 3V
            ' = Math.Round(read_temp1, 2)
            'DataArray(rowno, 1) = read_temp1
            Ch1_data(rowno) = read_temp1
        Next
        'Filter
        For rowno = 1 To (Data_Length / 4 - 6) '1 to 256
            Ch1_data(rowno) = (Ch1_data(rowno) + Ch1_data(rowno + 1) + Ch1_data(rowno + 2) _
               + Ch1_data(rowno + 3) + Ch1_data(rowno + 4)) / 5
            Ch1_data(rowno) = Math.Round(Ch1_data(rowno), 2)
            DataArray(rowno, 1) = Ch1_data(rowno)
        Next


        'CH2 16 Bit all msb-lsb data 2-3,6-7,10-11,.......
        For rowno = 1 To Data_Length / 4
            read_temp_msb = Ch1_dataArray(rowno * 4 - 2) '2,6,10
            read_temp_lsb = Ch1_dataArray(rowno * 4 - 1) '3,7,11
            read_temp1 = read_temp_msb * 256 + read_temp_lsb
            read_temp1 = read_temp1 * 3 / 4096
            'For 12 Bit data 0 to 3V
            'read_temp1 = Math.Round(read_temp1, 2)
            'DataArray(rowno, 2) = read_temp1
            Ch2_data(rowno) = read_temp1
        Next
        'Filter
        For rowno = 1 To (Data_Length / 4 - 6) '1 to 256
            Ch2_data(rowno) = (Ch2_data(rowno) + Ch2_data(rowno + 1) + Ch2_data(rowno + 2) _
               + Ch2_data(rowno + 3) + Ch2_data(rowno + 4)) / 5
            Ch2_data(rowno) = Math.Round(Ch2_data(rowno), 2)
            DataArray(rowno, 2) = Ch2_data(rowno)
        Next

        'Time/Sample and Frequency 
        MaxFreq_by_DataLength = (Max_Frequency / (Data_Length / 2))
        For rowno = 1 To Data_Length / 4
            DataArray(rowno, 0) = (rowno - 1) * Multiplier 'Time per sample
            DataArray(rowno - 1, 3) = (rowno - 1) * MaxFreq_by_DataLength 'Freq/sample
            'Because additional 0's are appended
        Next


    End Sub
#End Region
#Region "Plot"
    'Private Sub Plot_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Plot_Button.Click
    Private Sub Plot()
        Dim myPane As GraphPane = Aj_ZG1.GraphPane
        Dim rowno As Integer
        Dim Symbol As Boolean = False

        Dim list As New PointPairList
        Dim list2 As New PointPairList
        Dim x, y, y2 As String 'Double

        xcolumn = 0
        ycolumn = 1
        y1column = 2
        myPane.YAxis.Title.Text = "Vout Volts"
        myPane.Y2Axis.Title.Text = "Vin Volts"
        myPane.XAxis.Title.Text = "Sample Number"
        myPane.YAxis.IsVisible = True
        myPane.Y2Axis.IsVisible = True
        Sq_Display = False
        DFT_Display = False
        For rowno = 5 To (Data_Length / 4) - 8
            x = DataArray(rowno, 0)
            y = DataArray(rowno, 1)
            list.Add(x, y)
            y2 = DataArray(rowno, 2)
            list2.Add(x, y2)
        Next
        myPane.YAxis.Scale.Min = 0
        myPane.YAxis.Scale.Max = 3
        myPane.Y2Axis.Scale.Min = 0
        myPane.Y2Axis.Scale.Max = 3
        myPane.XAxis.Scale.Min = 5 ' 0
        myPane.XAxis.Scale.Max = Data_Length / 4 - 8 '* Multiplier




        'End of what to plot

        'Clear Previous Graph if Over Plot is Not Checked

        Aj_ZG1.GraphPane.CurveList.Clear()
        Aj_ZG1.GraphPane.GraphObjList.Clear()




        ' Set the Title
        myPane.Title.Text = "Aj ZRLC Test Plot" 'Title_TextBox.Text

        'Proceed with data processing Plotting only if no null values are detected
        'Populate the List assume no null values


        ' Generate a red curve with diamond symbols, and "Alpha" in the legend
        Dim myCurve As LineItem

        If Symbol = True Then
            myCurve = myPane.AddCurve(" ", list, Color.Red, SymbolType.Diamond)
            ' Fill the symbols with white
            myCurve.Symbol.Fill = New Fill(Color.White)
        Else
            myCurve = myPane.AddCurve(" ", list, Color.Red, SymbolType.None) 'No Symbols
        End If

        ' Generate a blue curve with circle symbols, and "Beta" in the legend

        If Symbol = True Then
            myCurve = myPane.AddCurve(" ", list2, Color.Blue, SymbolType.Circle)
            ' Fill the symbols with white
            myCurve.Symbol.Fill = New Fill(Color.White)
            ' Associate this curve with the Y2 axis
            myCurve.IsY2Axis = True
        Else
            myCurve = myPane.AddCurve(" ", list2, Color.Blue, SymbolType.None) 'No Symbols
            ' Associate this curve with the Y2 axis
            myCurve.IsY2Axis = True
        End If


        'Do not show the legends
        myPane.Legend.IsVisible = False
        ' Show the x axis grid
        myPane.XAxis.MajorGrid.IsVisible = True

        ' Make the Y axis scale red
        myPane.YAxis.Scale.FontSpec.FontColor = Color.Red
        myPane.YAxis.Title.FontSpec.FontColor = Color.Red
        ' turn off the opposite tics so the Y tics don't show up on the Y2 axis
        myPane.YAxis.MajorTic.IsOpposite = False
        myPane.YAxis.MinorTic.IsOpposite = False
        ' Don't display the Y zero line
        myPane.YAxis.MajorGrid.IsZeroLine = False
        ' Align the Y axis labels so they are flush to the axis
        myPane.YAxis.Scale.Align = AlignP.Inside
        ' Display the Y2 axis grid lines
        myPane.YAxis.MajorGrid.IsVisible = True

        ' Manually set the axis range




        'Enable the Y and Y2 axis display
        'myPane.YAxis.IsVisible = True


        ' Enable the Y2 axis display
        'myPane.Y2Axis.IsVisible = True
        'myPane.Y2Axis.Scale.Min = -12
        'myPane.Y2Axis.Scale.Max = 12

        ' Make the Y2 axis scale blue
        myPane.Y2Axis.Scale.FontSpec.FontColor = Color.Blue
        myPane.Y2Axis.Title.FontSpec.FontColor = Color.Blue
        ' turn off the opposite tics so the Y2 tics don't show up on the Y axis
        myPane.Y2Axis.MajorTic.IsOpposite = False
        myPane.Y2Axis.MinorTic.IsOpposite = False
        ' Display the Y2 axis grid lines
        myPane.Y2Axis.MajorGrid.IsVisible = True
        ' Align the Y2 axis labels so they are flush to the axis
        myPane.Y2Axis.Scale.Align = AlignP.Inside


        ' Fill the axis background with a gradient
        myPane.Chart.Fill = New Fill(Color.White, Color.LightGray, 45.0F)

        ' Add a text box with Aj-Scope Signature
        'Increaseing First 0.02F moves to Right , Reducing second 0.9F moves UP
        Dim mytext As New TextObj("Aj_" _
        & "S" & "c" & "o" & "p" & "e" & "2", 0.85F, 0.95F, _
        CoordType.ChartFraction, AlignH.Left, AlignV.Bottom)
        'mytext.FontSpec.StringAlignment = StringAlignment.Near
        myPane.GraphObjList.Add(mytext)



        ' Add a text box with instructions
        '
        'Dim text As New TextObj( _
        '"Zoom: left mouse & drag" & Chr(10) & "Pan: middle mouse & drag" & Chr(10) _
        '& "Context Menu: right mouse", _
        '0.05F, 0.95F, CoordType.ChartFraction, AlignH.Left, AlignV.Bottom)
        'text.FontSpec.StringAlignment = StringAlignment.Near
        'myPane.GraphObjList.Add(text)

        ' Enable scrollbars if needed
        Aj_ZG1.IsShowHScrollBar = True
        Aj_ZG1.IsShowVScrollBar = True
        Aj_ZG1.IsAutoScrollRange = True
        If display_y1 = False Then 'Y2 Not Required
            Aj_ZG1.IsScrollY2 = True
        End If
        Aj_ZG1.IsShowPointValues = True

        ' Size the control to fit the window
        'SetSize()

        ' Tell ZedGraph to calculate the axis ranges
        ' Note that you MUST call this after enabling IsAutoScrollRange, 
        'since AxisChange() sets
        ' up the proper scrolling parameters
        Aj_ZG1.AxisChange()
        ' Make sure the Graph gets redrawn
        Aj_ZG1.Invalidate()

        ' Size the control to fit the window
        'SetSize()


        'Not checking
        'MessageBox.Show("Null Entries Set To 0 - Exiting")

    End Sub


#End Region
#Region "MaxMin"

    Private Sub MaxMin()
        Dim v1by2, v2by2, sum As Double
        Dim val1, val2 As Boolean


        'Dim maxout, minout, amp1, amp2, Capacitance As Double
        Dim rowno As Integer
        'Dim freq, resistance As Integer
        Dim maxrow1, minrow1, maxrow2, minrow2 As Integer
        Dim sign1, sign2, period As Integer
        maxout = 0
        minout = 5
        For rowno = 5 To (Data_Length / 4 - 6)
            If DataArray(rowno, 1) > maxout Then
                maxout = DataArray(rowno, 1)
                maxrow1 = rowno
            End If
            If DataArray(rowno, 1) < minout Then
                minout = DataArray(rowno, 1)
                minrow1 = rowno
            End If
        Next
        amp1 = maxout - minout
        v1by2 = (maxout + minout) / 2
        Label1.Text = "Vout = " + Math.Round(amp1, 3).ToString + "V"

        maxout = 0
        minout = 5
        For rowno = 5 To (Data_Length / 4 - 6)
            If DataArray(rowno, 2) > maxout Then
                maxout = DataArray(rowno, 2)
                maxrow2 = rowno
            End If
            If DataArray(rowno, 2) < minout Then
                minout = DataArray(rowno, 2)
                minrow2 = rowno
            End If
        Next
        amp2 = maxout - minout
        v2by2 = (maxout + minout) / 2
        Label2.Text = "Vin = " + Math.Round(amp2, 3).ToString + "V"


        If F30K_RadioButton.Checked Then
            period = 32
        Else
            period = 64
        End If

        'This is the first method for sign computation 
        'later method is better
        sign1 = maxrow1 - maxrow2
        While sign1 > period / 2
            sign1 = sign1 - period
        End While
        sign2 = minrow1 - minrow2
        While sign2 > period / 2
            sign2 = sign2 - period
        End While
        'Label6.Text = maxrow1.ToString + "   " + maxrow2.ToString _
        '        + "   " + minrow1.ToString + "   " + minrow2.ToString _
        '        + "   " + sign1.ToString + "   " + sign2.ToString

        If sign1 < 0 Then
            sign = -1
        Else
            sign = 1
        End If
        gain = amp2 / amp1
        R = resistance

        gain = Math.Round(gain, 3)
        Label7.Text = "Vout/Vin =" + gain.ToString

        If gain > 0.9 Or gain < 0.1 Then
            Label6.Text = "Accuracy = Poor"
        Else
            Label6.Text = "Accuracy = Good"
        End If


        'Phase and sign better method
        sum = 0
        'jkQ = False

        For rowno = 16 To (16 + 63) 'equal to 360 deg
            If DataArray(rowno, 1) > v1by2 Then
                val1 = True
            Else
                val1 = False
            End If

            If DataArray(rowno + 16, 2) > v2by2 Then 'shifted by PI/2 deg
                val2 = True
            Else
                val2 = False
            End If

            'JK FF 'Has a problem as jkQ initialis cannot be defined correctly

            'If (val1 = False) And (val2 = False) Then
            '    jkQ = jkQ 'nochange
            'ElseIf (val1 = False) And (val2 = True) Then
            '    jkQ = False 'reset
            'ElseIf (val1 = True) And (val2 = False) Then
            '    jkQ = True 'set
            'ElseIf (val1 = True) And (val2 = True) Then
            '    jkQ = Not jkQ 'toggle
            'End If



            If (val1 Xor val2) = True Then
                sum = sum + 1
            Else
                sum = sum - 1
            End If
            'Sum -64 to 0 to +64 for 90deg lag  to 90 deg lead


            If sum < 0 Then
                sign = -1
            Else
                sign = 1
            End If

        Next
        sum = sum * 90 / 64


    End Sub
#End Region
#Region "Mode Select"

    Private Sub Mode_Select()

        If R_RadioButton.Checked Then
            Label3.Text = "Resistance"
        ElseIf L_RadioButton.Checked Then
            Label3.Text = "Inductance"
        ElseIf C_RadioButton.Checked Then
            Label3.Text = "Capacitance"

        End If
        If R180K_RadioButton.Checked Then
            resistance = 180000
            'R_Mode = 1
            rset = &H4B 'K 100K
            'mPort.Write(set_switch_cmd, 0, 1) 'write the Command String
        ElseIf R15K_RadioButton.Checked Then
            resistance = 15000
            'R_Mode = 2
            rset = &H4A 'J 10K
            'mPort.Write(set_switch_cmd, 0, 1) 'write the Command String
        ElseIf R1K2_RadioButton.Checked Then
            resistance = 1200
            'R_Mode = 3
            rset = &H48 'H 1K
            'mPort.Write(set_switch_cmd, 0, 1) 'write the Command String
        ElseIf R100_RadioButton.Checked Then
            resistance = R100
            'R_Mode = 4
            rset = &H47 'G 1K
            'mPort.Write(set_switch_cmd, 0, 1) 'write the Command String
        End If

        If F30K_RadioButton.Checked Then
            freq = F30K '32540
            'F_mode = 1
            ch12 = &H41
            'ch2 = &H42
        ElseIf F3K_RadioButton.Checked Then
            freq = 3000
            'F_mode = 2
            ch12 = &H42
            'ch2 = &H44
        ElseIf F300_RadioButton.Checked Then
            freq = 300
            'F_mode = 3
            ch12 = &H43
            'ch2 = &H46
        End If

        Label12.Text = "Test Frequency  " + freq.ToString + "  Hz"
        Label11.Text = "Test Resistance  " + resistance.ToString + " Ohms"

        Label17.Text = "Test Frequency  " + freq.ToString + "  Hz"
        Label18.Text = "Test Resistance  " + resistance.ToString + " Ohms"
    End Sub


#End Region
#Region "Measure ZRLC"
    'With  Corrections
    Private Sub ComputeZRLC()

        '------------------------Resistive Impedence---------------------------------
        If R_RadioButton.Checked Then
            'calculate_R()
            Label3.Text = "Resistance"
            Label15.Text = "Resistance"

            MaxMin()
            Resistor = (amp2 / (amp1 - amp2)) * resistance
            Resistor = Math.Round(Resistor, 3) - 2.5
            If Resistor > 1000000 Then
                Resistor = Resistor / 1000000
                Resistor = Math.Round(Resistor, 3)
                Label3.Text = "R = " + Resistor.ToString + " MOhms"
            ElseIf Resistor > 1000 Then
                Resistor = Resistor / 1000
                Resistor = Math.Round(Resistor, 3)
                Label3.Text = "R = " + Resistor.ToString + " KOhms"
            Else
                Resistor = Math.Round(Resistor, 3)
                Label3.Text = "R = " + Resistor.ToString + " Ohms"
            End If

            If R100_RadioButton.Checked = True And F3K_RadioButton.Checked = True Then
                mystep = 4
            End If

            R_Correct()

            '-----------------------Inductive Impedence-----------------------------------------

        ElseIf L_RadioButton.Checked Then
            'calculate_L()
            Label3.Text = "Inductance"
            MaxMin()
            'amp2 = amp2 / 2
            Inductance = ((amp2 / (amp1 - amp2)) * resistance) / (2 * PI * freq)
            Inductance = Math.Round(Inductance * 1000, 3)
            If Inductance > 1 Then
                Label3.Text = "L = " + Inductance.ToString + " mH"
            Else
                Inductance = Math.Round(Inductance * 1000, 3)
                Label3.Text = "L = " + Inductance.ToString + " uH"
            End If

            If R15K_RadioButton.Checked = True And F30K_RadioButton.Checked = True Then
                mystep = 1
            ElseIf R180K_RadioButton.Checked = True And (F30K_RadioButton.Checked = True) Then
                mystep = 0
            End If

            L_Correct()

            '--------------------------Capacitive Impedence------------------------------

        ElseIf C_RadioButton.Checked Then
            'calculate_C()
            Label3.Text = "Capacitance"
            MaxMin()
            'Const PI = 3.14159265
            'Compute capacitance
            Capacitance = (1 / (2 * PI * freq * resistance)) * Sqrt((amp1 / amp2) ^ 2 - 1)

            C = Capacitance
            Capacitance = Capacitance * (1000000000000) 'uF
            If Capacitance > 1000000 Then
                Capacitance = Capacitance / 1000000
                Capacitance = Math.Round(Capacitance, 3)
                Label3.Text = "C = " + Capacitance.ToString + " uF"
            ElseIf Capacitance > 1000 Then
                Capacitance = Capacitance / 1000
                Capacitance = Math.Round(Capacitance, 3)
                Label3.Text = "C = " + Capacitance.ToString + " nF"
            Else
                Capacitance = Math.Round(Capacitance, 3)
                Label3.Text = "C = " + Capacitance.ToString + " pF"
            End If


            'Correction for step 4 & 5
            If F30K_RadioButton.Checked = True And R180K_RadioButton.Checked = True Then
                mystep = 5
            ElseIf F3K_RadioButton.Checked = True And R180K_RadioButton.Checked = True Then
                mystep = 4
            ElseIf F3K_RadioButton.Checked = True And R100_RadioButton.Checked = True Then
                mystep = 1
            ElseIf F300_RadioButton.Checked = True And R100_RadioButton.Checked = True Then
                mystep = 0
            End If

            C_Correct()

        End If

        Label12.Text = "Test Frequency  " + freq.ToString + "  Hz"
        Label11.Text = "Test Resistance  " + resistance.ToString + " Ohms"
    End Sub
#End Region
#Region "Compute Z"
    Private Sub ComputeZ()
        Phase()
        Impedence() 'also writes the Z values
    End Sub
    Private Function Rxy(ByVal n As Integer, ByRef x() As Double, ByRef y() As Double) As Double
        'The code below will get  the cross correlation if you 
        'call it by:

        'crossCorrelation = Rxy(n, x, y)

        'You might need a value for pi:

        'Priivate Sub Form_Load()

        'PI = 4 * Atn(1.0#)

        'End Sub

        'to calculate the angle phi between the two vectors in 
        'degrees.  It gives a much more intuitive meaning than
        'the correlation itself does.  The formula for the angle 
        'is derived from cos(phi) = Rxy.  



        '   preconditions:  n is the number of pairs, n >0
        '                         n dimensional vector X
        '                         n dimensional vector Y
        '                         and at least one component of 
        '                                           X or Y is non-zero
        '   postcondition:   Rxy is the cross correlation of X, Y
        '
        '   (c) 2002 Reg Dodds
        '   May be freely used, and published as long as the
        '   layout and the comments are not altered in any way 
        '   at all.

        Dim i As Integer

        Dim sumX As Double
        Dim sumY As Double
        Dim sumXX As Double
        Dim sumYY As Double
        Dim sumXY As Double

        Dim over As Double
        Dim under As Double

        sumX = 0
        sumY = 0
        sumXX = 0
        sumYY = 0
        sumXY = 0

        For i = 1 To n

            sumX = sumX + x(i)
            sumY = sumY + y(i)
            sumXX = sumXX + x(i) * x(i)
            sumYY = sumYY + y(i) * y(i)
            sumXY = sumXY + x(i) * y(i)

        Next i

        If sumXX = 0 Or sumYY = 0 Then

            Rxy = 0     '  0 since the variance is 0

        Else

            over = (n * sumXY - sumX * sumY)
            under = (n * sumXX - sumX * sumX)
            '   a and b are the linear regression coefficients in 
            '   yEstimate(x) = a*x + b
            '   a = over / under
            '   b = (sumY * sumXX - sumX * sumXY) / under 
            Rxy = over / Sqrt((under * (n * sumYY - sumY * sumY)))
            '   pi = 4*atn(1)  '    Do this line in form_load 
            '   phi is the angle between the vector x and 
            '   the vector y given in degrees
            'phi = Atan(Sqrt(1 - Rxy * Rxy) / Rxy) * (180 / PI)
            '

        End If

    End Function
    Private Sub Phase() 'Phase using cross-corellation
        Dim crossCorrelation As Double = 0.0
        Dim J As Integer
        'crossCorrelation = Rxy(n, x, y)
        'Dim Ch1_data(255), Ch2_data(255) As Double

        For J = 0 To 127 '255
            Ch1_data(J) = DataArray(J, 1) 'Ch1_dataArray(J)
            Ch2_data(J) = DataArray(J, 2) 'Ch2_dataArray(J)
        Next
        crossCorrelation = Rxy(120, Ch1_data, Ch2_data)
        phi = Atan(Sqrt(1 - crossCorrelation * crossCorrelation) / crossCorrelation) ' * (180 / PI)
        'Label4.Text = "Phase = " + sign.ToString + Math.Round(phi * 180 / PI, 3).ToString + " deg"
        'Call
    End Sub
    Private Sub Impedence()
        'Using amp1, amp2, phi, resistance, sign
        Dim phi_deg, phi_estimate, phi_est_deg, temp_term As Double

        'Using Complex math gives the same values !! Zmag, Zmag1 & Zmag2
        'Dim Zmag2 As Double
        'Vout = New Complex(amp2 * Cos(phi), amp2 * Sin(phi))
        'Z2 = R * Vout / (amp1 - Vout)
        'Zmag2 = Z2.Magnitude

        'We alredy have the phi_deg from the correlate function
        'the sign will be added at the end to as +j/-j for Z= a + jB
        'First comupute Zmag
        'Working 0-80 deg of theta

        R = resistance
        phi_deg = (phi * 180 / PI) '- + 1

        temp_term = (amp1 - amp2 * Cos(phi)) ^ 2 + (amp2 * Sin(phi)) ^ 2

        Zmag = (R * amp2) / Sqrt(temp_term)


        deg = 0
        phi_est_deg = 0

        Do
            'While ((phi_deg - phi_est_deg) > 0.25)' And deg < 90)
            theta = deg * PI / 180
            phi_estimate = Atan((R * Sin(theta) / (Zmag + R * Cos(theta))))
            phi_est_deg = phi_estimate * 180 / PI
            deg = deg + 0.25
            'End While
        Loop While ((phi_deg - phi_est_deg) > 0.25 And deg < 95)

        phi_est_deg = Math.Round(phi_est_deg, 2)

        If sign = 1 Then
            Label4.Text = "Phase = " + "+ " + phi_est_deg.ToString + " deg"
        Else
            Label4.Text = "Phase = " + "- " + phi_est_deg.ToString + " deg"
            'Label5.Text = phi_est_deg.ToString + "  deg"
        End If


        A = Zmag * Cos(theta)
        B = Zmag * Sin(theta)

        A = Math.Round(A, 3)
        B = Math.Round(B, 3)
        B = B * sign

        Zmag1 = Math.Round(Zmag, 2)
        deg = Math.Round(deg, 2)
        deg = deg * sign
        'Zmag2 = Math.Round(Zmag2, 2)

        Label5.Text = Zmag1.ToString + "@ " + deg.ToString + "     " + A.ToString + "   " + B.ToString + " j "



    End Sub
#End Region
#Region "R-Auto"
    'Private Sub R_Auto_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles R_Auto_Button.Click
    Private Sub R_Auto()

        R_RadioButton.Checked = True
        'F1K_RadioButton.Checked = True
        'R100K_RadioButton.Checked = True
        'Application.DoEvents()

        'While (True)
        'Label3.Text = "Resistance"


        mystep = 1
        'Check 180K
        '45K to 720K and Greater
        resistance = 180000
        freq = 300
        ch12 = &H43 '300Hz
        R180K_RadioButton.Checked = True
        F300_RadioButton.Checked = True
        Label17.Text = "Test Frequency  " + freq.ToString + "  Hz"
        Label18.Text = "Test Resistance  " + resistance.ToString + " Ohms"

        rset = &H4B '180K
        Acquire(ch12, rset)
        MaxMin()
        gain = amp2 / amp1
        'R = resistance


        'Check 15K
        '3.75K to 60K
        If gain < 0.15 Then
            mystep = 2
            R15K_RadioButton.Checked = True
            resistance = 15000
            Label17.Text = "Test Frequency  " + freq.ToString + "  Hz"
            Label18.Text = "Test Resistance  " + resistance.ToString + " Ohms"

            rset = &H4A '15K
            Acquire(ch12, rset)
            MaxMin()
        End If

        gain = amp2 / amp1
        'R = resistance


        'Check 1.2K
        '300 Ohms to 4.8 k Ohms
        If gain < 0.15 Then
            mystep = 3
            R1K2_RadioButton.Checked = True
            resistance = 1200
            Label17.Text = "Test Frequency  " + freq.ToString + "  Hz"
            Label18.Text = "Test Resistance  " + resistance.ToString + " Ohms"

            rset = &H48 '1K2
            Acquire(ch12, rset)
            MaxMin()
        End If

        gain = amp2 / amp1
        'R = resistance


        'Check 100 at 3kHz
        '25 Ohms  to 400 Ohms
        If gain < 0.15 Then
            mystep = 4
            R100_RadioButton.Checked = True
            F3K_RadioButton.Checked = True
            resistance = R100
            freq = 3000
            Label17.Text = "Test Frequency  " + freq.ToString + "  Hz"
            Label18.Text = "Test Resistance  " + resistance.ToString + " Ohms"

            ch12 = &H42 '3kHz
            rset = &H47 '100 Ohms
            Acquire(ch12, rset)
            MaxMin()
        End If

        gain = amp2 / amp1
        'Label7.Text = Label7.Text + "  " + mystep.ToString

        'R = resistance
        'Display()

        Resistor = (amp2 / (amp1 - amp2)) * resistance
        Resistor = Math.Round(Resistor, 3) - 2.5
        If Resistor > 1000000 Then
            Resistor = Resistor / 1000000
            Resistor = Math.Round(Resistor, 3)
            Label3.Text = "R = " + Resistor.ToString + "  Meg Ohms"
            Label9.Text = "R = " + Resistor.ToString + "  MegOhms"
        ElseIf Resistor > 1000 Then
            Resistor = Resistor / 1000
            Resistor = Math.Round(Resistor, 3)
            Label3.Text = "R = " + Resistor.ToString + "  Kilo Ohms"
            Label9.Text = "R = " + Resistor.ToString + "  Kilo Ohms"
        Else
            Resistor = Math.Round(Resistor, 3)
            Label3.Text = "R = " + Resistor.ToString + "  Ohms"
            Label9.Text = "R = " + Resistor.ToString + "  Ohms"
        End If

        R_Correct()
        Display()
        System.Threading.Thread.Sleep(250) 'sleep 30mSec

    End Sub
#End Region
#Region "C-Auto"
    'Private Sub C_AutoButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C_AutoButton.Click
    Private Sub C_Auto()
        C_RadioButton.Checked = True
        mystep = 0
        'First Check 100 ohms  at 300 Hz
        '26uF to 3.98uF
        resistance = R100
        freq = 300
        ch12 = &H43 '100 Hz
        rset = &H47 '100 Ohms
        F300_RadioButton.Checked = True
        R100_RadioButton.Checked = True
        Label17.Text = "Test Frequency  " + freq.ToString + "  Hz"
        Label18.Text = "Test Resistance  " + resistance.ToString + " Ohms"
        Acquire(ch12, rset)
        MaxMin()

        gain = amp2 / amp1
        If gain < 0.85 Then
            'dont do anything
            'come out of If
        Else
            mystep = 1
            'Check 100 ohms  at 3000Hz
            '2.599uF to 0.3979uF
            resistance = R100
            freq = 3000
            rset = &H47 '100 Ohms
            ch12 = &H42 '3000Hz
            F3K_RadioButton.Checked = True
            R100_RadioButton.Checked = True
            Label17.Text = "Test Frequency  " + freq.ToString + "  Hz"
            Label18.Text = "Test Resistance  " + resistance.ToString + " Ohms"

            Acquire(ch12, rset)
            MaxMin()
            gain = amp2 / amp1
            If gain > 0.15 And gain < 0.85 Then
            Else
                mystep = 2
                'Next Check 1200 at 3000Hz
                '0.2166uF to 33.16nF
                resistance = 1200
                freq = 3000
                rset = &H48 '1.2K
                ch12 = &H42 '3000Hz
                F3K_RadioButton.Checked = True
                R1K2_RadioButton.Checked = True
                Label17.Text = "Test Frequency  " + freq.ToString + "  Hz"
                Label18.Text = "Test Resistance  " + resistance.ToString + " Ohms"

                Acquire(ch12, rset)
                MaxMin()
                gain = amp2 / amp1
                If gain > 0.15 And gain < 0.85 Then
                Else
                    mystep = 3
                    'Next Check 15K at 3kHz
                    '17.33nF to 2.65nF
                    resistance = 15000
                    rset = &H4A '15K Ohms
                    freq = 3000
                    ch12 = &H42 '3kHz
                    F3K_RadioButton.Checked = True
                    R15K_RadioButton.Checked = True
                    Label17.Text = "Test Frequency  " + freq.ToString + "  Hz"
                    Label18.Text = "Test Resistance  " + resistance.ToString + " Ohms"

                    Acquire(ch12, rset)
                    MaxMin()
                    gain = amp2 / amp1
                    If gain > 0.15 And gain < 0.85 Then
                    Else
                        mystep = 4
                        'Next Check 180K at 3kHz
                        '16nF to 2.3nF
                        resistance = 180000
                        rset = &H4B '180K Ohms
                        freq = 3000
                        ch12 = &H42 '3kHz
                        F3K_RadioButton.Checked = True
                        R180K_RadioButton.Checked = True
                        Label17.Text = "Test Frequency  " + freq.ToString + "  Hz"
                        Label18.Text = "Test Resistance  " + resistance.ToString + " Ohms"

                        Acquire(ch12, rset)
                        MaxMin()
                        gain = amp2 / amp1
                        If gain > 0.15 And gain < 0.85 Then
                        Else
                            mystep = 5
                            'Finally 180K at 32540Hz
                            '133pF to 20.38pF
                            resistance = 180000
                            rset = &H4B '180K Ohms
                            freq = F30K '32540
                            ch12 = &H41 '30kHz
                            F30K_RadioButton.Checked = True
                            R180K_RadioButton.Checked = True
                            Label17.Text = "Test Frequency  " + freq.ToString + "  Hz"
                            Label18.Text = "Test Resistance  " + resistance.ToString + " Ohms"

                            Acquire(ch12, rset)
                            MaxMin()

                            gain = amp2 / amp1

                        End If
                    End If
                End If
            End If
        End If
        'Label7.Text = Label7.Text + "  " + mystep.ToString

        'Display()


        'Compute capacitance based on magnitudes only
        Capacitance = (1 / (2 * PI * freq * resistance)) * Sqrt((amp1 / amp2) ^ 2 - 1)
        Capacitance = Capacitance * (1000000000000) 'uF


        If Capacitance > 1000000 Then
            Capacitance = Capacitance / 1000000
            Capacitance = Math.Round(Capacitance, 3)
            Label3.Text = "C = " + Capacitance.ToString + " uF"
        ElseIf Capacitance > 1000 Then
            Capacitance = Capacitance / 1000
            Capacitance = Math.Round(Capacitance, 3)
            Label3.Text = "C = " + Capacitance.ToString + " nF"
        Else
            Capacitance = Math.Round(Capacitance, 3)
            Label3.Text = "C = " + Capacitance.ToString + " pF"
        End If

        C_Correct() 'For mystep 5, 4, 1, 0 

        Label12.Text = "Test Frequency  " + freq.ToString + "  Hz"
        Label11.Text = "Test Resistance  " + resistance.ToString + " Ohms"

        Display()
        System.Threading.Thread.Sleep(250) 'sleep 30mSec

    End Sub
#End Region
#Region "L-Auto"
    'Private Sub L_Button_Click(sender As Object, e As EventArgs) Handles L_Button.Click
    Private Sub L_Auto()
        L_RadioButton.Checked = True

        'Label3.Text = "Inductance"

        'Check 180K at 30kHz
        '221mH to 3.51H and Greater
        mystep = 0
        resistance = 180000
        freq = F30K '32540
        ch12 = &H41 '30kHz
        R180K_RadioButton.Checked = True
        F30K_RadioButton.Checked = True
        Label17.Text = "Test Frequency  " + freq.ToString + "  Hz"
        Label18.Text = "Test Resistance  " + resistance.ToString + " Ohms"
        rset = &H4B '180K
        Acquire(ch12, rset)
        MaxMin()
        gain = amp2 / amp1

        'Check 15K at 30kHz (no change in freq)
        '18mH to 294mH
        If gain < 0.15 Then
            mystep = 1
            R15K_RadioButton.Checked = True
            resistance = 15000
            Label17.Text = "Test Frequency  " + freq.ToString + "  Hz"
            Label18.Text = "Test Resistance  " + resistance.ToString + " Ohms"
            rset = &H4A '15K
            Acquire(ch12, rset)
            MaxMin()
        End If

        gain = amp2 / amp1

        'Check 1.2K  at 30kHz (no change in freq)
        '1.471mH to 24mH
        If gain < 0.15 Then
            mystep = 2
            R1K2_RadioButton.Checked = True
            resistance = 1200
            Label17.Text = "Test Frequency  " + freq.ToString + "  Hz"
            Label18.Text = "Test Resistance  " + resistance.ToString + " Ohms"
            rset = &H48 '1K2
            Acquire(ch12, rset)
            MaxMin()
        End If

        gain = amp2 / amp1

        'Check 100 at 30kHz (no change in freq)
        '122mH and lower to 1.962mH
        If gain < 0.15 Then
            mystep = 3
            R100_RadioButton.Checked = True
            resistance = R100
            Label17.Text = "Test Frequency  " + freq.ToString + "  Hz"
            Label18.Text = "Test Resistance  " + resistance.ToString + " Ohms"
            rset = &H47 '100 Ohms
            Acquire(ch12, rset)
            MaxMin()
        End If

        gain = amp2 / amp1
        'Label7.Text = Label7.Text + "  " + mystep.ToString


        'Display()
        'calculate_L()

        MaxMin()

        Inductance = ((amp2 / (amp1 - amp2)) * resistance) / (2 * PI * freq)
        Inductance = Math.Round(Inductance * 1000, 3)
        If Inductance > 1 Then
            Label3.Text = Inductance.ToString + " mH"
        Else
            Inductance = Math.Round(Inductance * 1000, 3)
            Label3.Text = Inductance.ToString + " uH"
        End If

        L_Correct()

        Display()
        System.Threading.Thread.Sleep(250) 'sleep 30mSec
        'ComputeZ()

    End Sub


#End Region
#Region "Correct R L C"
    Private Sub C_Correct()
        ComputeZ()

        'Write uncorrected values

        Z2 = New Complex(Zmag * Cos(theta), sign * Zmag * Sin(theta))

        Capacitance = (-1 / (Complex.Imag(Z2) * 2 * PI * freq)) * 10 ^ 12

        If Capacitance > 1000000 Then
            Capacitance = Capacitance / 1000000
            Capacitance = Math.Round(Capacitance, 3)
            Label15.Text = "C = " + Capacitance.ToString + " uF"
        ElseIf Capacitance > 1000 Then
            Capacitance = Capacitance / 1000
            Capacitance = Math.Round(Capacitance, 3)
            Label15.Text = "C = " + Capacitance.ToString + " nF"
        Else
            Capacitance = Math.Round(Capacitance, 3)
            Label15.Text = "C = " + Capacitance.ToString + " pF"
        End If

        Zmag1 = Complex.Abs(Z2)
        deg = Complex.Arg(Z2) * 180 / PI

        A = Zmag1 * Cos(Complex.Arg(Z2))
        B = Zmag1 * Sin(Complex.Arg(Z2))

        Zmag1 = Math.Round(Zmag1, 2)
        deg = Math.Round(deg, 2)

        A = Math.Round(A, 2)
        B = Math.Round(B, 2)

        Label5.Text = Zmag1.ToString + "@ " + deg.ToString + "     " + A.ToString + "  " + B.ToString + " j "



        'Compute corrected values

        If mystep = 5 Then
            Z1 = ZC5L0
            Z3 = (Z1 * Z2) / (Z1 - Z2)
            Capacitance = (-1 / (Complex.Imag(Z3) * 2 * PI * freq)) * 10 ^ 12
        ElseIf mystep = 4 Then
            Z1 = ZC4
            Z3 = (Z1 * Z2) / (Z1 - Z2)
            Capacitance = (-1 / (Complex.Imag(Z3) * 2 * PI * freq)) * 10 ^ 12
            'ElseIf mystep = 1 Then

            '    Z1 = ZC1R4
            '    Z3 = Z2 - Z1
            '    Capacitance = (-1 / (Z3.Imaginary * 2 * PI * freq)) * 10 ^ 12
        ElseIf mystep = 0 Then
            Z1 = ZC0
            Z3 = Z2 - Z1
            Capacitance = (-1 / (Complex.Imag(Z3) * 2 * PI * freq)) * 10 ^ 12
        Else
            Z3 = Z2
            Capacitance = (-1 / (Complex.Imag(Z3) * 2 * PI * freq)) * 10 ^ 12
        End If



        If Capacitance > 1000000 Then
            Capacitance = Capacitance / 1000000
            Capacitance = Math.Round(Capacitance, 3)
            Label9.Text = "C = " + Capacitance.ToString + " uF"
        ElseIf Capacitance > 1000 Then
            Capacitance = Capacitance / 1000
            Capacitance = Math.Round(Capacitance, 3)
            Label9.Text = "C = " + Capacitance.ToString + " nF"
        Else
            Capacitance = Math.Round(Capacitance, 3)
            Label9.Text = "C = " + Capacitance.ToString + " pF"
        End If

        Zmag1 = Complex.Abs(Z3) 'Magnitude
        deg = Complex.Arg(Z3) * 180 / PI
        'Complex.Arg(Z3) = phase
        A = Zmag1 * Cos(Complex.Arg(Z3))
        B = Zmag1 * Sin(Complex.Arg(Z3))

        Zmag1 = Math.Round(Zmag1, 2)
        deg = Math.Round(deg, 2)

        A = Math.Round(A, 2)
        B = Math.Round(B, 2)

        Label10.Text = Zmag1.ToString + "@ " + deg.ToString + "     " + A.ToString + "  " + B.ToString + " j "

        Label12.Text = "Test Frequency  " + freq.ToString + "  Hz"
        Label11.Text = "Test Resistance  " + resistance.ToString + " Ohms"

    End Sub
    Private Sub R_Correct()
        'To Compensate for Z of Output Capacitor 100uF
        ComputeZ()
        'write uncorrected values Label5 and Label15
        Label5.Text = Zmag1.ToString + "@ " + deg.ToString + "     " + A.ToString + "  " + B.ToString + " j "

        'A is the real part of Z rounded off to 2 places
        If A > 1000000 Then
            A = A / 1000000
            A = Math.Round(A, 3)
            'Label9.Text = "R = " + A.ToString + "  MegOhms"
            Label15.Text = "R = " + A.ToString + "  MegOhms"
        ElseIf A > 1000 Then
            A = A / 1000
            A = Math.Round(A, 3)
            'Label9.Text = "R = " + A.ToString + "  Kilo Ohms"
            Label15.Text = "R = " + A.ToString + "  Kilo Ohms"

        Else
            A = Math.Round(A, 3)
            'Label9.Text = "R = " + A.ToString + "  Ohms"
            Label15.Text = "R = " + A.ToString + "  Ohms"
        End If

        'Label5.Text = Zmag1.ToString + "@ " + deg.ToString + "     " + A.ToString + "  " + B.ToString + " j "



        'Compute correction and write corrected values Label9 and label10

        Z2 = New Complex(Zmag * Cos(theta), sign * Zmag * Sin(theta))


        ' If mystep = 4 Then ' 100 Ohms 3kHz 
        'Z1 = ZC1R4
        'Z3 = Z2 - Z1

        'Else
        Z3 = Z2
        'End If


        A = Z3.Real
        B = Complex.Imag(Z3) 'Imaginary

        deg = Atan(B / A) * (180 / PI)

        A = Math.Round(A, 3)
        B = Math.Round(B, 2)
        deg = Math.Round(deg, 2)

        Label10.Text = Zmag1.ToString + "@ " + deg.ToString + "     " + A.ToString + "  " + B.ToString + " j "

        'A is the real part of Z rounded off to 3 places
        If A > 1000000 Then
            A = A / 1000000
            A = Math.Round(A, 3)
            Label9.Text = "R = " + A.ToString + "  MegOhms"
        ElseIf A > 1000 Then
            A = A / 1000
            A = Math.Round(A, 3)
            Label9.Text = "R = " + A.ToString + "  Kilo Ohms"
        Else
            A = Math.Round(A, 3)
            Label9.Text = "R = " + A.ToString + "  Ohms"
        End If
        A = Math.Round(A, 2)
        'Label10.Text = Zmag1.ToString + "@ " + deg.ToString + "     " + A.ToString + "  " + B.ToString + " j "


        Label12.Text = "Test Frequency  " + freq.ToString + "  Hz"
        Label11.Text = "Test Resistance  " + resistance.ToString + " Ohms"


    End Sub
    Private Sub L_Correct()
        ComputeZ()

        Z2 = New Complex(Zmag * Cos(theta), sign * Zmag * Sin(theta))

        'write uncorrected values
        B = Zmag * Sin(theta)
        Inductance = B / (2 * PI * freq)
        Inductance = Inductance * 10 ^ 3
        Inductance = Math.Round(Inductance, 3)
        If Inductance > 1 Then
            'Label9.Text = "L= " + Inductance.ToString + " mH"
            Label15.Text = "L= " + Inductance.ToString + " mH"
        Else
            Inductance = Math.Round(Inductance * 1000, 3)
            'Label9.Text = "L= " + Inductance.ToString + " uH"
            Label15.Text = "L= " + Inductance.ToString + " uH"
        End If

        B = Math.Round(B, 2)
        A = Math.Round(A, 2)
        deg = Math.Round(deg, 2)

        Label5.Text = Zmag1.ToString + "@ " + deg.ToString + "     " + A.ToString + "   " + B.ToString + " j "




        If mystep = 0 Then
            Z1 = ZC5L0
            Z3 = (Z1 * Z2) / (Z1 - Z2)
        ElseIf mystep = 1 Then
            Z1 = ZL1
            Z3 = (Z1 * Z2) / (Z1 - Z2)
        Else
            Z3 = Z2
        End If

        A = Z3.Real
        B = Complex.Imag(Z3)
        deg = Atan(B / A)

        Zmag1 = Complex.Abs(Z3) 'Magnitude
        deg = deg * (180 / PI)



        Inductance = B / (2 * PI * freq)
        Inductance = Inductance * 10 ^ 3
        Inductance = Math.Round(Inductance, 3)
        If Inductance > 1 Then
            Label9.Text = "L= " + Inductance.ToString + " mH"
            'Label15.Text = "L= " + Inductance.ToString + " mH"
        Else
            Inductance = Math.Round(Inductance * 1000, 3)
            Label9.Text = "L= " + Inductance.ToString + " uH"
            'Label15.Text = "L= " + Inductance.ToString + " uH"
        End If

        A = Math.Round(A, 2)
        B = Math.Round(B, 2)
        Zmag1 = Math.Round(Zmag1, 2)
        deg = Math.Round(deg, 2)

        Label10.Text = Zmag1.ToString + "@ " + deg.ToString + "     " + A.ToString + "   " + B.ToString + " j "


        Label12.Text = "Test Frequency  " + freq.ToString + "  Hz"
        Label11.Text = "Test Resistance  " + resistance.ToString + " Ohms"



    End Sub
#End Region
#Region "Calib"
    Private Sub Open_Calib_Button_Click(sender As Object, e As EventArgs) Handles Open_Calib_Button.Click
        Calib_Open()
    End Sub
    Private Sub Calib_Open()
        'For C 180K at 32540Hz and 180K at 3kHz
        C_RadioButton.Checked = True
        mystep = 5
        'First Zero value for 180K at 32540Hz
        '133pF to 20.38pF
        resistance = 180000
        rset = &H4B '180K Ohms
        freq = F30K '32540
        ch12 = &H41 '30kHz
        F30K_RadioButton.Checked = True
        R180K_RadioButton.Checked = True
        Acquire(ch12, rset)
        MaxMin()
        'Display()
        ComputeZ()
        ZC5L0 = New Complex(Zmag * Cos(theta), sign * Zmag * Sin(theta))

        mystep = 4
        'Next Check 180K at 3kHz
        '16nF to 2.3nF
        resistance = 180000
        rset = &H4B '180K Ohms
        freq = 3000
        ch12 = &H42 '3kHz
        F3K_RadioButton.Checked = True
        R180K_RadioButton.Checked = True
        Acquire(ch12, rset)
        MaxMin()
        'Display()
        ComputeZ()
        ZC4 = New Complex(Zmag * Cos(theta), sign * Zmag * Sin(theta))

        'Check 15K at 30kHz 
        '18mH to 294mH
        mystep = 2
        freq = F30K '32540
        ch12 = &H41 '30kHz
        F30K_RadioButton.Checked = True
        R15K_RadioButton.Checked = True
        resistance = 15000
        rset = &H4A '15K
        Acquire(ch12, rset)
        'Display()
        ComputeZ()
        ZL1 = New Complex(Zmag * Cos(theta), sign * Zmag * Sin(theta))

    End Sub
    Private Sub Calib_Short_Button_Click(sender As Object, e As EventArgs) Handles Calib_Short_Button.Click
        Calib_Short()
    End Sub
    Private Sub Calib_Short()
        C_RadioButton.Checked = True
        mystep = 0
        'First Check 100 ohms  at 300 Hz
        '26uF to 3.98uF
        resistance = R100
        freq = 300
        ch12 = &H43 '100 Hz
        rset = &H47 '100 Ohms
        F300_RadioButton.Checked = True
        R100_RadioButton.Checked = True
        Acquire(ch12, rset)
        MaxMin()
        'Display()
        ComputeZ()
        ZC0 = New Complex(Zmag * Cos(theta), sign * Zmag * Sin(theta))

        'mystep = 1
        ''Check 100 ohms  at 3000Hz
        ''2.599uF to 0.3979uF
        'resistance = R100
        'freq = 3000
        'rset = &H47 '100 Ohms
        'ch12 = &H42 '3000Hz
        'F3K_RadioButton.Checked = True
        'R100_RadioButton.Checked = True
        'Acquire(ch12, rset)
        'MaxMin()
        ''Display()
        'ComputeZ()
        'ZC1R4 = New Complex(Zmag * Cos(theta), sign * Zmag * Sin(theta))


    End Sub
#End Region
#Region "Run"
    Private Sub Run_Button_Click(sender As Object, e As EventArgs) Handles Run_Button.Click

        If mPort.IsOpen Then 'Only execute if connected

            'Dim keep_running As Boolean
            If Z_Panel.Visible = True Then
                Measure_Z()
            ElseIf RLC_Mode_Panel.Visible = True Then

                If Run_Button.Text = "MEASURE" Then

                    If Single_RadioButton.Checked = True Then
                        keep_running = False
                    Else
                        keep_running = True
                    End If


                    Run_Button.Text = "STOP"

                    Do
                        If Resistance_RadioButton.Checked = True Then
                            R_Auto()
                        ElseIf Capacitance_RadioButton.Checked = True Then
                            C_Auto()
                        ElseIf Inductance_RadioButton.Checked = True Then
                            L_Auto()
                        End If

                        Application.DoEvents()

                    Loop While (keep_running)


                End If

                'Run_Button is pressed when in "STOP" comes here 
                keep_running = False
                'Single_RadioButton.Checked = True
                Run_Button.Text = "MEASURE"
            End If
        Else
            MessageBox.Show("Not Connected")
        End If
    End Sub
    Private Sub Measure_Z()
        Mode_Select()
        Acquire(ch12, rset) 'acquired data with Freq and Switch settings
        Display()
        ComputeZRLC()
    End Sub
#End Region




End Class



