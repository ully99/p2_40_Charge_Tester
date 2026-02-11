using System;
using System.Drawing;
using System.Windows.Forms;

namespace p2_40_Charge_Tester.Forms
{
    /// <summary>
    /// 장비 연결 중 등 대기 시 사용자에게 표시하는 로딩 폼
    /// </summary>
    public partial class LoadingForm : Form
    {
        public LoadingForm(string message = "Connecting...")
        {
            InitializeComponent();
            lblMessage.Text = message;
        }

        public void SetMessage(string message)
        {
            if (lblMessage != null)
                lblMessage.Text = message;
        }
    }
}
