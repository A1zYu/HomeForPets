using CSharpFunctionalExtensions;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using HomeForPets.Application.Database;
using HomeForPets.Application.Dtos;
using HomeForPets.Application.Volunteers;
using HomeForPets.Application.Volunteers.AddPet;
using HomeForPets.Domain.Shared;
using HomeForPets.Domain.Shared.Ids;
using HomeForPets.Domain.VolunteersManagement;
using HomeForPets.Domain.VolunteersManagement.Enums;
using HomeForPets.Domain.VolunteersManagement.ValueObjects;
using Microsoft.Extensions.Logging;
using Moq;

namespace HomeForPets.Application.UnitTests;

public class AddPetToVolunteerTests
{
    private readonly Mock<IVolunteersRepository> _volunteerRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IValidator<AddPetCommand>> _validatorMock;

    public AddPetToVolunteerTests()
    {
        _validatorMock = new Mock<IValidator<AddPetCommand>>();
        _volunteerRepositoryMock = new Mock<IVolunteersRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
    }

    [Fact]
    public async void Handle_Should_Add_Pet_To_Volunteer_When_Command_Is_Valid()
    {
        //arrange
        var ct = new CancellationTokenSource().Token;
        var logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<AddPetHandler>();
        var volunteer = CreateVolunteer();

        var command = CreateAddPetCommand(volunteer);


        _volunteerRepositoryMock
            .Setup(v => v.GetById(volunteer.Id, ct))
            .ReturnsAsync(Result.Success<Volunteer, Error>(volunteer));

        _unitOfWorkMock
            .Setup(u => u.SaveChanges(ct))
            .Returns(Task.CompletedTask);

        _validatorMock
            .Setup(v => v.ValidateAsync(command, ct))
            .ReturnsAsync(new ValidationResult());

        var hadnler = new AddPetHandler(
            _volunteerRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _validatorMock.Object,
            logger);

        //act

        var result = await hadnler.Handle(command, ct);

        //assert

        result.IsSuccess.Should().BeTrue();
        volunteer.Pets.First(x=>x.Name==command.Name).Name.Should().Be("test");
    }


    [Fact]
    public async void Handle_Should_Return_Errror_When_Pet_Command_Is_Not_Valid()
    {
        //arrange
        var ct = new CancellationTokenSource().Token;
        var logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<AddPetHandler>();

        var volunteer = CreateVolunteer();

        var petName = "test";
        var description = "test";
        var petDetails = new PetDetailsDto("test", "test", 1, 2, false, false, DateTime.Now);
        var address = new AddressDto("test", "test", 1, 2);
        var phoneNumber = "89999999a99";
        var command = new AddPetCommand(volunteer.Id, petName, description, petDetails, address, phoneNumber,
            HelpStatus.NeedForHelp, []);
        
        _volunteerRepositoryMock
            .Setup(v => v.GetById(volunteer.Id, ct))
            .ReturnsAsync(Result.Success<Volunteer, Error>(volunteer));
        
        var errorsValidate = Errors.PhoneNumber.Validation("PhoneNumber").Serialize();
        
        var validationFailures = new List<ValidationFailure>
        {
            new("PhoneNumber", errorsValidate),
        };
        var validationResult = new ValidationResult(validationFailures);
        _validatorMock.Setup(v => v.ValidateAsync(command, ct))
            .ReturnsAsync(validationResult);

        _unitOfWorkMock.Setup(u => u.SaveChanges(ct)).Returns(Task.CompletedTask);
        
        var handler = new AddPetHandler(_volunteerRepositoryMock.Object,_unitOfWorkMock.Object,_validatorMock.Object,logger);
        //act
        var result =await handler.Handle(command, ct);

        //assert

        result.IsFailure.Should().BeTrue();
        result.Error.First().InvalidField.Should().Be("PhoneNumber");
    }

    private Volunteer CreateVolunteer()
    {
        var description = Description.Create("Test").Value;
        var phoneNumber = PhoneNumber.Create("89999999999").Value;

        var volunteer = Volunteer
            .Create(VolunteerId.NewId(), FullName.Create("test", "test").Value, phoneNumber, description,
                YearsOfExperience.Create(1).Value).Value;
        
        return volunteer;
    }

    private AddPetCommand CreateAddPetCommand(Volunteer volunteer)
    {
        var petName = "test";
        var description = "test";
        var petDetails = new PetDetailsDto("test", "test", 1, 2, false, false, DateTime.Now);
        var address = new AddressDto("test", "test", 1, 2);
        var phoneNumber = "89999999999";
        
        var command = new AddPetCommand(volunteer.Id, petName, description, petDetails, address, phoneNumber,
            HelpStatus.NeedForHelp, []);
        return command;
    }
}