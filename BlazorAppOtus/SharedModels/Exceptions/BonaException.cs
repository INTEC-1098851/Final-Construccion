using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SharedModels.Exceptions
{
    public class BonaException : Exception
    {

        public BonaException()
        {
        }

        public BonaException(string message) : base(message)
        {
        }

        public BonaException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BonaException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
