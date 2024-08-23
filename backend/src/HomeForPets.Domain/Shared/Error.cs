namespace HomeForPets.Domain.Shared;

public record Error
{
    public const string SEPARATOR = "||";
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
    public string Serialize()
    {
        return string.Join(SEPARATOR, Code, Message, ErrorType);
    }

    public static Error Deserialize(string serialized)
    {
        var parts = serialized.Split(SEPARATOR);

        if (parts.Length < 3)
        {
            throw new ArgumentException("Invalid serialized format");
        }

        if (Enum.TryParse<ErrorType>(parts[2], out var type) == false)
        {
            throw new ArgumentException("Invalid serialized format");
        }

        return new Error(parts[0], parts[1], type);  
    }
}

public enum ErrorType
{
    Validation,
    NotFound,
    Failure,
    Conflict
}