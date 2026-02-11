
namespace p2_40_Charge_Tester.UserControls
{
    partial class ChControl
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblTaskTime = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvTaskList = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel15 = new System.Windows.Forms.TableLayoutPanel();
            this.tboxLog = new System.Windows.Forms.RichTextBox();
            this.lblClearLog = new System.Windows.Forms.LinkLabel();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel16 = new System.Windows.Forms.TableLayoutPanel();
            this.tboxComm = new System.Windows.Forms.RichTextBox();
            this.lblClearComm = new System.Windows.Forms.LinkLabel();
            this.lblTaskStatus = new System.Windows.Forms.Label();
            this.lblUseStatus = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblNgCount = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lblOkCount = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lblTotalCount = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaskList)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel15.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tableLayoutPanel16.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lblTaskTime, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblTaskStatus, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblUseStatus, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(30, 20);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(293, 713);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblTaskTime
            // 
            this.lblTaskTime.AutoSize = true;
            this.lblTaskTime.BackColor = System.Drawing.Color.White;
            this.lblTaskTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTaskTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTaskTime.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaskTime.Location = new System.Drawing.Point(3, 150);
            this.lblTaskTime.Name = "lblTaskTime";
            this.lblTaskTime.Size = new System.Drawing.Size(287, 25);
            this.lblTaskTime.TabIndex = 7;
            this.lblTaskTime.Text = "-";
            this.lblTaskTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 185);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(293, 418);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvTaskList);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Size = new System.Drawing.Size(285, 389);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "TaskList";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvTaskList
            // 
            this.dgvTaskList.AllowUserToAddRows = false;
            this.dgvTaskList.AllowUserToDeleteRows = false;
            this.dgvTaskList.AllowUserToResizeColumns = false;
            this.dgvTaskList.AllowUserToResizeRows = false;
            this.dgvTaskList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTaskList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvTaskList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTaskList.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvTaskList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTaskList.EnableHeadersVisualStyles = false;
            this.dgvTaskList.Location = new System.Drawing.Point(3, 2);
            this.dgvTaskList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvTaskList.Name = "dgvTaskList";
            this.dgvTaskList.RowHeadersVisible = false;
            this.dgvTaskList.RowHeadersWidth = 51;
            this.dgvTaskList.RowTemplate.Height = 27;
            this.dgvTaskList.Size = new System.Drawing.Size(279, 385);
            this.dgvTaskList.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel15);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Size = new System.Drawing.Size(285, 389);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Log";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel15
            // 
            this.tableLayoutPanel15.ColumnCount = 2;
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel15.Controls.Add(this.tboxLog, 0, 1);
            this.tableLayoutPanel15.Controls.Add(this.lblClearLog, 1, 0);
            this.tableLayoutPanel15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel15.Location = new System.Drawing.Point(3, 2);
            this.tableLayoutPanel15.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel15.Name = "tableLayoutPanel15";
            this.tableLayoutPanel15.RowCount = 2;
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel15.Size = new System.Drawing.Size(279, 385);
            this.tableLayoutPanel15.TabIndex = 0;
            // 
            // tboxLog
            // 
            this.tableLayoutPanel15.SetColumnSpan(this.tboxLog, 2);
            this.tboxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tboxLog.Location = new System.Drawing.Point(3, 22);
            this.tboxLog.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tboxLog.Name = "tboxLog";
            this.tboxLog.Size = new System.Drawing.Size(273, 361);
            this.tboxLog.TabIndex = 2;
            this.tboxLog.Text = "";
            // 
            // lblClearLog
            // 
            this.lblClearLog.AutoSize = true;
            this.lblClearLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblClearLog.Location = new System.Drawing.Point(199, 0);
            this.lblClearLog.Margin = new System.Windows.Forms.Padding(0);
            this.lblClearLog.Name = "lblClearLog";
            this.lblClearLog.Size = new System.Drawing.Size(80, 20);
            this.lblClearLog.TabIndex = 1;
            this.lblClearLog.TabStop = true;
            this.lblClearLog.Text = "Clear";
            this.lblClearLog.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblClearLog.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblClearLog_LinkClicked);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tableLayoutPanel16);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage3.Size = new System.Drawing.Size(285, 389);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Comm";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel16
            // 
            this.tableLayoutPanel16.ColumnCount = 2;
            this.tableLayoutPanel16.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel16.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel16.Controls.Add(this.tboxComm, 0, 1);
            this.tableLayoutPanel16.Controls.Add(this.lblClearComm, 1, 0);
            this.tableLayoutPanel16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel16.Location = new System.Drawing.Point(3, 2);
            this.tableLayoutPanel16.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel16.Name = "tableLayoutPanel16";
            this.tableLayoutPanel16.RowCount = 2;
            this.tableLayoutPanel16.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel16.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel16.Size = new System.Drawing.Size(279, 385);
            this.tableLayoutPanel16.TabIndex = 0;
            // 
            // tboxComm
            // 
            this.tableLayoutPanel16.SetColumnSpan(this.tboxComm, 2);
            this.tboxComm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tboxComm.Location = new System.Drawing.Point(3, 22);
            this.tboxComm.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tboxComm.Name = "tboxComm";
            this.tboxComm.Size = new System.Drawing.Size(273, 361);
            this.tboxComm.TabIndex = 3;
            this.tboxComm.Text = "";
            // 
            // lblClearComm
            // 
            this.lblClearComm.AutoSize = true;
            this.lblClearComm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblClearComm.Location = new System.Drawing.Point(199, 0);
            this.lblClearComm.Margin = new System.Windows.Forms.Padding(0);
            this.lblClearComm.Name = "lblClearComm";
            this.lblClearComm.Size = new System.Drawing.Size(80, 20);
            this.lblClearComm.TabIndex = 2;
            this.lblClearComm.TabStop = true;
            this.lblClearComm.Text = "Clear";
            this.lblClearComm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblClearComm.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblClearComm_LinkClicked);
            // 
            // lblTaskStatus
            // 
            this.lblTaskStatus.AutoSize = true;
            this.lblTaskStatus.BackColor = System.Drawing.Color.White;
            this.lblTaskStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTaskStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTaskStatus.Font = new System.Drawing.Font("Calibri", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaskStatus.Location = new System.Drawing.Point(3, 40);
            this.lblTaskStatus.Margin = new System.Windows.Forms.Padding(3, 0, 3, 10);
            this.lblTaskStatus.Name = "lblTaskStatus";
            this.lblTaskStatus.Size = new System.Drawing.Size(287, 100);
            this.lblTaskStatus.TabIndex = 3;
            this.lblTaskStatus.Text = "-";
            this.lblTaskStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUseStatus
            // 
            this.lblUseStatus.AutoSize = true;
            this.lblUseStatus.BackColor = System.Drawing.Color.White;
            this.lblUseStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblUseStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUseStatus.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUseStatus.Location = new System.Drawing.Point(3, 0);
            this.lblUseStatus.Name = "lblUseStatus";
            this.lblUseStatus.Size = new System.Drawing.Size(287, 40);
            this.lblUseStatus.TabIndex = 2;
            this.lblUseStatus.Text = "-";
            this.lblUseStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 616);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(287, 94);
            this.panel1.TabIndex = 8;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.52769F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.94461F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.5277F));
            this.tableLayoutPanel2.Controls.Add(this.lblNgCount, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.label17, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblOkCount, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label16, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblTotalCount, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label15, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(5, 5);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.10427F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 54.89573F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(275, 82);
            this.tableLayoutPanel2.TabIndex = 9;
            // 
            // lblNgCount
            // 
            this.lblNgCount.AutoSize = true;
            this.lblNgCount.BackColor = System.Drawing.Color.MintCream;
            this.lblNgCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNgCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNgCount.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNgCount.ForeColor = System.Drawing.Color.Red;
            this.lblNgCount.Location = new System.Drawing.Point(192, 41);
            this.lblNgCount.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.lblNgCount.Name = "lblNgCount";
            this.lblNgCount.Size = new System.Drawing.Size(73, 36);
            this.lblNgCount.TabIndex = 12;
            this.lblNgCount.Text = "-";
            this.lblNgCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label17.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Red;
            this.label17.Location = new System.Drawing.Point(182, 0);
            this.label17.Margin = new System.Windows.Forms.Padding(0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(93, 36);
            this.label17.TabIndex = 11;
            this.label17.Text = "NG";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOkCount
            // 
            this.lblOkCount.AutoSize = true;
            this.lblOkCount.BackColor = System.Drawing.Color.MintCream;
            this.lblOkCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblOkCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOkCount.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOkCount.ForeColor = System.Drawing.Color.Green;
            this.lblOkCount.Location = new System.Drawing.Point(102, 41);
            this.lblOkCount.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.lblOkCount.Name = "lblOkCount";
            this.lblOkCount.Size = new System.Drawing.Size(70, 36);
            this.lblOkCount.TabIndex = 10;
            this.lblOkCount.Text = "-";
            this.lblOkCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label16.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.Green;
            this.label16.Location = new System.Drawing.Point(92, 0);
            this.label16.Margin = new System.Windows.Forms.Padding(0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(90, 36);
            this.label16.TabIndex = 9;
            this.label16.Text = "OK";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.AutoSize = true;
            this.lblTotalCount.BackColor = System.Drawing.Color.MintCream;
            this.lblTotalCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotalCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalCount.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalCount.Location = new System.Drawing.Point(10, 41);
            this.lblTotalCount.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(72, 36);
            this.lblTotalCount.TabIndex = 8;
            this.lblTotalCount.Text = "-";
            this.lblTotalCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(0, 0);
            this.label15.Margin = new System.Windows.Forms.Padding(0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(92, 36);
            this.label15.TabIndex = 4;
            this.label15.Text = "TOTAL";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ChControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ChControl";
            this.Padding = new System.Windows.Forms.Padding(30, 20, 30, 20);
            this.Size = new System.Drawing.Size(353, 753);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaskList)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel15.ResumeLayout(false);
            this.tableLayoutPanel15.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tableLayoutPanel16.ResumeLayout(false);
            this.tableLayoutPanel16.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dgvTaskList;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel15;
        private System.Windows.Forms.RichTextBox tboxLog;
        private System.Windows.Forms.LinkLabel lblClearLog;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel16;
        private System.Windows.Forms.RichTextBox tboxComm;
        private System.Windows.Forms.LinkLabel lblClearComm;
        private System.Windows.Forms.Label lblTaskStatus;
        private System.Windows.Forms.Label lblUseStatus;
        private System.Windows.Forms.Label lblTaskTime;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        public System.Windows.Forms.Label lblNgCount;
        public System.Windows.Forms.Label lblOkCount;
        public System.Windows.Forms.Label lblTotalCount;
    }
}
