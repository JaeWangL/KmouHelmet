using System;

namespace KmouHelmet.Backend.Infrastructure.Exceptions
{
    /// <summary>
    /// Exception type for app exceptions
    /// </summary>
    public class KmouDomainException : Exception
    {
        public KmouDomainException()
        { }

        public KmouDomainException(string message)
            : base(message)
        { }

        public KmouDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
