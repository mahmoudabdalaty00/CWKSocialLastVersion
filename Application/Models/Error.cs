using Domain.Models.Conasts;

namespace Application.Models
{
    public class Error
    {
        public string Message { get; set; }
        public ErrorCodes Code { get; set; }
    }
}
