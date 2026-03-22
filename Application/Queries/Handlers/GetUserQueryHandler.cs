using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using MediatR;

namespace Application.Queries.Handlers
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, Response<UserResponseDto>>
    {
        private readonly IUserRepository _userRepository;
        public GetUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Response<UserResponseDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);
            if (user == null)
                return Response<UserResponseDto>.Failure("Usuario no encontrado.");

            var dto = new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email.Value,
                Role = user.Role.ToString()
            };

            return Response<UserResponseDto>.Success(dto);
        }
    }
}
