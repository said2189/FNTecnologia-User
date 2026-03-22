using Application.Common;
using Application.DTOs;
using MediatR;

namespace Application.Commands
{
    public record LoginCommand(string Email, string Password) : IRequest<Response<LoginDto>>;
}
