namespace Domain.Exceptions
{
    public class PostNotValideException : DomainValidationException
    {
        public PostNotValideException()
        {
        }
        public PostNotValideException(string message) : base(message)
        {
        }

        public PostNotValideException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
