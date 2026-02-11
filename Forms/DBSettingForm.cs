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
    public partial class DBSettingForm : Form
    {
        ComSettingForm comsettingform;

        public DBSettingForm(ComSettingForm parentform)
        {
            this.comsettingform = parentform;
            InitializeComponent();
        }

        private void DBSettingForm_Load(object sender, EventArgs e)
        {
            LoadDbSettings();
        }

        private void LoadDbSettings()
        {
            try
            {
                checkboxMesUse.Checked = Settings.Instance.USE_MES;
                tboxIp.Text = Settings.Instance.DB_IP;
                numPort.Text = Settings.Instance.DB_PORT;
                tboxDatabase.Text = Settings.Instance.DB_NAME;
                tboxTable.Text = Settings.Instance.DB_TABLE;
                tboxId.Text = Settings.Instance.DB_USER;
                tboxPw.Text = Settings.Instance.DB_PW;

                checkboxInterlockUse.Checked = Settings.Instance.USE_INTERLOCK;
                tboxProcedure.Text = Settings.Instance.INTERLOCK_PROCEDURE_1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveDbSettings()
        {
            try
            {
                Settings.Instance.USE_MES = checkboxMesUse.Checked;
                Settings.Instance.DB_IP = tboxIp.Text;
                Settings.Instance.DB_PORT = numPort.Text;
                Settings.Instance.DB_NAME = tboxTable.Text;
                Settings.Instance.DB_TABLE = tboxDatabase.Text;
                Settings.Instance.DB_USER = tboxId.Text;
                Settings.Instance.DB_PW = tboxPw.Text;

                Settings.Instance.USE_INTERLOCK = checkboxInterlockUse.Checked;
                Settings.Instance.INTERLOCK_PROCEDURE_1 = tboxProcedure.Text;

                Settings.Instance.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SaveDbSettings();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
