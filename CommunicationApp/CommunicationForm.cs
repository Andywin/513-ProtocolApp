using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using ConvertProvider;
using System.Text.RegularExpressions;

namespace CommunicationApp
{
    public partial class CommunicationForm : Form
    {
        private SerialPort comm = new SerialPort();
        private StringBuilder myStringBuilder = new StringBuilder();//避免在事件处理方法中反复的创建，定义到外面。
        private long received_count = 0;//接收计数
        private long send_count = 0;//发送计数
        private bool Listening = false;//是否没有执行完invoke相关操作  
        private bool ClosingPort = false;//是否正在关闭串口，执行Application.DoEvents，并阻止再次invoke  
        
        public CommunicationForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PortComForm_Load(object sender, EventArgs e)
        {
            this.Text = "串口通信程序";

            //初始化下拉串口名称列表框
            string[] ports = SerialPort.GetPortNames();
            Array.Sort(ports);
            comboPortName.Items.AddRange(ports);
            comboPortName.SelectedIndex = comboPortName.Items.Count > 0 ? 0 : -1;
            comboBaudrate.SelectedIndex = comboBaudrate.Items.IndexOf("4800");
            //初始关闭发送按钮，防止误触发
            buttonSendByRawData.Enabled = false;
            buttonSendByProtocol.Enabled = false;
            
            //初始化SerialPort对象
            comm.NewLine = "\r\n"; //表示行尾的值。 默认值为换行符 (NewLine)
            comm.RtsEnable = true;//根据实际情况，如果为 true，则启用请求发送 (RTS)

            //添加事件注册
            comm.DataReceived += comm_DataReceived;

            dgvReceiveData.Columns["ProtocolDataLengthRcv"].DefaultCellStyle.BackColor = Color.LightGray;
            dgvSendData.Columns["ProtocolDataLength"].DefaultCellStyle.BackColor = Color.LightGray;
        }

        /// <summary>
        ///  添加接收数据处理程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comm_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //如果正在关闭，忽略操作，直接返回，尽快的完成串口监听线程的一次循环
            if (ClosingPort)
            {
                return;
            }
            try
            {
                Listening = true;//设置标记，说明已经开始处理数据，一会儿要使用系统UI
                int n = comm.BytesToRead;//先记录下来，避免某种原因，人为的原因，操作几次之间时间长，缓存不一致
                byte[] buf = new byte[n];//声明一个临时数组存储当前来的串口数据
                received_count += n;//增加接收计数
                comm.Read(buf, 0, n);//读取缓冲数据
        //        buffer.AddRange(buf);
                //对读取的数据进行分析
                this.AnalyseProtocolFromSerial(buf);
                //清除字符串构造器的内容
                myStringBuilder.Clear();
                //因为要访问ui资源，所以需要使用invoke方式同步ui。
                this.Invoke((EventHandler)(delegate
                {
                    //依次的拼接出16进制字符串
                    foreach (byte b in buf)
                    {
                        myStringBuilder.Append(b.ToString("X2") + " ");
                    }
                    //追加的形式添加到文本框末端，并滚动到最后。
                    this.txGet.AppendText(myStringBuilder.ToString());
                    this.txGet.AppendText("\n");
                    //修改接收计数
                    toolStripStatusDataRcv.Text = "已接收字节数：" + received_count.ToString();//更新界面
                }));

            }
            //catch (Exception ex)
            //{

            //    throw new Exception("串口接收数据异常", ex);
            //}
            finally
            {
                Listening = false;//串口接收完了，ui可以关闭串口了
            }
        }

        /// <summary>
        ///  单击打开、关闭串口按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOpenClose_Click(object sender, EventArgs e)
        {
            //根据当前串口对象，来判断操作
            if (comm.IsOpen)
            {
                ClosingPort = true;
                while (Listening)
                {
                    Application.DoEvents();
                }
                //打开时点击，则关闭串口
                comm.Close();
                ClosingPort = false;
            }

            else
            {
                //关闭时点击，则设置好端口，波特率后打开
                comm.PortName = comboPortName.Text;
                comm.BaudRate = int.Parse(comboBaudrate.Text);
                try
                {
                    comm.Open();
                }
                catch (Exception ex)
                {
                    //捕获到异常信息，创建一个新的comm对象，之前的不能用了。
                    comm = new SerialPort();
                    //现实异常信息给客户。
                    MessageBox.Show(ex.Message);
                }
            }
            //设置按钮的状态
            buttonOpenClose.Text = comm.IsOpen ? "关闭串口" : "打开串口";
            buttonSendByRawData.Enabled = comm.IsOpen;
            buttonSendByProtocol.Enabled = comm.IsOpen;
        }

