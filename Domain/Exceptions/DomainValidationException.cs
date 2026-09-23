namespace Domain.Exceptions
{
    public class DomainValidationException : Exception
    {
        public DomainValidationException()
        {
            ValidationErrors = new List<string>();
        }
        public DomainValidationException(string message) : base(message)
        {
            ValidationErrors = new List<string>();
        }

        public DomainValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
            ValidationErrors = new List<string>();
        }

        public List<string> ValidationErrors { get; }
    }
}
