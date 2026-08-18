using Microsoft.AspNetCore.Mvc;
using UserService.Domain;

namespace UserService.Api.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        app.MapGet("api/users", async (IUserRepository userRepository, CancellationToken ct) =>
        {
            Console.WriteLine("Get all users");
            var result = await userRepository.GetAllAsync(ct);
            return Results.Ok(result);
        });

        app.MapPost("api/users", async (UserAppService userAppService, [FromBody] User user, CancellationToken ct) =>
        {
            try
            {
                var (result, createdUser) = await userAppService.CreateUserAsync(user, ct);
                return result switch
                {
                    CreateUserResult.UsernameEmpty => Results.Problem(
                        statusCode: StatusCodes.Status400BadRequest,
                        title: "Username fehlt",
                        detail: "Username darf nicht leer sein."),
                    CreateUserResult.NameEmpty => Results.Problem(
                        statusCode: StatusCodes.Status400BadRequest,
                        title: "Name fehlt",
                        detail: "Name darf nicht leer sein."),
                    CreateUserResult.DateOfBirthEmpty => Results.Problem(
                        statusCode: StatusCodes.Status400BadRequest,
                        title: "Geburtsdatum fehlt",
                        detail: "Geburtsdatum muss angegeben werden."),
                    CreateUserResult.UsernameAlreadyExists => Results.Problem(
                        statusCode: StatusCodes.Status409Conflict,
                        title: "Username existiert bereits",
                        detail: $"Der Username '{user.Username}' ist bereits vergeben."),
                    CreateUserResult.Underage => Results.Problem(
                        statusCode: StatusCodes.Status400BadRequest,
                        title: "Ungültiges Alter",
                        detail: "Benutzer muss mindestens 16 Jahre alt sein."),
                    _ => Results.Created($"/api/users/{createdUser!.Username}", createdUser)
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Results.Problem(
                    statusCode: StatusCodes.Status500InternalServerError,
                    title: "Unerwarteter Fehler",
                    detail: "Der Benutzer konnte nicht angelegt werden.");
            }
        });

        app.MapGet("api/users/{username}", async (string username, IUserRepository repository, CancellationToken ct) =>
        {
            var result = await repository.GetByUsernameAsync(username, ct);
            return result == null
                ? Results.Problem(
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Benutzer nicht gefunden",
                    detail: $"Benutzer '{username}' wurde nicht gefunden.")
                : Results.Ok(result);
        });
    }
}
