using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using MediatR;

namespace Application.Queries.Handlers
{
    public class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, Response<UserResponseDto>>
    {
        private readonly IUserRepository _userRepository;
        public GetProfileQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Response<UserResponseDto>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);

            if (user == null)
                return Response<UserResponseDto>.Failure("Usuario no encontrado.");

            var dto = new UserResponseDto
            {
                Id = user.Id,
                Email = user.Email.Value,
                Role = user.Role.ToString(),
                Name = user.Name
            };

            return Response<UserResponseDto>.Success(dto);
        }
    }
}
