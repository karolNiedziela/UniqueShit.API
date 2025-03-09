namespace UniqueShit.Domain.Core.Primitives.Results
{
    public static class ResultExtensions
    {
        public static Result AggregateErrors(this Result returnResult, params Result[] results)
        {
            var errors = new List<Error>();

            foreach (var result in results)
            {
                if (!result.IsFailure)
                {
                    continue;
                }

                errors.AddRange(result.Errors);
            }

            return errors.Count != 0 ? errors : returnResult;
        }
    }
}
