using HomeForPets.Application.Dtos;
using HomeForPets.Application.VolunteersManagement.Commands.UpdatePaymentDetails;

namespace HomeForPets.Api.Controllers.Volunteers.Request;

public record UpdatePaymentDetailsRequest(IEnumerable<PaymentDetailsDto> PaymentDetails)
{
    public UpdatePaymentDetailsCommand ToCommand(Guid id) => new(id, PaymentDetails);
}