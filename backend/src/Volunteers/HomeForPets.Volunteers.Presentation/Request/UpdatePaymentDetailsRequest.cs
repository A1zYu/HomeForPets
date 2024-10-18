using HomeForPets.Core.Dtos.Volunteers;
using HomeForPets.Volunteers.Application.VolunteersManagement.Commands.UpdatePaymentDetails;

namespace HomeForPets.Volunteers.Controllers.Request;

public record UpdatePaymentDetailsRequest(IEnumerable<PaymentDetailsDto> PaymentDetails)
{
    public UpdatePaymentDetailsCommand ToCommand(Guid id) => new(id, PaymentDetails);
}