using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using p2_40_Charge_Tester.Forms;
using p2_40_Charge_Tester.Data;

namespace p2_40_Charge_Tester.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        private void LoginForm_Load(object sender, EventArgs e)
        {
            //this.ActiveControl = tboxPassword;
        }
        private void LoginForm_Shown(object sender, EventArgs e)
        {
            //tboxPassword.Focus();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
         

            if (tboxPassword.Text == Settings.Instance.Password)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Wrong Password!");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void tboxPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
                e.SuppressKeyPress = true; // 기본 Enter 동작 방지
            }
        }

       
    }
}
