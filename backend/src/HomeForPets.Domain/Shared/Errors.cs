namespace HomeForPets.Domain.Shared;

public static class Errors
{
    public static class General
    {
        public static Error ValueIsInvalid(string? name = null)
        {
            var value = name ?? "value";
            return Error.Validation("is.not.invalid", $"{value} is not invalid");
        }
        public static Error NotFound(Guid? id = null)
        {
            var forId = id==null?"": $" for id: '{id}' ";
            return Error.NotFound("record.not.found", $"record not found {forId}");
        }
        public static Error ValueIsRequired(string? name = null)
        {
            var label = name == null ? "" : " " + name + " ";
            return Error.Validation("length.is.invalid", $"invalid{label}length)");
            
        }
        public static Error AlreadyExist()
        {
            return Error.Validation("record.already.exist", "record already exist");
        }
    }
    public static class Volunteer
    {
        public static Error AlreadyExist()
        {
            return Error.Validation("record.already.exist", "record already exist");
        }
    }

    public static class Pet
    {
        public static Error SpeciesExistsInPet(Guid id)
        {
            return Error
                .Validation("species.exists.in.record",
                    $"species exists in record to pet: '{id}'");
        }
    }

    public static class PhoneNumber
    {
        public static Error Validation(string? number)
        {
            return Error.Validation("phone.number.not.invalid", $"phone number '{number}' not invalid");
        }
    }
}