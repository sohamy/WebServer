using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebpServer.Entity;
using WebpServer.Protocol;

namespace WebpServer.Services
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(CreateUserRequest request);
        Task<IReadOnlyList<User>> GetUsersAsync();
        Task<User?> GetUserAsync(Guid id);
    }
}
