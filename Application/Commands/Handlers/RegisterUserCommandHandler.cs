using Application.Common;
using Application.Interfaces;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;

namespace Application.Commands.Handlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Response<Guid>>
    {
        private readonly IUserRepository _userRepository;

        public RegisterUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Response<Guid>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetByEmailAsync(command.Email);
            if (existingUser != null)
                return Response<Guid>.Failure("Email ya existe.");

            Email email;
            try
            {
                email = Email.Create(command.Email);
            }
            catch (ArgumentException ex)
            {
                return Response<Guid>.Failure(ex.Message);
            }

            if (!Enum.TryParse<UserRole>(command.Role, true, out var role))
                return Response<Guid>.Failure("Rol inválido. Solo se permiten 'Usuario' o 'Admin'.");

            var user = new User
            {
                Name = command.Name,
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(command.Password),
                Role = role
            };

            var userId = await _userRepository.RegisterAsync(user);
            return Response<Guid>.Success(userId);

        }
    }
}
