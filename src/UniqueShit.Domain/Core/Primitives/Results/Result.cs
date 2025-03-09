namespace UniqueShit.Domain.Core.Primitives.Results
{
    public class Result
    {
        private readonly List<Error> _errors;

        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public List<Error> Errors => IsFailure ? _errors : [Error.None];

        protected Result()
        {
            IsSuccess = true;
            _errors = [Error.None];
        }

        protected Result(Error error)
        {
            IsSuccess = false;
            _errors = [error];
        }

        protected Result(List<Error> errors)
        {
            IsSuccess = false;
            _errors = errors;
        }

        public static Result Success() => new();

        public static implicit operator Result(Error error) => new(error);

        public static implicit operator Result(List<Error> errors) => new(errors);

        public static Result<TValue> Create<TValue>(TValue value, Error error)
            where TValue : class
            => value is null ? error : value;
    }
}
