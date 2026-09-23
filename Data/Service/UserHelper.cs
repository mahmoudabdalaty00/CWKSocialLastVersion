using Data.Specifications.Extention;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Data.Service
{
    public class UserHelper
    {
        public static bool CheckLoginValidity(ClaimsPrincipal User, out string userId, out UnauthorizedObjectResult unauthorizedObject)
        {

            userId = User.Claims.FirstOrDefault() != null ? User!.Claims!.FirstOrDefault()!.Subject!.Claims!.FirstOrDefault()!.Value : "";

            if (userId.IsNullOrEmpty())
            {
                unauthorizedObject = new UnauthorizedObjectResult("Login");
                return false;
            }
            unauthorizedObject = null;
            return true;
        }

        public static string GetUserId(ClaimsPrincipal User)
        {
            return User.Claims.FirstOrDefault() != null ? User!.Claims!.FirstOrDefault()!.Subject!.Claims!.FirstOrDefault()!.Value : "";
        }
    }
}
