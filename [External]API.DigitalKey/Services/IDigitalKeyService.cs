using _External_API.DigitalKey.Models;

namespace _External_API.DigitalKey.Services
{
    public interface IDigitalKeyService
    {
       Task<User> GetUserInformation(string nif);
    }
}
