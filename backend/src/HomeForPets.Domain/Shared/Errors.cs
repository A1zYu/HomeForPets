namespace HomeForPets.Domain.Shared;

public static class Errors
{
    public static class General
    {
        public static Error Validation(string? name = null)
        {
            var value = name ?? "value";
            return Error.Validation("is.not.invalid", $"{value} is not invalid");
        }
        public static Error NotFound(Guid? id = null)
        {
            var forId = id==null?"": $" for id: '{id}' ";
            return Error.Validation("is.not.invalid", $"record not found {forId}");
        }
    }
    public static class Volunteer
    {
        public static Error AlreadyExist()
        {
            return Error.Validation("record.already.exist", "record already exist");
        }
    }
}