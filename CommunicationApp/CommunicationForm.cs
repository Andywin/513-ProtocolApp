﻿using ConvertProvider;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace CommunicationApp
{
    //定义一个byte[]参数的委托，用来调用接收数据、数据分析以及显示的方法
    public delegate void InvokeDataRcvDelegate(byte[] buf);
    
    public partial class CommunicationForm : Form
    {
        private SerialPort comm = new SerialPort();
        private StringBuilder myStringBuilder = new StringBuilder();//避免在事件处理方法中反复的创建，定义到外面。
        private long receivedCount = 0;//接收计数
        private long sentCount = 0;//发送计数
        private bool listening = false;//是否没有执行完invoke相关操作  
        private bool closingPort = false;//是否正在关闭串口，执行Application.DoEvents，并阻止再次invoke  
        private bool isByNet = false;//是否是开启了网络发送或接收
        //声明发送用UdpClient
        private UdpClient udpClientSend;
        //声明接受用UdpClient
        private UdpClient udpClientRcv;
        //声明监听网络端口的线程
        Thread threadNet;
        //定义InvokeDataRcvDelegate委托的事件
        public event InvokeDataRcvDelegate invokeDataRcv;

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
            this.Text = "通信程序";

            //初始化下拉串口名称列表框和波特率默认选项
            string[] ports = SerialPort.GetPortNames();
            Array.Sort(ports);
            comboPortName.Items.AddRange(ports);
            comboPortName.SelectedIndex = comboPortName.Items.Count > 0 ? 0 : -1;
            comboBaudrate.SelectedIndex = comboBaudrate.Items.IndexOf("4800");

            //初始化网络通信textBox默认值
            textBoxIpAddress.Text = "127.0.0.1";
            textBoxPortNum.Text = "127";

            //初始化SerialPort对象
            comm.NewLine = "\r\n"; //表示行尾的值。 默认值为换行符 (NewLine)
            comm.RtsEnable = true;//根据实际情况，如果为 true，则启用请求发送 (RTS)

            //添加事件注册
            comm.DataReceived += SerialDataReceived;
            invokeDataRcv += ReceiveDataMethod;

            //设置数据长度列颜色为灰色
            dgvReceiveData.Columns["ProtocolDataLengthRcv"].DefaultCellStyle.BackColor = Color.LightGray;
            dgvSendData.Columns["ProtocolDataLength"].DefaultCellStyle.BackColor = Color.LightGray;
        }

        /// <summary>
        ///  添加串口接收数据处理程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SerialDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //如果正在关闭，忽略操作，直接返回，尽快的完成串口监听线程的一次循环
            if (closingPort)
            {
                return;
            }
            try
            {
                listening = true;//设置标记，说明已经开始处理数据，一会儿要使用系统UI
                int n = comm.BytesToRead;//先记录下来，避免某种原因，人为的原因，操作几次之间时间长，缓存不一致
                byte[] buf = new byte[n];//声明一个临时数组存储当前来的串口数据
                comm.Read(buf, 0, n);//读取缓冲数据

                //调用数据处理的事件委托，对读取的数据进行分析并显示
                invokeDataRcv(buf);
            }
            //catch (Exception ex)
            //{
            //    throw new Exception("串口接收数据异常", ex);
            //}
            finally
            {
                listening = false;//串口接收完了，ui可以关闭串口了
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
                closingPort = true;
                while (listening)
                {
                    Application.DoEvents();
                }
                //打开时点击，则关闭串口
                comm.Close();
                closingPort = false;
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
            buttonOpenClose.Text = comm.IsOpen ? "关闭串口收发" : "开启串口收发";
            buttonByNet.Enabled = !comm.IsOpen;//串口打开则disable网络收发
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
            if (comm.IsOpen) //通过串口发送
            {
                //转换列表为数组后发送
                comm.Write(buf.ToArray(), 0, buf.Count);
            }
            else if (isByNet)//通过网络发送
            {
                this.SendByNet(buf);
            }
            else //网口串口都未打开，则什么都不做
            {
                return;
            }
            //记录发送的字节数
                sendCount = buf.Count;
                sentCount += sendCount;//累加发送字节数
                toolStripStatusDataSent.Text = "已发送字节数：" + sentCount.ToString();//更新界面
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
                if (comm.IsOpen)
                {
                    //通过串口发送
                    comm.Write(buf.ToArray(), 0, buf.Count());
                }
                else if (isByNet)
                {
                    //通过网口发送
                    this.SendByNet(buf);
                }
                else //网口串口都未打开，则什么都不做
                {
                    return;
                }
                //记录发送的字节数
                sendCount = buf.Count;
                sentCount += sendCount;//累加发送字节数
                toolStripStatusDataSent.Text = "已发送字节数：" + sentCount.ToString();//更新界面

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
            sentCount = receivedCount = 0;
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
                closingPort = true;
                while (listening)
                {
                    Application.DoEvents();
                }
                //打开时点击，则关闭串口
                comm.Close();
                closingPort = false;
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

        #region 网络收发数据的代码
        /// <summary>
        /// 点击开启网络发送按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonByNet_Click(object sender, EventArgs e)
        {
            if (!isByNet)
            {
                isByNet = true;
                buttonByNet.Text = "关闭网络收发";
                buttonOpenClose.Enabled = false;
                //开一线程,监听网络端口
                threadNet = new Thread(new ThreadStart(ListenNet));
                //设置为后台
                threadNet.IsBackground = true;
                threadNet.Start();
            }
            else
            {
                isByNet = false;
                buttonByNet.Text = "开启网络收发";
                buttonOpenClose.Enabled = true;
                //终端接收网络数据线程
                threadNet.Abort();
                //关闭udp协议监听端口
                udpClientRcv.Close();
                //udpClientSend.Close();
            }
        }

        /// <summary>
        /// 监听端口发来的信息
        /// </summary>
        private void ListenNet()
        {
            //声明终结点和端口号
            IPEndPoint iep = null;
            int portNum;
            if (Int32.TryParse(textBoxPortNum.Text, out portNum))
            {
                //初始化接收用UdpClient
                udpClientRcv = new UdpClient(portNum);
                while (true)
                {
                    //获得网络发送过来的数据包
                    byte[] buf = udpClientRcv.Receive(ref iep);
                    //调用数据处理的事件委托，对读取的数据进行分析并显示
                    invokeDataRcv(buf);
                }
            }
        }

        /// <summary>
        /// 通过网络发送
        /// </summary>
        /// <param name="buf"></param>
        private void SendByNet(List<byte> buf)
        {
            //初始化发送用UdpClient
            udpClientSend = new UdpClient();
            //存储IP地址信息和端口号
            IPAddress addressTosend;
            int portNum;
            //向指定IP地址发送数据
            if (IPAddress.TryParse(textBoxIpAddress.Text, out addressTosend) && Int32.TryParse(textBoxPortNum.Text, out portNum))
            {
                //端口
                udpClientSend.Connect(addressTosend, portNum);
                udpClientSend.Send(buf.ToArray(), buf.Count);
                udpClientSend.Close();
            }
        } 
        #endregion

        /// <summary>
        /// 接收数据并进行处理的方法
        /// </summary>
        /// <param name="buf"></param>
        private void ReceiveDataMethod(byte[] buf)
        {
            receivedCount += buf.Length;//增加接收计数
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
                toolStripStatusDataRcv.Text = "已接收字节数：" + receivedCount.ToString();//更新界面
            }));
        }

   }
}
