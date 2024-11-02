namespace BestPracticeExceptionHandler.Extensions.PatternResultExtensions;

public static class ResultExtensions
{
    public static IActionResult ToActionResult(this BaseResult result)
    {
        return result.Error.ErrorType switch
        {
            ErrorTypes.Conflict => new ConflictObjectResult(result),
            ErrorTypes.AlreadyExist => new ConflictObjectResult(result),
            ErrorTypes.NotFound => new NotFoundObjectResult(result),
            ErrorTypes.None => new OkObjectResult(result),
            ErrorTypes.BadRequest => new BadRequestObjectResult(result),
            _ => new ObjectResult(result) { StatusCode = 500 }
        };
    }
}