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
using p2_40_Charge_Tester.LIB;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;


namespace p2_40_Charge_Tester.Forms
{
    public partial class RecipeSettingForm : Form
    {
        #region Field
        public MainForm mainform;
        private RecipeLocalBuffer _local = new RecipeLocalBuffer();
        private string currentTask = "";

        #endregion


        #region Init
        public RecipeSettingForm(MainForm parentform)
        {
            this.mainform = parentform;
            InitializeComponent();
        }

        private void RecipeSettingForm_Load(object sender, EventArgs e)
        {
            dgViewTaskList.DataSource = CreateTaskTable();
        }

        

        private DataTable CreateTaskTable()
        {
            DataTable table = new DataTable();

            DataColumn colNum = table.Columns.Add("Num", typeof(int));
            DataColumn colItem = table.Columns.Add("Item", typeof(string));
            DataColumn colEnable = table.Columns.Add("Enable", typeof(bool));

            colNum.ReadOnly = true;
            colItem.ReadOnly = true;

            table.Rows.Add(1, "INTERLOCK", _local.INTERLOCK_Enable);
            table.Rows.Add(2, "QR READ", _local.QR_READ_Enable);
            table.Rows.Add(3, "MCU INFO", _local.MCU_INFO_Enable);
            table.Rows.Add(4, "SDP", _local.SDP_Enable);
            table.Rows.Add(5, "DCP", _local.DCP_Enable);
            table.Rows.Add(6, "HVDCP", _local.HVDCP_Enable); 
            table.Rows.Add(7, "PPS", _local.PPS_Enable);
            table.Rows.Add(8, "CHARGE COUNT RESET", _local.CHARGE_COUNT_RESET_Enable);
            table.Rows.Add(9, "MES", _local.MES_Enable); 

            return table;
        }

        

        #endregion

