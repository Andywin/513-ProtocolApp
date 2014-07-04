using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertProvider
{
    /// <summary>
    ///  FloatProvider类，转换4字节数据到单精度浮点型数据
    /// </summary>
    public class FloatProvider : ProviderBase
    {

        /// <summary>
        ///  FloatProvider构造函数（解析数据），输入串口数据和参数进行初始化
        /// </summary>
        /// <param name="position">Float数据开始位置</param>
        /// <param name="length">数据长度</param>
        /// <param name="data">串口数据</param>
        public FloatProvider(int position, int length, byte[] data)
            : base(position, length, data)
        {
        }
                
        /// <summary>
        /// FloatProvider构造函数，输入要发送的字符串数据，转换为要发送的字节数据
        /// </summary>
        /// <param name="dataValue">要发送的字符串数据（转换前）</param>
        public FloatProvider(string dataValue)
            : base(dataValue)
        {
        }

        /// <summary>
        ///  GetData属性：转换后输出的浮点数
        /// </summary>
        public float GetData { get; private set; }

        //输出要发送的字节数据
        public byte[] DataToSend { get; protected set; }

        /// <summary>
        ///  转换方法：获取转换的浮点型数据
        /// </summary>
        protected override void ConvertFromSerial()
        {
            float myFloat = 0f;
            if (DataLength != 4)
            {
                throw new Exception("浮点型的数据长度必须为4字节");
            }
            //将指定位置开始的4字节的数据转换为浮点数
            myFloat = BitConverter.ToSingle(DataContent.ToArray(), DataPosition);
            GetData = myFloat;
        }

        /// <summary>
        /// 转换方法：输出转换的字节数据
        /// </summary>
        protected override void ConvertToSerial(string dataValue)
        {
            float praseResult;
            
            if (Single.TryParse(dataValue, out praseResult))
            {
                DataToSend=BitConverter.GetBytes(praseResult);
            }
            else
            {
                
            }
        }
    }
}
