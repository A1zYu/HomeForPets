using FluentAssertions;
using HomeForPets.Domain.Shared.Ids;
using HomeForPets.Domain.Shared.ValueObjects;
using HomeForPets.Domain.VolunteersManagement;
using HomeForPets.Domain.VolunteersManagement.Entities;
using HomeForPets.Domain.VolunteersManagement.Enums;
using HomeForPets.Domain.VolunteersManagement.ValueObjects;

namespace UnitTests;

public class VolunteerUnitTest
{
    [Fact]
    public async Task Add_Pet_With_Empty_Pets_Return_Success_Result()
    {
        //arrange
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

        var petId = PetId.NewId();
        
        var pet = Pet.Create(petId, name, description, petDetails, address, phoneNumber, 0,
            SpeciesBreed.Create(SpeciesId.NewId, Guid.NewGuid()).Value).Value;
        
        //act
        var result = volunteer.AddPet(pet);

        
        //assert
        var addedPetResult = volunteer.GetPetById(petId);
        result.IsSuccess.Should().BeTrue();
        Assert.True(addedPetResult.IsSuccess);
        Assert.Equal(addedPetResult.Value.Id.Value,pet.Id.Value);
        addedPetResult.Value.Position.Should().Be(Position.First);
    }

    [Fact]
    public async Task Move_Pet_Should_Not_Move_When_Pet_Already_At_New_Position()
    {
        var petsCount = 5;

        var volunteer = CreateVolunteerWithPets(5);

        var newPosition = Position.Create(3).Value;
        
        var firstPet = volunteer.Pets[0];
        var secondPet = volunteer.Pets[1];
        var thirdPet = volunteer.Pets[2];
        var fourthPet = volunteer.Pets[3];
        var fifthPet = volunteer.Pets[4];
        
        // act

        var result = volunteer.MovePet(newPosition,thirdPet);
        
        //assert
        result.IsSuccess.Should().BeTrue();
        firstPet.Position.Value.Should().Be(1);
        secondPet.Position.Value.Should().Be(2);
        thirdPet.Position.Value.Should().Be(3);
        fourthPet.Position.Value.Should().Be(4);
        fifthPet.Position.Value.Should().Be(5);

    }
    
    [Fact]
    public async Task Move_Pet_Should_Move_Other_Pets_Forward_When_New_Position()
    {
        var petsCount = 5;

        var volunteer = CreateVolunteerWithPets(5);

        var newPosition = Position.Create(4).Value;
        
        var firstPet = volunteer.Pets[0];
        var secondPet = volunteer.Pets[1];
        var thirdPet = volunteer.Pets[2];
        var fourthPet = volunteer.Pets[3];
        var fifthPet = volunteer.Pets[4];
        
        // act

        var result = volunteer.MovePet(newPosition,secondPet);
        // 1->1 2->4 3->2 4->3 5->5
        //assert
        result.IsSuccess.Should().BeTrue();
        firstPet.Position.Value.Should().Be(1);
        secondPet.Position.Value.Should().Be(4);
        thirdPet.Position.Value.Should().Be(2);
        fourthPet.Position.Value.Should().Be(3);
        fifthPet.Position.Value.Should().Be(5);

    }
    
    [Fact]
    public async Task Move_Pet_Should_Move_Other_Pets_Back_When_New_Position()
    {
        var petsCount = 5;

        var volunteer = CreateVolunteerWithPets(5);

        var newPosition = Position.Create(2).Value;
        
        var firstPet = volunteer.Pets[0];
        var secondPet = volunteer.Pets[1];
        var thirdPet = volunteer.Pets[2];
        var fourthPet = volunteer.Pets[3];
        var fifthPet = volunteer.Pets[4];
        
        // act

        var result = volunteer.MovePet(newPosition,thirdPet);
        // 1->1 2->3 3->2 4->4 5->5
        //assert
        result.IsSuccess.Should().BeTrue();
        firstPet.Position.Value.Should().Be(1);
        secondPet.Position.Value.Should().Be(3);
        thirdPet.Position.Value.Should().Be(2);
        fourthPet.Position.Value.Should().Be(4);
        fifthPet.Position.Value.Should().Be(5);

    }
    [Fact]
    public async Task Move_Pet_Should_Move_Other_Pets_Forward_When_New_Position_Is_Letter()
    {
        var petsCount = 5;

        var volunteer = CreateVolunteerWithPets(5);

        var newPosition = Position.Create(2).Value;
        
        var firstPet = volunteer.Pets[0];
        var secondPet = volunteer.Pets[1];
        var thirdPet = volunteer.Pets[2];
        var fourthPet = volunteer.Pets[3];
        var fifthPet = volunteer.Pets[4];
        
        // act

        var result = volunteer.MovePet(newPosition,fourthPet);
        // 1->1 2->3 3->4 4->2 5->5
        //assert
        result.IsSuccess.Should().BeTrue();
        firstPet.Position.Value.Should().Be(1);
        secondPet.Position.Value.Should().Be(3);
        thirdPet.Position.Value.Should().Be(4);
        fourthPet.Position.Value.Should().Be(2);
        fifthPet.Position.Value.Should().Be(5);

    }
    [Fact]
    public async Task Move_Pet_Should_Move_Other_Pets_Forward_When_New_Position_Is_Greater()
    {
        var petsCount = 5;

        var volunteer = CreateVolunteerWithPets(5);

        var newPosition = Position.Create(4).Value;
        
        var firstPet = volunteer.Pets[0];
        var secondPet = volunteer.Pets[1];
        var thirdPet = volunteer.Pets[2];
        var fourthPet = volunteer.Pets[3];
        var fifthPet = volunteer.Pets[4];
        
        // act

        var result = volunteer.MovePet(newPosition,secondPet);
        // 1->1 2->4 3->2 4->3 5->5
        //assert
        result.IsSuccess.Should().BeTrue();
        firstPet.Position.Value.Should().Be(1);
        secondPet.Position.Value.Should().Be(4);
        thirdPet.Position.Value.Should().Be(2);
        fourthPet.Position.Value.Should().Be(3);
        fifthPet.Position.Value.Should().Be(5);

    }

    [Fact]
    public async Task Create_File_Path_Should_Success()
    {
        //arrange
        var fileName = Guid.NewGuid();
        var extension = ".jpg";
        
        //act
        var result = FilePath.Create(fileName, extension);

        //assert
        result.IsSuccess.Should().BeTrue();
        var path = fileName + extension;
        result.Value.Path.Should().Be(path);
    }

    private Volunteer CreateVolunteerWithPets(int petsCount)
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
        for (int i = 0; i < petsCount; i++)
        {
            var pet = Pet.Create(PetId.NewId(), name, description, petDetails, address, phoneNumber,
                HelpStatus.NeedForHelp, speciesBreed);
            volunteer.AddPet(pet.Value);
        }

        return volunteer;
    }

}