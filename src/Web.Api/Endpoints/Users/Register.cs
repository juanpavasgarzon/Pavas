using Application.Users.Register;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Users;

internal sealed class Register : IEndpoint
{
    private sealed record Request(
        string Email,
        string FirstName,
        string LastName,
        string Password,
        string ConfirmPassword);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/register", Handle)
            .WithTags(Tags.Users);
    }

    private static async Task<IResult> Handle(Request request, ISender sender, CancellationToken cancellationToken)
    {
        var command = new RegisterUserCommand(
            request.Email,
            request.FirstName,
            request.LastName,
            request.Password,
            request.ConfirmPassword);

        Result<Guid> result = await sender.Send(command, cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem);
    }
}
