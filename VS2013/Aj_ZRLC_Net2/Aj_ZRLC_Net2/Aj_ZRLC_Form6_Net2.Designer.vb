<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Aj_ZRLC_Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Aj_ZRLC_Form1))
        Me.Connect_Panel = New System.Windows.Forms.Panel()
        Me.Connect_GroupBox = New System.Windows.Forms.GroupBox()
        Me.Click_to_Proceed_Button = New System.Windows.Forms.Button()
        Me.Com_Ports = New System.Windows.Forms.ListBox()
        Me.Get_Ports_Button = New System.Windows.Forms.Button()
        Me.Display_Panel = New System.Windows.Forms.Panel()
        Me.Aj_ZG1 = New ZedGraph.ZedGraphControl()
        Me.Mode_GroupBox = New System.Windows.Forms.GroupBox()
        Me.Measure_Z_Button = New System.Windows.Forms.Button()
        Me.Measure_RLC_Button = New System.Windows.Forms.Button()
        Me.Calib_Short_Button = New System.Windows.Forms.Button()
        Me.Exit_Button = New System.Windows.Forms.Button()
        Me.Open_Calib_Button = New System.Windows.Forms.Button()
        Me.Run_Button = New System.Windows.Forms.Button()
        Me.Select_RLC_GroupBox = New System.Windows.Forms.GroupBox()
        Me.Capacitance_RadioButton = New System.Windows.Forms.RadioButton()
        Me.Resistance_RadioButton = New System.Windows.Forms.RadioButton()
        Me.Inductance_RadioButton = New System.Windows.Forms.RadioButton()
        Me.Run_Mode_GroupBox = New System.Windows.Forms.GroupBox()
        Me.Single_RadioButton = New System.Windows.Forms.RadioButton()
        Me.Repeat_RadioButton = New System.Windows.Forms.RadioButton()
        Me.Test_Freq_GroupBox = New System.Windows.Forms.GroupBox()
        Me.F300_RadioButton = New System.Windows.Forms.RadioButton()
        Me.F3K_RadioButton = New System.Windows.Forms.RadioButton()
        Me.F30K_RadioButton = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Resistance_GroupBox = New System.Windows.Forms.GroupBox()
        Me.R180K_RadioButton = New System.Windows.Forms.RadioButton()
        Me.R100_RadioButton = New System.Windows.Forms.RadioButton()
        Me.R1K2_RadioButton = New System.Windows.Forms.RadioButton()
        Me.R15K_RadioButton = New System.Windows.Forms.RadioButton()
        Me.Measurment_Mode_GroupBox = New System.Windows.Forms.GroupBox()
        Me.C_RadioButton = New System.Windows.Forms.RadioButton()
        Me.L_RadioButton = New System.Windows.Forms.RadioButton()
        Me.R_RadioButton = New System.Windows.Forms.RadioButton()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Result_Panel = New System.Windows.Forms.Panel()
        Me.Result_GroupBox = New System.Windows.Forms.GroupBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Raw_Values_GroupBox = New System.Windows.Forms.GroupBox()
        Me.Z_Panel = New System.Windows.Forms.Panel()
        Me.Z_GroupBox = New System.Windows.Forms.GroupBox()
        Me.RLC_Mode_Panel = New System.Windows.Forms.Panel()
        Me.RLC_Mode_GroupBox = New System.Windows.Forms.GroupBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Block_Diagram_Panel = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Connect_Panel.SuspendLayout()
        Me.Connect_GroupBox.SuspendLayout()
        Me.Display_Panel.SuspendLayout()
        Me.Mode_GroupBox.SuspendLayout()
        Me.Select_RLC_GroupBox.SuspendLayout()
        Me.Run_Mode_GroupBox.SuspendLayout()
        Me.Test_Freq_GroupBox.SuspendLayout()
        Me.Resistance_GroupBox.SuspendLayout()
        Me.Measurment_Mode_GroupBox.SuspendLayout()
        Me.Result_Panel.SuspendLayout()
        Me.Result_GroupBox.SuspendLayout()
        Me.Raw_Values_GroupBox.SuspendLayout()
        Me.Z_Panel.SuspendLayout()
        Me.Z_GroupBox.SuspendLayout()
        Me.RLC_Mode_Panel.SuspendLayout()
        Me.RLC_Mode_GroupBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'Connect_Panel
        '
        Me.Connect_Panel.Controls.Add(Me.Connect_GroupBox)
        Me.Connect_Panel.Location = New System.Drawing.Point(5, 4)
        Me.Connect_Panel.Name = "Connect_Panel"
        Me.Connect_Panel.Size = New System.Drawing.Size(377, 170)
        Me.Connect_Panel.TabIndex = 0
        '
        'Connect_GroupBox
        '
        Me.Connect_GroupBox.Controls.Add(Me.Click_to_Proceed_Button)
        Me.Connect_GroupBox.Controls.Add(Me.Com_Ports)
        Me.Connect_GroupBox.Controls.Add(Me.Get_Ports_Button)
        Me.Connect_GroupBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Connect_GroupBox.Location = New System.Drawing.Point(7, 8)
        Me.Connect_GroupBox.Name = "Connect_GroupBox"
        Me.Connect_GroupBox.Size = New System.Drawing.Size(352, 159)
        Me.Connect_GroupBox.TabIndex = 0
        Me.Connect_GroupBox.TabStop = False
        Me.Connect_GroupBox.Text = "Connect to COM Port"
        '
        'Click_to_Proceed_Button
        '
        Me.Click_to_Proceed_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Click_to_Proceed_Button.Location = New System.Drawing.Point(72, 118)
        Me.Click_to_Proceed_Button.Name = "Click_to_Proceed_Button"
        Me.Click_to_Proceed_Button.Size = New System.Drawing.Size(148, 28)
        Me.Click_to_Proceed_Button.TabIndex = 29
        Me.Click_to_Proceed_Button.Text = "Wait for Connection"
        Me.Click_to_Proceed_Button.UseVisualStyleBackColor = True
        '
        'Com_Ports
        '
        Me.Com_Ports.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Com_Ports.FormattingEnabled = True
        Me.Com_Ports.ItemHeight = 16
        Me.Com_Ports.Location = New System.Drawing.Point(72, 60)
        Me.Com_Ports.Name = "Com_Ports"
        Me.Com_Ports.Size = New System.Drawing.Size(148, 52)
        Me.Com_Ports.TabIndex = 26
        '
        'Get_Ports_Button
        '
        Me.Get_Ports_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Get_Ports_Button.Location = New System.Drawing.Point(72, 23)
        Me.Get_Ports_Button.Name = "Get_Ports_Button"
        Me.Get_Ports_Button.Size = New System.Drawing.Size(148, 28)
        Me.Get_Ports_Button.TabIndex = 27
        Me.Get_Ports_Button.Text = "Available Com Ports"
        Me.Get_Ports_Button.UseVisualStyleBackColor = True
        '
        'Display_Panel
        '
        Me.Display_Panel.Controls.Add(Me.Aj_ZG1)
        Me.Display_Panel.Location = New System.Drawing.Point(419, 7)
        Me.Display_Panel.Name = "Display_Panel"
        Me.Display_Panel.Size = New System.Drawing.Size(496, 389)
        Me.Display_Panel.TabIndex = 1
        '
        'Aj_ZG1
        '
        Me.Aj_ZG1.Location = New System.Drawing.Point(0, 0)
        Me.Aj_ZG1.Name = "Aj_ZG1"
        Me.Aj_ZG1.ScrollGrace = 0.0R
        Me.Aj_ZG1.ScrollMaxX = 0.0R
        Me.Aj_ZG1.ScrollMaxY = 0.0R
        Me.Aj_ZG1.ScrollMaxY2 = 0.0R
        Me.Aj_ZG1.ScrollMinX = 0.0R
        Me.Aj_ZG1.ScrollMinY = 0.0R
        Me.Aj_ZG1.ScrollMinY2 = 0.0R
        Me.Aj_ZG1.Size = New System.Drawing.Size(468, 368)
        Me.Aj_ZG1.TabIndex = 1
        '
        'Mode_GroupBox
        '
        Me.Mode_GroupBox.Controls.Add(Me.Measure_Z_Button)
        Me.Mode_GroupBox.Controls.Add(Me.Measure_RLC_Button)
        Me.Mode_GroupBox.Controls.Add(Me.Calib_Short_Button)
        Me.Mode_GroupBox.Controls.Add(Me.Exit_Button)
        Me.Mode_GroupBox.Controls.Add(Me.Open_Calib_Button)
        Me.Mode_GroupBox.Location = New System.Drawing.Point(428, 404)
        Me.Mode_GroupBox.Name = "Mode_GroupBox"
        Me.Mode_GroupBox.Size = New System.Drawing.Size(487, 107)
        Me.Mode_GroupBox.TabIndex = 0
        Me.Mode_GroupBox.TabStop = False
        Me.Mode_GroupBox.Text = "Commands"
        '
        'Measure_Z_Button
        '
        Me.Measure_Z_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Measure_Z_Button.Location = New System.Drawing.Point(6, 17)
        Me.Measure_Z_Button.Name = "Measure_Z_Button"
        Me.Measure_Z_Button.Size = New System.Drawing.Size(106, 28)
        Me.Measure_Z_Button.TabIndex = 14
        Me.Measure_Z_Button.Text = "IMPEDENCE"
        Me.Measure_Z_Button.UseVisualStyleBackColor = True
        '
        'Measure_RLC_Button
        '
        Me.Measure_RLC_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Measure_RLC_Button.Location = New System.Drawing.Point(118, 17)
        Me.Measure_RLC_Button.Name = "Measure_RLC_Button"
        Me.Measure_RLC_Button.Size = New System.Drawing.Size(106, 28)
        Me.Measure_RLC_Button.TabIndex = 13
        Me.Measure_RLC_Button.Text = "R  L  C"
        Me.Measure_RLC_Button.UseVisualStyleBackColor = True
        '
        'Calib_Short_Button
        '
        Me.Calib_Short_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Calib_Short_Button.Location = New System.Drawing.Point(338, 17)
        Me.Calib_Short_Button.Name = "Calib_Short_Button"
        Me.Calib_Short_Button.Size = New System.Drawing.Size(106, 28)
        Me.Calib_Short_Button.TabIndex = 7
        Me.Calib_Short_Button.Text = "Cal-Short"
        Me.Calib_Short_Button.UseVisualStyleBackColor = True
        '
        'Exit_Button
        '
        Me.Exit_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Exit_Button.Location = New System.Drawing.Point(182, 69)
        Me.Exit_Button.Name = "Exit_Button"
        Me.Exit_Button.Size = New System.Drawing.Size(106, 28)
        Me.Exit_Button.TabIndex = 2
        Me.Exit_Button.Text = "EXIT"
        Me.Exit_Button.UseVisualStyleBackColor = True
        '
        'Open_Calib_Button
        '
        Me.Open_Calib_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Open_Calib_Button.Location = New System.Drawing.Point(230, 17)
        Me.Open_Calib_Button.Name = "Open_Calib_Button"
        Me.Open_Calib_Button.Size = New System.Drawing.Size(102, 28)
        Me.Open_Calib_Button.TabIndex = 6
        Me.Open_Calib_Button.Text = "Cal-Open"
        Me.Open_Calib_Button.UseVisualStyleBackColor = True
        '
        'Run_Button
        '
        Me.Run_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Run_Button.Location = New System.Drawing.Point(127, 473)
        Me.Run_Button.Name = "Run_Button"
        Me.Run_Button.Size = New System.Drawing.Size(106, 28)
        Me.Run_Button.TabIndex = 12
        Me.Run_Button.Text = "MEASURE"
        Me.Run_Button.UseVisualStyleBackColor = True
        '
        'Select_RLC_GroupBox
        '
        Me.Select_RLC_GroupBox.Controls.Add(Me.Capacitance_RadioButton)
        Me.Select_RLC_GroupBox.Controls.Add(Me.Resistance_RadioButton)
        Me.Select_RLC_GroupBox.Controls.Add(Me.Inductance_RadioButton)
        Me.Select_RLC_GroupBox.Location = New System.Drawing.Point(118, 21)
        Me.Select_RLC_GroupBox.Name = "Select_RLC_GroupBox"
        Me.Select_RLC_GroupBox.Size = New System.Drawing.Size(106, 91)
        Me.Select_RLC_GroupBox.TabIndex = 13
        Me.Select_RLC_GroupBox.TabStop = False
        Me.Select_RLC_GroupBox.Text = "Select_RLC"
        '
        'Capacitance_RadioButton
        '
        Me.Capacitance_RadioButton.AutoSize = True
        Me.Capacitance_RadioButton.Location = New System.Drawing.Point(11, 65)
        Me.Capacitance_RadioButton.Name = "Capacitance_RadioButton"
        Me.Capacitance_RadioButton.Size = New System.Drawing.Size(85, 17)
        Me.Capacitance_RadioButton.TabIndex = 11
        Me.Capacitance_RadioButton.Text = "Capacitance"
        Me.Capacitance_RadioButton.UseVisualStyleBackColor = True
        '
        'Resistance_RadioButton
        '
        Me.Resistance_RadioButton.AutoSize = True
        Me.Resistance_RadioButton.Checked = True
        Me.Resistance_RadioButton.Location = New System.Drawing.Point(11, 19)
        Me.Resistance_RadioButton.Name = "Resistance_RadioButton"
        Me.Resistance_RadioButton.Size = New System.Drawing.Size(78, 17)
        Me.Resistance_RadioButton.TabIndex = 9
        Me.Resistance_RadioButton.TabStop = True
        Me.Resistance_RadioButton.Text = "Resistance"
        Me.Resistance_RadioButton.UseVisualStyleBackColor = True
        '
        'Inductance_RadioButton
        '
        Me.Inductance_RadioButton.AutoSize = True
        Me.Inductance_RadioButton.Location = New System.Drawing.Point(11, 42)
        Me.Inductance_RadioButton.Name = "Inductance_RadioButton"
        Me.Inductance_RadioButton.Size = New System.Drawing.Size(79, 17)
        Me.Inductance_RadioButton.TabIndex = 10
        Me.Inductance_RadioButton.Text = "Inductance"
        Me.Inductance_RadioButton.UseVisualStyleBackColor = True
        '
        'Run_Mode_GroupBox
        '
        Me.Run_Mode_GroupBox.Controls.Add(Me.Single_RadioButton)
        Me.Run_Mode_GroupBox.Controls.Add(Me.Repeat_RadioButton)
        Me.Run_Mode_GroupBox.Location = New System.Drawing.Point(8, 21)
        Me.Run_Mode_GroupBox.Name = "Run_Mode_GroupBox"
        Me.Run_Mode_GroupBox.Size = New System.Drawing.Size(100, 91)
        Me.Run_Mode_GroupBox.TabIndex = 11
        Me.Run_Mode_GroupBox.TabStop = False
        Me.Run_Mode_GroupBox.Text = "Run_Mode"
        '
        'Single_RadioButton
        '
        Me.Single_RadioButton.AutoSize = True
        Me.Single_RadioButton.Checked = True
        Me.Single_RadioButton.Location = New System.Drawing.Point(10, 19)
        Me.Single_RadioButton.Name = "Single_RadioButton"
        Me.Single_RadioButton.Size = New System.Drawing.Size(54, 17)
        Me.Single_RadioButton.TabIndex = 9
        Me.Single_RadioButton.TabStop = True
        Me.Single_RadioButton.Text = "Single"
        Me.Single_RadioButton.UseVisualStyleBackColor = True
        '
        'Repeat_RadioButton
        '
        Me.Repeat_RadioButton.AutoSize = True
        Me.Repeat_RadioButton.Location = New System.Drawing.Point(10, 42)
        Me.Repeat_RadioButton.Name = "Repeat_RadioButton"
        Me.Repeat_RadioButton.Size = New System.Drawing.Size(60, 17)
        Me.Repeat_RadioButton.TabIndex = 10
        Me.Repeat_RadioButton.Text = "Repeat"
        Me.Repeat_RadioButton.UseVisualStyleBackColor = True
        '
        'Test_Freq_GroupBox
        '
        Me.Test_Freq_GroupBox.Controls.Add(Me.F300_RadioButton)
        Me.Test_Freq_GroupBox.Controls.Add(Me.F3K_RadioButton)
        Me.Test_Freq_GroupBox.Controls.Add(Me.F30K_RadioButton)
        Me.Test_Freq_GroupBox.Location = New System.Drawing.Point(122, 21)
        Me.Test_Freq_GroupBox.Name = "Test_Freq_GroupBox"
        Me.Test_Freq_GroupBox.Size = New System.Drawing.Size(106, 91)
        Me.Test_Freq_GroupBox.TabIndex = 6
        Me.Test_Freq_GroupBox.TabStop = False
        Me.Test_Freq_GroupBox.Text = "Test Frequency"
        '
        'F300_RadioButton
        '
        Me.F300_RadioButton.AutoSize = True
        Me.F300_RadioButton.Location = New System.Drawing.Point(6, 65)
        Me.F300_RadioButton.Name = "F300_RadioButton"
        Me.F300_RadioButton.Size = New System.Drawing.Size(59, 17)
        Me.F300_RadioButton.TabIndex = 2
        Me.F300_RadioButton.Text = "300 Hz"
        Me.F300_RadioButton.UseVisualStyleBackColor = True
        '
        'F3K_RadioButton
        '
        Me.F3K_RadioButton.AutoSize = True
        Me.F3K_RadioButton.Checked = True
        Me.F3K_RadioButton.Location = New System.Drawing.Point(6, 42)
        Me.F3K_RadioButton.Name = "F3K_RadioButton"
        Me.F3K_RadioButton.Size = New System.Drawing.Size(65, 17)
        Me.F3K_RadioButton.TabIndex = 1
        Me.F3K_RadioButton.TabStop = True
        Me.F3K_RadioButton.Text = "3000 Hz"
        Me.F3K_RadioButton.UseVisualStyleBackColor = True
        '
        'F30K_RadioButton
        '
        Me.F30K_RadioButton.AutoSize = True
        Me.F30K_RadioButton.Location = New System.Drawing.Point(6, 19)
        Me.F30K_RadioButton.Name = "F30K_RadioButton"
        Me.F30K_RadioButton.Size = New System.Drawing.Size(56, 17)
        Me.F30K_RadioButton.TabIndex = 0
        Me.F30K_RadioButton.Text = "30kHz"
        Me.F30K_RadioButton.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 16)
        Me.Label1.MinimumSize = New System.Drawing.Size(60, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Vin"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(88, 16)
        Me.Label2.MinimumSize = New System.Drawing.Size(60, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Vout"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(29, 58)
        Me.Label3.MinimumSize = New System.Drawing.Size(100, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(100, 16)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "R-L-C"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Resistance_GroupBox
        '
        Me.Resistance_GroupBox.Controls.Add(Me.R180K_RadioButton)
        Me.Resistance_GroupBox.Controls.Add(Me.R100_RadioButton)
        Me.Resistance_GroupBox.Controls.Add(Me.R1K2_RadioButton)
        Me.Resistance_GroupBox.Controls.Add(Me.R15K_RadioButton)
        Me.Resistance_GroupBox.Location = New System.Drawing.Point(242, 21)
        Me.Resistance_GroupBox.Name = "Resistance_GroupBox"
        Me.Resistance_GroupBox.Size = New System.Drawing.Size(136, 91)
        Me.Resistance_GroupBox.TabIndex = 11
        Me.Resistance_GroupBox.TabStop = False
        Me.Resistance_GroupBox.Text = "Test Resistance"
        '
        'R180K_RadioButton
        '
        Me.R180K_RadioButton.AutoSize = True
        Me.R180K_RadioButton.Location = New System.Drawing.Point(75, 42)
        Me.R180K_RadioButton.Name = "R180K_RadioButton"
        Me.R180K_RadioButton.Size = New System.Drawing.Size(50, 17)
        Me.R180K_RadioButton.TabIndex = 3
        Me.R180K_RadioButton.Text = "180K"
        Me.R180K_RadioButton.UseVisualStyleBackColor = True
        '
        'R100_RadioButton
        '
        Me.R100_RadioButton.AutoSize = True
        Me.R100_RadioButton.Location = New System.Drawing.Point(10, 42)
        Me.R100_RadioButton.Name = "R100_RadioButton"
        Me.R100_RadioButton.Size = New System.Drawing.Size(43, 17)
        Me.R100_RadioButton.TabIndex = 2
        Me.R100_RadioButton.Text = "100"
        Me.R100_RadioButton.UseVisualStyleBackColor = True
        '
        'R1K2_RadioButton
        '
        Me.R1K2_RadioButton.AutoSize = True
        Me.R1K2_RadioButton.Checked = True
        Me.R1K2_RadioButton.Location = New System.Drawing.Point(10, 19)
        Me.R1K2_RadioButton.Name = "R1K2_RadioButton"
        Me.R1K2_RadioButton.Size = New System.Drawing.Size(47, 17)
        Me.R1K2_RadioButton.TabIndex = 1
        Me.R1K2_RadioButton.TabStop = True
        Me.R1K2_RadioButton.Text = "1.2K"
        Me.R1K2_RadioButton.UseVisualStyleBackColor = True
        '
        'R15K_RadioButton
        '
        Me.R15K_RadioButton.AutoSize = True
        Me.R15K_RadioButton.Location = New System.Drawing.Point(75, 21)
        Me.R15K_RadioButton.Name = "R15K_RadioButton"
        Me.R15K_RadioButton.Size = New System.Drawing.Size(44, 17)
        Me.R15K_RadioButton.TabIndex = 0
        Me.R15K_RadioButton.Text = "15K"
        Me.R15K_RadioButton.UseVisualStyleBackColor = True
        '
        'Measurment_Mode_GroupBox
        '
        Me.Measurment_Mode_GroupBox.Controls.Add(Me.C_RadioButton)
        Me.Measurment_Mode_GroupBox.Controls.Add(Me.L_RadioButton)
        Me.Measurment_Mode_GroupBox.Controls.Add(Me.R_RadioButton)
        Me.Measurment_Mode_GroupBox.Location = New System.Drawing.Point(11, 21)
        Me.Measurment_Mode_GroupBox.Name = "Measurment_Mode_GroupBox"
        Me.Measurment_Mode_GroupBox.Size = New System.Drawing.Size(100, 91)
        Me.Measurment_Mode_GroupBox.TabIndex = 12
        Me.Measurment_Mode_GroupBox.TabStop = False
        Me.Measurment_Mode_GroupBox.Text = "Select Z "
        '
        'C_RadioButton
        '
        Me.C_RadioButton.AutoSize = True
        Me.C_RadioButton.Location = New System.Drawing.Point(9, 65)
        Me.C_RadioButton.Name = "C_RadioButton"
        Me.C_RadioButton.Size = New System.Drawing.Size(85, 17)
        Me.C_RadioButton.TabIndex = 3
        Me.C_RadioButton.Text = "Capacitive Z"
        Me.C_RadioButton.UseVisualStyleBackColor = True
        '
        'L_RadioButton
        '
        Me.L_RadioButton.AutoSize = True
        Me.L_RadioButton.Location = New System.Drawing.Point(10, 42)
        Me.L_RadioButton.Name = "L_RadioButton"
        Me.L_RadioButton.Size = New System.Drawing.Size(79, 17)
        Me.L_RadioButton.TabIndex = 2
        Me.L_RadioButton.Text = "Inductive Z"
        Me.L_RadioButton.UseVisualStyleBackColor = True
        '
        'R_RadioButton
        '
        Me.R_RadioButton.AutoSize = True
        Me.R_RadioButton.Checked = True
        Me.R_RadioButton.Location = New System.Drawing.Point(11, 19)
        Me.R_RadioButton.Name = "R_RadioButton"
        Me.R_RadioButton.Size = New System.Drawing.Size(78, 17)
        Me.R_RadioButton.TabIndex = 1
        Me.R_RadioButton.TabStop = True
        Me.R_RadioButton.Text = "Resistive Z"
        Me.R_RadioButton.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(249, 16)
        Me.Label4.MinimumSize = New System.Drawing.Size(100, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(100, 13)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Phase Difference"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(13, 99)
        Me.Label5.MinimumSize = New System.Drawing.Size(300, 20)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(300, 20)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "Impedence"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(253, 119)
        Me.Label6.MinimumSize = New System.Drawing.Size(125, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(125, 13)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "Accuracy"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(154, 16)
        Me.Label7.MinimumSize = New System.Drawing.Size(70, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(70, 13)
        Me.Label7.TabIndex = 16
        Me.Label7.Text = "Gain "
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(101, 31)
        Me.Label9.MinimumSize = New System.Drawing.Size(150, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(163, 24)
        Me.Label9.TabIndex = 18
        Me.Label9.Text = "Component Value"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(7, 97)
        Me.Label10.MinimumSize = New System.Drawing.Size(350, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(350, 20)
        Me.Label10.TabIndex = 19
        Me.Label10.Text = "Component Impedence"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Result_Panel
        '
        Me.Result_Panel.Controls.Add(Me.Result_GroupBox)
        Me.Result_Panel.Location = New System.Drawing.Point(5, 4)
        Me.Result_Panel.Name = "Result_Panel"
        Me.Result_Panel.Size = New System.Drawing.Size(408, 170)
        Me.Result_Panel.TabIndex = 20
        '
        'Result_GroupBox
        '
        Me.Result_GroupBox.Controls.Add(Me.Label8)
        Me.Result_GroupBox.Controls.Add(Me.Label12)
        Me.Result_GroupBox.Controls.Add(Me.Label11)
        Me.Result_GroupBox.Controls.Add(Me.Label10)
        Me.Result_GroupBox.Controls.Add(Me.Label9)
        Me.Result_GroupBox.Location = New System.Drawing.Point(10, 3)
        Me.Result_GroupBox.Name = "Result_GroupBox"
        Me.Result_GroupBox.Size = New System.Drawing.Size(384, 158)
        Me.Result_GroupBox.TabIndex = 0
        Me.Result_GroupBox.TabStop = False
        Me.Result_GroupBox.Text = "Results"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(13, 138)
        Me.Label12.MinimumSize = New System.Drawing.Size(150, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(150, 13)
        Me.Label12.TabIndex = 21
        Me.Label12.Text = "Test Frequency"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(188, 138)
        Me.Label11.MinimumSize = New System.Drawing.Size(150, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(150, 13)
        Me.Label11.TabIndex = 20
        Me.Label11.Text = "Test Resistance"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(45, 42)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(72, 13)
        Me.Label13.TabIndex = 21
        Me.Label13.Text = "By-Magnitude"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(220, 42)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(75, 13)
        Me.Label14.TabIndex = 22
        Me.Label14.Text = "By-Impedence"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(207, 58)
        Me.Label15.MinimumSize = New System.Drawing.Size(100, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(100, 16)
        Me.Label15.TabIndex = 23
        Me.Label15.Text = "R-L-C"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(19, 86)
        Me.Label16.MinimumSize = New System.Drawing.Size(300, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(300, 13)
        Me.Label16.TabIndex = 24
        Me.Label16.Text = "Un-Corrected Impedence Ohms"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Raw_Values_GroupBox
        '
        Me.Raw_Values_GroupBox.Controls.Add(Me.Label15)
        Me.Raw_Values_GroupBox.Controls.Add(Me.Label13)
        Me.Raw_Values_GroupBox.Controls.Add(Me.Label16)
        Me.Raw_Values_GroupBox.Controls.Add(Me.Label6)
        Me.Raw_Values_GroupBox.Controls.Add(Me.Label7)
        Me.Raw_Values_GroupBox.Controls.Add(Me.Label14)
        Me.Raw_Values_GroupBox.Controls.Add(Me.Label4)
        Me.Raw_Values_GroupBox.Controls.Add(Me.Label5)
        Me.Raw_Values_GroupBox.Controls.Add(Me.Label3)
        Me.Raw_Values_GroupBox.Controls.Add(Me.Label1)
        Me.Raw_Values_GroupBox.Controls.Add(Me.Label2)
        Me.Raw_Values_GroupBox.Location = New System.Drawing.Point(15, 180)
        Me.Raw_Values_GroupBox.Name = "Raw_Values_GroupBox"
        Me.Raw_Values_GroupBox.Size = New System.Drawing.Size(384, 139)
        Me.Raw_Values_GroupBox.TabIndex = 25
        Me.Raw_Values_GroupBox.TabStop = False
        Me.Raw_Values_GroupBox.Text = "Raw Values"
        '
        'Z_Panel
        '
        Me.Z_Panel.Controls.Add(Me.Z_GroupBox)
        Me.Z_Panel.Location = New System.Drawing.Point(4, 338)
        Me.Z_Panel.Name = "Z_Panel"
        Me.Z_Panel.Size = New System.Drawing.Size(409, 129)
        Me.Z_Panel.TabIndex = 26
        '
        'Z_GroupBox
        '
        Me.Z_GroupBox.Controls.Add(Me.Measurment_Mode_GroupBox)
        Me.Z_GroupBox.Controls.Add(Me.Resistance_GroupBox)
        Me.Z_GroupBox.Controls.Add(Me.Test_Freq_GroupBox)
        Me.Z_GroupBox.Location = New System.Drawing.Point(11, 3)
        Me.Z_GroupBox.Name = "Z_GroupBox"
        Me.Z_GroupBox.Size = New System.Drawing.Size(384, 121)
        Me.Z_GroupBox.TabIndex = 13
        Me.Z_GroupBox.TabStop = False
        Me.Z_GroupBox.Text = "Impedence Mode"
        '
        'RLC_Mode_Panel
        '
        Me.RLC_Mode_Panel.Controls.Add(Me.RLC_Mode_GroupBox)
        Me.RLC_Mode_Panel.Location = New System.Drawing.Point(-1, 338)
        Me.RLC_Mode_Panel.Name = "RLC_Mode_Panel"
        Me.RLC_Mode_Panel.Size = New System.Drawing.Size(414, 129)
        Me.RLC_Mode_Panel.TabIndex = 27
        Me.RLC_Mode_Panel.Visible = False
        '
        'RLC_Mode_GroupBox
        '
        Me.RLC_Mode_GroupBox.Controls.Add(Me.Label18)
        Me.RLC_Mode_GroupBox.Controls.Add(Me.Label17)
        Me.RLC_Mode_GroupBox.Controls.Add(Me.Select_RLC_GroupBox)
        Me.RLC_Mode_GroupBox.Controls.Add(Me.Run_Mode_GroupBox)
        Me.RLC_Mode_GroupBox.Location = New System.Drawing.Point(10, 3)
        Me.RLC_Mode_GroupBox.Name = "RLC_Mode_GroupBox"
        Me.RLC_Mode_GroupBox.Size = New System.Drawing.Size(390, 121)
        Me.RLC_Mode_GroupBox.TabIndex = 0
        Me.RLC_Mode_GroupBox.TabStop = False
        Me.RLC_Mode_GroupBox.Text = "R L C Auto Mode"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(230, 65)
        Me.Label18.MinimumSize = New System.Drawing.Size(150, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(150, 13)
        Me.Label18.TabIndex = 23
        Me.Label18.Text = "Test Resistance"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(228, 42)
        Me.Label17.MinimumSize = New System.Drawing.Size(150, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(150, 13)
        Me.Label17.TabIndex = 22
        Me.Label17.Text = "Test Frequency"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Block_Diagram_Panel
        '
        Me.Block_Diagram_Panel.BackgroundImage = Global.Aj_ZRLC_Net2.My.Resources.Resources.BlockDia_1
        Me.Block_Diagram_Panel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Block_Diagram_Panel.Location = New System.Drawing.Point(419, 4)
        Me.Block_Diagram_Panel.Name = "Block_Diagram_Panel"
        Me.Block_Diagram_Panel.Size = New System.Drawing.Size(496, 396)
        Me.Block_Diagram_Panel.TabIndex = 2
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(38, 84)
        Me.Label8.MinimumSize = New System.Drawing.Size(300, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(300, 13)
        Me.Label8.TabIndex = 25
        Me.Label8.Text = "Corrected Impedence Ohms"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Aj_ZRLC_Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(927, 518)
        Me.Controls.Add(Me.Block_Diagram_Panel)
        Me.Controls.Add(Me.Mode_GroupBox)
        Me.Controls.Add(Me.Run_Button)
        Me.Controls.Add(Me.Raw_Values_GroupBox)
        Me.Controls.Add(Me.Result_Panel)
        Me.Controls.Add(Me.Display_Panel)
        Me.Controls.Add(Me.Connect_Panel)
        Me.Controls.Add(Me.Z_Panel)
        Me.Controls.Add(Me.RLC_Mode_Panel)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Aj_ZRLC_Form1"
        Me.Text = "Aj - ZRLC"
        Me.Connect_Panel.ResumeLayout(False)
        Me.Connect_GroupBox.ResumeLayout(False)
        Me.Display_Panel.ResumeLayout(False)
        Me.Mode_GroupBox.ResumeLayout(False)
        Me.Select_RLC_GroupBox.ResumeLayout(False)
        Me.Select_RLC_GroupBox.PerformLayout()
        Me.Run_Mode_GroupBox.ResumeLayout(False)
        Me.Run_Mode_GroupBox.PerformLayout()
        Me.Test_Freq_GroupBox.ResumeLayout(False)
        Me.Test_Freq_GroupBox.PerformLayout()
        Me.Resistance_GroupBox.ResumeLayout(False)
        Me.Resistance_GroupBox.PerformLayout()
        Me.Measurment_Mode_GroupBox.ResumeLayout(False)
        Me.Measurment_Mode_GroupBox.PerformLayout()
        Me.Result_Panel.ResumeLayout(False)
        Me.Result_GroupBox.ResumeLayout(False)
        Me.Result_GroupBox.PerformLayout()
        Me.Raw_Values_GroupBox.ResumeLayout(False)
        Me.Raw_Values_GroupBox.PerformLayout()
        Me.Z_Panel.ResumeLayout(False)
        Me.Z_GroupBox.ResumeLayout(False)
        Me.RLC_Mode_Panel.ResumeLayout(False)
        Me.RLC_Mode_GroupBox.ResumeLayout(False)
        Me.RLC_Mode_GroupBox.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Connect_Panel As System.Windows.Forms.Panel
    Friend WithEvents Connect_GroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents Com_Ports As System.Windows.Forms.ListBox
    Friend WithEvents Get_Ports_Button As System.Windows.Forms.Button
    Friend WithEvents Click_to_Proceed_Button As System.Windows.Forms.Button
    Friend WithEvents Display_Panel As System.Windows.Forms.Panel
    Friend WithEvents Mode_GroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents Exit_Button As System.Windows.Forms.Button
    Friend WithEvents Aj_ZG1 As ZedGraph.ZedGraphControl
    Friend WithEvents Test_Freq_GroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Resistance_GroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents R100_RadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents R1K2_RadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents R15K_RadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents F300_RadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents F3K_RadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents F30K_RadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents Measurment_Mode_GroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents C_RadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents L_RadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents R_RadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents R180K_RadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Open_Calib_Button As System.Windows.Forms.Button
    Friend WithEvents Result_Panel As System.Windows.Forms.Panel
    Friend WithEvents Result_GroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Calib_Short_Button As System.Windows.Forms.Button
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Raw_Values_GroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents Z_Panel As System.Windows.Forms.Panel
    Friend WithEvents Z_GroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents Select_RLC_GroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents Capacitance_RadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents Resistance_RadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents Inductance_RadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents Run_Button As System.Windows.Forms.Button
    Friend WithEvents Run_Mode_GroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents Single_RadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents Repeat_RadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents RLC_Mode_Panel As System.Windows.Forms.Panel
    Friend WithEvents RLC_Mode_GroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Measure_Z_Button As System.Windows.Forms.Button
    Friend WithEvents Measure_RLC_Button As System.Windows.Forms.Button
    Friend WithEvents Block_Diagram_Panel As System.Windows.Forms.Panel
    Friend WithEvents Label8 As System.Windows.Forms.Label

End Class
