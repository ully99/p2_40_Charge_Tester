
namespace p2_40_Charge_Tester.Forms
{
    partial class FtpSettingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FtpSettingForm));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cboxFtpUse = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tboxFtpHost = new System.Windows.Forms.TextBox();
            this.tboxFtpUser = new System.Windows.Forms.TextBox();
            this.tboxFtpPw = new System.Windows.Forms.TextBox();
            this.numFtpPort = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnLoadFtpSetting = new System.Windows.Forms.Button();
            this.btnSaveFtpSetting = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFtpPort)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(396, 300);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "FTP Setting";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 28);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(390, 269);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.label8, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.tboxFtpHost, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.tboxFtpUser, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.tboxFtpPw, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.numFtpPort, 1, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(390, 209);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DimGray;
            this.tableLayoutPanel2.SetColumnSpan(this.panel1, 2);
            this.panel1.Controls.Add(this.cboxFtpUse);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.ForeColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(388, 40);
            this.panel1.TabIndex = 0;
            // 
            // cboxFtpUse
            // 
            this.cboxFtpUse.AutoSize = true;
            this.cboxFtpUse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboxFtpUse.Location = new System.Drawing.Point(0, 0);
            this.cboxFtpUse.Name = "cboxFtpUse";
            this.cboxFtpUse.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.cboxFtpUse.Size = new System.Drawing.Size(388, 40);
            this.cboxFtpUse.TabIndex = 4;
            this.cboxFtpUse.Text = "FTP USE";
            this.cboxFtpUse.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Gainsboro;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.Location = new System.Drawing.Point(1, 42);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(154, 40);
            this.label5.TabIndex = 1;
            this.label5.Text = "Host";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Gainsboro;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.Location = new System.Drawing.Point(1, 83);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(154, 40);
            this.label6.TabIndex = 2;
            this.label6.Text = "Port";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Gainsboro;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label7.Location = new System.Drawing.Point(1, 124);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(154, 40);
            this.label7.TabIndex = 3;
            this.label7.Text = "User";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Gainsboro;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label8.Location = new System.Drawing.Point(1, 165);
            this.label8.Margin = new System.Windows.Forms.Padding(0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(154, 43);
            this.label8.TabIndex = 4;
            this.label8.Text = "Password";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tboxFtpHost
            // 
            this.tboxFtpHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tboxFtpHost.Location = new System.Drawing.Point(159, 45);
            this.tboxFtpHost.Name = "tboxFtpHost";
            this.tboxFtpHost.Size = new System.Drawing.Size(227, 32);
            this.tboxFtpHost.TabIndex = 5;
            // 
            // tboxFtpUser
            // 
            this.tboxFtpUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tboxFtpUser.Location = new System.Drawing.Point(159, 127);
            this.tboxFtpUser.Name = "tboxFtpUser";
            this.tboxFtpUser.Size = new System.Drawing.Size(227, 32);
            this.tboxFtpUser.TabIndex = 7;
            // 
            // tboxFtpPw
            // 
            this.tboxFtpPw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tboxFtpPw.Location = new System.Drawing.Point(159, 168);
            this.tboxFtpPw.Name = "tboxFtpPw";
            this.tboxFtpPw.PasswordChar = '*';
            this.tboxFtpPw.Size = new System.Drawing.Size(227, 32);
            this.tboxFtpPw.TabIndex = 8;
            // 
            // numFtpPort
            // 
            this.numFtpPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numFtpPort.Location = new System.Drawing.Point(159, 86);
            this.numFtpPort.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numFtpPort.Name = "numFtpPort";
            this.numFtpPort.Size = new System.Drawing.Size(227, 32);
            this.numFtpPort.TabIndex = 9;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 4;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.Controls.Add(this.btnLoadFtpSetting, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnSaveFtpSetting, 3, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 212);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(384, 54);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // btnLoadFtpSetting
            // 
            this.btnLoadFtpSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLoadFtpSetting.Location = new System.Drawing.Point(195, 3);
            this.btnLoadFtpSetting.Name = "btnLoadFtpSetting";
            this.btnLoadFtpSetting.Size = new System.Drawing.Size(90, 48);
            this.btnLoadFtpSetting.TabIndex = 0;
            this.btnLoadFtpSetting.Text = "Load";
            this.btnLoadFtpSetting.UseVisualStyleBackColor = true;
            this.btnLoadFtpSetting.Click += new System.EventHandler(this.btnLoadFtpSetting_Click);
            // 
            // btnSaveFtpSetting
            // 
            this.btnSaveFtpSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSaveFtpSetting.Location = new System.Drawing.Point(291, 3);
            this.btnSaveFtpSetting.Name = "btnSaveFtpSetting";
            this.btnSaveFtpSetting.Size = new System.Drawing.Size(90, 48);
            this.btnSaveFtpSetting.TabIndex = 1;
            this.btnSaveFtpSetting.Text = "Save";
            this.btnSaveFtpSetting.UseVisualStyleBackColor = true;
            this.btnSaveFtpSetting.Click += new System.EventHandler(this.btnSaveFtpSetting_Click);
            // 
            // FtpSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 300);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FtpSettingForm";
            this.Text = "FTP Setting";
            this.Load += new System.EventHandler(this.FtpSettingForm_Load);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFtpPort)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox cboxFtpUse;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tboxFtpHost;
        private System.Windows.Forms.TextBox tboxFtpPw;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button btnLoadFtpSetting;
        private System.Windows.Forms.Button btnSaveFtpSetting;
        private System.Windows.Forms.TextBox tboxFtpUser;
        private System.Windows.Forms.NumericUpDown numFtpPort;
    }
}