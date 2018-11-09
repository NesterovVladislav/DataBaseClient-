using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseClient
{

    [Serializable]
    public class DBWorkerException : ApplicationException
    {
        public DBWorkerException() { }
        public DBWorkerException(string message) : base(message) { }
        public DBWorkerException(string message, Exception inner) : base(message, inner) { }
        protected DBWorkerException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

}
