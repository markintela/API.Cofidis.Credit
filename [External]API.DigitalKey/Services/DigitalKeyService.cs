using _External_API.DigitalKey.Models;

namespace _External_API.DigitalKey.Services
{
    public class DigitalKeyService : IDigitalKeyService
    {
        private readonly List<User> _users;

        public DigitalKeyService()
        {

            _users = new List<User>
            {
                new User
                {
                    Name = "João Silva",
                    NIF = "123456789",
                    Salary = 1500.00m,
                    Age = 30,
                    Nationality = "Portuguese",
                    Loans = new List<Loan>
                    {
                        new Loan { Date = DateTime.Now.AddMonths(-6), Amount = 500, IsActive = true },
                        new Loan { Date = DateTime.Now.AddYears(-1), Amount = 2000, IsActive = false }
                    }
                },
                new User
                {
                    Name = "Maria Oliveira",
                    NIF = "987654321",
                    Salary = 2500.00m,
                    Age = 40,
                    Nationality = "Brazilian",
                    Loans = new List<Loan>
                    {
                        new Loan { Date = DateTime.Now.AddMonths(-3), Amount = 3000, IsActive = false }
                    }
                }
            };
        }

        public async Task<User> GetUserInformation(string nif)
        {
            return _users.FirstOrDefault(user => user.NIF == nif);
        }
    }
}
