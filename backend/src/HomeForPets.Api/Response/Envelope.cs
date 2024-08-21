using HomeForPets.Domain.Shared;

namespace HomeForPets.Api.Response;

public record ResponseError(string? ErrorCode, string? ErrorMessage, string? InvalidFile);
public record Envelope
{
    public Envelope(object? result, IEnumerable<ResponseError>? errors)
    {
        Result = result;
        ResponseErrors = errors.ToList();
    }
    public object? Result { get; }
    public IReadOnlyList<ResponseError> ResponseErrors { get; }
    public DateTime TimeGenerated  => DateTime.Now;

    public static Envelope Ok(object? result=null) => new(result, []);
    public static Envelope Error(IEnumerable<ResponseError> errors) => new(null, errors);
}