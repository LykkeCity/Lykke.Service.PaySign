using System;
using System.Runtime.Serialization;

namespace Lykke.Service.PaySign.Core.Exceptions
{
    public class KeyAlreadyExistsException : Exception
    {
        public KeyAlreadyExistsException()
        {
        }

        public KeyAlreadyExistsException(string keyName) : base("Key already exists")
        {
            KeyName = keyName;
        }

        public KeyAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected KeyAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public string KeyName { get; set; }
    }
}
