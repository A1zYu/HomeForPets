using CSharpFunctionalExtensions;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using HomeForPets.Application.Database;
using HomeForPets.Application.Dtos;
using HomeForPets.Application.FileProvider;
using HomeForPets.Application.Volunteers;
using HomeForPets.Application.Volunteers.UploadFilesToPet;
using HomeForPets.Domain.Shared;
using HomeForPets.Domain.Shared.Ids;
using HomeForPets.Domain.VolunteersManagement;
using HomeForPets.Domain.VolunteersManagement.Entities;
using HomeForPets.Domain.VolunteersManagement.Enums;
using HomeForPets.Domain.VolunteersManagement.ValueObjects;
using Microsoft.Extensions.Logging;
using Moq;

namespace HomeForPets.Application.UnitTests;

public class UploadFilesToPetTests
{
    [Fact]
    public async void Handle_Should_Upload_Files_To_Pets()
    {
        //arrange
        var logger =LoggerFactory.Create(builder=>builder.AddConsole()).CreateLogger<UploadFilesToPetPhotoHandler>();
        
        var ct = new CancellationTokenSource().Token;
        
        var volunteer = CreateVolunteerWithPet();

        var stream = new MemoryStream();
        var fileName = Guid.NewGuid()+".jpg";

        var uploadFileDto = new UploadFileDto(stream, fileName);
        List<UploadFileDto> files = [uploadFileDto, uploadFileDto];
        
        var command = new UploadFilesToPetPhotoCommand(volunteer.Id,volunteer.Pets[0].Id,files);

        var fileProviderMock = new Mock<IFileProvider>();

        List<FilePath> filePaths =
        [
            FilePath.Create(fileName).Value,
            FilePath.Create(fileName).Value
        ];

        fileProviderMock
            .Setup(v => v.UploadFiles(It.IsAny<List<FileData>>(), ct))
            .ReturnsAsync(Result.Success<IReadOnlyList<FilePath>, Error>(filePaths));


        var volunteerRepository = new Mock<IVolunteersRepository>();

        volunteerRepository
            .Setup(v => v.GetById(volunteer.Id, ct))
            .ReturnsAsync(Result.Success<Volunteer,Error>(volunteer));

        var unitOfWorkMock = new Mock<IUnitOfWork>();

        unitOfWorkMock
            .Setup(u => u.SaveChanges(ct)).Returns(Task.CompletedTask);

        var validatorMock = new Mock<IValidator<UploadFilesToPetPhotoCommand>>();
        validatorMock.Setup(v => v.ValidateAsync(command, ct))
            .ReturnsAsync(new ValidationResult());

        var handler = new UploadFilesToPetPhotoHandler(
            fileProviderMock.Object,
            volunteerRepository.Object,
            unitOfWorkMock.Object,
            validatorMock.Object,
            logger);
        
        //act
        var result = await handler.Handle(command, ct);
        
        //assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(volunteer.Id);
    }
    [Fact]
    public async Task Handle_Should_Returns_Error_Is_Not_Valid()
    {
        //arrange
        var logger =LoggerFactory.Create(builder=>builder.AddConsole()).CreateLogger<UploadFilesToPetPhotoHandler>();
        
        var ct = new CancellationTokenSource().Token;
        
        var volunteer = CreateVolunteerWithPet();

        var stream = new MemoryStream();
        var fileName = Guid.NewGuid()+".jpg";

        var uploadFileDto = new UploadFileDto(stream, fileName);
        List<UploadFileDto> files = [uploadFileDto, uploadFileDto];
        
        var command = new UploadFilesToPetPhotoCommand(volunteer.Id,volunteer.Pets[0].Id,files);
        var errorValidate = Errors.General.ValueIsInvalid(nameof(command.Files)).Serialize();
        var validationFailures = new List<ValidationFailure>
        {
            new (nameof(command.Files), errorValidate),
        };
        var validationResult = new ValidationResult(validationFailures);
        
        var fileProviderMock = new Mock<IFileProvider>();

        List<FilePath> filePaths =
        [
            FilePath.Create(fileName).Value,
            FilePath.Create(fileName).Value
        ];

        fileProviderMock
            .Setup(v => v.UploadFiles(It.IsAny<List<FileData>>(), ct))
            .ReturnsAsync(Result.Success<IReadOnlyList<FilePath>, Error>(filePaths));


        var volunteerRepository = new Mock<IVolunteersRepository>();

        volunteerRepository
            .Setup(v => v.GetById(volunteer.Id, ct))
            .ReturnsAsync(Result.Success<Volunteer,Error>(volunteer));

        var unitOfWorkMock = new Mock<IUnitOfWork>();

        unitOfWorkMock
            .Setup(u => u.SaveChanges(ct)).Returns(Task.CompletedTask);

        var errors = Errors.General.ValueIsInvalid().Serialize();
        var validatorMock = new Mock<IValidator<UploadFilesToPetPhotoCommand>>();
        validatorMock.Setup(v => v.ValidateAsync(command, ct))
            .ReturnsAsync(new ValidationResult(new List<ValidationFailure>(){new("File Name",errors)}));

        var handler = new UploadFilesToPetPhotoHandler(
            fileProviderMock.Object,
            volunteerRepository.Object,
            unitOfWorkMock.Object,
            validatorMock.Object,
            logger);
        
        //act
        var result = await handler.Handle(command, ct);
        
        //assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(volunteer.Id);
    }

    private Volunteer CreateVolunteerWithPet()
    {
        var name = "Test";
        
        var description = Description.Create("Test").Value;
        
        var petDetails = PetDetails.
            Create("test", "test", 1, 1, false, false, DateTime.Now.Date)
            .Value;
        var address = Address.Create("test", "test", 1, 1).Value;
        
        var phoneNumber = PhoneNumber.Create("89999999999").Value;
        var volunteer = 
            Volunteer.Create(VolunteerId.NewId(), FullName.Create("test", "test").Value, phoneNumber, description, 
                YearsOfExperience.Create(1).Value).Value;
        var speciesBreed = SpeciesBreed.Create(SpeciesId.Create(Guid.Empty), Guid.Empty).Value;
        var pet = Pet.Create(PetId.NewId(), name, description, petDetails, address, phoneNumber,
            HelpStatus.NeedForHelp, speciesBreed);
        volunteer.AddPet(pet.Value);
        return volunteer;
    }
}