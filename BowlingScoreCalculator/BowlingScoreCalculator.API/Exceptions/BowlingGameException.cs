using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BowlingScoreCalculator.API.Domain.Exceptions
{
    public class BowlingGameException : Exception
    {
        public BowlingGameException()
        {
        }

        public BowlingGameException(string message) : base(message)
        {
        }

        public BowlingGameException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BowlingGameException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
