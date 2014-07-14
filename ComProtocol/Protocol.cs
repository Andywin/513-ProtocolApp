using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace ConvertProvider
{
    /// <summary>
    /// 定义协议
    /// </summary>
    public class Protocol
    {
        /// <summary>
        /// 设置协议的各项属性
        /// </summary>
        public DataType DataType { get; private set; }//数据类型
        public string Name { get; private set; }//协议名称
        public int StartingPosition { get; private set; }//起始位
        public int Count { get; private set; }//数据个数
        public string Content { get; private set; }//存储为DataType格式的数据内容
        public int DataLength { get; private set; }//单个数据长度
        public byte[] RawData { get; private set; }//单个数据的原始数据
        public bool IsLittleEndian { get; private set; }//确定数据是否是以LittleEndian形式进行存储的
        //私有变量
        private byte[] bufferData;//全部原始数据内容
        private List<byte> rawDatas = new List<byte>();//此处为协议占用的原始数据内容
        private bool toSerialFlag;//设置标志：向串口发送数据为TRUE，接收串口数据为FALSE
        
        /// <summary>
        /// 发送协议的构造函数，根据各项属性初始化该数据协议
        /// </summary>
        /// <param name="ptcName">协议名</param>
        /// <param name="dataTypeString">数据类型</param>
        /// <param name="startingPosition">起始位</param>
        /// <param name="count">个数</param>
        /// <param name="contentToSend">要发送的内容</param>
        /// <param name="littleEndian">确定数据的存储形式：是否是低字节在前</param>
        public Protocol(string ptcName, string dataTypeString, int startingPosition, int count, string contentToSend, bool littleEndian)
        {
            this.Name = ptcName;
            this.StartingPosition = startingPosition;
            this.Count = count;
            this.Content = contentToSend;
            this.IsLittleEndian = littleEndian;
            this.SetDataType(dataTypeString);
            if (this.DataType == DataType.String)
            {
                this.DataLength = this.Content.Length;
            }
        }

        /// <summary>
        /// 接收协议的构造函数，根据各项属性初始化该数据协议
        /// </summary>
        /// <param name="ptcName">协议名</param>
        /// <param name="dataTypeString">数据类型</param>
        /// <param name="startingPosition">起始位</param>
        /// <param name="count">个数</param>
        /// <param name="dataLength">字符串数据长度</param>
        /// <param name="littleEndian">确定数据的存储形式：是否是低字节在前</param>
        public Protocol(string ptcName, string dataTypeString, int startingPosition, int count, int dataLength, bool littleEndian)
        {
            this.Name = ptcName;
            this.StartingPosition = startingPosition;
            this.Count = count;
            this.IsLittleEndian = littleEndian;
            this.SetDataType(dataTypeString);
            if (this.DataType == DataType.String)
            {
                this.DataLength = dataLength;
            }
        }

        /// <summary>
        /// 设置协议数据类型的方法
        /// </summary>
        /// <param name="dataType"></param>
        public void SetDataType(string dataTypeString)
        {
            this.DataType = (DataType)Enum.Parse(typeof(DataType), dataTypeString);
            switch (this.DataType)
            {
                case DataType.Boolean:
                    this.DataLength = 1;
                    break;
                case DataType.Short:
                    this.DataLength = 2;
                    break;
                case DataType.Ushort:
                    this.DataLength = 2;
                    break;
                case DataType.Int:
                    this.DataLength = 4;
                    break;
                case DataType.Uint:
                    this.DataLength = 4;
                    break;
                case DataType.Long:
                    this.DataLength = 8;
                    break;
                case DataType.Ulong:
                    this.DataLength = 8;
                    break;
                case DataType.Float:
                    this.DataLength = 4;
                    break;
                case DataType.Double:
                    this.DataLength = 8;
                    break;
                case DataType.Char:
                    this.DataLength = 2;
                    break;
                case DataType.String:
                    this.Count = 1;
                    break;
                default:
                    this.DataLength = 0;
                    break;
            }
        }

        /// <summary>
        /// 根据数据类型，分析数据，得到解析后的数据结果
        /// </summary>
        private void DataAnalysis()
        {
            switch (this.DataType)
            {
                case DataType.Boolean:
                    AnalyseBoolean();
                    break;
                case DataType.Short:
                    AnalyseShort();
                    break;
                case DataType.Ushort:
                    AnalyseUshort();
                    break;
                case DataType.Int:
                    AnalyseInt();
                    break;
                case DataType.Uint:
                    AnalyseUint();
                    break;
                case DataType.Long:
                    AnalyseLong();
                    break;
                case DataType.Ulong:
                    AnalyseUlong();
                    break;
                case DataType.Float:
                    AnalyseFloat();
                    break;
                case DataType.Double:
                    AnalyseDouble();
                    break;
                case DataType.Char:
                    AnalyseChar();
                    break;
                case DataType.String:
                    AnalyseString();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 接收：根据原始数据和该协议解析数据
        /// </summary>
        public void GetContentFromSerial(byte[] buffer)
        {
            //存储串口数据
            this.bufferData = buffer;
            //置发送数据标志为false
            this.toSerialFlag = false;
            Content = string.Empty;
            //从BufferData数组中复制该原始数据到RawData数组
            RawData = new byte[DataLength * Count];
            Array.Copy(bufferData, StartingPosition, RawData, 0, DataLength * Count);
            //分析数据
            DataAnalysis();
        }

        /// <summary>
        /// 发送：根据数据内容，转换为16进制串口byte[]数据进行发送
        /// </summary>
        public byte[] GetBytesToSerial()
        {
            //置发送数据标志为true
            this.toSerialFlag = true;
            //分析数据
            DataAnalysis();
            return rawDatas.ToArray();
        }

        /// <summary>
        /// 对接收协议的LittleEndian属性进行判断，如果与本机属性不一致，则进行转换
        /// </summary>
        /// <param name="position">存储单个数据的起始位</param>
        private void RcvCheckLittleEndian(int position)
        {
            if (this.IsLittleEndian != BitConverter.IsLittleEndian)
            {
                //对以position开始的DataLength长度的bufferData数据逆序排列
                Array.Reverse(bufferData, position, DataLength);
            }
        }

        /// <summary>
        /// 对发送协议的LittleEndian属性进行判断，如果与本机属性不一致，则进行转换
        /// </summary>
        /// <param name="temp">需要转换的原始数据</param>
        /// <returns>转换后的数据结果</returns>
        private byte[] SndCheckLittleEndian(byte[] temp)
        {
            if (this.IsLittleEndian != BitConverter.IsLittleEndian)
            {
                Array.Reverse(temp);
            }
            return temp;
        }

        #region 从串口解析各类数据的实现代码
        /// <summary>
        /// Boolean数据的解析实现
        /// </summary>
        private void AnalyseBoolean()
        {
            if (!toSerialFlag)//接收：从串口数据解析为DataType类型数据
            {
                //设置中间变量position，存储单个数据的起始位
                int position = StartingPosition;
                for (int i = 0; i < Count; i++)
                {
                    RcvCheckLittleEndian(position);//判断LittleEndian选项与本机是否一致，否则转换
                    BooleanProvider myBoolean = new BooleanProvider(position, DataLength, bufferData);
                    Content += myBoolean.GetData.ToString();
                    Content += " ";
                    position += DataLength;
                }
            }
            else//发送：从DataType类型数据解析为串口数据
            {
                //设置分割字符串，分割数据内容为DataType类型的数据string数组(以空格和逗号分割)
                string[] splitResult = Content.Split(new Char[] { ' ', ',' });
                if (splitResult.Length == Count)
                {
                    for (int i = 0; i < Count; i++)
                    {
                        BooleanProvider myBoolean = new BooleanProvider(splitResult[i]);
                        //判断LittleEndian选项与本机是否一致，否则转换
                        rawDatas.AddRange(SndCheckLittleEndian(myBoolean.DataToSend));
                    }
                }
                else
                {
                    throw new DataContentNotMatchCountException("错误：数据个数和数据内容不符，请使用单个空格或逗号分割数据");
                }
            }
        }

        /// <summary>
        /// Short数据的解析实现
        /// </summary>
        private void AnalyseShort()
        {
            if (!toSerialFlag)//接收：从串口数据解析为DataType类型数据
            {
                //设置中间变量position，存储单个数据的起始位
                int position = StartingPosition;
                for (int i = 0; i < Count; i++)
                {
                    RcvCheckLittleEndian(position);//判断LittleEndian与本机是否一致，否则转换
                    ShortProvider myShort = new ShortProvider(position, DataLength, bufferData);
                    Content += myShort.GetData.ToString();
                    Content += " ";
                    position += DataLength;
                }
            }
            else//发送：从DataType类型数据解析为串口数据
            {
                //设置分割字符串，分割数据内容为DataType类型的数据string数组(以空格和逗号分割)
                string[] splitResult = Content.Split(new Char[] { ' ', ',' });
                if (splitResult.Length == Count)
                {
                    for (int i = 0; i < Count; i++)
                    {
                        ShortProvider myShort = new ShortProvider(splitResult[i]);
                        //判断LittleEndian选项与本机是否一致，否则转换
                        rawDatas.AddRange(SndCheckLittleEndian(myShort.DataToSend));
                    }
                }
                else
                {
                    throw new DataContentNotMatchCountException("错误：数据个数和数据内容不符，请使用单个空格或逗号分割数据");
                }
            }
        }

        /// <summary>
        /// Ushort数据的解析实现
        /// </summary>
        private void AnalyseUshort()
        {
            if (!toSerialFlag)//接收：从串口数据解析为DataType类型数据
            {
                //设置中间变量position，存储单个数据的起始位
                int position = StartingPosition;
                for (int i = 0; i < Count; i++)
                {
                    RcvCheckLittleEndian(position);//判断LittleEndian与本机是否一致，否则转换
                    UshortProvider myUshort = new UshortProvider(position, DataLength, bufferData);
                    Content += myUshort.GetData.ToString();
                    Content += " ";
                    position += DataLength;
                }
            }
            else//发送：从DataType类型数据解析为串口数据
            {
                //设置分割字符串，分割数据内容为DataType类型的数据string数组(以空格和逗号分割)
                string[] splitResult = Content.Split(new Char[] { ' ', ',' });
                if (splitResult.Length == Count)
                {
                    for (int i = 0; i < Count; i++)
                    {
                        UshortProvider myUshort = new UshortProvider(splitResult[i]);
                        //判断LittleEndian选项与本机是否一致，否则转换
                        rawDatas.AddRange(SndCheckLittleEndian(myUshort.DataToSend));
                    }
                }
                else
                {
                    throw new DataContentNotMatchCountException("错误：数据个数和数据内容不符，请使用单个空格或逗号分割数据");
                }
            }
        }

        /// <summary>
        /// Int数据的解析实现
        /// </summary>
        private void AnalyseInt()
        {
            if (!toSerialFlag)//接收：从串口数据解析为DataType类型数据
            {
                //设置中间变量position，存储单个数据的起始位
                int position = StartingPosition;
                for (int i = 0; i < Count; i++)
                {
                    RcvCheckLittleEndian(position);//判断LittleEndian与本机是否一致，否则转换
                    IntProvider myInt = new IntProvider(position, DataLength, bufferData);
                    Content += myInt.GetData.ToString();
                    Content += " ";
                    position += DataLength;
                }
            }
            else//发送：从DataType类型数据解析为串口数据
            {
                //设置分割字符串，分割数据内容为DataType类型的数据string数组(以空格和逗号分割)
                string[] splitResult = Content.Split(new Char[] { ' ', ',' });
                if (splitResult.Length == Count)
                {
                    for (int i = 0; i < Count; i++)
                    {
                        IntProvider myInt = new IntProvider(splitResult[i]);
                        //判断LittleEndian选项与本机是否一致，否则转换
                        rawDatas.AddRange(SndCheckLittleEndian(myInt.DataToSend));
                    }
                }
                else
                {
                    throw new DataContentNotMatchCountException("错误：数据个数和数据内容不符，请使用单个空格或逗号分割数据");
                }
            }
        }

        /// <summary>
        /// Uint数据的解析实现
        /// </summary>
        private void AnalyseUint()
        {
            if (!toSerialFlag)//接收：从串口数据解析为DataType类型数据
            {
                //设置中间变量position，存储单个数据的起始位
                int position = StartingPosition;
                for (int i = 0; i < Count; i++)
                {
                    RcvCheckLittleEndian(position);//判断LittleEndian与本机是否一致，否则转换
                    UintProvider myUint = new UintProvider(position, DataLength, bufferData);
                    Content += myUint.GetData.ToString();
                    Content += " ";
                    position += DataLength;
                }
            }
            else//发送：从DataType类型数据解析为串口数据
            {
                //设置分割字符串，分割数据内容为DataType类型的数据string数组(以空格和逗号分割)
                string[] splitResult = Content.Split(new Char[] { ' ', ',' });
                if (splitResult.Length == Count)
                {
                    for (int i = 0; i < Count; i++)
                    {
                        UintProvider myUint = new UintProvider(splitResult[i]);
                        //判断LittleEndian选项与本机是否一致，否则转换
                        rawDatas.AddRange(SndCheckLittleEndian(myUint.DataToSend));
                    }
                }
                else
                {
                    throw new DataContentNotMatchCountException("错误：数据个数和数据内容不符，请使用单个空格或逗号分割数据");
                }
            }
        }

        /// <summary>
        /// Long数据的解析实现
        /// </summary>
        private void AnalyseLong()
        {
            if (!toSerialFlag)//接收：从串口数据解析为DataType类型数据
            {
                //设置中间变量position，存储单个数据的起始位
                int position = StartingPosition;
                for (int i = 0; i < Count; i++)
                {
                    RcvCheckLittleEndian(position);//判断LittleEndian与本机是否一致，否则转换
                    LongProvider myLong = new LongProvider(position, DataLength, bufferData);
                    Content += myLong.GetData.ToString();
                    Content += " ";
                    position += DataLength;
                }
            }
            else//发送：从DataType类型数据解析为串口数据
            {
                //设置分割字符串，分割数据内容为DataType类型的数据string数组(以空格和逗号分割)
                string[] splitResult = Content.Split(new Char[] { ' ', ',' });
                if (splitResult.Length == Count)
                {
                    for (int i = 0; i < Count; i++)
                    {
                        LongProvider myLong = new LongProvider(splitResult[i]);
                        //判断LittleEndian选项与本机是否一致，否则转换
                        rawDatas.AddRange(SndCheckLittleEndian(myLong.DataToSend));
                    }
                }
                else
                {
                    throw new DataContentNotMatchCountException("错误：数据个数和数据内容不符，请使用单个空格或逗号分割数据");
                }
            }
        }

        /// <summary>
        /// Ulong数据的解析实现
        /// </summary>
        private void AnalyseUlong()
        {
            if (!toSerialFlag)//接收：从串口数据解析为DataType类型数据
            {
                //设置中间变量position，存储单个数据的起始位
                int position = StartingPosition;
                for (int i = 0; i < Count; i++)
                {
                    RcvCheckLittleEndian(position);//判断LittleEndian与本机是否一致，否则转换
                    UlongProvider myUlong = new UlongProvider(position, DataLength, bufferData);
                    Content += myUlong.GetData.ToString();
                    Content += " ";
                    position += DataLength;
                }
            }
            else//发送：从DataType类型数据解析为串口数据
            {
                //设置分割字符串，分割数据内容为DataType类型的数据string数组(以空格和逗号分割)
                string[] splitResult = Content.Split(new Char[] { ' ', ',' });
                if (splitResult.Length == Count)
                {
                    for (int i = 0; i < Count; i++)
                    {
                        UlongProvider myUlong = new UlongProvider(splitResult[i]);
                        //判断LittleEndian选项与本机是否一致，否则转换
                        rawDatas.AddRange(SndCheckLittleEndian(myUlong.DataToSend));
                    }
                }
                else
                {
                    throw new DataContentNotMatchCountException("错误：数据个数和数据内容不符，请使用单个空格或逗号分割数据");
                }
            }
        }

        /// <summary>
        /// Float数据的解析实现
        /// </summary>
        private void AnalyseFloat()
        {
            if (!toSerialFlag)//接收：从串口数据解析为DataType类型数据
            {
                //设置中间变量position，存储单个数据的起始位
                int position = StartingPosition;
                for (int i = 0; i < Count; i++)
                {
                    RcvCheckLittleEndian(position);//判断LittleEndian与本机是否一致，否则转换
                    FloatProvider myFloat = new FloatProvider(position, DataLength, bufferData);
                    Content += myFloat.GetData.ToString();
                    Content += " ";
                    position += DataLength;
                }
            }
            else//发送：从DataType类型数据解析为串口数据
            {
                //设置分割字符串，分割数据内容为DataType类型的数据string数组(以空格和逗号分割)
                string[] splitResult = Content.Split(new Char[] { ' ', ',' });
                if (splitResult.Length == Count)
                {
                    for (int i = 0; i < Count; i++)
                    {
                        FloatProvider myFloat = new FloatProvider(splitResult[i]);
                        //判断LittleEndian选项与本机是否一致，否则转换
                        rawDatas.AddRange(SndCheckLittleEndian(myFloat.DataToSend));
                    }
                }
                else
                {
                    throw new DataContentNotMatchCountException("错误：数据个数和数据内容不符，请使用单个空格或逗号分割数据");
                }
            }
        }

        /// <summary>
        /// Double数据的解析实现
        /// </summary>
        private void AnalyseDouble()
        {
            if (!toSerialFlag)//接收：从串口数据解析为DataType类型数据
            {
                //设置中间变量position，存储单个数据的起始位
                int position = StartingPosition;
                for (int i = 0; i < Count; i++)
                {
                    RcvCheckLittleEndian(position);//判断LittleEndian与本机是否一致，否则转换
                    DoubleProvider myDouble = new DoubleProvider(position, DataLength, bufferData);
                    Content += myDouble.GetData.ToString();
                    Content += " ";
                    position += DataLength;
                }
            }
            else//发送：从DataType类型数据解析为串口数据
            {
                //设置分割字符串，分割数据内容为DataType类型的数据string数组(以空格和逗号分割)
                string[] splitResult = Content.Split(new Char[] { ' ', ',' });
                if (splitResult.Length == Count)
                {
                    for (int i = 0; i < Count; i++)
                    {
                        DoubleProvider myDouble = new DoubleProvider(splitResult[i]);
                        //判断LittleEndian选项与本机是否一致，否则转换
                        rawDatas.AddRange(SndCheckLittleEndian(myDouble.DataToSend));
                    }
                }
                else
                {
                    throw new DataContentNotMatchCountException("错误：数据个数和数据内容不符，请使用单个空格或逗号分割数据");
                }
            }
        }

        /// <summary>
        /// Char数据的解析实现
        /// </summary>
        private void AnalyseChar()
        {
            if (!toSerialFlag)//接收：从串口数据解析为DataType类型数据
            {
                //设置中间变量position，存储单个数据的起始位
                int position = StartingPosition;
                for (int i = 0; i < Count; i++)
                {
                    RcvCheckLittleEndian(position);//判断LittleEndian与本机是否一致，否则转换
                    CharProvider myChar = new CharProvider(position, DataLength, bufferData);
                    Content += myChar.GetData.ToString();
                    Content += " ";
                    position += DataLength;
                }
            }
            else//发送：从DataType类型数据解析为串口数据
            {
                //设置分割字符串，分割数据内容为DataType类型的数据string数组(以空格和逗号分割)
                string[] splitResult = Content.Split(new Char[] { ' ', ',' });
                if (splitResult.Length == Count)
                {
                    for (int i = 0; i < Count; i++)
                    {
                        CharProvider myChar = new CharProvider(splitResult[i]);
                        //判断LittleEndian选项与本机是否一致，否则转换
                        rawDatas.AddRange(SndCheckLittleEndian(myChar.DataToSend));
                    }
                }
                else
                {
                    throw new DataContentNotMatchCountException("错误：数据个数和数据内容不符，请使用单个空格或逗号分割数据");
                }
            }
        }

        /// <summary>
        /// String数据的解析实现
        /// </summary>
        private void AnalyseString()
        {
            if (!toSerialFlag)//接收：从串口数据解析为DataType类型数据
            {
                //设置中间变量position，存储单个数据的起始位
                int position = StartingPosition;
                //接收字符串数据时，不论Count是几个，直接全部按照一个接收
               // Count = 1;
              //  DataLength = StringLength;
                RcvCheckLittleEndian(position);//判断LittleEndian与本机是否一致，否则转换
                StringProvider myString = new StringProvider(position, DataLength, bufferData);
                Content = myString.GetData.ToString();
            }
            else//发送：从DataType类型数据解析为串口数据
            {
                StringProvider myString = new StringProvider(Content);
                //Count = 1;
                DataLength = myString.DataToSend.Length;
                //判断LittleEndian选项与本机是否一致，否则转换
                rawDatas.AddRange(SndCheckLittleEndian(myString.DataToSend));
            }
        }
        #endregion



    }
}
