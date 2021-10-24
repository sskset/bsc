using System;
using System.Runtime.Serialization;

namespace BowlingScoreCalculator.API.Domain.Exceptions
{
    public class GameFrameException : Exception
    {
        public GameFrameException()
        {
        }

        public GameFrameException(string message) : base(message)
        {
        }

        public GameFrameException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GameFrameException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
