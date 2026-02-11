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

namespace p2_40_Charge_Tester.UserControls
{
    public partial class ProductionControl : UserControl
    {
        public ProductionControl()
        {
            InitializeComponent();
            BindCounts();
        }

        private void BindCounts()
        {
            lblTotalCount.DataBindings.Clear();
            lblOkCount.DataBindings.Clear();
            lblNgCount.DataBindings.Clear();
            lblOkRate.DataBindings.Clear();
            lblNgRate.DataBindings.Clear();

            var set = Settings.Instance;

            lblTotalCount.DataBindings.Add(
                "Text", set, nameof(set.TotalCount), true, DataSourceUpdateMode.OnPropertyChanged);

            lblOkCount.DataBindings.Add(
                "Text", set, nameof(set.OkCount), true, DataSourceUpdateMode.OnPropertyChanged);

            lblNgCount.DataBindings.Add(
                "Text", set, nameof(set.NgCount), true, DataSourceUpdateMode.OnPropertyChanged);

            var okRateBinding = lblOkRate.DataBindings.Add("Text", set, nameof(set.OkRate), true, DataSourceUpdateMode.OnPropertyChanged);
            okRateBinding.Format += (s, e) =>
            {
                // OkRate가 0~1이므로 %로 표시
                var v = e.Value is double d ? d : 0.0;
                e.Value = $"{v * 100:0.00}%";
            };

            var ngRateBinding = lblNgRate.DataBindings.Add("Text", set, nameof(set.NgRate), true, DataSourceUpdateMode.OnPropertyChanged);
            ngRateBinding.Format += (s, e) =>
            {
                var v = e.Value is double d ? d : 0.0;
                e.Value = $"{v * 100:0.00}%";
            };

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                 "Production Count를 초기화하시겠습니까?",
                 "Count Reset",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Question,
                 MessageBoxDefaultButton.Button2 // 기본값 = 아니오
             );


            if (result != DialogResult.Yes) return;

            // 속성을 하나씩 0으로 바꾸는 대신, 내부 메서드 하나만 호출합니다.
            Settings.Instance.ResetAllCounts();
        }
    }
}