        /// <summary>
        /// 点击按16进制原始数据发送按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSendByRawData_Click(object sender, EventArgs e)
        {
            //定义一个变量，记录发送了几个字节
            int sendCount = 0;
            //16进制发送
            //用正则得到有效的十六进制数
            MatchCollection mc = Regex.Matches(txSend.Text, @"(?i)[\da-f]{2}");
            List<byte> buf = new List<byte>();//填充到这个临时列表中
            //依次添加到列表中
            foreach (Match m in mc)
            {
                buf.Add(byte.Parse(m.Value, System.Globalization.NumberStyles.HexNumber));
            }
            //转换列表为数组后发送
            comm.Write(buf.ToArray(), 0, buf.Count);
            //记录发送的字节数
            sendCount = buf.Count;
            send_count += sendCount;//累加发送字节数
            toolStripStatusDataSent.Text = "已发送字节数：" + send_count.ToString();//更新界面
        }

        /// <summary>
        /// 点击按协议发送按钮，对数据进行编码发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSendByProtocol_Click(object sender, EventArgs e)
        {
            //表格如果为空，则返回
            if (dgvSendData.Rows.Count == 1)
                return;

            try
            {
                //定义一个变量，记录发送了几个字节
                int sendCount = 0;
                //设置一个List<Protocol>存储所有要发送的协议
                List<Protocol> sendProtocols = new List<Protocol>();
                //循环各行，存储各行信息到协议
                for (int i = 0; i < dgvSendData.RowCount - 1; i++)
                {
                    ///获取某行的信息
                    string ptcName = dgvSendData.Rows[i].Cells["ProtocolName"].Value.ToString();
                    //获取DataType列中的DataType信息
                    string dataTypeString = dgvSendData.Rows[i].Cells["ProtocolDataType"].Value.ToString();
                    int startingPosition = Convert.ToInt32(dgvSendData.Rows[i].Cells["StartingPosition"].Value);
                    int count = Convert.ToInt32(dgvSendData.Rows[i].Cells["ProtocolCount"].Value);
                    if (dgvSendData.Rows[i].Cells["DataContent"].Value == null)
                    {
                        throw new DataContentIsNullException("错误：数据内容不能为空，请填写数据内容");
                    }
                    string contentToSend = dgvSendData.Rows[i].Cells["DataContent"].Value.ToString();
                    //初始化发送协议
                    Protocol sendProtocol = new Protocol(ptcName, dataTypeString, startingPosition, count, contentToSend);
                    //将该行协议存入myProtocols的List中
                    sendProtocols.Add(sendProtocol);
                }

                //求byte[]最大长度
                int maxLength = sendProtocols[0].StartingPosition + sendProtocols[0].DataLength * sendProtocols[0].Count;
                for (int i = 1; i < sendProtocols.Count; i++)
                {
                    int newLength = sendProtocols[i].StartingPosition + sendProtocols[i].DataLength * sendProtocols[i].Count;
                    if (maxLength < newLength)
                    {
                        maxLength = newLength;
                    }
                }
                //将空位置零
                List<byte> buf = new List<byte>(maxLength);
                for (int i = 0; i < maxLength; i++)
                {
                    buf.Add(0);
                }
                //将每个协议中的数据存储到buf中
                foreach (var myProtocol in sendProtocols)
                {
                    //根据协议获得该条协议要发送的串口数据
                    byte[] temp = myProtocol.GetBytesToSerial();
                    for (int i = 0; i < temp.Length; i++)
                    {
                        buf[myProtocol.StartingPosition + i] = temp[i];
                    }
                }
                //向串口写数据
                comm.Write(buf.ToArray(), 0, buf.Count());
                //记录发送的字节数
                sendCount = buf.Count;
                send_count += sendCount;//累加发送字节数
                toolStripStatusDataSent.Text = "已发送字节数：" + send_count.ToString();//更新界面

            }
            catch (Exception innerException)
            {
                MessageBox.Show(innerException.Message, "警告!");
            }
        }

