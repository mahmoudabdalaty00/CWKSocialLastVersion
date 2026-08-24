//Session :  6
using API.Contracts.Common;
using Application.Models;
using Domain.Models.Conasts;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IActionResult HandlerErrorResponse(List<Error> errors)
        {
            if (errors is null || errors.Count == 0)
            {
                return BadRequest(BuildErrorResponse(
                    HttpStatusCode.BadRequest,
                    new List<string> { "No error details were provided." }));
            }

            // Group errors by status code (the enum value IS the HTTP status code)
            var grouped = errors
                .GroupBy(e => (int)e.Code)
                .OrderBy(g => g.Key) // deterministic order if multiple codes are present
                .ToList();

            var primaryGroup = grouped.First();
            var status = (HttpStatusCode)primaryGroup.Key;
            var messages = primaryGroup.Select(e => e.Message).ToList();

            var apiError = BuildErrorResponse(status, messages);

            return status == HttpStatusCode.NotFound
                ? NotFound(apiError)
                : StatusCode((int)status, apiError);
        }

        private static ErrorResponse BuildErrorResponse(HttpStatusCode status, List<string> messages)
        {
            var apiError = new ErrorResponse
            {
                StatusCode = (int)status,
                StatusPhrase = status.ToString(),
                TimeStamp = DateTime.UtcNow,
            };

            apiError.Errors.AddRange(messages);

            return apiError;
        }
    }
}

//using API.Contracts.Common;
//using Application.Models;
//using Domain.Models.Conasts;
//using Microsoft.AspNetCore.Mvc;

//namespace API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class BaseController : ControllerBase
//    {
//        protected IActionResult HandlerErrorResponse(List<Error> errors)
//        {
//            if (errors.Any(e => e.Code == ErrorCodes.NotFound))
//            {
//                var error = errors
//                    .FirstOrDefault(e => e.Code == ErrorCodes.NotFound);

//                var apiError = new ErrorResponse
//                {
//                    StatusCode = 404,
//                    StatusPhrase = "Not Found",
//                    TimeStamp = DateTime.UtcNow,
//                };

//                apiError.Errors.Add(error.Message);

//                return NotFound(apiError);

//            }
//            if (errors.Any(e => e.Code == ErrorCodes.ServerError))
//            {
//                var error = errors
//                    .FirstOrDefault(e => e.Code == ErrorCodes.ServerError);
//                var apiError = new ErrorResponse
//                {
//                    StatusCode = 500,
//                    StatusPhrase = "Internal Server Error",
//                    TimeStamp = DateTime.UtcNow,
//                };

//                apiError.Errors.Add(error.Message);

//                return StatusCode(500, apiError);
//            }

//            return BadRequest();
//        }
//    }
//}