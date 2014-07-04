using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertProvider
{
    /// <summary>
    /// DoubleProvider类，转换8字节数据到双精度浮点型数据
    /// </summary>
    public class DoubleProvider : ProviderBase
    {
        /// <summary>
        ///  DoubleProvider构造函数（解析数据），输入串口数据和参数进行初始化
        /// </summary>
        /// <param name="position">double数据开始位置</param>
        /// <param name="length">数据长度</param>
        /// <param name="data">串口数据</param>
        public DoubleProvider(int position, int length, byte[] data)
            : base(position, length, data)
        {
        }

        /// <summary>
        /// DoubleProvider构造函数，输入要发送的字符串数据，转换为要发送的字节数据
        /// </summary>
        /// <param name="dataValue">要发送的字符串数据（转换前）</param>
        public DoubleProvider(string dataValue)
            : base(dataValue)
        {
        }

        //输出要发送的字节数据
        public byte[] DataToSend { get; protected set; }

        /// <summary>
        ///  GetData属性：转换后输出的双精度浮点数
        /// </summary>
        public double GetData { get; private set; }

        /// <summary>
        ///  转换方法：获取转换的双精度浮点型数据
        /// </summary>
        protected override void ConvertFromSerial()
        {
            double myDouble = 0d;
            if (DataLength != 8)
            {
                throw new Exception("双精度浮点型的数据长度必须为8字节");
            }
            //将指定位置开始的8字节的数据转换为双精度浮点数
            myDouble = BitConverter.ToDouble(DataContent.ToArray(), DataPosition);
            GetData = myDouble;
        }

        /// <summary>
        /// 转换方法：输出转换的字节数据
        /// </summary>
        protected override void ConvertToSerial(string dataValue)
        {
            double praseResult;
            
            if (Double.TryParse(dataValue, out praseResult))
            {
                DataToSend=BitConverter.GetBytes(praseResult);
            }
            else
            {
                
            }
        }

    }
}
