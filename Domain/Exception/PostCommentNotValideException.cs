namespace Domain.Exceptions
{
    public class PostCommentNotValideException : DomainValidationException
    {
        public PostCommentNotValideException()
        {
        }
        public PostCommentNotValideException(string message) : base(message)
        {
        }

        public PostCommentNotValideException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
