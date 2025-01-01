using SharedKernel;

namespace Domain.Users;

public static class UserErrors
{
    public static readonly Error NotFound = Error.NotFound(
        "Users.NotFound",
        "The provided user was not found");

    public static readonly Error Unauthorized = Error.Failure(
        "Users.Unauthorized",
        "You are not authorized to perform this action.");

    public static readonly Error NotFoundByEmail = Error.NotFound(
        "Users.NotFoundByEmail",
        "The user with the specified email was not found");

    public static readonly Error EmailNotUnique = Error.Conflict(
        "Users.EmailNotUnique",
        "The provided email is not unique");
}
