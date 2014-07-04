using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertProvider
{
    /// <summary>
    /// 提供数据转换基类(抽象类），供继承
    /// </summary>
    public abstract class ProviderBase
    {
        // 设置属性，存储数据位置、长度、数据内容
        public int DataPosition { get; private set; }
        public int DataLength { get; private set; }
        public byte[] DataContent { get; private set; }

        /// <summary>
        ///  基类构造函数，初始化数据位置、长度、数据内容等各种属性
        /// </summary>
        /// <param name="position">数据位置</param>
        /// <param name="length">数据长度</param>
        /// <param name="data">数据内容</param>
        public ProviderBase(int position, int length, byte[] data)
        {
            DataPosition = position;
            DataLength = length;
            DataContent = data;
            ConvertFromSerial();
        }

        /// <summary>
        /// 基类构造函数（重载），输入需要转换的字符串，转换为List(byte)
        /// </summary>
        /// <param name="dataToByte"></param>
        public ProviderBase(string dataToByte)
        {
            ConvertToSerial(dataToByte);
        }

        /// <summary>
        ///  定义抽象方法ConvertFromSerial()，从串口获得数据转换为相应的数据类型
        ///  继承的类必须实现此方法
        /// </summary>
        protected abstract void ConvertFromSerial();

        /// <summary>
        /// 定义抽象方法ConvertToSerial，从相应的数据类型转换为串口供发送的数据
        /// 继承的类必须实现此方法
        /// </summary>
        /// <param name="dataValue">数据内容</param>
        protected abstract void ConvertToSerial(string dataValue);

    }
}
