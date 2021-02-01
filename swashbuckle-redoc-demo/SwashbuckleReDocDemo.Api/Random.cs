using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SwashbuckleReDocDemo.Api
{
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (!context.ModelState.IsValid)
            {
                /*
                   {
                        "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                        "title": "One or more validation errors occurred.",
                        "status": 400,
                        "traceId": "00-7b3c76dacb67b643a0a9593419e5ddae-5375938cd3166c48-00",
                        "errors": {
                            "id": [
                                "The value '1' is not valid."
                            ]
                        }
                    }
                 */

                context.Result = new ObjectResult(new
                {
                    title = "One or more validation errors occurred.",
                    status = StatusCodes.Status400BadRequest,
                    errors = context.ModelState.ToDictionary(
                        k => k.Key, 
                        v => v.Value.Errors
                            .Select(e => e.ErrorMessage))
                })
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            // if (context.Exception is HttpResponseException exception)
            // {
            //     context.Result = new ObjectResult(exception.Value)
            //     {
            //         StatusCode = exception.Status,
            //     };
            //     context.ExceptionHandled = true;
            // }
        }
    }
}