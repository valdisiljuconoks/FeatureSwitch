using System;
using System.Runtime.Serialization;

namespace FeatureSwitch.Abstractions
{
    [Serializable]
    internal class ConfigurationErrorsException : Exception
    {
        public ConfigurationErrorsException() { }

        public ConfigurationErrorsException(string message) : base(message) { }

        public ConfigurationErrorsException(string message, Exception innerException) : base(message, innerException) { }

        protected ConfigurationErrorsException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
