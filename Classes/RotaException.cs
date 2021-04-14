using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JDGrooming.Classes
{
    public class RotaException : Exception
    {
        public RotaException()
        {
        }

        public RotaException(string message) : base(message)
        {
        }

        public RotaException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RotaException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
