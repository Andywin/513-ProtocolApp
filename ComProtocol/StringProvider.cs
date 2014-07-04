using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertProvider
{
    /// <summary>
    ///  StringProvider类，使用ASCII编码转换指定位置开始的指定字节数目的数据到字符串
    /// </summary>
    public class StringProvider : ProviderBase
    {
        /// <summary>
        ///  StringProvider构造函数（解析数据），输入串口数据和参数进行初始化
        /// </summary>
        /// <param name="position">String数据开始位置</param>
        /// <param name="length">数据长度</param>
        /// <param name="data">串口数据</param>
        public StringProvider(int position, int length, byte[] data)
            : base(position, length, data)
        {
        }

        /// <summary>
        /// ShortProvider构造函数（发送数据），输入要发送的字符串数据，转换为要发送的字节数据
        /// </summary>
        /// <param name="dataValue">要发送的字符串数据（转换前）</param>
        public StringProvider(string dataValue)
            : base(dataValue)
        {
        }

        /// <summary>
        ///  GetData属性：使用ASCII码转换后输出的字符串
        /// </summary>
        public string GetData { get; private set; }

        //输出要发送的字节数据
        public byte[] DataToSend { get; protected set; }

        /// <summary>
        ///  转换方法：获取转换的字符串数据
        /// </summary>
        protected override void ConvertFromSerial()
        {
            //StringBuilder myString = new StringBuilder();
            string myString = "";
            // 将指定位置开始的指定字节数目的数据转换为字符串（使用ASCII编码）
            myString = Encoding.ASCII.GetString(DataContent.ToArray(), DataPosition, DataLength);
            // 将指定位置开始的指定字节数目的数据转换为字符串（直接输出16进制字符，带连字符“-”）   
            // myString = BitConverter.ToString(DataContent.ToArray(), DataPosition, DataLength);
            GetData = myString;
        }

        /// <summary>
        /// 转换方法：输出转换的字节数据
        /// </summary>
        protected override void ConvertToSerial(string dataValue)
        {
            
            if (dataValue != null)
            {
                //使用ASCII编码转换字符串到字节List
                DataToSend = Encoding.ASCII.GetBytes(dataValue);
            }
            else
            {
                
            }
        }
    }
}
