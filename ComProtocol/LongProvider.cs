using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertProvider
{
    /// <summary>
    ///  LongProvider类，转换8字节数据到Int64位有符号整数
    /// </summary>
    public class LongProvider : ProviderBase
    {
        /// <summary>
        ///  LongProvider构造函数（解析数据），输入串口数据和参数进行初始化
        /// </summary>
        /// <param name="position">Long数据开始位置</param>
        /// <param name="length">数据长度</param>
        /// <param name="data">串口数据</param>
        public LongProvider(int position, int length, byte[] data)
            : base(position, length, data)
        {
        }

        /// <summary>
        /// LongProvider构造函数，输入要发送的字符串数据，转换为要发送的字节数据
        /// </summary>
        /// <param name="dataValue">要发送的字符串数据（转换前）</param>
        public LongProvider(string dataValue)
            : base(dataValue)
        {
        }

        /// <summary>
        ///  GetData属性：转换后输出的64位有符号整数
        /// </summary>
        public long GetData { get; private set; }

        //输出要发送的字节数据
        public byte[] DataToSend { get; protected set; }

        /// <summary>
        ///  转换方法：获取转换的64位有符号整数数据
        /// </summary>
        protected override void ConvertFromSerial()
        {
            long myLong = 0;
            if (DataLength != 8)
            {
                throw new Exception("64位有符号整数型的数据长度必须为8字节");
            }
            //将指定位置开始的8字节的数据转换为64位有符号整数
            myLong = BitConverter.ToInt64(DataContent.ToArray(), DataPosition);
            GetData = myLong;
        }

        /// <summary>
        /// 转换方法：输出转换的字节数据
        /// </summary>
        protected override void ConvertToSerial(string dataValue)
        {
            long praseResult;
            
            if (Int64.TryParse(dataValue, out praseResult))
            {
                DataToSend=BitConverter.GetBytes(praseResult);
            }
            else
            {
                
            }
        }
    }
}
