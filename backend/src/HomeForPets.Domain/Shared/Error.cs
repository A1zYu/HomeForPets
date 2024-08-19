namespace HomeForPets.Domain.Shared;

public record Error
{
    public string Code { get; set; }
    public string Message { get; set; }
    public ErrorType ErrorType { get; set; }

    public Error(string code, string message, ErrorType errorType)
    {
        Code = code;
        Message = message;
        ErrorType = errorType;
    }

    public static Error Validation(string code, string message) => new Error(code, message, ErrorType.Validation);
    public static Error NotFound(string code, string message) => new Error(code, message, ErrorType.NotFound);
    public static Error Failure(string code, string message) => new Error(code, message, ErrorType.Failure);
    public static Error Conflict(string code, string message) => new Error(code, message, ErrorType.Conflict);
}

public enum ErrorType
{
    Validation,
    NotFound,
    Failure,
    Conflict
}