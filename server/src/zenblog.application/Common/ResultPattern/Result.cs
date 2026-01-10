namespace zenblog.application.Common.ResultPattern;

public record Result
{
    public bool IsSuccess { get; } 
    public IEnumerable<Error>? Errors { get; } = [];

    protected Result(bool isSuccess, Error? error)
    {
        IsSuccess = isSuccess;
        if(error is not null)
        {
            Errors= [error];
        }
    }
    public Result() { }
    public static Result Success() => new(true, null);
    public static Result Failure(Error error) => new(false, error ?? 
    throw new ArgumentNullException(nameof(error)));

    public static implicit operator Result(Error error) => Failure(error);
}

public record Result<T> : Result
{
    public T? Data { get; }
    private Result(T data) : base(true, null) => Data = data;
    private Result(Error error) : base(false, error) { }

    public static Result<T> Success(T data) => new(data);
    public static new Result<T> Failure(Error error) => new(error);

    public static implicit operator Result<T>(T data) => new(data);

    public static implicit operator Result<T>(Error error) => new(error);
}

public enum ErrorType { NotFound, Validation, Unauthorized, EntityFrameworkCore,IdentityResult}

public record Error(string Id, ErrorType Type, string Description);

// Predefined errors (avoids magic strings)
public static class Errors
{
    public static Error NotFound{get;} = new ("NotFound", ErrorType.NotFound, "The requested resource was not found.");
    public static Error FailedToDelete { get; } = new("FailedToDelete", ErrorType.EntityFrameworkCore, "Failed to delete the resource.");
    public static Error FailedToUpdate { get; } = new("FailedToUpdate", ErrorType.EntityFrameworkCore, "Failed to update the resource.");
    public static Error FailedToCreate { get; } = new("FailedToCreate", ErrorType.EntityFrameworkCore, "Failed to create the resource.");
    public static Error ValidationFailed { get; } = new("ValidationFailed", ErrorType.Validation, "One or more validation errors occurred.");   
    public static Error AccountNotFound { get; } = new("AccountNotFound", ErrorType.NotFound, "Account not found.");
    public static Error InsufficientFunds { get; } = new("InsufficientFunds", ErrorType.Validation, "Insufficient balance.");
    public static Error FailedToCreateUser{get;}= new("RegisterUser",ErrorType.IdentityResult,"Creation of new user failded");
    public static Error InvalidCredentials{get;}= new("InvalidCredentials",ErrorType.Unauthorized,"The provided credentials are invalid.");
}