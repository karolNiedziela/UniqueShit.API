namespace UniqueShit.Domain.Core.Primitives.Results
{
    public interface IResult<out TValue> : IResult
    {
        TValue Value { get; }
    }

    public interface IResult
    {
        List<Error> Errors { get; }

        bool IsFailure { get; }
    }
}
