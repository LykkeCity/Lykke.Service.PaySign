using System;
using System.Runtime.Serialization;

namespace Lykke.Service.PaySign.Core.Exceptions
{
    public class KeyNotFoundException : Exception
    {
        public KeyNotFoundException()
        {
        }

        public KeyNotFoundException(string keyName) : base("Key not found")
        {
            KeyName = keyName;
        }

        public KeyNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected KeyNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public string KeyName { get; set; }
    }
}
