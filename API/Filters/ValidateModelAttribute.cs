using API.Contracts.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters
{
    /// <summary>
    /// A filter that validates the model state and returns a bad request response if it's invalid.
    /// </summary>
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var apiError = new ErrorResponse();
                foreach (var key in context.ModelState.Keys)
                {
                    var state = context.ModelState[key];
                    if (state.Errors.Count > 0)
                    {
                        apiError.Errors.Add(state.Errors[0].ErrorMessage);
                    }
                }
                context.Result = new JsonResult(apiError) { StatusCode = 500 };
                return;
            }
        }
    }


    public class ValidateGuidAttribute : ActionFilterAttribute
    {
        private readonly string _Key;

        public ValidateGuidAttribute(string key)
        {
            _Key = key;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ActionArguments.TryGetValue(_Key, out var value))
                return;

            if (Guid.TryParse(value?.ToString(), out var guidValue))
                return;

            var apiError = new ErrorResponse
            {
                StatusCode = 400,
                StatusPhrase = "Bad Request",
                TimeStamp = DateTime.UtcNow,
            };

            apiError.Errors.Add($"The parameter '{_Key}' is not a valid GUID.");
            context.Result = new ObjectResult(apiError);
        }
    }
}
