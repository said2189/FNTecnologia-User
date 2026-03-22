using Application.DTOs;
using Application.Interfaces;
using Dapper;
using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.Persistence;
using System.Data;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbConnectionFactory _connectionFactory;
        public UserRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<User?> GetByEmailAsync(string email)
        {
            using var connection = _connectionFactory.CreateConnection();

            var dto = await connection.QueryFirstOrDefaultAsync<UserDto>(
                "sp_GetUserByEmail",
                new { Email = email },
                commandType: CommandType.StoredProcedure
            );

            if (dto == null) return null;

            return new User
            {
                Id = dto.Id,
                Name = dto.Name,
                Email = Email.Create(dto.Email),
                PasswordHash = dto.PasswordHash,
                Role = Enum.TryParse<UserRole>(dto.Role, true, out var role) ? role : UserRole.Usuario
            };

        }
        public async Task<User?> GetByIdAsync(Guid id)
        {
            using var connection = _connectionFactory.CreateConnection();

            var dto = await connection.QueryFirstOrDefaultAsync<UserDto>(
                "sp_GetUserById",
                new { Id = id },
                commandType: CommandType.StoredProcedure
            );

            if (dto == null) return null;

            return new User
            {
                Id = dto.Id,
                Name = dto.Name,
                Email = Email.Create(dto.Email),
                PasswordHash = dto.PasswordHash,
                Role = Enum.TryParse<UserRole>(dto.Role, true, out var role) ? role : UserRole.Usuario
            };
        }
        public async Task<Guid> RegisterAsync(User user)
        {
            using var connection = _connectionFactory.CreateConnection();
            user.Id = Guid.NewGuid();

            await connection.ExecuteAsync(
              "sp_RegisterUser",
              new
              {
                  user.Id,
                  user.Name,
                  Email = user.Email.Value,
                  user.PasswordHash,
                  user.Role
              },
              commandType: CommandType.StoredProcedure
          );

            return user.Id;
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            using var connection = _connectionFactory.CreateConnection();
            return await connection.QueryAsync<User>(
                "sp_GetAllUsers",
                commandType: CommandType.StoredProcedure
            );
        }
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            using var connection = _connectionFactory.CreateConnection();

            var dtos = await connection.QueryAsync<UserDto>(
                "sp_GetAllUsers",
                commandType: CommandType.StoredProcedure
            );

            return dtos.Select(dto => new User
            {
                Id = dto.Id,
                Name = dto.Name,
                Email = Email.Create(dto.Email),
                PasswordHash = dto.PasswordHash,
                Role = Enum.TryParse<UserRole>(dto.Role, true, out var role) ? role : UserRole.Usuario
            });
        }
    }
}
