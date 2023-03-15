using HvZAPI.Contexts;
using HvZAPI.Models;
using HvZAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace HvZAPI.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly HvZDbContext _context;
        public UserService(HvZDbContext context)
        {
            _context = context;
        }

        public async Task<User> AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task DeleteUser(int UserId)
        {

            
            var user = await GetUserById(UserId);
            foreach(var player in user.Players)
            {
                var kills = await _context.Kills.Where(x => x.KillerId == player.Id || x.VictimId == player.Id).ToListAsync();
                foreach(var kill in kills)
                    _context.Kills.Remove(kill);
                await _context.SaveChangesAsync();
            }

            user.Players.Clear();
            await _context.SaveChangesAsync();
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
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
