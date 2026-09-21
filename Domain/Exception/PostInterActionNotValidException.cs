namespace Domain.Exceptions
{
    public class PostInterActionNotValidException : DomainValidationException
    {
        public PostInterActionNotValidException()
        {
        }
        public PostInterActionNotValidException(string message) : base(message)
        {
        }

        public PostInterActionNotValidException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
