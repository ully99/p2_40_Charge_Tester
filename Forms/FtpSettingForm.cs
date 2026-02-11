using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using p2_40_Charge_Tester.Data;
using p2_40_Charge_Tester.Communication;

namespace p2_40_Charge_Tester.Forms
{
    public partial class FtpSettingForm : Form
    {
        ComSettingForm comsettingform;

        #region Init
        public FtpSettingForm(ComSettingForm parentform)
        {
            this.comsettingform = parentform;
            InitializeComponent();
        }

        private void FtpSettingForm_Load(object sender, EventArgs e)
        {
            LoadFtpSetting();
        }

        private void LoadFtpSetting()
        {
            try
            {
                cboxFtpUse.Checked = Settings.Instance.USE_FTP;
                tboxFtpHost.Text = Settings.Instance.FTP_HOST;
                numFtpPort.Text = Settings.Instance.FTP_PORT;
                tboxFtpUser.Text = Settings.Instance.FTP_USER;
                tboxFtpPw.Text = Settings.Instance.FTP_PW;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveFtpSetting()
        {
            try
            {
                Settings.Instance.USE_FTP = cboxFtpUse.Checked;
                Settings.Instance.FTP_HOST = tboxFtpHost.Text;
                Settings.Instance.FTP_PORT = numFtpPort.Text;
                Settings.Instance.FTP_USER = tboxFtpUser.Text;
                Settings.Instance.FTP_PW = tboxFtpPw.Text;
                Settings.Instance.FTP_USE_SSL = false; //겉으로 드러내지말고 false 상태로 사용


                Settings.Instance.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Event
        private void btnLoadFtpSetting_Click(object sender, EventArgs e)
        {
            LoadFtpSetting();
        }

        private void btnSaveFtpSetting_Click(object sender, EventArgs e)
        {
            SaveFtpSetting();
            this.Close();
        }

        #endregion
    }
}
