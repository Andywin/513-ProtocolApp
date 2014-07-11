using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConvertProvider;

namespace CommunicationApp
{
    public partial class ProtocolConfigForm : Form
    {
        private Dictionary<string, bool> checkState; //存储DataGridView中复选框的状态
        DataSet protocol = new DataSet(); //存储数据的DataSet
        private int newProtocolFlag = 1; //设置计数器，添加的新协议数
        DataGridView dgvExchange; //设置一个DataGridView变量存储父窗体的DataGridView
        DataSet dataSetExchange; //设置一个DataSet变量存储父窗体的DataSet

        public ProtocolConfigForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 显示本对话框，存储母窗体表格
        /// </summary>
        /// <param name="parent">母窗体</param>
        /// <param name="dgvParent">母窗体表格</param>
        public void ShowDialog(IWin32Window parent, DataGridView dgvParent)
        {
            this.dgvExchange = dgvParent;
            this.ShowDialog(parent);
        }

        public void ShowDialog(IWin32Window parent, DataSet dataSetParent)
        {
            this.dataSetExchange = dataSetParent;
            this.ShowDialog(parent);
        }

        private void ProtocolConfig_Load(object sender, EventArgs e)
        {
            //载入DataSet数据，包含通信协议，协议内容存储在ProtocolXML.xml文件中
            protocol.ReadXml("../../ProtocolXMLfile.xml");
            dgvProtocolLib.AutoGenerateColumns = true;
            //绑定数据表
            BindingDataTable();

            // 初始化含有复选框选中状态的词典类实例
            checkState = new Dictionary<string, bool>();
            InitializeCheckState();
        }

        private void BindingDataTable()
        {
            //对数据表进行绑定
            dgvProtocolLib.DataSource = protocol;
            dgvProtocolLib.DataMember = "CommunicationProtocol";
            //设置首列复选框
            dgvProtocolLib.VirtualMode = true;
            dgvProtocolLib.Columns.Insert(0, new DataGridViewCheckBoxColumn(false));
            dgvProtocolLib.Columns[0].Resizable = DataGridViewTriState.False;
            dgvProtocolLib.Columns[0].Frozen = true;
            dgvProtocolLib.Columns[0].DividerWidth = 1;
            dgvProtocolLib.Columns[0].Width = 50;
            //设置列名
            dgvProtocolLib.Columns[0].HeaderText = "选中";
            dgvProtocolLib.Columns[0].Resizable = DataGridViewTriState.False;
           // dgvProtocolLib.Columns[0].Width = 100;
            dgvProtocolLib.Columns[1].HeaderText = "协议名称   ";
            dgvProtocolLib.Columns[2].HeaderText = "数据类型   ";
            dgvProtocolLib.Columns[3].HeaderText = "起始位   ";
            dgvProtocolLib.Columns[4].HeaderText = "个数   ";
            dgvProtocolLib.Columns[5].HeaderText = "数据长度";
            dgvProtocolLib.Columns[5].ReadOnly = true;
            dgvProtocolLib.Columns[5].DefaultCellStyle.BackColor = Color.LightGray;
            dgvProtocolLib.Columns[6].Visible = false;
            //初始化DataType的ComboBox
            InitializeDTComboBox();

            //设置禁止点击列标题进行重排
            for (int i = 0; i < dgvProtocolLib.Columns.Count; i++)
            {
                dgvProtocolLib.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dgvProtocolLib.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        /// <summary>
        /// 初始化DataType的ComboBox
        /// </summary>
        private void InitializeDTComboBox()
        {
            for (int i = 0; i < dgvProtocolLib.Rows.Count; i++)
            {
                DataGridViewComboBoxCell comboBoxCell = new DataGridViewComboBoxCell();
                comboBoxCell.Style.BackColor = Color.LightGray;
                comboBoxCell.Items.AddRange("Boolean", "Short", "Ushort", "Int", "Uint", "Long", "Ulong", "Float", "Double", "Char", "String");
                dgvProtocolLib.Rows[i].Cells["ProtocolDataType"] = comboBoxCell;
            }
        }

        /// <summary>
        /// 初始化checkState词典类实例的值
        /// </summary>
        private void InitializeCheckState()
        {
            checkState.Clear();
            for (int i = 0; i < dgvProtocolLib.Rows.Count; i++)
            {
                checkState.Add(dgvProtocolLib.Rows[i].Cells["Guid"].Value.ToString(), false);
            }
        }

        #region 设置虚拟列（复选框列）状态，并保持其状态
        /// <summary>
        /// 单击单元格内容时，判断是否选中复选框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvProtocolLib_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvProtocolLib.DataSource == null || !dgvProtocolLib.Columns.Contains("ProtocolName") || e.RowIndex == -1)
            {
                return;
            }
            if (e.ColumnIndex == 0 && (dgvProtocolLib.Rows[e.RowIndex].Cells["ProtocolName"].Value != null))
            {
                //设置选中的复选框的值为当前值
                dgvProtocolLib.Rows[e.RowIndex].Cells[0].Value = (bool)dgvProtocolLib.Rows[e.RowIndex].Cells[0].EditedFormattedValue;
            }
        }

        /// <summary>
        /// 需要单元格的值以设置单元格格式时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvProtocolLib_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            if (dgvProtocolLib.DataSource == null || !dgvProtocolLib.Columns.Contains("Guid"))
            {
                return;
            }
            //如果需要此处的值，从词典类中获取
            if (e.ColumnIndex == 0 && (dgvProtocolLib.Rows[e.RowIndex].Cells["Guid"].Value != null))
            {
                //获取DataType列中的Guid信息
                string protocolGuid = (string)dgvProtocolLib.Rows[e.RowIndex].Cells["Guid"].Value;
                //将该行是否选中存储到词典类中
                e.Value = checkState[protocolGuid];
            }
        }

