using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertProvider
{
    /// <summary>
    ///  UIntProvider类，转换4字节数据到UInt32位无符号整数
    /// </summary>
    public class UintProvider : ProviderBase
    {
        /// <summary>
        ///  UintProvider构造函数（解析数据），输入串口数据和参数进行初始化
        /// </summary>
        /// <param name="position">UInt数据开始位置</param>
        /// <param name="length">数据长度</param>
        /// <param name="data">串口数据</param>
        public UintProvider(int position, int length, byte[] data)
            : base(position, length, data)
        {
        }

        /// <summary>
        /// UintProvider构造函数，输入要发送的字符串数据，转换为要发送的字节数据
        /// </summary>
        /// <param name="dataValue">要发送的字符串数据（转换前）</param>
        public UintProvider(string dataValue)
            : base(dataValue)
        {
        }

        /// <summary>
        ///  GetData属性：转换后输出的32位无符号整数
        /// </summary>
        public uint GetData { get; private set; }

        //输出要发送的字节数据
        public byte[] DataToSend { get; protected set; }

        /// <summary>
        ///  转换方法：获取转换的32位无符号整数数据
        /// </summary>
        protected override void ConvertFromSerial()
        {
            uint myUInt = 0;
            if (DataLength != 4)
            {
                throw new Exception("32位无符号整数型的数据长度必须为4字节");
            }
            //将指定位置开始的4字节的数据转换为32位无符号整数
            myUInt = BitConverter.ToUInt32(DataContent.ToArray(), DataPosition);
            GetData = myUInt;
        }

        /// <summary>
        /// 转换方法：输出转换的字节数据
        /// </summary>
        protected override void ConvertToSerial(string dataValue)
        {
            uint praseResult;
            
            if (UInt32.TryParse(dataValue, out praseResult))
            {
                DataToSend=BitConverter.GetBytes(praseResult);
            }
            else
            {
                
            }
        }
    }
}