        #region Event
        private void dgViewTaskList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string selectedTask = dgViewTaskList.Rows[e.RowIndex].Cells["Item"].Value.ToString();
                currentTask = selectedTask;
                dgViewSetValue.DataSource = LoadTaskSettingsToRightTable(selectedTask);
            }
        }
        private DataTable LoadTaskSettingsToRightTable(string task)
        {
            DataTable table = new DataTable();
            DataColumn colParam = table.Columns.Add("Parameter", typeof(string));
            DataColumn colValue = table.Columns.Add("Value", typeof(object));

            colParam.ReadOnly = true;

            switch (task)
            {
                case "INTERLOCK":
                    break;

                case "QR READ":
                    table.Rows.Add("[Delay] Step", _local.QR_READ_Step_Delay);
                    table.Rows.Add("[판정] 조건 자릿수", _local.QR_READ_Len);
                    break;

                case "MCU INFO":
                    table.Rows.Add("[Delay] Step", _local.MCU_INFO_Step_Delay);
                    table.Rows.Add("[Delay] PBA Delay", _local.MCU_INFO_Pba_Delay);
                    table.Rows.Add("[Delay] TCP 01", _local.MCU_INFO_Tcp_01_Delay);
                    table.Rows.Add("[Delay] TCP 02", _local.MCU_INFO_Tcp_02_Delay);

                    table.Rows.Add("[판정] MCU ID 조건 자릿수", _local.MCU_INFO_Mcu_Id_Len);
                    break;

                case "SDP":
                    table.Rows.Add("[Delay] Step", _local.SDP_Step_Delay);
                    table.Rows.Add("[Delay] PBA Delay", _local.SDP_Pba_Delay);
                    table.Rows.Add("[Delay] SDP TA Delay", _local.SDP_TA_Delay);
                    table.Rows.Add("[Delay] TCP 01", _local.SDP_Tcp_01_Delay);
                    table.Rows.Add("[Delay] TCP 02", _local.SDP_Tcp_02_Delay);
                    table.Rows.Add("[판정] TA TYPE", _local.SDP_TA_Type);
                    table.Rows.Add("[판정] USB_SDP Current Min (mA)", _local.SDP_USB_Current_Min);
                    table.Rows.Add("[판정] USB_SDP Current Max (mA)", _local.SDP_USB_Current_Max);
                    table.Rows.Add("[판정] CHG_SDP Current Min (mA)", _local.SDP_CHG_Current_Min);
                    table.Rows.Add("[판정] CHG_SDP Current Max (mA)", _local.SDP_CHG_Current_Max);
                    break;

                case "DCP":
                    table.Rows.Add("[Delay] Step", _local.DCP_Step_Delay);
                    table.Rows.Add("[Delay] PBA Delay", _local.DCP_Pba_Delay);
                    table.Rows.Add("[Delay] DCP TA Delay", _local.DCP_TA_Delay);
                    table.Rows.Add("[Delay] TCP 01", _local.DCP_Tcp_01_Delay);
                    table.Rows.Add("[Delay] TCP 02", _local.DCP_Tcp_02_Delay);
                    table.Rows.Add("[판정] TA TYPE", _local.DCP_TA_Type);
                    table.Rows.Add("[판정] USB_DCP Current Min (mA)", _local.DCP_USB_Current_Min);
                    table.Rows.Add("[판정] USB_DCP Current Max (mA)", _local.DCP_USB_Current_Max);
                    table.Rows.Add("[판정] CHG_DCP Current Min (mA)", _local.DCP_CHG_Current_Min);
                    table.Rows.Add("[판정] CHG_DCP Current Max (mA)", _local.DCP_CHG_Current_Max);
                    break;

                case "HVDCP": 
                    table.Rows.Add("[Delay] Step", _local.HVDCP_Step_Delay);
                    table.Rows.Add("[Delay] PBA Delay", _local.HVDCP_Pba_Delay);
                    table.Rows.Add("[Delay] HVDCP TA Delay", _local.HVDCP_TA_Delay);
                    table.Rows.Add("[Delay] TCP 01", _local.HVDCP_Tcp_01_Delay);
                    table.Rows.Add("[Delay] TCP 02", _local.HVDCP_Tcp_02_Delay);
                    table.Rows.Add("[판정] TA TYPE", _local.HVDCP_TA_Type);
                    table.Rows.Add("[판정] USB_HVDCP Current Min (mA)", _local.HVDCP_USB_Current_Min);
                    table.Rows.Add("[판정] USB_HVDCP Current Max (mA)", _local.HVDCP_USB_Current_Max);
                    table.Rows.Add("[판정] CHG_HVDCP Current Min (mA)", _local.HVDCP_CHG_Current_Min);
                    table.Rows.Add("[판정] CHG_HVDCP Current Max (mA)", _local.HVDCP_CHG_Current_Max);
                    break;

                case "PPS": 
                    table.Rows.Add("[Delay] Step", _local.PPS_Step_Delay);
                    table.Rows.Add("[Delay] PBA Delay", _local.PPS_Pba_Delay);
                    table.Rows.Add("[Delay] PPS TA Delay", _local.PPS_TA_Delay);
                    table.Rows.Add("[Delay] TCP 01", _local.PPS_Tcp_01_Delay);
                    table.Rows.Add("[Delay] TCP 02", _local.PPS_Tcp_02_Delay);
                    table.Rows.Add("[판정] TA TYPE", _local.PPS_TA_Type);
                    table.Rows.Add("[판정] USB_PPS Current Min (mA)", _local.PPS_USB_Current_Min);
                    table.Rows.Add("[판정] USB_PPS Current Max (mA)", _local.PPS_USB_Current_Max);
                    table.Rows.Add("[판정] CHG_PPS Current Min (mA)", _local.PPS_CHG_Current_Min);
                    table.Rows.Add("[판정] CHG_PPS Current Max (mA)", _local.PPS_CHG_Current_Max);
                    break;

                case "CHARGE COUNT RESET":
                    table.Rows.Add("[Delay] Step", _local.CHARGE_COUNT_RESET_Step_Delay);
                    table.Rows.Add("[Delay] PBA Delay", _local.CHARGE_COUNT_RESET_Pba_Delay);
                    table.Rows.Add("[Delay] TA Delay", _local.CHARGE_COUNT_RESET_TA_Delay); 
                    table.Rows.Add("[Delay] TCP 01", _local.CHARGE_COUNT_RESET_Tcp_01_Delay);
                    table.Rows.Add("[Delay] TCP 02", _local.CHARGE_COUNT_RESET_Tcp_02_Delay);
                    break;

                case "MES":
                    break;
            }

            return table;
        }

        private void dgViewSetValue_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgViewSetValue.Columns[e.ColumnIndex].Name == "Parameter")
            {
                string paramName = e.Value?.ToString();

                if (paramName != null)
                {
                    if (paramName.Contains("Delay") || paramName.Contains("딜레이") || paramName.Contains("설정"))
                    {
                        dgViewSetValue.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 224, 192); // 주황
                    }
                    else if (paramName.Contains("판정") || paramName.Contains("사용") || paramName.Contains("판단"))
                    {
                        dgViewSetValue.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 255); // 하늘
                    }
                }
            }
        }

        private void dgViewSetValue_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgViewSetValue.IsCurrentCellDirty)
            {
                dgViewSetValue.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
        private void dgViewSetValue_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 1) return;

            string param = dgViewSetValue.Rows[e.RowIndex].Cells[0].Value?.ToString();
            string value = dgViewSetValue.Rows[e.RowIndex].Cells[1].Value?.ToString();
            if (string.IsNullOrWhiteSpace(param) || string.IsNullOrWhiteSpace(currentTask)) return;

            try
            {
                switch (currentTask)
                {
                    case "INTERLOCK":
                        break;

                    case "QR READ":
                        if (param.Contains("Step")) _local.QR_READ_Step_Delay = int.Parse(value);
                        else if (param.Contains("자릿수")) _local.QR_READ_Len = int.Parse(value);
                        break;

                    case "MCU INFO":
                        if (param.Contains("Step")) _local.MCU_INFO_Step_Delay = int.Parse(value);
                        else if (param.Contains("PBA Delay")) _local.MCU_INFO_Pba_Delay = int.Parse(value);
                        else if (param.Contains("TCP 01")) _local.MCU_INFO_Tcp_01_Delay = int.Parse(value);
                        else if (param.Contains("TCP 02")) _local.MCU_INFO_Tcp_02_Delay = int.Parse(value);
                        else if (param.Contains("MCU ID")) _local.MCU_INFO_Mcu_Id_Len = int.Parse(value);
                        break;

                    case "SDP":
                        if (param.Contains("Step")) _local.SDP_Step_Delay = int.Parse(value);
                        else if (param.Contains("PBA Delay")) _local.SDP_Pba_Delay = int.Parse(value);
                        else if (param.Contains("SDP TA Delay")) _local.SDP_TA_Delay = int.Parse(value);
                        else if (param.Contains("TCP 01")) _local.SDP_Tcp_01_Delay = int.Parse(value);
                        else if (param.Contains("TCP 02")) _local.SDP_Tcp_02_Delay = int.Parse(value);
                        else if (param.Contains("TA TYPE")) _local.SDP_TA_Type = short.Parse(value);
                        else if (param.Contains("USB_SDP Current Min")) _local.SDP_USB_Current_Min = float.Parse(value);
                        else if (param.Contains("USB_SDP Current Max")) _local.SDP_USB_Current_Max = float.Parse(value);
                        else if (param.Contains("CHG_SDP Current Min")) _local.SDP_CHG_Current_Min = short.Parse(value);
                        else if (param.Contains("CHG_SDP Current Max")) _local.SDP_CHG_Current_Max = short.Parse(value);
                        break;

                    case "DCP":
                        if (param.Contains("Step")) _local.DCP_Step_Delay = int.Parse(value);
                        else if (param.Contains("PBA Delay")) _local.DCP_Pba_Delay = int.Parse(value);
                        else if (param.Contains("DCP TA Delay")) _local.DCP_TA_Delay = int.Parse(value);
                        else if (param.Contains("TCP 01")) _local.DCP_Tcp_01_Delay = int.Parse(value);
                        else if (param.Contains("TCP 02")) _local.DCP_Tcp_02_Delay = int.Parse(value);
                        else if (param.Contains("TA TYPE")) _local.DCP_TA_Type = short.Parse(value);
                        else if (param.Contains("USB_DCP Current Min")) _local.DCP_USB_Current_Min = float.Parse(value);
                        else if (param.Contains("USB_DCP Current Max")) _local.DCP_USB_Current_Max = float.Parse(value);
                        else if (param.Contains("CHG_DCP Current Min")) _local.DCP_CHG_Current_Min = short.Parse(value);
                        else if (param.Contains("CHG_DCP Current Max")) _local.DCP_CHG_Current_Max = short.Parse(value);
                        break;

                    case "HVDCP": 
                        if (param.Contains("Step")) _local.HVDCP_Step_Delay = int.Parse(value);
                        else if (param.Contains("PBA Delay")) _local.HVDCP_Pba_Delay = int.Parse(value);
                        else if (param.Contains("HVDCP TA Delay")) _local.HVDCP_TA_Delay = int.Parse(value);
                        else if (param.Contains("TCP 01")) _local.HVDCP_Tcp_01_Delay = int.Parse(value);
                        else if (param.Contains("TCP 02")) _local.HVDCP_Tcp_02_Delay = int.Parse(value);
                        else if (param.Contains("TA TYPE")) _local.HVDCP_TA_Type = short.Parse(value);
                        else if (param.Contains("USB_HVDCP Current Min")) _local.HVDCP_USB_Current_Min = float.Parse(value);
                        else if (param.Contains("USB_HVDCP Current Max")) _local.HVDCP_USB_Current_Max = float.Parse(value);
                        else if (param.Contains("CHG_HVDCP Current Min")) _local.HVDCP_CHG_Current_Min = short.Parse(value);
                        else if (param.Contains("CHG_HVDCP Current Max")) _local.HVDCP_CHG_Current_Max = short.Parse(value);
                        break;

                    case "PPS": 
                        if (param.Contains("Step")) _local.PPS_Step_Delay = int.Parse(value);
                        else if (param.Contains("PBA Delay")) _local.PPS_Pba_Delay = int.Parse(value);
                        else if (param.Contains("PPS TA Delay")) _local.PPS_TA_Delay = int.Parse(value);
                        else if (param.Contains("TCP 01")) _local.PPS_Tcp_01_Delay = int.Parse(value);
                        else if (param.Contains("TCP 02")) _local.PPS_Tcp_02_Delay = int.Parse(value);
                        else if (param.Contains("TA TYPE")) _local.PPS_TA_Type = short.Parse(value);
                        else if (param.Contains("USB_PPS Current Min")) _local.PPS_USB_Current_Min = float.Parse(value);
                        else if (param.Contains("USB_PPS Current Max")) _local.PPS_USB_Current_Max = float.Parse(value);
                        else if (param.Contains("CHG_PPS Current Min")) _local.PPS_CHG_Current_Min = short.Parse(value);
                        else if (param.Contains("CHG_PPS Current Max")) _local.PPS_CHG_Current_Max = short.Parse(value);
                        break;

                    case "CHARGE COUNT RESET":
                        if (param.Contains("Step")) _local.CHARGE_COUNT_RESET_Step_Delay = int.Parse(value);
                        else if (param.Contains("PBA")) _local.CHARGE_COUNT_RESET_Pba_Delay = int.Parse(value);
                        else if (param.Contains("TA Delay")) _local.CHARGE_COUNT_RESET_TA_Delay = int.Parse(value); 
                        else if (param.Contains("TCP 01")) _local.CHARGE_COUNT_RESET_Tcp_01_Delay = int.Parse(value);
                        else if (param.Contains("TCP 02")) _local.CHARGE_COUNT_RESET_Tcp_02_Delay = int.Parse(value);
                        break;

                    case "MES":
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"로컬 변수에 값 저장 실패 {ex.Message}");
            }
        }

        private void dgViewTaskList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == dgViewTaskList.Columns["Enable"].Index)
            {
                string task = dgViewTaskList.Rows[e.RowIndex].Cells["Item"].Value?.ToString();
                if (string.IsNullOrWhiteSpace(task)) return;

                bool enabled = Convert.ToBoolean(dgViewTaskList.Rows[e.RowIndex].Cells["Enable"].Value);

                switch (task)
                {
                    case "INTERLOCK": _local.INTERLOCK_Enable = enabled; break;
                    case "QR READ": _local.QR_READ_Enable = enabled; break;
                    case "MCU INFO": _local.MCU_INFO_Enable = enabled; break;
                    case "SDP": _local.SDP_Enable = enabled; break;
                    case "DCP": _local.DCP_Enable = enabled; break;
                    case "MES": _local.MES_Enable = enabled; break;
                    case "HVDCP": _local.HVDCP_Enable = enabled; break;
                    case "CHARGE COUNT RESET": _local.CHARGE_COUNT_RESET_Enable = enabled; break;
                }
            }
        }

        private void dgViewTaskList_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgViewTaskList.IsCurrentCellDirty)
                dgViewTaskList.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void btnAllCheck_Click(object sender, EventArgs e)
        {
            SetAllEnableStatus(true);
        }

        private void btnClearCheck_Click(object sender, EventArgs e)
        {
            SetAllEnableStatus(false);
        }

        private void SetAllEnableStatus(bool isChecked)
        {
            DataTable dt = dgViewTaskList.DataSource as DataTable;
            if (dt == null) return;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["Enable"] = isChecked;

                // 중요: 이벤트를 안 타니까, 강제로 로직을 실행시킴
                // e 인자에 null을 넣어도 현재 핸들러 로직상 문제는 없지만, 
                // 안전하게 인덱스를 지정해서 호출할 수 있습니다.
                UpdateLocalVariableFromRow(i, isChecked);
            }
            dgViewTaskList.Refresh();
        }

        private void UpdateLocalVariableFromRow(int rowIndex, bool enabled)
        {
            // 1. 행 인덱스 유효성 검사
            if (rowIndex < 0 || rowIndex >= dgViewTaskList.Rows.Count) return;

            // 2. Task 이름 가져오기
            string task = dgViewTaskList.Rows[rowIndex].Cells["Item"].Value?.ToString();
            if (string.IsNullOrWhiteSpace(task)) return;

            // 3. Task 이름에 따른 _local 버퍼 업데이트
            switch (task)
            {
                case "INTERLOCK": _local.INTERLOCK_Enable = enabled; break;
                case "QR READ": _local.QR_READ_Enable = enabled; break;
                case "MCU INFO": _local.MCU_INFO_Enable = enabled; break;
                case "SDP": _local.SDP_Enable = enabled; break;
                case "DCP": _local.DCP_Enable = enabled; break;
                case "MES": _local.MES_Enable = enabled; break;
                case "HVDCP": _local.HVDCP_Enable = enabled; break;
                case "PPS": _local.PPS_Enable = enabled; break;
                case "CHARGE COUNT RESET": _local.CHARGE_COUNT_RESET_Enable = enabled; break;
                default:
                    Console.WriteLine($"알 수 없는 Task: {task}");
                    break;
            }
        }

        #endregion

        #region JSON
        private void SaveRecipeToJson(string filePath)
        {
            List<string> taskOrder = new List<string>();
            foreach (DataGridViewRow row in dgViewTaskList.Rows)
            {
                if (row.IsNewRow) continue;
                string task = row.Cells["Item"].Value?.ToString();
                if (!string.IsNullOrEmpty(task))
                    taskOrder.Add(task);
            }

            JObject settings = new JObject
            {
                ["INTERLOCK_Enable"] = _local.INTERLOCK_Enable,

                ["QR_READ_Enable"] = _local.QR_READ_Enable,
                ["QR_READ_Step_Delay"] = _local.QR_READ_Step_Delay,
                ["QR_READ_Len"] = _local.QR_READ_Len,

                ["MCU_INFO_Enable"] = _local.MCU_INFO_Enable,
                ["MCU_INFO_Step_Delay"] = _local.MCU_INFO_Step_Delay,
                ["MCU_INFO_Pba_Delay"] = _local.MCU_INFO_Pba_Delay,
                ["MCU_INFO_Tcp_01_Delay"] = _local.MCU_INFO_Tcp_01_Delay,
                ["MCU_INFO_Tcp_02_Delay"] = _local.MCU_INFO_Tcp_02_Delay,
                ["MCU_INFO_Mcu_Id_Len"] = _local.MCU_INFO_Mcu_Id_Len,

                ["SDP_Enable"] = _local.SDP_Enable,
                ["SDP_Step_Delay"] = _local.SDP_Step_Delay,
                ["SDP_Pba_Delay"] = _local.SDP_Pba_Delay,
                ["SDP_TA_Delay"] = _local.SDP_TA_Delay,
                ["SDP_Tcp_01_Delay"] = _local.SDP_Tcp_01_Delay,
                ["SDP_Tcp_02_Delay"] = _local.SDP_Tcp_02_Delay,
                ["SDP_TA_Type"] = _local.SDP_TA_Type,
                ["SDP_USB_Current_Min"] = _local.SDP_USB_Current_Min,
                ["SDP_USB_Current_Max"] = _local.SDP_USB_Current_Max,
                ["SDP_CHG_Current_Min"] = _local.SDP_CHG_Current_Min,
                ["SDP_CHG_Current_Max"] = _local.SDP_CHG_Current_Max,

                ["DCP_Enable"] = _local.DCP_Enable,
                ["DCP_Step_Delay"] = _local.DCP_Step_Delay,
                ["DCP_Pba_Delay"] = _local.DCP_Pba_Delay,
                ["DCP_TA_Delay"] = _local.DCP_TA_Delay,
                ["DCP_Tcp_01_Delay"] = _local.DCP_Tcp_01_Delay,
                ["DCP_Tcp_02_Delay"] = _local.DCP_Tcp_02_Delay,
                ["DCP_TA_Type"] = _local.DCP_TA_Type,
                ["DCP_USB_Current_Min"] = _local.DCP_USB_Current_Min,
                ["DCP_USB_Current_Max"] = _local.DCP_USB_Current_Max,
                ["DCP_CHG_Current_Min"] = _local.DCP_CHG_Current_Min,
                ["DCP_CHG_Current_Max"] = _local.DCP_CHG_Current_Max,

                ["HVDCP_Enable"] = _local.HVDCP_Enable,
                ["HVDCP_Step_Delay"] = _local.HVDCP_Step_Delay,
                ["HVDCP_Pba_Delay"] = _local.HVDCP_Pba_Delay,
                ["HVDCP_TA_Delay"] = _local.HVDCP_TA_Delay,
                ["HVDCP_Tcp_01_Delay"] = _local.HVDCP_Tcp_01_Delay,
                ["HVDCP_Tcp_02_Delay"] = _local.HVDCP_Tcp_02_Delay,
                ["HVDCP_TA_Type"] = _local.HVDCP_TA_Type,
                ["HVDCP_USB_Current_Min"] = _local.HVDCP_USB_Current_Min,
                ["HVDCP_USB_Current_Max"] = _local.HVDCP_USB_Current_Max,
                ["HVDCP_CHG_Current_Min"] = _local.HVDCP_CHG_Current_Min,
                ["HVDCP_CHG_Current_Max"] = _local.HVDCP_CHG_Current_Max,

                ["PPS_Enable"] = _local.PPS_Enable,
                ["PPS_Step_Delay"] = _local.PPS_Step_Delay,
                ["PPS_Pba_Delay"] = _local.PPS_Pba_Delay,
                ["PPS_TA_Delay"] = _local.PPS_TA_Delay,
                ["PPS_Tcp_01_Delay"] = _local.PPS_Tcp_01_Delay,
                ["PPS_Tcp_02_Delay"] = _local.PPS_Tcp_02_Delay,
                ["PPS_TA_Type"] = _local.PPS_TA_Type,
                ["PPS_USB_Current_Min"] = _local.PPS_USB_Current_Min,
                ["PPS_USB_Current_Max"] = _local.PPS_USB_Current_Max,
                ["PPS_CHG_Current_Min"] = _local.PPS_CHG_Current_Min,
                ["PPS_CHG_Current_Max"] = _local.PPS_CHG_Current_Max,

                ["CHARGE_COUNT_RESET_Enable"] = _local.CHARGE_COUNT_RESET_Enable,
                ["CHARGE_COUNT_RESET_Step_Delay"] = _local.CHARGE_COUNT_RESET_Step_Delay,
                ["CHARGE_COUNT_RESET_Pba_Delay"] = _local.CHARGE_COUNT_RESET_Pba_Delay,
                ["CHARGE_COUNT_RESET_TA_Delay"] = _local.CHARGE_COUNT_RESET_TA_Delay, 
                ["CHARGE_COUNT_RESET_Tcp_01_Delay"] = _local.CHARGE_COUNT_RESET_Tcp_01_Delay,
                ["CHARGE_COUNT_RESET_Tcp_02_Delay"] = _local.CHARGE_COUNT_RESET_Tcp_02_Delay,

                ["MES_Enable"] = _local.MES_Enable
            };

            JObject recipe = new JObject
            {
                ["Type"] = "Recipe",
                ["Settings"] = settings,
                ["TaskOrder"] = JArray.FromObject(taskOrder)
            };

            string json = JsonConvert.SerializeObject(recipe, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        private void btnSaveRecipe_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON files (*.json)|*.json";
            saveFileDialog.Title = "레시피 저장";
            saveFileDialog.FileName = "Recipe.json";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveRecipeToJson(saveFileDialog.FileName);
            }
        }

        private void LoadRecipeFromJson(string filePath)
        {
            try
            {
                string json = File.ReadAllText(filePath);
                dynamic recipe = JsonConvert.DeserializeObject(json);

                if (recipe.Type == null || recipe.Type.ToString() != "Recipe")
                {
                    MessageBox.Show("유효하지 않은 레시피 파일입니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                dynamic settings = recipe.Settings;

                _local.INTERLOCK_Enable = settings.INTERLOCK_Enable;
                _local.QR_READ_Enable = settings.QR_READ_Enable;
                _local.MCU_INFO_Enable = settings.MCU_INFO_Enable;
                _local.SDP_Enable = settings.SDP_Enable;
                _local.DCP_Enable = settings.DCP_Enable;
                _local.MES_Enable = settings.MES_Enable;

                // Values
                _local.QR_READ_Step_Delay = settings.QR_READ_Step_Delay;
                _local.QR_READ_Len = settings.QR_READ_Len;
                _local.MCU_INFO_Step_Delay = settings.MCU_INFO_Step_Delay;
                _local.MCU_INFO_Pba_Delay = settings.MCU_INFO_Pba_Delay;
                _local.MCU_INFO_Tcp_01_Delay = settings.MCU_INFO_Tcp_01_Delay;
                _local.MCU_INFO_Tcp_02_Delay = settings.MCU_INFO_Tcp_02_Delay;

                _local.MCU_INFO_Mcu_Id_Len = settings.MCU_INFO_Mcu_Id_Len;


                _local.SDP_Step_Delay = (int)settings.SDP_Step_Delay;
                _local.SDP_Pba_Delay = (int)settings.SDP_Pba_Delay;
                _local.SDP_TA_Delay = (int)settings.SDP_TA_Delay;
                _local.SDP_Tcp_01_Delay = (int)settings.SDP_Tcp_01_Delay;
                _local.SDP_Tcp_02_Delay = (int)settings.SDP_Tcp_02_Delay;
                _local.SDP_TA_Type = (short)settings.SDP_TA_Type;
                _local.SDP_USB_Current_Min = (float)settings.SDP_USB_Current_Min;
                _local.SDP_USB_Current_Max = (float)settings.SDP_USB_Current_Max;
                _local.SDP_CHG_Current_Min = (short)settings.SDP_CHG_Current_Min;
                _local.SDP_CHG_Current_Max = (short)settings.SDP_CHG_Current_Max;


                _local.DCP_Step_Delay = (int)settings.DCP_Step_Delay;
                _local.DCP_Pba_Delay = (int)settings.DCP_Pba_Delay;
                _local.DCP_TA_Delay = (int)settings.DCP_TA_Delay;
                _local.DCP_Tcp_01_Delay = (int)settings.DCP_Tcp_01_Delay;
                _local.DCP_Tcp_02_Delay = (int)settings.DCP_Tcp_02_Delay;
                _local.DCP_TA_Type = (short)settings.DCP_TA_Type;
                _local.DCP_USB_Current_Min = (float)settings.DCP_USB_Current_Min;
                _local.DCP_USB_Current_Max = (float)settings.DCP_USB_Current_Max;
                _local.DCP_CHG_Current_Min = (short)settings.DCP_CHG_Current_Min;
                _local.DCP_CHG_Current_Max = (short)settings.DCP_CHG_Current_Max;

                _local.HVDCP_Enable = settings.HVDCP_Enable;
                _local.HVDCP_Step_Delay = (int)settings.HVDCP_Step_Delay;
                _local.HVDCP_Pba_Delay = (int)settings.HVDCP_Pba_Delay;
                _local.HVDCP_TA_Delay = (int)settings.HVDCP_TA_Delay;
                _local.HVDCP_Tcp_01_Delay = (int)settings.HVDCP_Tcp_01_Delay;
                _local.HVDCP_Tcp_02_Delay = (int)settings.HVDCP_Tcp_02_Delay;
                _local.HVDCP_TA_Type = (short)settings.HVDCP_TA_Type;
                _local.HVDCP_USB_Current_Min = (float)settings.HVDCP_USB_Current_Min;
                _local.HVDCP_USB_Current_Max = (float)settings.HVDCP_USB_Current_Max;
                _local.HVDCP_CHG_Current_Min = (short)settings.HVDCP_CHG_Current_Min;
                _local.HVDCP_CHG_Current_Max = (short)settings.HVDCP_CHG_Current_Max;

                _local.PPS_Enable = settings.PPS_Enable;
                _local.PPS_Step_Delay = (int)(settings.PPS_Step_Delay);
                _local.PPS_Pba_Delay = (int)(settings.PPS_Pba_Delay);
                _local.PPS_TA_Delay = (int)(settings.PPS_TA_Delay);
                _local.PPS_Tcp_01_Delay = (int)(settings.PPS_Tcp_01_Delay);
                _local.PPS_Tcp_02_Delay = (int)(settings.PPS_Tcp_02_Delay);
                _local.PPS_TA_Type = (short)(settings.PPS_TA_Type);
                _local.PPS_USB_Current_Min = (float)(settings.PPS_USB_Current_Min);
                _local.PPS_USB_Current_Max = (float)(settings.PPS_USB_Current_Max);
                _local.PPS_CHG_Current_Min = (short)(settings.PPS_CHG_Current_Min);
                _local.PPS_CHG_Current_Max = (short)(settings.PPS_CHG_Current_Max);

                _local.CHARGE_COUNT_RESET_Enable = settings.CHARGE_COUNT_RESET_Enable;
                _local.CHARGE_COUNT_RESET_Step_Delay = (int)(settings.CHARGE_COUNT_RESET_Step_Delay);
                _local.CHARGE_COUNT_RESET_Pba_Delay = (int)(settings.CHARGE_COUNT_RESET_Pba_Delay);
                _local.CHARGE_COUNT_RESET_TA_Delay = (int)(settings.CHARGE_COUNT_RESET_TA_Delay); 
                _local.CHARGE_COUNT_RESET_Tcp_01_Delay = (int)(settings.CHARGE_COUNT_RESET_Tcp_01_Delay);
                _local.CHARGE_COUNT_RESET_Tcp_02_Delay = (int)(settings.CHARGE_COUNT_RESET_Tcp_02_Delay);

                dgViewTaskList.DataSource = null;
                dgViewSetValue.DataSource = null;

                // TaskOrder 반영
                List<string> taskOrder = new List<string>();
                if (recipe.TaskOrder != null)
                {
                    foreach (var task in recipe.TaskOrder)
                        taskOrder.Add(task.ToString());
                }

                DataTable table = CreateTaskTable();
                DataTable reorderedTable = table.Clone();

                foreach (string taskName in taskOrder)
                {
                    DataRow[] found = table.Select($"Item = '{taskName.Replace("'", "''")}'");
                    if (found.Length > 0)
                        reorderedTable.ImportRow(found[0]);
                }

                foreach (DataRow row in table.Rows)
                {
                    string item = row["Item"].ToString();
                    if (!taskOrder.Contains(item))
                        reorderedTable.ImportRow(row);
                }

                dgViewTaskList.DataSource = null;
                dgViewTaskList.DataSource = reorderedTable;

                MessageBox.Show("레시피 불러오기 성공!", "완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"레시피 불러오기 오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLoadRecipe_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json";
            openFileDialog.Title = "레시피 불러오기";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                LoadRecipeFromJson(openFileDialog.FileName);
            }
        }

        #endregion

        #region Order Move

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            int index = dgViewTaskList.CurrentCell?.RowIndex ?? -1;
            if (index > 0)
                MoveRow(dgViewTaskList, index, index - 1);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            int index = dgViewTaskList.CurrentCell?.RowIndex ?? -1;
            var table = dgViewTaskList.DataSource as DataTable;
            if (table != null && index < table.Rows.Count - 1)
                MoveRow(dgViewTaskList, index, index + 1);
        }
        private void MoveRow(DataGridView grid, int fromIndex, int toIndex)
        {
            var table = grid.DataSource as DataTable;
            if (table == null || fromIndex < 0 || toIndex < 0 || fromIndex >= table.Rows.Count || toIndex >= table.Rows.Count)
                return;

            // 데이터 복사
            var temp = table.NewRow();
            temp.ItemArray = table.Rows[fromIndex].ItemArray;

            // 삭제 → 재삽입
            table.Rows.RemoveAt(fromIndex);
            table.Rows.InsertAt(temp, toIndex);

            // 선택 줄 갱신
            grid.ClearSelection();
            grid.CurrentCell = grid.Rows[toIndex].Cells[0];
            grid.Rows[toIndex].Selected = true;
        }
        private void btnPwChange_Click(object sender, EventArgs e)
        {
            var form = new ChangePasswordForm(this)
            {
                StartPosition = FormStartPosition.CenterScreen
            };
            form.ShowDialog(this);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        #endregion

        
    }
}
