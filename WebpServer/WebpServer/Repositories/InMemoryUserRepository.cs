using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebpServer.Entity;

namespace WebpServer.Repositories
{
    public class InMemoryUserRepository : IUserRepository
    {
        // 진짜 DB 대신 메모리에만 저장하는 리스트
        private List<User> _users = new();

        public Task<User> AddAsync(User user)
        {
            user.Id = Guid.NewGuid();
            _users.Add(user);
            return Task.FromResult(user);
        }

        public Task<User?> GetByIdAsync(Guid id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            return Task.FromResult(user);
        }

        public Task<IReadOnlyList<User>> GetAllAsync()
        {
            // 외부에서 리스트를 마음대로 건드리지 못하게 ToList로 복사
            return Task.FromResult<IReadOnlyList<User>>(_users.ToList());
        }
    }
}
