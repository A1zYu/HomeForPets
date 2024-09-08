using HomeForPets.Application.Dtos;

namespace HomeForPets.Application.Volunteers.UpdatePaymentDetails;

public record UpdatePaymentDetailsCommand(Guid VolunteerId,IEnumerable<PaymentDetailsDto> PaymentDetailsDto);