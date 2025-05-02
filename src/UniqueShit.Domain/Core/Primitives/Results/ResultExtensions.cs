namespace UniqueShit.Domain.Core.Primitives.Results
{
    public static class ResultExtensions
    {
        public static Result AggregateErrors(this Result returnResult, IEnumerable<Result> results)
        {
            var errors = results
                .Where(result => result.IsFailure)
                .SelectMany(result => result.Errors)
                .ToList();

            return errors.Count > 0 ? errors : returnResult;
        }
    }
}
