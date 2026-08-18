namespace UserService.Domain;

public enum CreateUserResult
{
    Created,
    UsernameAlreadyExists,
    Underage,
    UsernameEmpty,
    NameEmpty,
    DateOfBirthEmpty
}
