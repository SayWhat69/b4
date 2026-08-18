using Xunit;

namespace UserService.Domain.Tests;

public class UserTests
{
    [Theory]
    [InlineData(2008, 8, 18, 2024, 8, 18, true)]  // Geburtstag ist genau heute (16. Geburtstag)
    [InlineData(2008, 8, 19, 2024, 8, 18, false)] // ein Tag vor dem 16. Geburtstag
    [InlineData(2008, 8, 17, 2024, 8, 18, true)]  // ein Tag nach dem 16. Geburtstag
    [InlineData(2008, 2, 29, 2024, 2, 28, false)] // Schaltjahr: ein Tag vor dem 16. Geburtstag (29. Februar)
    [InlineData(2008, 2, 29, 2024, 2, 29, true)]  // Schaltjahr: genau am 16. Geburtstag (29. Februar)
    [InlineData(2020, 1, 1, 2024, 1, 1, false)]   // deutlich zu jung
    [InlineData(1990, 5, 5, 2024, 1, 1, true)]    // deutlich alt genug
    public void IsAtLeast16YearsOld_ReturnsExpectedResult(
        int dobYear, int dobMonth, int dobDay,
        int todayYear, int todayMonth, int todayDay,
        bool expected)
    {
        var dateOfBirth = new DateOnly(dobYear, dobMonth, dobDay);
        var today = new DateOnly(todayYear, todayMonth, todayDay);

        var result = User.IsAtLeast16YearsOld(dateOfBirth, today);

        Assert.Equal(expected, result);
    }
}
