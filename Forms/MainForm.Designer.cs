
namespace p2_40_Charge_Tester
{
    partial class MainForm
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

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnManualOpen = new System.Windows.Forms.Button();
            this.btnCalibrationOpen = new System.Windows.Forms.Button();
            this.btnRecipeSettingsOpen = new System.Windows.Forms.Button();
            this.btnComSettingsOpen = new System.Windows.Forms.Button();
            this.lblValueModel = new System.Windows.Forms.Label();
            this.lblTextModel = new System.Windows.Forms.Label();
            this.lblValueLotNo = new System.Windows.Forms.Label();
            this.lblTextLotNo = new System.Windows.Forms.Label();
            this.lblValueMcuNo = new System.Windows.Forms.Label();
            this.lblTextMcuNo = new System.Windows.Forms.Label();
            this.PboxITM = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRecipeOpen = new System.Windows.Forms.Button();
            this.tboxRecipeFile = new System.Windows.Forms.RichTextBox();
            this.tboxRecipeFilePath = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.tabControl5 = new System.Windows.Forms.TabControl();
            this.tabPage13 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel22 = new System.Windows.Forms.TableLayoutPanel();
            this.tboxJigLog = new System.Windows.Forms.RichTextBox();
            this.lblClearJigLog = new System.Windows.Forms.LinkLabel();
            this.tabPage14 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel31 = new System.Windows.Forms.TableLayoutPanel();
            this.tboxJigComm = new System.Windows.Forms.RichTextBox();
            this.lblClearJigComm = new System.Windows.Forms.LinkLabel();
            this.statusStripMes = new System.Windows.Forms.StatusStrip();
            this.lblMesStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblFtpStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel_Channels = new System.Windows.Forms.TableLayoutPanel();
            this.chControl_ch3 = new p2_40_Charge_Tester.UserControls.ChControl();
            this.chControl_ch4 = new p2_40_Charge_Tester.UserControls.ChControl();
            this.chControl_ch1 = new p2_40_Charge_Tester.UserControls.ChControl();
            this.chControl_ch2 = new p2_40_Charge_Tester.UserControls.ChControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.btnStartStop = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panel3 = new System.Windows.Forms.Panel();
            this.productionControl1 = new p2_40_Charge_Tester.UserControls.ProductionControl();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PboxITM)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.tabControl5.SuspendLayout();
            this.tabPage13.SuspendLayout();
            this.tableLayoutPanel22.SuspendLayout();
            this.tabPage14.SuspendLayout();
            this.tableLayoutPanel31.SuspendLayout();
            this.statusStripMes.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel_Channels.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.tableLayoutPanel1.ColumnCount = 11;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 342F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.838754F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.37624F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.838754F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.37624F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.838754F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.37624F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.838754F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.838754F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.838754F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.838754F));
            this.tableLayoutPanel1.Controls.Add(this.btnManualOpen, 10, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnCalibrationOpen, 9, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnRecipeSettingsOpen, 8, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnComSettingsOpen, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblValueModel, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblTextModel, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblValueLotNo, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblTextLotNo, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblValueMcuNo, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblTextMcuNo, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.PboxITM, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1901, 85);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // btnManualOpen
            // 
            this.btnManualOpen.BackColor = System.Drawing.Color.White;
            this.btnManualOpen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnManualOpen.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnManualOpen.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnManualOpen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnManualOpen.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.btnManualOpen.Location = new System.Drawing.Point(1754, 6);
            this.btnManualOpen.Margin = new System.Windows.Forms.Padding(11, 6, 11, 6);
            this.btnManualOpen.Name = "btnManualOpen";
            this.btnManualOpen.Size = new System.Drawing.Size(136, 73);
            this.btnManualOpen.TabIndex = 11;
            this.btnManualOpen.Text = "Manual";
            this.btnManualOpen.UseVisualStyleBackColor = false;
            // 
            // btnCalibrationOpen
            // 
            this.btnCalibrationOpen.BackColor = System.Drawing.Color.White;
            this.btnCalibrationOpen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCalibrationOpen.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCalibrationOpen.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnCalibrationOpen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnCalibrationOpen.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.btnCalibrationOpen.Location = new System.Drawing.Point(1601, 6);
            this.btnCalibrationOpen.Margin = new System.Windows.Forms.Padding(11, 6, 11, 6);
            this.btnCalibrationOpen.Name = "btnCalibrationOpen";
            this.btnCalibrationOpen.Size = new System.Drawing.Size(131, 73);
            this.btnCalibrationOpen.TabIndex = 10;
            this.btnCalibrationOpen.Text = "Calibration";
            this.btnCalibrationOpen.UseVisualStyleBackColor = false;
            // 
            // btnRecipeSettingsOpen
            // 
            this.btnRecipeSettingsOpen.BackColor = System.Drawing.Color.White;
            this.btnRecipeSettingsOpen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRecipeSettingsOpen.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnRecipeSettingsOpen.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnRecipeSettingsOpen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnRecipeSettingsOpen.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.btnRecipeSettingsOpen.Location = new System.Drawing.Point(1448, 6);
            this.btnRecipeSettingsOpen.Margin = new System.Windows.Forms.Padding(11, 6, 11, 6);
            this.btnRecipeSettingsOpen.Name = "btnRecipeSettingsOpen";
            this.btnRecipeSettingsOpen.Size = new System.Drawing.Size(131, 73);
            this.btnRecipeSettingsOpen.TabIndex = 9;
            this.btnRecipeSettingsOpen.Text = "Recipe Settings";
            this.btnRecipeSettingsOpen.UseVisualStyleBackColor = false;
            // 
            // btnComSettingsOpen
            // 
            this.btnComSettingsOpen.BackColor = System.Drawing.Color.White;
            this.btnComSettingsOpen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnComSettingsOpen.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnComSettingsOpen.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnComSettingsOpen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnComSettingsOpen.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.btnComSettingsOpen.Location = new System.Drawing.Point(1295, 6);
            this.btnComSettingsOpen.Margin = new System.Windows.Forms.Padding(11, 6, 11, 6);
            this.btnComSettingsOpen.Name = "btnComSettingsOpen";
            this.btnComSettingsOpen.Size = new System.Drawing.Size(131, 73);
            this.btnComSettingsOpen.TabIndex = 8;
            this.btnComSettingsOpen.Text = "COM Settings";
            this.btnComSettingsOpen.UseVisualStyleBackColor = false;
            // 
            // lblValueModel
            // 
            this.lblValueModel.AutoSize = true;
            this.lblValueModel.BackColor = System.Drawing.Color.Honeydew;
            this.lblValueModel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblValueModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblValueModel.Font = new System.Drawing.Font("Calibri", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValueModel.Location = new System.Drawing.Point(1134, 12);
            this.lblValueModel.Margin = new System.Windows.Forms.Padding(11, 12, 11, 12);
            this.lblValueModel.Name = "lblValueModel";
            this.lblValueModel.Size = new System.Drawing.Size(139, 61);
            this.lblValueModel.TabIndex = 7;
            this.lblValueModel.Text = "-";
            this.lblValueModel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTextModel
            // 
            this.lblTextModel.AutoSize = true;
            this.lblTextModel.BackColor = System.Drawing.Color.MintCream;
            this.lblTextModel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTextModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTextModel.Font = new System.Drawing.Font("Calibri", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextModel.Location = new System.Drawing.Point(981, 12);
            this.lblTextModel.Margin = new System.Windows.Forms.Padding(11, 12, 11, 12);
            this.lblTextModel.Name = "lblTextModel";
            this.lblTextModel.Size = new System.Drawing.Size(131, 61);
            this.lblTextModel.TabIndex = 6;
            this.lblTextModel.Text = "Model";
            this.lblTextModel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblValueLotNo
            // 
            this.lblValueLotNo.AutoSize = true;
            this.lblValueLotNo.BackColor = System.Drawing.Color.Honeydew;
            this.lblValueLotNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblValueLotNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblValueLotNo.Font = new System.Drawing.Font("Calibri", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValueLotNo.Location = new System.Drawing.Point(820, 12);
            this.lblValueLotNo.Margin = new System.Windows.Forms.Padding(11, 12, 11, 12);
            this.lblValueLotNo.Name = "lblValueLotNo";
            this.lblValueLotNo.Size = new System.Drawing.Size(139, 61);
            this.lblValueLotNo.TabIndex = 5;
            this.lblValueLotNo.Text = "-";
            this.lblValueLotNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTextLotNo
            // 
            this.lblTextLotNo.AutoSize = true;
            this.lblTextLotNo.BackColor = System.Drawing.Color.MintCream;
            this.lblTextLotNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTextLotNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTextLotNo.Font = new System.Drawing.Font("Calibri", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextLotNo.Location = new System.Drawing.Point(667, 12);
            this.lblTextLotNo.Margin = new System.Windows.Forms.Padding(11, 12, 11, 12);
            this.lblTextLotNo.Name = "lblTextLotNo";
            this.lblTextLotNo.Size = new System.Drawing.Size(131, 61);
            this.lblTextLotNo.TabIndex = 4;
            this.lblTextLotNo.Text = "Lot No.";
            this.lblTextLotNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblValueMcuNo
            // 
            this.lblValueMcuNo.AutoSize = true;
            this.lblValueMcuNo.BackColor = System.Drawing.Color.Honeydew;
            this.lblValueMcuNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblValueMcuNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblValueMcuNo.Font = new System.Drawing.Font("Calibri", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValueMcuNo.Location = new System.Drawing.Point(506, 12);
            this.lblValueMcuNo.Margin = new System.Windows.Forms.Padding(11, 12, 11, 12);
            this.lblValueMcuNo.Name = "lblValueMcuNo";
            this.lblValueMcuNo.Size = new System.Drawing.Size(139, 61);
            this.lblValueMcuNo.TabIndex = 3;
            this.lblValueMcuNo.Text = "-";
            this.lblValueMcuNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTextMcuNo
            // 
            this.lblTextMcuNo.AutoSize = true;
            this.lblTextMcuNo.BackColor = System.Drawing.Color.MintCream;
            this.lblTextMcuNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTextMcuNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTextMcuNo.Font = new System.Drawing.Font("Calibri", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextMcuNo.Location = new System.Drawing.Point(353, 12);
            this.lblTextMcuNo.Margin = new System.Windows.Forms.Padding(11, 12, 11, 12);
            this.lblTextMcuNo.Name = "lblTextMcuNo";
            this.lblTextMcuNo.Size = new System.Drawing.Size(131, 61);
            this.lblTextMcuNo.TabIndex = 2;
            this.lblTextMcuNo.Text = "Mcu No.";
            this.lblTextMcuNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PboxITM
            // 
            this.PboxITM.BackColor = System.Drawing.Color.White;
            this.PboxITM.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PboxITM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PboxITM.Image = global::p2_40_Charge_Tester.Properties.Resources.ITMImage;
            this.PboxITM.Location = new System.Drawing.Point(6, 6);
            this.PboxITM.Margin = new System.Windows.Forms.Padding(6);
            this.PboxITM.Name = "PboxITM";
            this.PboxITM.Size = new System.Drawing.Size(330, 73);
            this.PboxITM.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PboxITM.TabIndex = 1;
            this.PboxITM.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.tableLayoutPanel3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(23, 89);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(23, 4, 23, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(1855, 61);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Recipe";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 5;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 171F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 114F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel3.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnRecipeOpen, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.tboxRecipeFile, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.tboxRecipeFilePath, 3, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 22);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 12);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1849, 35);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Honeydew;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(799, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 35);
            this.label2.TabIndex = 6;
            this.label2.Text = "Path";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Honeydew;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 35);
            this.label1.TabIndex = 4;
            this.label1.Text = "Recipe File";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnRecipeOpen
            // 
            this.btnRecipeOpen.BackColor = System.Drawing.Color.White;
            this.btnRecipeOpen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRecipeOpen.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnRecipeOpen.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnRecipeOpen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnRecipeOpen.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRecipeOpen.Location = new System.Drawing.Point(1663, 0);
            this.btnRecipeOpen.Margin = new System.Windows.Forms.Padding(3, 0, 3, 4);
            this.btnRecipeOpen.Name = "btnRecipeOpen";
            this.btnRecipeOpen.Size = new System.Drawing.Size(183, 31);
            this.btnRecipeOpen.TabIndex = 8;
            this.btnRecipeOpen.Text = "Select Recipe";
            this.btnRecipeOpen.UseVisualStyleBackColor = false;
            // 
            // tboxRecipeFile
            // 
            this.tboxRecipeFile.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tboxRecipeFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tboxRecipeFile.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tboxRecipeFile.Location = new System.Drawing.Point(171, 0);
            this.tboxRecipeFile.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.tboxRecipeFile.Multiline = false;
            this.tboxRecipeFile.Name = "tboxRecipeFile";
            this.tboxRecipeFile.Size = new System.Drawing.Size(620, 35);
            this.tboxRecipeFile.TabIndex = 9;
            this.tboxRecipeFile.Text = "";
            // 
            // tboxRecipeFilePath
            // 
            this.tboxRecipeFilePath.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tboxRecipeFilePath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tboxRecipeFilePath.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tboxRecipeFilePath.Location = new System.Drawing.Point(910, 0);
            this.tboxRecipeFilePath.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.tboxRecipeFilePath.Multiline = false;
            this.tboxRecipeFilePath.Name = "tboxRecipeFilePath";
            this.tboxRecipeFilePath.Size = new System.Drawing.Size(745, 35);
            this.tboxRecipeFilePath.TabIndex = 10;
            this.tboxRecipeFilePath.Text = "";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.groupBox6, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.statusStripMes, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.groupBox1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1901, 1130);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.tabControl5);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Location = new System.Drawing.Point(3, 954);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox6.Size = new System.Drawing.Size(1895, 146);
            this.groupBox6.TabIndex = 6;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "JIG Communication";
            // 
            // tabControl5
            // 
            this.tabControl5.Controls.Add(this.tabPage13);
            this.tabControl5.Controls.Add(this.tabPage14);
            this.tabControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl5.Location = new System.Drawing.Point(3, 20);
            this.tabControl5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl5.Name = "tabControl5";
            this.tabControl5.SelectedIndex = 0;
            this.tabControl5.Size = new System.Drawing.Size(1889, 124);
            this.tabControl5.TabIndex = 0;
            // 
            // tabPage13
            // 
            this.tabPage13.Controls.Add(this.tableLayoutPanel22);
            this.tabPage13.Location = new System.Drawing.Point(4, 25);
            this.tabPage13.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage13.Name = "tabPage13";
            this.tabPage13.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage13.Size = new System.Drawing.Size(1881, 95);
            this.tabPage13.TabIndex = 0;
            this.tabPage13.Text = "Log";
            this.tabPage13.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel22
            // 
            this.tableLayoutPanel22.ColumnCount = 2;
            this.tableLayoutPanel22.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel22.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel22.Controls.Add(this.tboxJigLog, 0, 1);
            this.tableLayoutPanel22.Controls.Add(this.lblClearJigLog, 1, 0);
            this.tableLayoutPanel22.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel22.Location = new System.Drawing.Point(3, 2);
            this.tableLayoutPanel22.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel22.Name = "tableLayoutPanel22";
            this.tableLayoutPanel22.RowCount = 2;
            this.tableLayoutPanel22.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel22.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel22.Size = new System.Drawing.Size(1875, 91);
            this.tableLayoutPanel22.TabIndex = 1;
            // 
            // tboxJigLog
            // 
            this.tableLayoutPanel22.SetColumnSpan(this.tboxJigLog, 2);
            this.tboxJigLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tboxJigLog.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tboxJigLog.Location = new System.Drawing.Point(3, 22);
            this.tboxJigLog.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tboxJigLog.Name = "tboxJigLog";
            this.tboxJigLog.Size = new System.Drawing.Size(1869, 67);
            this.tboxJigLog.TabIndex = 2;
            this.tboxJigLog.Text = "";
            // 
            // lblClearJigLog
            // 
            this.lblClearJigLog.AutoSize = true;
            this.lblClearJigLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblClearJigLog.Location = new System.Drawing.Point(1798, 0);
            this.lblClearJigLog.Name = "lblClearJigLog";
            this.lblClearJigLog.Size = new System.Drawing.Size(74, 20);
            this.lblClearJigLog.TabIndex = 0;
            this.lblClearJigLog.TabStop = true;
            this.lblClearJigLog.Text = "Clear";
            this.lblClearJigLog.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabPage14
            // 
            this.tabPage14.Controls.Add(this.tableLayoutPanel31);
            this.tabPage14.Location = new System.Drawing.Point(4, 25);
            this.tabPage14.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage14.Name = "tabPage14";
            this.tabPage14.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage14.Size = new System.Drawing.Size(1881, 95);
            this.tabPage14.TabIndex = 1;
            this.tabPage14.Text = "Comm";
            this.tabPage14.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel31
            // 
            this.tableLayoutPanel31.ColumnCount = 2;
            this.tableLayoutPanel31.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel31.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel31.Controls.Add(this.tboxJigComm, 0, 1);
            this.tableLayoutPanel31.Controls.Add(this.lblClearJigComm, 1, 0);
            this.tableLayoutPanel31.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel31.Location = new System.Drawing.Point(3, 2);
            this.tableLayoutPanel31.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel31.Name = "tableLayoutPanel31";
            this.tableLayoutPanel31.RowCount = 2;
            this.tableLayoutPanel31.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel31.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel31.Size = new System.Drawing.Size(1875, 91);
            this.tableLayoutPanel31.TabIndex = 1;
            // 
            // tboxJigComm
            // 
            this.tableLayoutPanel31.SetColumnSpan(this.tboxJigComm, 2);
            this.tboxJigComm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tboxJigComm.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tboxJigComm.Location = new System.Drawing.Point(3, 22);
            this.tboxJigComm.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tboxJigComm.Name = "tboxJigComm";
            this.tboxJigComm.Size = new System.Drawing.Size(1869, 67);
            this.tboxJigComm.TabIndex = 2;
            this.tboxJigComm.Text = "";
            // 
            // lblClearJigComm
            // 
            this.lblClearJigComm.AutoSize = true;
            this.lblClearJigComm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblClearJigComm.Location = new System.Drawing.Point(1798, 0);
            this.lblClearJigComm.Name = "lblClearJigComm";
            this.lblClearJigComm.Size = new System.Drawing.Size(74, 20);
            this.lblClearJigComm.TabIndex = 0;
            this.lblClearJigComm.TabStop = true;
            this.lblClearJigComm.Text = "Clear";
            this.lblClearJigComm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // statusStripMes
            // 
            this.statusStripMes.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStripMes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblMesStatus,
            this.lblFtpStatus});
            this.statusStripMes.Location = new System.Drawing.Point(0, 1104);
            this.statusStripMes.Name = "statusStripMes";
            this.statusStripMes.Size = new System.Drawing.Size(1901, 26);
            this.statusStripMes.TabIndex = 4;
            this.statusStripMes.Text = "statusStrip1";
            // 
            // lblMesStatus
            // 
            this.lblMesStatus.Name = "lblMesStatus";
            this.lblMesStatus.Size = new System.Drawing.Size(85, 20);
            this.lblMesStatus.Text = "MES Status";
            // 
            // lblFtpStatus
            // 
            this.lblFtpStatus.Name = "lblFtpStatus";
            this.lblFtpStatus.Size = new System.Drawing.Size(79, 20);
            this.lblFtpStatus.Text = "FTP Status";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 83.85057F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.14943F));
            this.tableLayoutPanel4.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.panel3, 1, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 163);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1895, 786);
            this.tableLayoutPanel4.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.tableLayoutPanel_Channels);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.tableLayoutPanel4.SetRowSpan(this.panel1, 2);
            this.panel1.Size = new System.Drawing.Size(1582, 780);
            this.panel1.TabIndex = 0;
            // 
            // tableLayoutPanel_Channels
            // 
            this.tableLayoutPanel_Channels.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel_Channels.ColumnCount = 4;
            this.tableLayoutPanel_Channels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel_Channels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel_Channels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel_Channels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel_Channels.Controls.Add(this.chControl_ch3, 0, 0);
            this.tableLayoutPanel_Channels.Controls.Add(this.chControl_ch4, 0, 0);
            this.tableLayoutPanel_Channels.Controls.Add(this.chControl_ch1, 0, 0);
            this.tableLayoutPanel_Channels.Controls.Add(this.chControl_ch2, 0, 0);
            this.tableLayoutPanel_Channels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_Channels.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel_Channels.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel_Channels.Name = "tableLayoutPanel_Channels";
            this.tableLayoutPanel_Channels.RowCount = 1;
            this.tableLayoutPanel_Channels.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_Channels.Size = new System.Drawing.Size(1580, 778);
            this.tableLayoutPanel_Channels.TabIndex = 0;
            // 
            // chControl_ch3
            // 
            this.chControl_ch3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chControl_ch3.Location = new System.Drawing.Point(793, 5);
            this.chControl_ch3.Name = "chControl_ch3";
            this.chControl_ch3.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.chControl_ch3.Size = new System.Drawing.Size(386, 768);
            this.chControl_ch3.TabIndex = 3;
            // 
            // chControl_ch4
            // 
            this.chControl_ch4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chControl_ch4.Location = new System.Drawing.Point(1187, 5);
            this.chControl_ch4.Name = "chControl_ch4";
            this.chControl_ch4.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.chControl_ch4.Size = new System.Drawing.Size(388, 768);
            this.chControl_ch4.TabIndex = 2;
            // 
            // chControl_ch1
            // 
            this.chControl_ch1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chControl_ch1.Location = new System.Drawing.Point(5, 5);
            this.chControl_ch1.Name = "chControl_ch1";
            this.chControl_ch1.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.chControl_ch1.Size = new System.Drawing.Size(386, 768);
            this.chControl_ch1.TabIndex = 1;
            // 
            // chControl_ch2
            // 
            this.chControl_ch2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chControl_ch2.Location = new System.Drawing.Point(399, 5);
            this.chControl_ch2.Name = "chControl_ch2";
            this.chControl_ch2.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.chControl_ch2.Size = new System.Drawing.Size(386, 768);
            this.chControl_ch2.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.tableLayoutPanel5);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(1591, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(301, 190);
            this.panel2.TabIndex = 1;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.btnStartStop, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.progressBar1, 0, 1);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(299, 188);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // btnStartStop
            // 
            this.btnStartStop.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnStartStop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStartStop.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartStop.Location = new System.Drawing.Point(20, 20);
            this.btnStartStop.Margin = new System.Windows.Forms.Padding(20);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(259, 98);
            this.btnStartStop.TabIndex = 0;
            this.btnStartStop.Text = "START";
            this.btnStartStop.UseVisualStyleBackColor = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar1.Location = new System.Drawing.Point(20, 153);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(20, 15, 20, 15);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(259, 20);
            this.progressBar1.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.productionControl1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(1591, 199);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(301, 584);
            this.panel3.TabIndex = 2;
            // 
            // productionControl1
            // 
            this.productionControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.productionControl1.Location = new System.Drawing.Point(0, 0);
            this.productionControl1.Name = "productionControl1";
            this.productionControl1.Size = new System.Drawing.Size(299, 582);
            this.productionControl1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1901, 1130);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Charge Tester (p2 4.0) (ver 20260204 : Demo Ver)";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PboxITM)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.tabControl5.ResumeLayout(false);
            this.tabPage13.ResumeLayout(false);
            this.tableLayoutPanel22.ResumeLayout(false);
            this.tableLayoutPanel22.PerformLayout();
            this.tabPage14.ResumeLayout(false);
            this.tableLayoutPanel31.ResumeLayout(false);
            this.tableLayoutPanel31.PerformLayout();
            this.statusStripMes.ResumeLayout(false);
            this.statusStripMes.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel_Channels.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnManualOpen;
        private System.Windows.Forms.Button btnCalibrationOpen;
        private System.Windows.Forms.Button btnRecipeSettingsOpen;
        private System.Windows.Forms.Button btnComSettingsOpen;
        public System.Windows.Forms.Label lblValueModel;
        private System.Windows.Forms.Label lblTextModel;
        public System.Windows.Forms.Label lblValueLotNo;
        private System.Windows.Forms.Label lblTextLotNo;
        public System.Windows.Forms.Label lblValueMcuNo;
        private System.Windows.Forms.Label lblTextMcuNo;
        private System.Windows.Forms.PictureBox PboxITM;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRecipeOpen;
        private System.Windows.Forms.RichTextBox tboxRecipeFile;
        private System.Windows.Forms.RichTextBox tboxRecipeFilePath;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.StatusStrip statusStripMes;
        private System.Windows.Forms.ToolStripStatusLabel lblMesStatus;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TabControl tabControl5;
        private System.Windows.Forms.TabPage tabPage13;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel22;
        private System.Windows.Forms.RichTextBox tboxJigLog;
        private System.Windows.Forms.LinkLabel lblClearJigLog;
        private System.Windows.Forms.TabPage tabPage14;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel31;
        private System.Windows.Forms.RichTextBox tboxJigComm;
        private System.Windows.Forms.LinkLabel lblClearJigComm;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Channels;
        private UserControls.ProductionControl productionControl1;
        private UserControls.ChControl chControl_ch2;
        private UserControls.ChControl chControl_ch3;
        private UserControls.ChControl chControl_ch4;
        private UserControls.ChControl chControl_ch1;
        private System.Windows.Forms.ToolStripStatusLabel lblFtpStatus;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

