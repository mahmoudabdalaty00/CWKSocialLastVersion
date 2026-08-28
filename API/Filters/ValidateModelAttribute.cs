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
}
