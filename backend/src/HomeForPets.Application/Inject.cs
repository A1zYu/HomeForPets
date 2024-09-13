using FluentValidation;
using HomeForPets.Application.File.Create;
using HomeForPets.Application.File.Delete;
using HomeForPets.Application.File.Get;
using HomeForPets.Application.Volunteers.AddPet;
using HomeForPets.Application.Volunteers.CreateVolunteer;
using HomeForPets.Application.Volunteers.Delete;
using HomeForPets.Application.Volunteers.GetAllVolunteers;
using HomeForPets.Application.Volunteers.Update;
using HomeForPets.Application.Volunteers.UpdatePaymentDetails;
using HomeForPets.Application.Volunteers.UpdateSocialNetworks;
using HomeForPets.Application.Volunteers.UploadFilesToPet;
using Microsoft.Extensions.DependencyInjection;

namespace HomeForPets.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {
        service.AddScoped<CreateVolunteerHandler>();
        service.AddScoped<UpdateVolunteerHandler>();
        service.AddScoped<DeleteVolunteerHandler>();
        service.AddScoped<UpdateSocialNetworkHandler>();
        service.AddScoped<UpdatePaymentDetailsHandler>();
        service.AddScoped<CreateFileHandler>();
        service.AddScoped<GetFileHandler>();
        service.AddScoped<DeleteFileHandler>();
        service.AddScoped<AddPetHandler>();
        service.AddScoped<UploadFilesToPetPhotoHandler>();
        service.AddScoped<GetAllVolunteersHandler>();
        service.AddValidatorsFromAssembly(typeof(Inject).Assembly);
        return service;
    }
}