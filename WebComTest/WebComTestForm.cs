using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace WebComTest
{

    //定义一个object参数的委托，用来调用修改Form Textbox的方法
    public delegate void ChangeTextDelegate(object obj);

    public partial class WebComTestForm : Form
    {
        //声明发送用UdpClient
        UdpClient udpClientSend;
        //声明接受用UdpClient
        UdpClient udpClientRcv;
        //定义ChangeTextDelegate委托的事件
        public event ChangeTextDelegate changeTextDlgt;

        public WebComTestForm()
        {
            //屏蔽跨线程改控件属性那个异常
            //CheckForIllegalCrossThreadCalls = false;
            //初始化
            udpClientSend = new UdpClient();
            udpClientRcv = new UdpClient(888);
            InitializeComponent();

            //开一线程,监听IP协议端口
            Thread th = new Thread(new ThreadStart(listen));
            //设置为后台
            th.IsBackground = true;
            th.Start();
        }

        private void listen()
        {
           //声明终结点
            IPEndPoint iep = null;
            while (true)
            {
                //获得Form1发送过来的数据包
                string text = System.Text.Encoding.UTF8.GetString(udpClientRcv.Receive(ref iep));
                //调用委托，另开一个线程，更新接收到的内容
                changeTextDlgt(text);
            }
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            //保存要发送的文本
            string tempToSend = textBoxDataToSend.Text;
            //将文本转化为字节数组
            byte[] dataToSend = Encoding.ASCII.GetBytes(tempToSend);
            //向指定IP地址发送数据
            IPAddress addressTosend;
            //int portToSend = Int32.Parse(textBoxPortNum.Text);
            if(IPAddress.TryParse(textBoxIPtoSend.Text,out addressTosend))
            {
                //端口默认为888
                udpClientSend.Connect(addressTosend, 888);
                udpClientSend.Send(dataToSend, dataToSend.Length);
            }
        }

        private void WebComTestForm_Load(object sender, EventArgs e)
        {
            //将方法赋予委托的实例
            changeTextDlgt = ChangeReceiveText;
        }

        /// <summary>
        /// 委托实例
        /// </summary>
        /// <param name="obj">在多线程之间传递的数据：需要更改的Text</param>
        private void ChangeReceiveText(object obj)
        {
            if (this.textBoxDataRcvd.InvokeRequired)
            {
                //由于windows窗体及其他控件不具备跨线程的能力，
                //所以这里必须调用控件的异步委托方法
                //this.textBoxDataRcvd.Invoke(new EventHandler(ControlDelegate),
                //    new object[] { obj, EventArgs.Empty });
                this.textBoxDataRcvd.Invoke((EventHandler)(delegate
                {
                    this.textBoxDataRcvd.Text = obj.ToString();
                }));
            }
        }

        /// <summary>
        /// 控件调用的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControlDelegate(object sender, EventArgs e)
        {
            this.textBoxDataRcvd.Text = sender.ToString();
        }



    }
}
