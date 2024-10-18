namespace HomeForPets.SharedKernel;

public record Error
{
    private const string SEPARATOR = "||";
    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Failure);
    public static readonly Error NullValue = new("error.nullValue", "Null value was provided", ErrorType.Failure);
    public string Code { get; set; }
    public string Message { get; set; }
    public ErrorType ErrorType { get; set; }
    public string? InvalidField { get; }

    private Error(string code, string message, ErrorType errorType, string? invalidField = null)
    {
        Code = code;
        Message = message;
        ErrorType = errorType;
        InvalidField = invalidField;
    }

    public static Error Validation(string code, string message, string? invalidField = null) =>
        new(code, message, ErrorType.Validation, invalidField);

    public static Error NotFound(string code, string message) => new(code, message, ErrorType.NotFound);
    public static Error Failure(string code, string message) => new(code, message, ErrorType.Failure);
    public static Error Conflict(string code, string message) => new(code, message, ErrorType.Conflict);

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

    public ErrorList ToErrorList() => new([this]);
}

public enum ErrorType
{
    Validation,
    NotFound,
    Failure,
    Conflict
}