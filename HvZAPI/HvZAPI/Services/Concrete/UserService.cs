using HvZAPI.Models;
using HvZAPI.Services.Interfaces;

namespace HvZAPI.Services.Concrete
{
    public class UserService : IUserService
    {
        public Task<User> AddUser(User User)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUser(int UserId)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUser(int UserId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetUsers()
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateUser(User User)
        {
            throw new NotImplementedException();
        }
    }
}
