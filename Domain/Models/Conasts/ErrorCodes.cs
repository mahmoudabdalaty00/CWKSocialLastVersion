namespace Domain.Models.Conasts
{
    public enum ErrorCodes
    {
        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        Conflict = 409,
        UnprocessableEntity = 422,
        ServerError = 500,
    }
}
