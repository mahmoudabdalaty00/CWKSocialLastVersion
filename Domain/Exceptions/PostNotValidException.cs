namespace Domain.Exceptions
{
    public class PostNotValidException : DomainValidationException
    {
        public PostNotValidException()
        {
        }
        public PostNotValidException(string message) : base(message)
        {
        }

        public PostNotValidException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
