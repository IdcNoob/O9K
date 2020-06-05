namespace O9K.Core.Exceptions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [Serializable]
    public class O9KException : Exception
    {
        public O9KException()
            : base()
        {
        }

        public O9KException(string message)
            : base(message)
        {
        }

        public O9KException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected O9KException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override IDictionary Data { get; } = new Dictionary<string, object>();
    }
}