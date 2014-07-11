using ConvertProvider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
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
        //声明接收用UdpClient
        private UdpClient udpClientRcv;
        //声明发送用TcpClient
        private TcpClient tcpClientSend;
        //声明接收用TcpListener和TcpClient
        private TcpListener tcpListenerRcv;
        private TcpClient clientTCP;
        //声明监听网络端口的线程
        Thread threadNet;
        //声明监听TCP协议的线程
        Thread threadTCP;
        //定义定时发送的Timer
        System.Windows.Forms.Timer sendTimerByPtc = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer sendTimerByRaw = new System.Windows.Forms.Timer();
        //定义InvokeDataRcvDelegate委托的事件
        public event InvokeDataRcvDelegate invokeDataRcv;

        //定义数据列表，存储需要保存的发送协议表格
        DataSet dataSetSend = new DataSet();
        DataTable ptcsToSave = new DataTable("Protocols");
        //定义dgvSendData的各列
        DataGridViewTextBoxColumn protocolNameColumn = new DataGridViewTextBoxColumn();
        DataGridViewComboBoxColumn protocolDataTypeColumn = new DataGridViewComboBoxColumn();
        DataGridViewTextBoxColumn protocolDataLengthColumn = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn startingPositionColumn = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn protocolCountColumn = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn protocolContentColumn = new DataGridViewTextBoxColumn();

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
            
            //设置Timer的计时时间，绑定事件
            sendTimerByPtc.Interval = 1000;//设置发送间隔为1000ms（1s）
            sendTimerByPtc.Tick += buttonSendByProtocol_Click;//将定时器绑定到按协议发送的方法
            sendTimerByRaw.Interval = 1000;//设置发送间隔为1000ms（1s）
            sendTimerByRaw.Tick += buttonSendByRawData_Click;//将定时器绑定到按原始数据发送的方法
            
            //初始化发送协议的DataTable
            DataColumn[] sendDataColumns = new DataColumn[]{
            new DataColumn("ProtocolName"),
            new DataColumn("ProtocolDataType"),
            new DataColumn("ProtocolDataLength"),
            new DataColumn("StartingPosition"),
            new DataColumn("ProtocolCount"),
            new DataColumn("ProtocolContent")
            };
            ptcsToSave.Columns.AddRange(sendDataColumns);//列加入到DataTable中
            dataSetSend.Tables.Add(ptcsToSave);//DataTable加入到DataSet中

            //将DataSet绑定到发送数据的表格
            BindingDataTableSend();
          //  dgvSendData.Columns["ProtocolDataLength"].DefaultCellStyle.BackColor = Color.LightGray;
        }

        /// <summary>
        /// 绑定发送数据表格
        /// </summary>
        private void BindingDataTableSend()
        {            
            //将各列添加到dgvSendData中
            dgvSendData.Columns.AddRange(new DataGridViewColumn[]{
                protocolNameColumn,
                protocolDataTypeColumn,
                protocolDataLengthColumn,
                startingPositionColumn,
                protocolCountColumn,
                protocolContentColumn
            });

            dgvSendData.AutoGenerateColumns = false;//设置DataGridView不自动生成列
            dgvSendData.DataSource = dataSetSend;//绑定DataGridView到DataSet
            dgvSendData.DataMember = ptcsToSave.TableName;//使DataMember定为ptcsToSave

            //使用DataPropertyName将DataGridView的各列绑定到DataTable中各列
            protocolNameColumn.DataPropertyName = "ProtocolName";
            protocolDataTypeColumn.DataPropertyName = "ProtocolDataType";
            protocolDataLengthColumn.DataPropertyName = "ProtocolDataLength";
            startingPositionColumn.DataPropertyName = "StartingPosition";
            protocolCountColumn.DataPropertyName = "ProtocolCount";
            protocolContentColumn.DataPropertyName = "ProtocolContent";
            //设置各列HeaderText属性
            protocolNameColumn.HeaderText = "协议名";
            protocolDataTypeColumn.HeaderText = "数据类型";
            protocolDataLengthColumn.HeaderText = "数据长度";
            startingPositionColumn.HeaderText = "起始位置    ";
            protocolCountColumn.HeaderText = "个数    ";
            protocolContentColumn.HeaderText = "数据内容  ";
            //设置各列Name属性
            protocolNameColumn.Name = "ProtocolName";
            protocolDataTypeColumn.Name = "ProtocolDataType";
            protocolDataLengthColumn.Name = "ProtocolDataLength";
            startingPositionColumn.Name = "StartingPosition";
            protocolCountColumn.Name = "ProtocolCount";
            protocolContentColumn.Name = "ProtocolContent";
            //填充下拉列表框
            protocolDataTypeColumn.Items.AddRange("Boolean", "Short", "Ushort", "Int", "Uint", "Long", "Ulong", "Float", "Double", "Char", "String");

            dgvSendData.Columns[2].ReadOnly = true;
            dgvSendData.Columns[2].DefaultCellStyle.BackColor = Color.LightGray;
            dgvSendData.AllowUserToResizeColumns = false;//阻止用户手动调整列宽
            dgvSendData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //设置禁止点击列标题进行重排
            for (int i = 0; i < dgvSendData.Columns.Count; i++)
            {
                dgvSendData.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
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
        private void buttonBySerialPort_Click(object sender, EventArgs e)
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
            buttonBySerialPort.Text = comm.IsOpen ? "关闭串口收发" : "开启串口收发";
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
            myProtocolConfigForm.ShowDialog(this, dataSetSend);
            //重新绑定数据列:先清空，再绑定
            dgvSendData.DataSource = null;
            dgvSendData.Columns.Clear();
            BindingDataTableSend();
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
            //清空绑定数据列
            dgvSendData.DataSource = null;
            dgvSendData.Rows.Clear();
            dataSetSend.Tables["Protocols"].Rows.Clear();
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
                buttonBySerialPort.Enabled = false;
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
                buttonBySerialPort.Enabled = true;
                //终止接收网络数据线程
                threadNet.Abort();
                if (threadTCP != null)
                {
                    threadTCP.Abort();
                }
                if (udpClientRcv != null)
                {
                    //关闭udp协议监听端口
                    udpClientRcv.Close();                 
                }
                if (tcpListenerRcv != null)
                {
                    //关闭TCP协议监听端口
                    tcpListenerRcv.Stop();                    
                }
            }
        }

        /// <summary>
        /// 监听端口发来的信息
        /// </summary>
        private void ListenNet()
        {
            int portNum;//设置端口号，如果是无法解析出正确端口号，则返回
            if (!Int32.TryParse(textBoxPortNum.Text, out portNum))
            {
                return;
            }
            if (radioButtonUdp.Checked)//选中Udp协议
            {
                //声明终结点和端口号
                IPEndPoint iep = null;
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
            else if (radioButtonTCP.Checked)//选中TCP/IP协议
            {
                //IPAddress localIP = Dns.GetHostAddresses(Dns.GetHostName())[0];
                threadTCP = new Thread(new ThreadStart(ListenTCP));
                threadTCP.IsBackground = true;
                threadTCP.Start();
            }
        }

        /// <summary>
        /// 侦听TCP的线程方法
        /// </summary>
        private void ListenTCP()
        {
            int portNum;//设置端口号
            Int32.TryParse(textBoxPortNum.Text, out portNum);
            //定义TcpListener,开启侦听网络端口
            tcpListenerRcv = new TcpListener(IPAddress.Any, portNum);
            tcpListenerRcv.Start();
            byte[] buf;
            while (true)
            {
                if (tcpListenerRcv.Pending())
                {
                    //开启阻塞式接收TCP连接，接收到一个TcpClient对象
                    clientTCP = tcpListenerRcv.AcceptTcpClient();
                    //获得用于读写的NetworkStream对象
                    NetworkStream netStream = clientTCP.GetStream();
                    //使用一个StreamReader读取stream对象中的数据
                    StreamReader sr = new StreamReader(netStream);
                    string stringBuf = sr.ReadToEnd();//以字符串形式读取netStream流中的内容
                    //转存为byte[] buf，以ASCII格式解码（因为发送的时候StreamWriter也是用ASCII编码的）
                    buf = Encoding.ASCII.GetBytes(stringBuf);
                    sr.Close();//关闭StreamReader流
                    clientTCP.Close();
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
            //存储IP地址信息和端口号
            IPAddress addressTosend;
            int portNum;
            if (!(IPAddress.TryParse(textBoxIpAddress.Text, out addressTosend) && Int32.TryParse(textBoxPortNum.Text, out portNum)))
            {
                return;//如果是无法解析出正确端口号和IP地址，则返回
            }
            if (radioButtonUdp.Checked)//选中Udp协议
            {
                //初始化发送用UdpClient
                udpClientSend = new UdpClient();
                //连接到相应端口，向指定IP地址发送数据
                udpClientSend.Connect(addressTosend, portNum);
                udpClientSend.Send(buf.ToArray(), buf.Count);
                udpClientSend.Close();
            }
            else if (radioButtonTCP.Checked)//选中TCP/IP协议
            {
                //初始化发送用tcpClientSend
                tcpClientSend = new TcpClient();
                //连接到相应端口，向指定IP地址发送数据
                tcpClientSend.Connect(addressTosend, portNum);
                //获得用于发送的NetworkStream对象
                NetworkStream netStream = tcpClientSend.GetStream();
                //使用一个StreamWriter存储要写入stream对象中的数据
                StreamWriter sw = new StreamWriter(netStream, Encoding.ASCII);
                //以ASCII格式编码，存储到要发送的String流中
                string stringBuf = Encoding.ASCII.GetString(buf.ToArray());
                //以字符串形式写入netStream流中的内容
                sw.Write(stringBuf);
                sw.Flush();
                sw.Close();//关闭StreamWriter流
                tcpClientSend.Close();//关闭tcpClient              
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

        /// <summary>
        /// 点击按协议发送框中的定时发送按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonTimingSendByPtc_Click(object sender, EventArgs e)
        {
            //如果按原始数据发送的定时器在启动状态，则停止该计时器
            if (sendTimerByRaw.Enabled)
            {
                sendTimerByRaw.Enabled = false;
                buttonTimingSendByRaw.Text = "开启定时发送";
            }
            if (buttonTimingSendByPtc.Text == "开启定时发送")
            {
                buttonTimingSendByPtc.Text = "关闭定时发送";
                sendTimerByPtc.Start();
            }
            else
            {
                buttonTimingSendByPtc.Text = "开启定时发送";
                sendTimerByPtc.Stop();
            }
        }

        /// <summary>
        /// 点击按原始数据发送框中的定时发送按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonTimingSendByRaw_Click(object sender, EventArgs e)
        {
            //如果按协议发送的定时器在启动状态，则停止该计时器
            if (sendTimerByPtc.Enabled)
            {
                sendTimerByPtc.Enabled = false;
                buttonTimingSendByPtc.Text = "开启定时发送";
            }
            if (buttonTimingSendByRaw.Text == "开启定时发送")
            {
                buttonTimingSendByRaw.Text = "关闭定时发送";
                sendTimerByRaw.Start();
            }
            else
            {
                buttonTimingSendByRaw.Text = "开启定时发送";
                sendTimerByRaw.Stop();
            }
        }

        /// <summary>
        /// 单击保存协议按钮，对表格中的协议进行保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSavePtc_Click(object sender, EventArgs e)
        {
            //初始化保存对话框
            SaveFileDialog saveDialog = new SaveFileDialog();
            //设置保存对话框的各种属性
            saveDialog.OverwritePrompt = true;
            saveDialog.FileName = "myProtocol";
            saveDialog.DefaultExt = "xml";
            saveDialog.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
            saveDialog.FilterIndex = 1;
            saveDialog.InitialDirectory = @"D:\";
            saveDialog.RestoreDirectory = true;

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                dataSetSend.WriteXml(saveDialog.FileName);
            }
            
        }

        /// <summary>
        /// 单击加载协议按钮，从对话框中加载XML格式的协议并显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLoadPtc_Click(object sender, EventArgs e)
        {
            //初始化打开对话框，设置各种属性
            OpenFileDialog loadDialog = new OpenFileDialog();
            loadDialog.DefaultExt = "xml";
            loadDialog.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
            loadDialog.FilterIndex = 1;
            loadDialog.InitialDirectory = @"D:\";
            loadDialog.RestoreDirectory = true;

            if (loadDialog.ShowDialog() == DialogResult.OK)
            {
                dataSetSend.ReadXml(loadDialog.FileName);
                //重新绑定数据列:先清空，再绑定
                dgvSendData.DataSource = null;
                dgvSendData.Columns.Clear();
                BindingDataTableSend();
            }
        }


   }
}
