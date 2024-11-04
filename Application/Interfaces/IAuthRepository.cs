using Core;
using System;
using System.Threading.Tasks;

namespace Application
{
    public interface IAuthRepository
    {
        public Task<User> RegisterUserAsync(User user); 
        public Task<User> LoginAsync(Login login); 
    }
}
