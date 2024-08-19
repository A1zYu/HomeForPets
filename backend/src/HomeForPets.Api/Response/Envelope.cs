using HomeForPets.Domain.Shared;

namespace HomeForPets.Api.Response;

public record Envelope
{
    public Envelope(object? result, Error? error)
    {
        Result = result;
        ErrorCode = error?.Code;
        ErrorMessage = error?.Message;
    }
    public object? Result { get; }
    public string? ErrorCode { get; }
    public string? ErrorMessage { get; }
    public DateTime TimeGenerated  => DateTime.Now;

    public static Envelope Ok(object? result=null) => new(result, null);
    public static Envelope Error(Error error) => new(null, error);
}