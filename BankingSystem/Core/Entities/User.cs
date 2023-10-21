namespace BankingSystem.Core.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public string Role { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public User(Guid id, string email, string role, string passwordHash, DateTime createdAt, DateTime updatedAt) // TODO: Refactor to value objects with domain validation
        {
            Id = id;
            Email = email;
            PasswordHash = passwordHash;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}
