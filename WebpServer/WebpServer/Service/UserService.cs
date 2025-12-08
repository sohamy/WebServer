using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebpServer.Entity;
using WebpServer.Protocol;
using WebpServer.Repositories;

namespace WebpServer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<User> CreateUserAsync(CreateUserRequest request)
        {
            // 여기서 비즈니스 로직을 더 넣을 수 있음 (중복 이메일 체크 등)
            var user = new User
            {
                Email = request.Email,
                Nickname = request.Nickname,
                Age = request.Age
            };

            return await _repo.AddAsync(user);
        }

        public Task<IReadOnlyList<User>> GetUsersAsync()
        {
            return _repo.GetAllAsync();
        }

        public Task<User?> GetUserAsync(Guid id)
        {
            return _repo.GetByIdAsync(id);
        }
    }
}
