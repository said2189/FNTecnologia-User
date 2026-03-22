using Application.Common;
using Application.DTOs;
using MediatR;

namespace Application.Queries
{
    public record GetAllUsersQuery() : IRequest<Response<IEnumerable<UserResponseDto>>>;
}
