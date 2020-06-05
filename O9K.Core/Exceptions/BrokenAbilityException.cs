namespace O9K.Core.Exceptions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [Serializable]
    public class BrokenAbilityException : Exception
    {
        public BrokenAbilityException()
            : base()
        {
        }

        public BrokenAbilityException(string message)
            : base(message)
        {
        }

        public BrokenAbilityException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected BrokenAbilityException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override IDictionary Data { get; } = new Dictionary<string, object>();
    }
}