using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions
{
    internal class UserProfileNotValideException : DomainValidationException
    {
        public UserProfileNotValideException()
        {
        }
        public UserProfileNotValideException(string message) : base(message)
        {
        }

        public UserProfileNotValideException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

    }
}
