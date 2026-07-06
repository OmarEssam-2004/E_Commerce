using E_Commerce.Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        public static ActionResult<T> ToActionResult<T>(Result result)
        {
            if (result.IsSuccess)
            {
                return new OkObjectResult(result);
            }
            else
            {
                return ToProblem(result.Errors);
            }
        }
        public static ActionResult<T> ToActionResult<T>(Result<T> result)
        {
            if (result.IsSuccess)
            {
                return new OkObjectResult(result.Data);
            }
            else
            {
                return ToProblem(result.Errors);
            }
        }
        protected static ObjectResult ToProblem(IReadOnlyList<Error> errors)
        {
            var freshErrors = errors[0];

            var statusCode = freshErrors.ErrorType switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                ErrorType.Forbidden => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status500InternalServerError
            };



            var problems = new ProblemDetails
            {
                Title = freshErrors.Code,
                Detail = freshErrors.Description,
                Status = statusCode,
                Extensions = { ["Errors"] = errors }
            };
            return new ObjectResult(problems) { StatusCode = statusCode };
        }



        
    }
}
