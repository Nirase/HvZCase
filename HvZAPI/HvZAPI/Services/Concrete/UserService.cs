using HvZAPI.Contexts;
using HvZAPI.Models;
using HvZAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HvZAPI.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly HvZDbContext _context;
        public UserService(HvZDbContext context)
        {
            _context = context;
        }

        public Task<User> AddUser(User User)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUser(int UserId)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserById(int UserId)
        {
            var user = await _context.Users.Include(x => x.Players).FirstOrDefaultAsync(x => x.Id == UserId);
            if (user is null)
                throw new Exception("User not found");
            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.Include(x => x.Players).ToListAsync();
        }

        public Task<User> UpdateUser(User User)
        {
            throw new NotImplementedException();
        }
    }
}
