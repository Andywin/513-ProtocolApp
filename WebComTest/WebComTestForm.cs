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
    public partial class WebComTestForm : Form
    {
        //声明发送用UdpClient
        UdpClient udpClientSend = null;
        //声明接受用UdpClient
        UdpClient udpClientRcv = null;

        public WebComTestForm()
        {
            //屏蔽跨线程改控件属性那个异常
            CheckForIllegalCrossThreadCalls = false;
            //初始化
            udpClientSend = new UdpClient();
            udpClientRcv = new UdpClient(888);
            InitializeComponent();

            //开一线程
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
                //更新接收到的内容
                this.textBoxDataRcvd.Text = text;
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



    }
}
