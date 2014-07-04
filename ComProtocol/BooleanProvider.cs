using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertProvider
{
    /// <summary>
    ///  BooleanProvider类，转换1字节数据到布尔值
    /// </summary>
    public class BooleanProvider : ProviderBase
    {
        /// <summary>
        ///  BoolProvider构造函数（解析数据），输入串口数据和参数进行初始化
        /// </summary>
        /// <param name="position">Boolean数据开始位置</param>
        /// <param name="length">数据长度</param>
        /// <param name="data">串口数据</param>
        public BooleanProvider(int position, int length, byte[] data)
            : base(position, length, data)
        {
        }

        /// <summary>
        /// BoolProvider构造函数，输入要发送的字符串数据，转换为要发送的字节数据
        /// </summary>
        /// <param name="dataValue">要发送的字符串数据（转换前）</param>
        public BooleanProvider(string dataValue)
            : base(dataValue)
        {
        }

        /// <summary>
        ///  GetData属性：转换后输出的字符
        /// </summary>
        public bool GetData { get; private set; }

        //输出要发送的字节数据
        public byte[] DataToSend { get; protected set; }

        /// <summary>
        ///  转换方法：获取转换的布尔数据
        /// </summary>
        protected override void ConvertFromSerial()
        {
            bool myBool = false;
            if (DataLength != 1)
            {
                throw new Exception("布尔型的数据长度必须为1字节");
            }
            //将指定位置开始的1字节的数据转换为布尔值
            myBool = BitConverter.ToBoolean(DataContent.ToArray(), DataPosition);
            GetData = myBool;
        }

        /// <summary>
        /// 转换方法：输出转换的字节数据
        /// </summary>
        protected override void ConvertToSerial(string dataValue)
        {
            bool praseResult;
            
            if (Boolean.TryParse(dataValue, out praseResult))
            {
                DataToSend=BitConverter.GetBytes(praseResult);
            }
            else
            {
                
            }
        }
    }
}
