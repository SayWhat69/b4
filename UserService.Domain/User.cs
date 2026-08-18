namespace UserService.Domain;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }

    public static bool IsAtLeast16YearsOld(DateOnly dateOfBirth, DateOnly today)
    {
        return today.AddYears(-16) >= dateOfBirth;
    }
}
