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
}
