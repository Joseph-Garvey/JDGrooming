using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JDGrooming.Classes
{
    public class AllergyException : Exception
    {
        public AllergyException()
        {
        }

        public AllergyException(string message) : base(message)
        {
        }

        public AllergyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AllergyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
