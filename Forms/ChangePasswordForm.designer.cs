
namespace p2_40_Charge_Tester.Forms
{
    partial class ChangePasswordForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangePasswordForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCancel = new MaterialSkin.Controls.MaterialButton();
            this.tboxNewPwCheck = new MaterialSkin.Controls.MaterialTextBox2();
            this.materialLabel5 = new MaterialSkin.Controls.MaterialLabel();
            this.tboxNewPw = new MaterialSkin.Controls.MaterialTextBox2();
            this.materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.tboxCurrentPw = new MaterialSkin.Controls.MaterialTextBox2();
            this.btnSubmit = new MaterialSkin.Controls.MaterialButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(347, 502);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.btnCancel, 1, 7);
            this.tableLayoutPanel2.Controls.Add(this.tboxNewPwCheck, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.materialLabel5, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.tboxNewPw, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.materialLabel3, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.materialLabel1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.tboxCurrentPw, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.btnSubmit, 0, 7);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(13, 13);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 8;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(321, 476);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = false;
            this.btnCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCancel.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnCancel.Depth = 0;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.HighEmphasis = true;
            this.btnCancel.Icon = null;
            this.btnCancel.Location = new System.Drawing.Point(180, 420);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(20);
            this.btnCancel.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnCancel.Size = new System.Drawing.Size(121, 36);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnCancel.UseAccentColor = false;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tboxNewPwCheck
            // 
            this.tboxNewPwCheck.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tboxNewPwCheck.AnimateReadOnly = false;
            this.tboxNewPwCheck.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tboxNewPwCheck.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.tableLayoutPanel2.SetColumnSpan(this.tboxNewPwCheck, 2);
            this.tboxNewPwCheck.Depth = 0;
            this.tboxNewPwCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tboxNewPwCheck.HideSelection = true;
            this.tboxNewPwCheck.Hint = "Confirm Password";
            this.tboxNewPwCheck.LeadingIcon = null;
            this.tboxNewPwCheck.Location = new System.Drawing.Point(3, 341);
            this.tboxNewPwCheck.MaxLength = 32767;
            this.tboxNewPwCheck.MouseState = MaterialSkin.MouseState.OUT;
            this.tboxNewPwCheck.Name = "tboxNewPwCheck";
            this.tboxNewPwCheck.PasswordChar = '●';
            this.tboxNewPwCheck.PrefixSuffixText = null;
            this.tboxNewPwCheck.ReadOnly = false;
            this.tboxNewPwCheck.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tboxNewPwCheck.SelectedText = "";
            this.tboxNewPwCheck.SelectionLength = 0;
            this.tboxNewPwCheck.SelectionStart = 0;
            this.tboxNewPwCheck.ShortcutsEnabled = true;
            this.tboxNewPwCheck.Size = new System.Drawing.Size(315, 48);
            this.tboxNewPwCheck.TabIndex = 8;
            this.tboxNewPwCheck.TabStop = false;
            this.tboxNewPwCheck.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.tboxNewPwCheck.TrailingIcon = null;
            this.tboxNewPwCheck.UseSystemPasswordChar = true;
            this.tboxNewPwCheck.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tboxNewPwCheck_KeyDown);
            // 
            // materialLabel5
            // 
            this.materialLabel5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.materialLabel5.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.materialLabel5, 2);
            this.materialLabel5.Depth = 0;
            this.materialLabel5.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel5.Location = new System.Drawing.Point(94, 305);
            this.materialLabel5.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel5.Name = "materialLabel5";
            this.materialLabel5.Size = new System.Drawing.Size(132, 19);
            this.materialLabel5.TabIndex = 7;
            this.materialLabel5.Text = "Confirm Password";
            this.materialLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tboxNewPw
            // 
            this.tboxNewPw.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tboxNewPw.AnimateReadOnly = false;
            this.tboxNewPw.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tboxNewPw.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.tableLayoutPanel2.SetColumnSpan(this.tboxNewPw, 2);
            this.tboxNewPw.Depth = 0;
            this.tboxNewPw.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tboxNewPw.HideSelection = true;
            this.tboxNewPw.Hint = "New Password";
            this.tboxNewPw.LeadingIcon = null;
            this.tboxNewPw.Location = new System.Drawing.Point(3, 241);
            this.tboxNewPw.MaxLength = 32767;
            this.tboxNewPw.MouseState = MaterialSkin.MouseState.OUT;
            this.tboxNewPw.Name = "tboxNewPw";
            this.tboxNewPw.PasswordChar = '●';
            this.tboxNewPw.PrefixSuffixText = null;
            this.tboxNewPw.ReadOnly = false;
            this.tboxNewPw.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tboxNewPw.SelectedText = "";
            this.tboxNewPw.SelectionLength = 0;
            this.tboxNewPw.SelectionStart = 0;
            this.tboxNewPw.ShortcutsEnabled = true;
            this.tboxNewPw.Size = new System.Drawing.Size(315, 48);
            this.tboxNewPw.TabIndex = 5;
            this.tboxNewPw.TabStop = false;
            this.tboxNewPw.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.tboxNewPw.TrailingIcon = null;
            this.tboxNewPw.UseSystemPasswordChar = true;
            this.tboxNewPw.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tboxNewPw_KeyDown);
            // 
            // materialLabel3
            // 
            this.materialLabel3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.materialLabel3.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.materialLabel3, 2);
            this.materialLabel3.Depth = 0;
            this.materialLabel3.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel3.Location = new System.Drawing.Point(107, 205);
            this.materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            this.materialLabel3.Size = new System.Drawing.Size(106, 19);
            this.materialLabel3.TabIndex = 4;
            this.materialLabel3.Text = "New Password";
            this.materialLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.label1, 2);
            this.label1.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(312, 49);
            this.label1.TabIndex = 0;
            this.label1.Text = "Change Password";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // materialLabel1
            // 
            this.materialLabel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.materialLabel1.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.materialLabel1, 2);
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel1.Location = new System.Drawing.Point(97, 105);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(126, 19);
            this.materialLabel1.TabIndex = 1;
            this.materialLabel1.Text = "Current Password";
            this.materialLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tboxCurrentPw
            // 
            this.tboxCurrentPw.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tboxCurrentPw.AnimateReadOnly = false;
            this.tboxCurrentPw.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tboxCurrentPw.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.tableLayoutPanel2.SetColumnSpan(this.tboxCurrentPw, 2);
            this.tboxCurrentPw.Depth = 0;
            this.tboxCurrentPw.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tboxCurrentPw.HideSelection = true;
            this.tboxCurrentPw.Hint = "Current Password";
            this.tboxCurrentPw.LeadingIcon = null;
            this.tboxCurrentPw.Location = new System.Drawing.Point(3, 141);
            this.tboxCurrentPw.MaxLength = 32767;
            this.tboxCurrentPw.MouseState = MaterialSkin.MouseState.OUT;
            this.tboxCurrentPw.Name = "tboxCurrentPw";
            this.tboxCurrentPw.PasswordChar = '●';
            this.tboxCurrentPw.PrefixSuffixText = null;
            this.tboxCurrentPw.ReadOnly = false;
            this.tboxCurrentPw.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tboxCurrentPw.SelectedText = "";
            this.tboxCurrentPw.SelectionLength = 0;
            this.tboxCurrentPw.SelectionStart = 0;
            this.tboxCurrentPw.ShortcutsEnabled = true;
            this.tboxCurrentPw.Size = new System.Drawing.Size(315, 48);
            this.tboxCurrentPw.TabIndex = 2;
            this.tboxCurrentPw.TabStop = false;
            this.tboxCurrentPw.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.tboxCurrentPw.TrailingIcon = null;
            this.tboxCurrentPw.UseSystemPasswordChar = true;
            this.tboxCurrentPw.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tboxCurrentPw_KeyDown);
            // 
            // btnSubmit
            // 
            this.btnSubmit.AutoSize = false;
            this.btnSubmit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSubmit.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnSubmit.Depth = 0;
            this.btnSubmit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSubmit.HighEmphasis = true;
            this.btnSubmit.Icon = null;
            this.btnSubmit.Location = new System.Drawing.Point(20, 420);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(20);
            this.btnSubmit.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnSubmit.Size = new System.Drawing.Size(120, 36);
            this.btnSubmit.TabIndex = 9;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnSubmit.UseAccentColor = false;
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // ChangePasswordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 502);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChangePasswordForm";
            this.Text = "ChangePasswordForm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialButton btnCancel;
        private MaterialSkin.Controls.MaterialTextBox2 tboxNewPwCheck;
        private MaterialSkin.Controls.MaterialLabel materialLabel5;
        private MaterialSkin.Controls.MaterialTextBox2 tboxNewPw;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
        private MaterialSkin.Controls.MaterialTextBox2 tboxCurrentPw;
        private MaterialSkin.Controls.MaterialButton btnSubmit;
    }
}