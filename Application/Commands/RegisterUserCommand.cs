using Application.Common;
using MediatR;

namespace Application.Commands
{
    public record RegisterUserCommand(string Name, string Email, string Password, string Role)
    : IRequest<Response<Guid>>;
}
