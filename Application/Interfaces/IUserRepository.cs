using Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application
{
    public interface IUserRepository
    {
        public Task<User> GetUserByCodeAsync(long id);
        public Task<IEnumerable<User>> GetUserListAsync();
    }
}
