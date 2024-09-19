using HomeForPets.Application.Abstaction;
using HomeForPets.Application.Dtos;

namespace HomeForPets.Application.VolunteersManagement.Commands.UpdatePaymentDetails;

public record UpdatePaymentDetailsCommand(Guid VolunteerId,IEnumerable<PaymentDetailsDto> PaymentDetailsDto) : ICommand;