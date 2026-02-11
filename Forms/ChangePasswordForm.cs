using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using p2_40_Charge_Tester.Data;


namespace p2_40_Charge_Tester.Forms
{
    public partial class ChangePasswordForm : Form
    {
        RecipeSettingForm recipeSettingForm;
        public ChangePasswordForm(RecipeSettingForm parentform)
        {
            this.recipeSettingForm = parentform;
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string Current_pw = tboxCurrentPw.Text;
            string New_pw = tboxNewPw.Text;
            string New_pw_check = tboxNewPwCheck.Text;

            if (string.IsNullOrWhiteSpace(Current_pw) || string.IsNullOrWhiteSpace(New_pw) || string.IsNullOrWhiteSpace(New_pw_check))
            {
                MessageBox.Show("Fill all password box!");
                tboxCurrentPw.Clear();
                tboxCurrentPw.Focus();
                return;
            }

            if (Current_pw != Settings.Instance.Password)
            {
                MessageBox.Show("Current password is not correct!");
                return;
            }

            if (New_pw != New_pw_check)
            {
                MessageBox.Show("Mismatched new password!");
                tboxNewPwCheck.Clear();
                tboxNewPwCheck.Focus();
                return;
            }

            Settings.Instance.Password = New_pw;
            Settings.Instance.Save();
            MessageBox.Show("Password change sucess!");
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tboxCurrentPw_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSubmit.PerformClick();
                e.SuppressKeyPress = true; // 기본 Enter 동작 방지
            }
        }

        private void tboxNewPw_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSubmit.PerformClick();
                e.SuppressKeyPress = true; // 기본 Enter 동작 방지
            }
        }

        private void tboxNewPwCheck_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSubmit.PerformClick();
                e.SuppressKeyPress = true; // 기본 Enter 동작 방지
            }
        }
    }
}
