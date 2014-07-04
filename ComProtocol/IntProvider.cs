using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertProvider
{
    /// <summary>
    /// IntProvider类，转换4字节数据到Int32位有符号整数
    /// </summary>
    public class IntProvider : ProviderBase
    {
        /// <summary>
        ///  IntProvider构造函数（解析数据），输入串口数据和参数进行初始化
        /// </summary>
        /// <param name="position">Int数据开始位置</param>
        /// <param name="length">数据长度</param>
        /// <param name="data">串口数据</param>
        public IntProvider(int position, int length, byte[] data)
            : base(position, length, data)
        {
        }

        /// <summary>
        /// IntProvider构造函数，输入要发送的字符串数据，转换为要发送的字节数据
        /// </summary>
        /// <param name="dataValue">要发送的字符串数据（转换前）</param>
        public IntProvider(string dataValue)
            : base(dataValue)
        {
        }

        /// <summary>
        ///  GetData属性：转换后输出的32位有符号整数
        /// </summary>
        public int GetData { get; private set; }

        //输出要发送的字节数据
        public byte[] DataToSend { get; protected set; }

        /// <summary>
        ///  转换方法：获取转换的32位有符号整数数据
        /// </summary>
        protected override void ConvertFromSerial()
        {
            int myInt = 0;
            if (DataLength != 4)
            {
                throw new Exception("32位有符号整数型的数据长度必须为4字节");
            }
            //将指定位置开始的4字节的数据转换为32位有符号整数
            myInt = BitConverter.ToInt32(DataContent.ToArray(), DataPosition);
            GetData = myInt;
        }

        /// <summary>
        /// 转换方法：输出转换的字节数据
        /// </summary>
        protected override void ConvertToSerial(string dataValue)
        {
            int praseResult;
            
            if (Int32.TryParse(dataValue, out praseResult))
            {
                DataToSend=BitConverter.GetBytes(praseResult);
            }
            else
            {
                
            }
        }
    }
}
