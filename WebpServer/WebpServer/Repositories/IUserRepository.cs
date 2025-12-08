using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebpServer.Entity;

namespace WebpServer.Repositories
{
    public interface IUserRepository
    {
        Task<User> AddAsync(User user);
        Task<User?> GetByIdAsync(Guid id);
        Task<IReadOnlyList<User>> GetAllAsync();
    }
}
