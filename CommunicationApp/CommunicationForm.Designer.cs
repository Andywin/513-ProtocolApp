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
            this.buttonOpenClose = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txGet = new System.Windows.Forms.TextBox();
            this.buttonSendByRawData = new System.Windows.Forms.Button();
            this.txSend = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.buttonClearSend = new System.Windows.Forms.Button();
            this.buttonSendByProtocol = new System.Windows.Forms.Button();
            this.buttonChoosePtcSend = new System.Windows.Forms.Button();
            this.dgvSendData = new System.Windows.Forms.DataGridView();
            this.ProtocolName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProtocolDataType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ProtocolDataLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartingPosition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProtocolCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataContent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusDataSent = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusDataRcv = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonClearReceive = new System.Windows.Forms.Button();
            this.dgvReceiveData = new System.Windows.Forms.DataGridView();
            this.ProtocolNameRcv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProtocolDataTypeRcv = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ProtocolDataLengthRcv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartingPositionRcv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CountRcv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataContentRcv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RawDataRcv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonChoosePtcReceive = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSendData)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReceiveData)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboPortName
            // 
            this.comboPortName.FormattingEnabled = true;
            this.comboPortName.Location = new System.Drawing.Point(58, 21);
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
            this.label2.Location = new System.Drawing.Point(206, 25);
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
            this.comboBaudrate.Location = new System.Drawing.Point(265, 21);
            this.comboBaudrate.Name = "comboBaudrate";
            this.comboBaudrate.Size = new System.Drawing.Size(121, 20);
            this.comboBaudrate.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboPortName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comboBaudrate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(404, 53);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "串口设置";
            // 
            // buttonOpenClose
            // 
            this.buttonOpenClose.Location = new System.Drawing.Point(430, 21);
            this.buttonOpenClose.Name = "buttonOpenClose";
            this.buttonOpenClose.Size = new System.Drawing.Size(75, 23);
            this.buttonOpenClose.TabIndex = 5;
            this.buttonOpenClose.Text = "打开串口";
            this.buttonOpenClose.UseVisualStyleBackColor = true;
            this.buttonOpenClose.Click += new System.EventHandler(this.buttonOpenClose_Click);
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(519, 21);
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
            this.txGet.Location = new System.Drawing.Point(3, 280);
            this.txGet.Multiline = true;
            this.txGet.Name = "txGet";
            this.txGet.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txGet.Size = new System.Drawing.Size(579, 48);
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
            // buttonClearSend
            // 
            this.buttonClearSend.Location = new System.Drawing.Point(318, 225);
            this.buttonClearSend.Name = "buttonClearSend";
            this.buttonClearSend.Size = new System.Drawing.Size(75, 23);
            this.buttonClearSend.TabIndex = 13;
            this.buttonClearSend.Text = "清空";
            this.buttonClearSend.UseVisualStyleBackColor = true;
            this.buttonClearSend.Click += new System.EventHandler(this.buttonClearSend_Click);
            // 
            // buttonSendByProtocol
            // 
            this.buttonSendByProtocol.Location = new System.Drawing.Point(480, 225);
            this.buttonSendByProtocol.Name = "buttonSendByProtocol";
            this.buttonSendByProtocol.Size = new System.Drawing.Size(93, 23);
            this.buttonSendByProtocol.TabIndex = 10;
            this.buttonSendByProtocol.Text = "按协议发送";
            this.buttonSendByProtocol.UseVisualStyleBackColor = true;
            this.buttonSendByProtocol.Click += new System.EventHandler(this.buttonSendByProtocol_Click);
            // 
            // buttonChoosePtcSend
            // 
            this.buttonChoosePtcSend.Location = new System.Drawing.Point(399, 225);
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
            this.dgvSendData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProtocolName,
            this.ProtocolDataType,
            this.ProtocolDataLength,
            this.StartingPosition,
            this.ProtocolCount,
            this.DataContent});
            this.dgvSendData.Location = new System.Drawing.Point(6, 14);
            this.dgvSendData.Name = "dgvSendData";
            this.dgvSendData.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvSendData.RowTemplate.Height = 23;
            this.dgvSendData.Size = new System.Drawing.Size(567, 205);
            this.dgvSendData.TabIndex = 1;
            // 
            // ProtocolName
            // 
            this.ProtocolName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ProtocolName.HeaderText = "协议名";
            this.ProtocolName.Name = "ProtocolName";
            this.ProtocolName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ProtocolName.Width = 47;
            // 
            // ProtocolDataType
            // 
            this.ProtocolDataType.HeaderText = "数据类型";
            this.ProtocolDataType.Items.AddRange(new object[] {
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
            this.ProtocolDataType.MaxDropDownItems = 12;
            this.ProtocolDataType.Name = "ProtocolDataType";
            this.ProtocolDataType.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // ProtocolDataLength
            // 
            this.ProtocolDataLength.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ProtocolDataLength.HeaderText = "数据长度";
            this.ProtocolDataLength.Name = "ProtocolDataLength";
            this.ProtocolDataLength.ReadOnly = true;
            this.ProtocolDataLength.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ProtocolDataLength.Width = 59;
            // 
            // StartingPosition
            // 
            this.StartingPosition.HeaderText = "起始位置";
            this.StartingPosition.Name = "StartingPosition";
            this.StartingPosition.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.StartingPosition.Width = 80;
            // 
            // ProtocolCount
            // 
            this.ProtocolCount.HeaderText = "个数";
            this.ProtocolCount.Name = "ProtocolCount";
            this.ProtocolCount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ProtocolCount.Width = 60;
            // 
            // DataContent
            // 
            this.DataContent.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DataContent.HeaderText = "数据内容";
            this.DataContent.Name = "DataContent";
            this.DataContent.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DataContent.Width = 59;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusDataSent,
            this.toolStripStatusDataRcv});
            this.statusStrip1.Location = new System.Drawing.Point(0, 432);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(617, 22);
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
            this.tabControl1.Location = new System.Drawing.Point(12, 61);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(593, 360);
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
            this.tabPage1.Size = new System.Drawing.Size(585, 334);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "接收数据";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 265);
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
            this.groupBox2.Size = new System.Drawing.Size(579, 253);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "按协议解析";
            // 
            // buttonClearReceive
            // 
            this.buttonClearReceive.Location = new System.Drawing.Point(417, 224);
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
            this.dgvReceiveData.Size = new System.Drawing.Size(567, 204);
            this.dgvReceiveData.TabIndex = 12;
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
            this.StartingPositionRcv.HeaderText = "起始位置";
            this.StartingPositionRcv.Name = "StartingPositionRcv";
            this.StartingPositionRcv.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.StartingPositionRcv.Width = 80;
            // 
            // CountRcv
            // 
            this.CountRcv.HeaderText = "个数";
            this.CountRcv.Name = "CountRcv";
            this.CountRcv.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CountRcv.Width = 60;
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
            // buttonChoosePtcReceive
            // 
            this.buttonChoosePtcReceive.Location = new System.Drawing.Point(498, 224);
            this.buttonChoosePtcReceive.Name = "buttonChoosePtcReceive";
            this.buttonChoosePtcReceive.Size = new System.Drawing.Size(75, 23);
            this.buttonChoosePtcReceive.TabIndex = 11;
            this.buttonChoosePtcReceive.Text = "协议库";
            this.buttonChoosePtcReceive.UseVisualStyleBackColor = true;
            this.buttonChoosePtcReceive.Click += new System.EventHandler(this.buttonChoosePtcReceive_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.buttonSendByRawData);
            this.tabPage2.Controls.Add(this.txSend);
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(585, 334);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "发送数据";
            this.tabPage2.UseVisualStyleBackColor = true;
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
            // CommunicationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(617, 454);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.buttonOpenClose);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(633, 493);
            this.MinimumSize = new System.Drawing.Size(633, 493);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboPortName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBaudrate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonOpenClose;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn ProtocolName;
        private System.Windows.Forms.DataGridViewComboBoxColumn ProtocolDataType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProtocolDataLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartingPosition;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProtocolCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataContent;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProtocolNameRcv;
        private System.Windows.Forms.DataGridViewComboBoxColumn ProtocolDataTypeRcv;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProtocolDataLengthRcv;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartingPositionRcv;
        private System.Windows.Forms.DataGridViewTextBoxColumn CountRcv;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataContentRcv;
        private System.Windows.Forms.DataGridViewTextBoxColumn RawDataRcv;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusDataRcv;
    }
}

