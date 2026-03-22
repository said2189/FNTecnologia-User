using Application.Common;
using Application.DTOs;
using MediatR;

namespace Application.Queries
{
    public record GetProfileQuery(Guid Id) : IRequest<Response<UserResponseDto>>;
}
