using HomeForPets.Application.Dtos;

namespace HomeForPets.Application.Volunteers.UpdatePaymentDetails;

public record UpdatePaymentDetailsRequest(Guid VolunteerId,UpdatePaymentDetailsDto PaymentDetailsDto);
public record UpdatePaymentDetailsDto(IEnumerable<PaymentDetailsDto> PaymentDetails);