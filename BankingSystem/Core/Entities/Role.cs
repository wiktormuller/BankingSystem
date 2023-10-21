namespace BankingSystem.Core.Entities
{
    public class Role
    {
        public string Value { get; }

        private Role(string value)
        {
            Value = value;
        }

        public static Role CreateAdmin() => new("admin");
        public static Role CreateUser() => new("user");
    }
}
