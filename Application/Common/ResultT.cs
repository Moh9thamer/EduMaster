namespace Application.Common;

public sealed class Result<T> : Result
{
    public T? Data { get; }

    private Result(bool succeeded, T? data, IReadOnlyList<string> errors)
        : base(succeeded, errors)
    {
        Data = data;
    }

    public static Result<T> Ok(T data) => new(true, data, []);
    public new static Result<T> Fail(params string[] errors) => new(false, default, errors);
    public new static Result<T> Fail(IEnumerable<string> errors) => new(false, default, errors.ToList());
}