        /// <summary>
        /// 点击复位按钮，复位接受和发送的字节数计数器并更新界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonReset_Click(object sender, EventArgs e)
        {
            //复位接受和发送的字节数计数器并更新界面。
            send_count = received_count = 0;
            toolStripStatusDataSent.Text = "已发送字节数：0";
            toolStripStatusDataRcv.Text = "已接收字节数：0";
            txGet.Text = "";
            txSend.Text = "";
        }

        /// <summary>
        /// 关闭程序时，记得关闭串口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PortComForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //根据当前串口对象，来判断操作
            if (comm.IsOpen)
            {
                ClosingPort = true;
                while (Listening)
                {
                    Application.DoEvents();
                }
                //打开时点击，则关闭串口
                comm.Close();
                ClosingPort = false;
            }
        }
        
        #region 接收协议解析
        /// <summary>
        /// 解析协议，输出解析后的协议文本
        /// </summary>
        /// <param name="bufferData"></param>
        private void AnalyseProtocolFromSerial(byte[] bufferData)
        {
            //设置一个List<Protocol>存储所有要接收的协议
            List<Protocol> receiveProtocols = new List<Protocol>();
            
            for (int i = 0; i < dgvReceiveData.RowCount - 1; i++)
            {
                ///获取某行的信息
                string ptcName = dgvReceiveData.Rows[i].Cells["ProtocolNameRcv"].Value.ToString();
                //获取DataType列中的DataType信息
                string dataTypeString = dgvReceiveData.Rows[i].Cells["ProtocolDataTypeRcv"].Value.ToString();
                int startingPosition = Convert.ToInt32(dgvReceiveData.Rows[i].Cells["StartingPositionRcv"].Value);
                int count = Convert.ToInt32(dgvReceiveData.Rows[i].Cells["CountRcv"].Value);
                int dataLength = Convert.ToInt32(dgvReceiveData.Rows[i].Cells["ProtocolDataLengthRcv"].Value);
                //初始化接收协议
                Protocol rcvProtocol = new Protocol(ptcName, dataTypeString, startingPosition, count, dataLength);
                //将该行协议存入myProtocols的List中
                receiveProtocols.Add(rcvProtocol);
            }

            //对每一个协议，执行该协议的GetContentFromSerial(byte[] buffer)方法
            foreach (Protocol rcvProtocol in receiveProtocols)
            {
                rcvProtocol.GetContentFromSerial(bufferData);
            }

            //设置表格，显示相应的解析数据完成的结果
            //因为要访问ui资源，所以需要使用invoke方式同步ui。
            this.Invoke((EventHandler)(delegate
            {
                for (int i = 0; i < dgvReceiveData.RowCount - 1; i++)
                {
                    dgvReceiveData.Rows[i].Cells["DataContentRcv"].Value = receiveProtocols[i].Content;
                    dgvReceiveData.Rows[i].Cells["RawDataRcv"].Value = BitConverter.ToString(receiveProtocols[i].RawData);
                }
            }));
        }
        #endregion

        /// <summary>
        /// 单击发送数据Tab中的选择协议按钮，显示选择协议窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonChoosePtcSend_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            ProtocolConfigForm myProtocolConfigForm = new ProtocolConfigForm();
            //传入需要修改的dgvSendData表格
            myProtocolConfigForm.ShowDialog(this, dgvSendData);
            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// 单击接收数据Tab中的选择协议按钮，显示选择协议窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonChoosePtcReceive_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            ProtocolConfigForm myProtocolConfigForm = new ProtocolConfigForm();
            //传入需要修改的dgvReceiveData表格
            myProtocolConfigForm.ShowDialog(this, dgvReceiveData);
            this.Cursor = Cursors.Default;
            
        }

        /// <summary>
        /// 单击接收数据Tab中的清空按钮，清空dgvReceiveData表格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClearReceive_Click(object sender, EventArgs e)
        {
            dgvReceiveData.Rows.Clear();
        }

        /// <summary>
        /// 单击发送数据Tab中的清空按钮，清空dgvSendData表格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClearSend_Click(object sender, EventArgs e)
        {
            dgvSendData.Rows.Clear();
        }
   }
}
