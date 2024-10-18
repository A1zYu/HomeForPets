using CSharpFunctionalExtensions;
using HomeForPets.Accounts.Contacts.Interfaces.Request;
using HomeForPets.Core;
using HomeForPets.SharedKernel;

namespace HomeForPets.Accounts.Contacts.Interfaces;

public interface IAccountsContract
{
    Task<UnitResult<ErrorList>> RegisterUser(RegisterAccountRequest request, CancellationToken ct);
}