namespace Domain.Models.Conasts
{
    public enum ErrorCodes
    {
        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        Conflict = 409,
        ServerError = 500,
        ValidationError = 422,
        DbError = 1001,
    }
}
