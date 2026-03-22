using Domain.ValueObjects;

namespace Domain.Entities
{
    public enum UserRole
    {
        Usuario,
        Admin
    }
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public required Email Email { get; set; }
        public string PasswordHash { get; set; } = string.Empty;
        public UserRole Role { get; set; }
    }
}
