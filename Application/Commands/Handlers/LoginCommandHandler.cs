using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using MediatR;

namespace Application.Commands.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Response<LoginDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public LoginCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<Response<LoginDto>> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(command.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(command.Password, user.PasswordHash))
                return Response<LoginDto>.Failure("Credenciales invalidas.");

            var token = _jwtTokenGenerator.GenerateToken(user);

            return Response<LoginDto>.Success(new LoginDto
            {
                Token = token,
                Role = user.Role.ToString()
            });
        }
    }
}
