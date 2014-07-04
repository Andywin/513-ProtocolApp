namespace CommunicationApp
{
    partial class ProtocolConfigForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvProtocolLib = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelProtocolChosen = new System.Windows.Forms.ToolStripStatusLabel();
            this.buttonAddNewProtocol = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonSaveProtocol = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonDeleteProtocol = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProtocolLib)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvProtocolLib
            // 
            this.dgvProtocolLib.AllowUserToAddRows = false;
            this.dgvProtocolLib.AllowUserToResizeColumns = false;
            this.dgvProtocolLib.AllowUserToResizeRows = false;
            this.dgvProtocolLib.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvProtocolLib.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvProtocolLib.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProtocolLib.Location = new System.Drawing.Point(12, 12);
            this.dgvProtocolLib.Name = "dgvProtocolLib";
            this.dgvProtocolLib.RowTemplate.Height = 23;
            this.dgvProtocolLib.Size = new System.Drawing.Size(421, 308);
            this.dgvProtocolLib.TabIndex = 0;
            this.dgvProtocolLib.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProtocolLib_CellContentClick);
            this.dgvProtocolLib.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProtocolLib_CellValueChanged);
            this.dgvProtocolLib.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.dgvProtocolLib_CellValueNeeded);
            this.dgvProtocolLib.CellValuePushed += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.dgvProtocolLib_CellValuePushed);
            this.dgvProtocolLib.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvProtocolLib_EditingControlShowing);
            this.dgvProtocolLib.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvProtocolLib_RowPostPaint);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelProtocolChosen});
            this.statusStrip1.Location = new System.Drawing.Point(0, 323);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(526, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelProtocolChosen
            // 
            this.toolStripStatusLabelProtocolChosen.Name = "toolStripStatusLabelProtocolChosen";
            this.toolStripStatusLabelProtocolChosen.Size = new System.Drawing.Size(123, 17);
            this.toolStripStatusLabelProtocolChosen.Text = "选中的数据协议数：0";
            // 
            // buttonAddNewProtocol
            // 
            this.buttonAddNewProtocol.Location = new System.Drawing.Point(439, 227);
            this.buttonAddNewProtocol.Name = "buttonAddNewProtocol";
            this.buttonAddNewProtocol.Size = new System.Drawing.Size(75, 23);
            this.buttonAddNewProtocol.TabIndex = 2;
            this.buttonAddNewProtocol.Text = "添加新协议";
            this.buttonAddNewProtocol.UseVisualStyleBackColor = true;
            this.buttonAddNewProtocol.Click += new System.EventHandler(this.buttonAddNewProtocol_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(439, 28);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 3;
            this.buttonOK.Text = "确认";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonSaveProtocol
            // 
            this.buttonSaveProtocol.Location = new System.Drawing.Point(439, 256);
            this.buttonSaveProtocol.Name = "buttonSaveProtocol";
            this.buttonSaveProtocol.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveProtocol.TabIndex = 4;
            this.buttonSaveProtocol.Text = "保存协议库";
            this.buttonSaveProtocol.UseVisualStyleBackColor = true;
            this.buttonSaveProtocol.Click += new System.EventHandler(this.buttonSaveProtocol_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(439, 57);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonDeleteProtocol
            // 
            this.buttonDeleteProtocol.Location = new System.Drawing.Point(439, 285);
            this.buttonDeleteProtocol.Name = "buttonDeleteProtocol";
            this.buttonDeleteProtocol.Size = new System.Drawing.Size(75, 23);
            this.buttonDeleteProtocol.TabIndex = 6;
            this.buttonDeleteProtocol.Text = "删除协议";
            this.buttonDeleteProtocol.UseVisualStyleBackColor = true;
            this.buttonDeleteProtocol.Click += new System.EventHandler(this.buttonDeleteProtocol_Click);
            // 
            // ProtocolConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 345);
            this.Controls.Add(this.buttonDeleteProtocol);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSaveProtocol);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonAddNewProtocol);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.dgvProtocolLib);
            this.Name = "ProtocolConfigForm";
            this.Text = "协议库";
            this.Load += new System.EventHandler(this.ProtocolConfig_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProtocolLib)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvProtocolLib;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelProtocolChosen;
        private System.Windows.Forms.Button buttonAddNewProtocol;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonSaveProtocol;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonDeleteProtocol;
        private System.Windows.Forms.ColorDialog colorDialog1;


    }
}