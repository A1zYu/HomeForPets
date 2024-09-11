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
            return Error.NotFound("is.not.invalid", $"record not found {forId}");
        }
        public static Error ValueIsRequired(string? name = null)
        {
            var label = name == null ? "" : " " + name + " ";
            return Error.Validation("length.is.invalid", $"invalid{label}length)");
        }
    }
    public static class Volunteer
    {
        public static Error AlreadyExist()
        {
            return Error.Validation("record.already.exist", "record already exist");
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