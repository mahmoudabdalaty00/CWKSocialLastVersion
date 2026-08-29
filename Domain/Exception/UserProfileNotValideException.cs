using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions
{
    public class UserProfileNotValideException :Exception
    {
        internal UserProfileNotValideException()
        {
            ValidationErrors = new List<string>();
        }
        internal UserProfileNotValideException(string message) : base(message)
        {
            ValidationErrors = new List<string>();
        }

        public List<string> ValidationErrors { get; }
    }
}
