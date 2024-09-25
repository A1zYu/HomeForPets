using HomeForPets.Application.Abstaction;
using HomeForPets.Application.Dtos;
using HomeForPets.Application.Dtos.Volunteers;

namespace HomeForPets.Application.VolunteersManagement.Commands.UpdatePaymentDetails;

public record UpdatePaymentDetailsCommand(Guid VolunteerId,IEnumerable<PaymentDetailsDto> PaymentDetailsDto) : ICommand;