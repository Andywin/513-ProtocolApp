namespace CommunicationApp
{
    partial class CommunicationForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.comboPortName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBaudrate = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonBySerialPort = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txGet = new System.Windows.Forms.TextBox();
            this.buttonSendByRawData = new System.Windows.Forms.Button();
            this.txSend = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.buttonLoadPtc = new System.Windows.Forms.Button();
            this.buttonSavePtc = new System.Windows.Forms.Button();
            this.buttonTimingSendByPtc = new System.Windows.Forms.Button();
            this.buttonClearSend = new System.Windows.Forms.Button();
            this.buttonSendByProtocol = new System.Windows.Forms.Button();
            this.buttonChoosePtcSend = new System.Windows.Forms.Button();
            this.dgvSendData = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusDataSent = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusDataRcv = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonClearReceive = new System.Windows.Forms.Button();
            this.dgvReceiveData = new System.Windows.Forms.DataGridView();
            this.buttonChoosePtcReceive = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.buttonTimingSendByRaw = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonByNet = new System.Windows.Forms.Button();
            this.textBoxPortNum = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxIpAddress = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.radioButtonTCP = new System.Windows.Forms.RadioButton();
            this.radioButtonUdp = new System.Windows.Forms.RadioButton();
            this.ProtocolNameRcv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProtocolDataTypeRcv = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ProtocolDataLengthRcv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartingPositionRcv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CountRcv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataContentRcv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RawDataRcv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSendData)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReceiveData)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboPortName
            // 
            this.comboPortName.FormattingEnabled = true;
            this.comboPortName.Location = new System.Drawing.Point(72, 21);
            this.comboPortName.Name = "comboPortName";
            this.comboPortName.Size = new System.Drawing.Size(121, 20);
            this.comboPortName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(11, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "串口：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(11, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "波特率：";
            // 
            // comboBaudrate
            // 
            this.comboBaudrate.FormattingEnabled = true;
            this.comboBaudrate.Items.AddRange(new object[] {
            "300",
            "600",
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.comboBaudrate.Location = new System.Drawing.Point(72, 54);
            this.comboBaudrate.Name = "comboBaudrate";
            this.comboBaudrate.Size = new System.Drawing.Size(121, 20);
            this.comboBaudrate.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboPortName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comboBaudrate);
            this.groupBox1.Controls.Add(this.buttonBySerialPort);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(622, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(212, 123);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "串口通信";
            // 
            // buttonBySerialPort
            // 
            this.buttonBySerialPort.Location = new System.Drawing.Point(84, 91);
            this.buttonBySerialPort.Name = "buttonBySerialPort";
            this.buttonBySerialPort.Size = new System.Drawing.Size(109, 23);
            this.buttonBySerialPort.TabIndex = 5;
            this.buttonBySerialPort.Text = "开启串口收发";
            this.buttonBySerialPort.UseVisualStyleBackColor = true;
            this.buttonBySerialPort.Click += new System.EventHandler(this.buttonBySerialPort_Click);
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(740, 348);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(75, 23);
            this.buttonReset.TabIndex = 6;
            this.buttonReset.Text = "复位";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 240);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "原始数据";
            // 
            // txGet
            // 
            this.txGet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txGet.Location = new System.Drawing.Point(9, 281);
            this.txGet.Multiline = true;
            this.txGet.Name = "txGet";
            this.txGet.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txGet.Size = new System.Drawing.Size(579, 50);
            this.txGet.TabIndex = 0;
            // 
            // buttonSendByRawData
            // 
            this.buttonSendByRawData.Location = new System.Drawing.Point(483, 278);
            this.buttonSendByRawData.Name = "buttonSendByRawData";
            this.buttonSendByRawData.Size = new System.Drawing.Size(93, 23);
            this.buttonSendByRawData.TabIndex = 6;
            this.buttonSendByRawData.Text = "发送原始数据";
            this.buttonSendByRawData.UseVisualStyleBackColor = true;
            this.buttonSendByRawData.Click += new System.EventHandler(this.buttonSendByRawData_Click);
            // 
            // txSend
            // 
            this.txSend.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txSend.Location = new System.Drawing.Point(9, 281);
            this.txSend.Multiline = true;
            this.txSend.Name = "txSend";
            this.txSend.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txSend.Size = new System.Drawing.Size(468, 47);
            this.txSend.TabIndex = 4;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.buttonLoadPtc);
            this.groupBox4.Controls.Add(this.buttonSavePtc);
            this.groupBox4.Controls.Add(this.buttonTimingSendByPtc);
            this.groupBox4.Controls.Add(this.buttonClearSend);
            this.groupBox4.Controls.Add(this.buttonSendByProtocol);
            this.groupBox4.Controls.Add(this.buttonChoosePtcSend);
            this.groupBox4.Controls.Add(this.dgvSendData);
            this.groupBox4.Location = new System.Drawing.Point(3, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(579, 254);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "数据内容协议";
            // 
            // buttonLoadPtc
            // 
            this.buttonLoadPtc.Location = new System.Drawing.Point(87, 226);
            this.buttonLoadPtc.Name = "buttonLoadPtc";
            this.buttonLoadPtc.Size = new System.Drawing.Size(75, 23);
            this.buttonLoadPtc.TabIndex = 15;
            this.buttonLoadPtc.Text = "加载协议";
            this.buttonLoadPtc.UseVisualStyleBackColor = true;
            this.buttonLoadPtc.Click += new System.EventHandler(this.buttonLoadPtc_Click);
            // 
            // buttonSavePtc
            // 
            this.buttonSavePtc.Location = new System.Drawing.Point(6, 226);
            this.buttonSavePtc.Name = "buttonSavePtc";
            this.buttonSavePtc.Size = new System.Drawing.Size(75, 23);
            this.buttonSavePtc.TabIndex = 13;
            this.buttonSavePtc.Text = "保存协议";
            this.buttonSavePtc.UseVisualStyleBackColor = true;
            this.buttonSavePtc.Click += new System.EventHandler(this.buttonSavePtc_Click);
            // 
            // buttonTimingSendByPtc
            // 
            this.buttonTimingSendByPtc.Location = new System.Drawing.Point(480, 226);
            this.buttonTimingSendByPtc.Name = "buttonTimingSendByPtc";
            this.buttonTimingSendByPtc.Size = new System.Drawing.Size(94, 23);
            this.buttonTimingSendByPtc.TabIndex = 14;
            this.buttonTimingSendByPtc.Text = "开启定时发送";
            this.buttonTimingSendByPtc.UseVisualStyleBackColor = true;
            this.buttonTimingSendByPtc.Click += new System.EventHandler(this.buttonTimingSendByPtc_Click);
            // 
            // buttonClearSend
            // 
            this.buttonClearSend.Location = new System.Drawing.Point(237, 226);
            this.buttonClearSend.Name = "buttonClearSend";
            this.buttonClearSend.Size = new System.Drawing.Size(75, 23);
            this.buttonClearSend.TabIndex = 13;
            this.buttonClearSend.Text = "清空";
            this.buttonClearSend.UseVisualStyleBackColor = true;
            this.buttonClearSend.Click += new System.EventHandler(this.buttonClearSend_Click);
            // 
            // buttonSendByProtocol
            // 
            this.buttonSendByProtocol.Location = new System.Drawing.Point(399, 226);
            this.buttonSendByProtocol.Name = "buttonSendByProtocol";
            this.buttonSendByProtocol.Size = new System.Drawing.Size(75, 23);
            this.buttonSendByProtocol.TabIndex = 10;
            this.buttonSendByProtocol.Text = "按协议发送";
            this.buttonSendByProtocol.UseVisualStyleBackColor = true;
            this.buttonSendByProtocol.Click += new System.EventHandler(this.buttonSendByProtocol_Click);
            // 
            // buttonChoosePtcSend
            // 
            this.buttonChoosePtcSend.Location = new System.Drawing.Point(318, 226);
            this.buttonChoosePtcSend.Name = "buttonChoosePtcSend";
            this.buttonChoosePtcSend.Size = new System.Drawing.Size(75, 23);
            this.buttonChoosePtcSend.TabIndex = 12;
            this.buttonChoosePtcSend.Text = "协议库";
            this.buttonChoosePtcSend.UseVisualStyleBackColor = true;
            this.buttonChoosePtcSend.Click += new System.EventHandler(this.buttonChoosePtcSend_Click);
            // 
            // dgvSendData
            // 
            this.dgvSendData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSendData.Location = new System.Drawing.Point(6, 14);
            this.dgvSendData.Name = "dgvSendData";
            this.dgvSendData.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvSendData.RowTemplate.Height = 23;
            this.dgvSendData.Size = new System.Drawing.Size(567, 205);
            this.dgvSendData.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusDataSent,
            this.toolStripStatusDataRcv});
            this.statusStrip1.Location = new System.Drawing.Point(0, 378);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(842, 22);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusDataSent
            // 
            this.toolStripStatusDataSent.Name = "toolStripStatusDataSent";
            this.toolStripStatusDataSent.Size = new System.Drawing.Size(99, 17);
            this.toolStripStatusDataSent.Text = "已发送字节数：0";
            // 
            // toolStripStatusDataRcv
            // 
            this.toolStripStatusDataRcv.Name = "toolStripStatusDataRcv";
            this.toolStripStatusDataRcv.Size = new System.Drawing.Size(99, 17);
            this.toolStripStatusDataRcv.Text = "已接收字节数：0";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(604, 363);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.txGet);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(596, 337);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "接收数据";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 266);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "原始数据：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonClearReceive);
            this.groupBox2.Controls.Add(this.dgvReceiveData);
            this.groupBox2.Controls.Add(this.buttonChoosePtcReceive);
            this.groupBox2.Location = new System.Drawing.Point(3, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(585, 254);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "按协议解析";
            // 
            // buttonClearReceive
            // 
            this.buttonClearReceive.Location = new System.Drawing.Point(419, 225);
            this.buttonClearReceive.Name = "buttonClearReceive";
            this.buttonClearReceive.Size = new System.Drawing.Size(75, 23);
            this.buttonClearReceive.TabIndex = 13;
            this.buttonClearReceive.Text = "清空";
            this.buttonClearReceive.UseVisualStyleBackColor = true;
            this.buttonClearReceive.Click += new System.EventHandler(this.buttonClearReceive_Click);
            // 
            // dgvReceiveData
            // 
            this.dgvReceiveData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReceiveData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProtocolNameRcv,
            this.ProtocolDataTypeRcv,
            this.ProtocolDataLengthRcv,
            this.StartingPositionRcv,
            this.CountRcv,
            this.DataContentRcv,
            this.RawDataRcv});
            this.dgvReceiveData.Location = new System.Drawing.Point(6, 14);
            this.dgvReceiveData.Name = "dgvReceiveData";
            this.dgvReceiveData.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvReceiveData.RowTemplate.Height = 23;
            this.dgvReceiveData.Size = new System.Drawing.Size(569, 205);
            this.dgvReceiveData.TabIndex = 12;
            // 
            // buttonChoosePtcReceive
            // 
            this.buttonChoosePtcReceive.Location = new System.Drawing.Point(500, 225);
            this.buttonChoosePtcReceive.Name = "buttonChoosePtcReceive";
            this.buttonChoosePtcReceive.Size = new System.Drawing.Size(75, 23);
            this.buttonChoosePtcReceive.TabIndex = 11;
            this.buttonChoosePtcReceive.Text = "协议库";
            this.buttonChoosePtcReceive.UseVisualStyleBackColor = true;
            this.buttonChoosePtcReceive.Click += new System.EventHandler(this.buttonChoosePtcReceive_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.buttonTimingSendByRaw);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.buttonSendByRawData);
            this.tabPage2.Controls.Add(this.txSend);
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(596, 337);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "发送数据";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // buttonTimingSendByRaw
            // 
            this.buttonTimingSendByRaw.Location = new System.Drawing.Point(483, 305);
            this.buttonTimingSendByRaw.Name = "buttonTimingSendByRaw";
            this.buttonTimingSendByRaw.Size = new System.Drawing.Size(93, 23);
            this.buttonTimingSendByRaw.TabIndex = 15;
            this.buttonTimingSendByRaw.Text = "开启定时发送";
            this.buttonTimingSendByRaw.UseVisualStyleBackColor = true;
            this.buttonTimingSendByRaw.Click += new System.EventHandler(this.buttonTimingSendByRaw_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 266);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "原始数据：";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonByNet);
            this.groupBox3.Controls.Add(this.textBoxPortNum);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.textBoxIpAddress);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.radioButtonTCP);
            this.groupBox3.Controls.Add(this.radioButtonUdp);
            this.groupBox3.Location = new System.Drawing.Point(623, 175);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(211, 160);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "网络通信";
            // 
            // buttonByNet
            // 
            this.buttonByNet.Location = new System.Drawing.Point(83, 127);
            this.buttonByNet.Name = "buttonByNet";
            this.buttonByNet.Size = new System.Drawing.Size(109, 23);
            this.buttonByNet.TabIndex = 6;
            this.buttonByNet.Text = "开启网络收发";
            this.buttonByNet.UseVisualStyleBackColor = true;
            this.buttonByNet.Click += new System.EventHandler(this.buttonByNet_Click);
            // 
            // textBoxPortNum
            // 
            this.textBoxPortNum.Location = new System.Drawing.Point(59, 88);
            this.textBoxPortNum.Name = "textBoxPortNum";
            this.textBoxPortNum.Size = new System.Drawing.Size(62, 21);
            this.textBoxPortNum.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 91);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 4;
            this.label7.Text = "端口号";
            // 
            // textBoxIpAddress
            // 
            this.textBoxIpAddress.Location = new System.Drawing.Point(59, 51);
            this.textBoxIpAddress.Name = "textBoxIpAddress";
            this.textBoxIpAddress.Size = new System.Drawing.Size(133, 21);
            this.textBoxIpAddress.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "IP地址";
            // 
            // radioButtonTCP
            // 
            this.radioButtonTCP.AutoSize = true;
            this.radioButtonTCP.Location = new System.Drawing.Point(109, 20);
            this.radioButtonTCP.Name = "radioButtonTCP";
            this.radioButtonTCP.Size = new System.Drawing.Size(83, 16);
            this.radioButtonTCP.TabIndex = 1;
            this.radioButtonTCP.Text = "TCP/IP协议";
            this.radioButtonTCP.UseVisualStyleBackColor = true;
            // 
            // radioButtonUdp
            // 
            this.radioButtonUdp.AutoSize = true;
            this.radioButtonUdp.Checked = true;
            this.radioButtonUdp.Location = new System.Drawing.Point(12, 20);
            this.radioButtonUdp.Name = "radioButtonUdp";
            this.radioButtonUdp.Size = new System.Drawing.Size(65, 16);
            this.radioButtonUdp.TabIndex = 0;
            this.radioButtonUdp.TabStop = true;
            this.radioButtonUdp.Text = "UDP协议";
            this.radioButtonUdp.UseVisualStyleBackColor = true;
            // 
            // ProtocolNameRcv
            // 
            this.ProtocolNameRcv.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ProtocolNameRcv.HeaderText = "协议名";
            this.ProtocolNameRcv.Name = "ProtocolNameRcv";
            this.ProtocolNameRcv.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ProtocolNameRcv.Width = 47;
            // 
            // ProtocolDataTypeRcv
            // 
            this.ProtocolDataTypeRcv.FillWeight = 99.33774F;
            this.ProtocolDataTypeRcv.HeaderText = "数据类型";
            this.ProtocolDataTypeRcv.Items.AddRange(new object[] {
            "Boolean",
            "Short",
            "Ushort",
            "Int",
            "Uint",
            "Long",
            "Ulong",
            "Float",
            "Double",
            "Char",
            "String"});
            this.ProtocolDataTypeRcv.MaxDropDownItems = 12;
            this.ProtocolDataTypeRcv.Name = "ProtocolDataTypeRcv";
            this.ProtocolDataTypeRcv.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // ProtocolDataLengthRcv
            // 
            this.ProtocolDataLengthRcv.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ProtocolDataLengthRcv.HeaderText = "数据长度";
            this.ProtocolDataLengthRcv.Name = "ProtocolDataLengthRcv";
            this.ProtocolDataLengthRcv.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ProtocolDataLengthRcv.Width = 59;
            // 
            // StartingPositionRcv
            // 
            this.StartingPositionRcv.FillWeight = 93.8032F;
            this.StartingPositionRcv.HeaderText = "起始位置";
            this.StartingPositionRcv.Name = "StartingPositionRcv";
            this.StartingPositionRcv.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.StartingPositionRcv.Width = 94;
            // 
            // CountRcv
            // 
            this.CountRcv.FillWeight = 106.859F;
            this.CountRcv.HeaderText = "个数";
            this.CountRcv.Name = "CountRcv";
            this.CountRcv.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CountRcv.Width = 47;
            // 
            // DataContentRcv
            // 
            this.DataContentRcv.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DataContentRcv.HeaderText = "数据内容";
            this.DataContentRcv.Name = "DataContentRcv";
            this.DataContentRcv.ReadOnly = true;
            this.DataContentRcv.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DataContentRcv.Width = 59;
            // 
            // RawDataRcv
            // 
            this.RawDataRcv.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.RawDataRcv.HeaderText = "原始数据";
            this.RawDataRcv.Name = "RawDataRcv";
            this.RawDataRcv.ReadOnly = true;
            this.RawDataRcv.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.RawDataRcv.Width = 59;
            // 
            // CommunicationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(842, 400);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "CommunicationForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PortComForm_FormClosing);
            this.Load += new System.EventHandler(this.PortComForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSendData)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReceiveData)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboPortName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBaudrate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonBySerialPort;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.TextBox txGet;
        private System.Windows.Forms.Button buttonSendByRawData;
        private System.Windows.Forms.TextBox txSend;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonSendByProtocol;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusDataSent;
        private System.Windows.Forms.DataGridView dgvSendData;
        private System.Windows.Forms.Button buttonChoosePtcSend;
        private System.Windows.Forms.Button buttonChoosePtcReceive;
        private System.Windows.Forms.DataGridView dgvReceiveData;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonClearSend;
        private System.Windows.Forms.Button buttonClearReceive;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusDataRcv;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBoxPortNum;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxIpAddress;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton radioButtonTCP;
        private System.Windows.Forms.RadioButton radioButtonUdp;
        private System.Windows.Forms.Button buttonByNet;
        private System.Windows.Forms.Button buttonTimingSendByPtc;
        private System.Windows.Forms.Button buttonTimingSendByRaw;
        private System.Windows.Forms.Button buttonSavePtc;
        private System.Windows.Forms.Button buttonLoadPtc;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProtocolNameRcv;
        private System.Windows.Forms.DataGridViewComboBoxColumn ProtocolDataTypeRcv;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProtocolDataLengthRcv;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartingPositionRcv;
        private System.Windows.Forms.DataGridViewTextBoxColumn CountRcv;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataContentRcv;
        private System.Windows.Forms.DataGridViewTextBoxColumn RawDataRcv;
    }
}

