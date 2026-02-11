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
    public partial class McuLotModelSettingForm : Form
    {
        MainForm mainform;
        public McuLotModelSettingForm(MainForm parentform)
        {
            this.mainform = parentform;
            InitializeComponent();
        }

        private void McuLotModelSettingForm_Load(object sender, EventArgs e)
        {
            var set = Settings.Instance;

            // 1. 텍스트 박스 채우기 (tboxMcuNo, tboxLotNo)
            tboxMcuNo.Text = set.Mcu_No;
            tboxLotNo.Text = set.Lot_No;

            // 2. 모델 콤보박스 목록 채우기 (cboxModel)
            cboxModel.Items.Clear();
            if (set.ModelList != null)
            {
                foreach (string model in set.ModelList)
                {
                    cboxModel.Items.Add(model);
                }
            }

            // 3. 현재 선택된 모델 표시
            if (!string.IsNullOrEmpty(set.Model_No) && cboxModel.Items.Contains(set.Model_No))
            {
                cboxModel.SelectedItem = set.Model_No;
            }
            else if (cboxModel.Items.Count > 0)
            {
                cboxModel.SelectedIndex = 0;
            }
        }

        private void btnAddModel_Click(object sender, EventArgs e)
        {
            // 1. tboxAddModel에서 텍스트 가져오기
            string newModel = tboxAddModel.Text.Trim();

            // 2. 빈 값 검사
            if (string.IsNullOrEmpty(newModel))
            {
                MessageBox.Show("추가할 모델명을 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tboxAddModel.Focus(); // 입력창으로 포커스 이동
                return;
            }

            // 3. 중복 검사 (이미 리스트에 있는지)
            if (cboxModel.Items.Contains(newModel))
            {
                MessageBox.Show("이미 존재하는 모델입니다.", "중복 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tboxAddModel.SelectAll(); // 수정하기 편하게 전체 선택
                tboxAddModel.Focus();
                return;
            }

            // 4. 리스트에 추가하고 선택 상태로 변경
            cboxModel.Items.Add(newModel);
            cboxModel.SelectedItem = newModel;

            // 5. 입력창 비우기 (다음 입력을 위해)
            tboxAddModel.Text = "";
        }

        private void btnDeleteModel_Click(object sender, EventArgs e)
        {
            if (cboxModel.SelectedItem == null)
            {
                MessageBox.Show("삭제할 모델을 선택해주세요.");
                return;
            }

            string selectedModel = cboxModel.SelectedItem.ToString();

            if (MessageBox.Show($"'{selectedModel}' 모델을 삭제하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                cboxModel.Items.Remove(selectedModel);

                // 삭제 후 텍스트 초기화
                cboxModel.Text = "";
                if (cboxModel.Items.Count > 0)
                    cboxModel.SelectedIndex = 0;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var set = Settings.Instance;

            // 1. 화면의 값을 변수에 저장
            set.Mcu_No = tboxMcuNo.Text;
            set.Lot_No = tboxLotNo.Text;

            // 2. 선택된 모델 저장 (선택 안하고 타이핑만 된 상태도 고려)
            if (cboxModel.SelectedItem != null)
            {
                set.Model_No = cboxModel.SelectedItem.ToString();
            }
            else
            {
                set.Model_No = cboxModel.Text;
            }

            // 3. 모델 리스트 갱신 (화면에 있는 리스트 그대로 저장)
            set.ModelList = new List<string>();
            foreach (var item in cboxModel.Items)
            {
                set.ModelList.Add(item.ToString());
            }

            // 4. 파일로 영구 저장 (config.json)
            set.Save();

            // 5. 메인 화면 갱신
            mainform.lblValueMcuNo.Text = set.Mcu_No;
            mainform.lblValueLotNo.Text = set.Lot_No;
            mainform.lblValueModel.Text = set.Model_No;

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
