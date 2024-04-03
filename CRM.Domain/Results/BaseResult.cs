namespace CRM.Domain.Results;

public class BaseResult
{
    public bool IsSuccess => ErrorMessage == null;
    public string? ErrorMessage { get; set; }
}

public class BaseResult<T> : BaseResult
{
    public T? Data { get; set; }

    public BaseResult(string errorMessage, T data)
    {
        ErrorMessage = errorMessage;
        Data = data;
    }

    public BaseResult() {}
}