        /// <summary>
        /// 单元格内容改变，需要存储数据时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvProtocolLib_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {
            if (dgvProtocolLib.DataSource == null || !dgvProtocolLib.Columns.Contains("Guid"))
            {
                return;
            }
            //虚拟列中的单元格值改变时，需要存储到词典类中
            if (e.ColumnIndex == 0 && (dgvProtocolLib.Rows[e.RowIndex].Cells["Guid"].Value != null))
            {
                //获取DataType列中的DataType信息
                string protocolGuid = (string)dgvProtocolLib.Rows[e.RowIndex].Cells["Guid"].Value;

                //更新选中的复选框值到词典类中
                checkState[protocolGuid] = (bool)e.Value;
            }
        }
        #endregion

        /// <summary>
        /// 设置重绘单元格行时，把首列之前的列名提示符绘制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvProtocolLib_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dgvProtocolLib.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString(e.RowIndex.ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);
            }
        }

        #region 表格中输入数字有效性检查（禁止输入其他字符）
        /// <summary>
        /// 定义编辑单元格事件，设置单元格的输入（位置、长度）只能是整数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvProtocolLib_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if ((this.dgvProtocolLib.CurrentCell.ColumnIndex >= 3) && (this.dgvProtocolLib.CurrentCell.ColumnIndex <= 4))
            {
                e.Control.KeyPress += Control_KeyPress;
            }
            else
            {
                e.Control.KeyPress -= Control_KeyPress;
            }
        }

        /// <summary>
        /// 设置按键时出发的事件，只能输入数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            //设置能输入的值，8为Backspace键
            if (e.KeyChar != 8 && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        #endregion

        #region 更新状态栏
        /// <summary>
        /// 单元格选中内容发生更改时，更新状态栏，更新表格，使DataLength根据DataType改变而改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvProtocolLib_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                this.UptateStatusBar();
            }
            else if (e.ColumnIndex == 2 && e.RowIndex != -1)
            {
                switch (dgvProtocolLib.CurrentCell.Value.ToString())
                {
                    case "Boolean":
                        dgvProtocolLib.Rows[e.RowIndex].Cells["DataLength"].Value = 1;
                        break;
                    case "Short":
                        dgvProtocolLib.Rows[e.RowIndex].Cells["DataLength"].Value = 2;
                        break;
                    case "Ushort":
                        dgvProtocolLib.Rows[e.RowIndex].Cells["DataLength"].Value = 2;
                        break;
                    case "Int":
                        dgvProtocolLib.Rows[e.RowIndex].Cells["DataLength"].Value = 4;
                        break;
                    case "Uint":
                        dgvProtocolLib.Rows[e.RowIndex].Cells["DataLength"].Value = 4;
                        break;
                    case "Long":
                        dgvProtocolLib.Rows[e.RowIndex].Cells["DataLength"].Value = 8;
                        break;
                    case "Ulong":
                        dgvProtocolLib.Rows[e.RowIndex].Cells["DataLength"].Value = 8;
                        break;
                    case "Float":
                        dgvProtocolLib.Rows[e.RowIndex].Cells["DataLength"].Value = 4;
                        break;
                    case "Double":
                        dgvProtocolLib.Rows[e.RowIndex].Cells["DataLength"].Value = 8;
                        break;
                    case "Char":
                        dgvProtocolLib.Rows[e.RowIndex].Cells["DataLength"].Value = 2;
                        break;
                    case "String":
                        dgvProtocolLib.Rows[e.RowIndex].Cells["DataLength"].Value = "AUTO";
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 更新下方状态栏，显示选中了哪些数据协议
        /// </summary>
        private void UptateStatusBar()
        {
            //计算词典中选中协议的数量，更新状态栏
            int number = 0;
            foreach (bool isChecked in checkState.Values)
            {
                if (isChecked)
                {
                    number++;
                }
            }
            toolStripStatusLabelProtocolChosen.Text = "选中的数据协议数：" + number.ToString();
        } 
        #endregion

        /// <summary>
        /// 添加新协议
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAddNewProtocol_Click(object sender, EventArgs e)
        {
            //重新绑定数据列:先清空，再绑定
            dgvProtocolLib.DataSource = null;
            dgvProtocolLib.Columns.Clear();
            DataRow newRow = protocol.Tables["CommunicationProtocol"].NewRow();
            newRow["ProtocolName"] = string.Format("newProtocol" + newProtocolFlag);
            newProtocolFlag++;
            newRow["ProtocolDataType"] = "Int";
            newRow["StartingPosition"] = 0;
            newRow["ProtocolCount"] = 1;
            newRow["DataLength"] = 4;
            newRow["Guid"] = Guid.NewGuid().ToString();
            protocol.Tables["CommunicationProtocol"].Rows.Add(newRow);
            //绑定数据列
            BindingDataTable();
            //初始化checkState词典类实例的值
            InitializeCheckState();
            //将焦点置于最新的单元格,并编辑ProtocolName单元格
            dgvProtocolLib.CurrentCell = dgvProtocolLib.Rows[dgvProtocolLib.Rows.Count - 1].Cells["ProtocolName"];
            dgvProtocolLib.BeginEdit(true);
        }

        /// <summary>
        /// 单击取消按钮，关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 单击确认按钮，把选中的所有协议存储到dgv或者dataSet中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (this.dgvExchange != null)
            {
                this.SavingDataGridView();              
            }
            else if (this.dataSetExchange != null)
            {
                this.SavingDataSet();
            }
            this.Close();
        }

        /// <summary>
        /// 对DataSet进行保存
        /// </summary>
        private void SavingDataSet()
        {
            for (int i = 0; i < dgvProtocolLib.Rows.Count; i++)
            {
                string protocolGuid = dgvProtocolLib.Rows[i].Cells["Guid"].Value.ToString();
                if (checkState[protocolGuid])
                {
                    //新建DataRow实例，存储要添加的行
                   DataRow rowsToAdd = dataSetExchange.Tables["Protocols"].NewRow();
                   rowsToAdd[0] = dgvProtocolLib.Rows[i].Cells["ProtocolName"].Value;
                   rowsToAdd[1] = dgvProtocolLib.Rows[i].Cells["ProtocolDataType"].Value;
                   rowsToAdd[2] = dgvProtocolLib.Rows[i].Cells["DataLength"].Value;
                   rowsToAdd[3] = dgvProtocolLib.Rows[i].Cells["StartingPosition"].Value;
                   rowsToAdd[4] = dgvProtocolLib.Rows[i].Cells["ProtocolCount"].Value;
                    //将dataRow添加到dataSet中
                   dataSetExchange.Tables["Protocols"].Rows.Add(rowsToAdd);
                }
            }
        }

        /// <summary>
        /// 添加表格信息到父窗体表格中
        /// </summary>
        private void SavingDataGridView()
        {
            for (int i = 0; i < dgvProtocolLib.Rows.Count; i++)
            {
                string protocolGuid = dgvProtocolLib.Rows[i].Cells["Guid"].Value.ToString();
                if (checkState[protocolGuid])
                {
                   int index = dgvExchange.Rows.Add();
                   dgvExchange.Rows[index].Cells[0].Value = dgvProtocolLib.Rows[i].Cells["ProtocolName"].Value;
                   dgvExchange.Rows[index].Cells[1].Value = dgvProtocolLib.Rows[i].Cells["ProtocolDataType"].Value;
                   dgvExchange.Rows[index].Cells[2].Value = dgvProtocolLib.Rows[i].Cells["DataLength"].Value;
                   dgvExchange.Rows[index].Cells[3].Value = dgvProtocolLib.Rows[i].Cells["StartingPosition"].Value;
                   dgvExchange.Rows[index].Cells[4].Value = dgvProtocolLib.Rows[i].Cells["ProtocolCount"].Value;
                }
            }
        }

        /// <summary>
        /// 保存协议库，对DataSet协议库所有内容进行保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSaveProtocol_Click(object sender, EventArgs e)
        {
            protocol.WriteXml("../../ProtocolXMLfile.xml");
            MessageBox.Show(this, "保存协议成功");
        }

        /// <summary>
        /// 点击删除协议按钮，删除选中的一条协议
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDeleteProtocol_Click(object sender, EventArgs e)
        {
            //如果什么都没选，则什么都不做
            if (!checkState.Values.Contains(true))
            {
                return;
            }
            //弹出确认对话框
            var result = MessageBox.Show(this, "确认删除选中的协议么？", "警告", MessageBoxButtons.OKCancel);
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                for (int i = dgvProtocolLib.Rows.Count - 1; i >= 0; i--)
                {
                    string protocolGuid = dgvProtocolLib.Rows[i].Cells["Guid"].Value.ToString();
                    if (checkState[protocolGuid])
                    {
                        dgvProtocolLib.Rows.RemoveAt(i);
                    }
                }
            }
        }
    }
}
