using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertProvider
{
    /// <summary>
    /// 数据内容与个数不符的异常
    /// </summary>
    public class DataContentNotMatchCountException : Exception
    {
        public DataContentNotMatchCountException()
        {
        }
        public DataContentNotMatchCountException(string message)
            : base(message)
        {

        }
        public DataContentNotMatchCountException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    /// <summary>
    /// 数据内容为空异常
    /// </summary>
    public class DataContentIsNullException : Exception
    {
        public DataContentIsNullException()
        {
        }
        public DataContentIsNullException(string message)
            : base(message)
        {
        }
        public DataContentIsNullException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    /// <summary>
    /// IP地址不正确的异常
    /// </summary>
    public class IPAddressInvalidException : Exception
    {
        public IPAddressInvalidException()
        {
        }
        public IPAddressInvalidException(string message)
            : base(message)
        {
        }
        public IPAddressInvalidException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    /// <summary>
    /// 服务器无法连接异常
    /// </summary>
    public class ServerNotAvaliableException : Exception
    {
        public ServerNotAvaliableException()
        {
        }
        public ServerNotAvaliableException(string message)
            : base(message)
        {
        }
        public ServerNotAvaliableException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
