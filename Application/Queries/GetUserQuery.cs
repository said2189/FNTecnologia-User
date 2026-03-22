using Application.Common;
using Application.DTOs;
using MediatR;

namespace Application.Queries
{
    public class GetUserQuery : IRequest<Response<UserResponseDto>>
    {
        public Guid Id { get; set; }
        public GetUserQuery(Guid id)
        {
            Id = id;
        }
    }
}
