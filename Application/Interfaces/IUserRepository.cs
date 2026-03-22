using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(Guid id);
        Task<Guid> RegisterAsync(User user);
        Task<IEnumerable<User>> GetAllAsync();
    }
}
