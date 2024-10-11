using CSharpFunctionalExtensions;
using HomeForPets.Accounts.Application.Commands.RegisterUser;
using HomeForPets.Accounts.Contacts.Interfaces;
using HomeForPets.Accounts.Contacts.Interfaces.Request;
using HomeForPets.Core;

namespace HomeForPets.Accounts.Presentation;

public class AccountsContract(RegisterAccountHandler registerAccountHandler) : IAccountsContract
{
    public async Task<UnitResult<ErrorList>> RegisterUser(RegisterAccountRequest request, CancellationToken ct)
    {
     
        return await registerAccountHandler.Handle(request.ToCommand(), ct);
    }
}