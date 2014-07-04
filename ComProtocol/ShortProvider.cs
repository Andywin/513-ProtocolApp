using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertProvider
{
    /// <summary>
    ///  ShortProvider类，转换2字节数据到Int16位无符号整数
    /// </summary>
    public class ShortProvider : ProviderBase
    {
        /// <summary>
        ///  ShortProvider构造函数（解析数据），输入串口数据和参数进行初始化
        /// </summary>
        /// <param name="position">Short数据开始位置</param>
        /// <param name="length">数据长度</param>
        /// <param name="data">串口数据</param>
        public ShortProvider(int position, int length, byte[] data)
            : base(position, length, data)
        {
        }

        /// <summary>
        /// ShortProvider构造函数，输入要发送的字符串数据，转换为要发送的字节数据
        /// </summary>
        /// <param name="dataValue">要发送的字符串数据（转换前）</param>
        public ShortProvider(string dataValue)
            : base(dataValue)
        {
        }

        /// <summary>
        ///  GetData属性：转换后输出的16位有符号整数
        /// </summary>
        public short GetData { get; private set; }

        //输出要发送的字节数据
        public byte[] DataToSend { get; protected set; }

        /// <summary>
        ///  转换方法：获取转换的16位有符号整数数据
        /// </summary>
        protected override void ConvertFromSerial()
        {
            short myShort = 0;
            if (DataLength != 2)
            {
                throw new Exception("16位有符号整数型的数据长度必须为2字节");
            }
            //将指定位置开始的2字节的数据转换为16位有符号整数
            myShort = BitConverter.ToInt16(DataContent.ToArray(), DataPosition);
            GetData = myShort;
        }

        /// <summary>
        /// 转换方法：输出转换的字节数据
        /// </summary>
        protected override void ConvertToSerial(string dataValue)
        {
            short praseResult;
            
            if (Int16.TryParse(dataValue, out praseResult))
            {
                DataToSend=BitConverter.GetBytes(praseResult);
            }
            else
            {
                
            }
        }
    }
}
