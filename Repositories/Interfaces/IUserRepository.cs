using MicroProjectApplication.Models;

namespace MicroProjectApplication.Repositories.Interfaces
{
    public interface IUserRepository
    {
        // Atomically adds the user if no user with the same email (case-insensitive) exists.
        // Returns null when the email already exists.
        User AddIfEmailUnique(string email);
    }
}
