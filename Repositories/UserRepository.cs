using MicroProjectApplication.Data;
using MicroProjectApplication.Models;
using MicroProjectApplication.Repositories.Interfaces;
using System;
using System.Linq;

namespace MicroProjectApplication.Repositories
{
    public class UserRepository : IUserRepository
    {
        public User AddIfEmailUnique(string email)
        {
            lock (InMemoryDataStore.UsersLock)
            {
                if (InMemoryDataStore.Users.Any(u => string.Equals(u.Email, email, StringComparison.OrdinalIgnoreCase)))
                    return null;

                var user = new User
                {
                    Id = InMemoryDataStore.GetNextUserId(),
                    Email = email,
                    CreatedAt = DateTime.UtcNow
                };
                InMemoryDataStore.Users.Add(user);
                return user;
            }
        }
    }
}
