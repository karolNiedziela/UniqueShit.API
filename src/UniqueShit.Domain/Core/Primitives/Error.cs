namespace UniqueShit.Domain.Core.Primitives
{
    public sealed record Error(string Code, string Message, ErrorType ErrorType = ErrorType.Validation)
    {
        public static readonly Error None = new(string.Empty, string.Empty);

        public static Error Validation(string errorCode, string message)
            => new(errorCode, message, ErrorType.Validation);

        public static Error NotFound(string errorCode, string message)
            => new(errorCode, message, ErrorType.NotFound);

        public static Error Conflict(string errorCode, string message)
            => new(errorCode, message, ErrorType.Conflict);

    }
}
