namespace Application.Common;

public class Result
{
    public bool Succeeded { get; }
    public IReadOnlyList<string> Errors { get; }

    protected Result(bool succeeded, IReadOnlyList<string> errors)
    {
        Succeeded = succeeded;
        Errors = errors;
    }

    public static Result Ok() => new(true, []);
    public static Result Fail(params string[] errors) => new(false, errors);
    public static Result Fail(IEnumerable<string> errors) => new(false, errors.ToList());
}
