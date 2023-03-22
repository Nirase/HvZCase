using HvZAPI.Contexts;
using HvZAPI.Exceptions;
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
            var foundUser = await _context.Users.FirstOrDefaultAsync(x => x.KeycloakId == user.KeycloakId);
            if (foundUser != null)
                throw new UserAlreadyExistsException($"User with keycloak sub {foundUser.KeycloakId} already exists");
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task DeleteUser(int UserId)
        {

            
            var user = await GetUserById(UserId);
            if(user is null)
                throw new UserNotFoundException("User not found");

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
                throw new UserNotFoundException("User not found");
            return user;
        }

        public async Task<User> GetUserByKeycloakId(string keycloakId)
        {
            var user = await _context.Users.Include(x => x.Players).FirstOrDefaultAsync(x => x.KeycloakId == keycloakId);
            if (user is null)
                throw new UserNotFoundException("User not found");
            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.Include(x => x.Players).ToListAsync();
        }

        public async Task<User> UpdateUser(User user)
        {
            var foundUser = await GetUserById(user.Id);
            if (foundUser is null)
                throw new UserNotFoundException("User not found");
            foundUser.FirstName = user.FirstName;
            foundUser.LastName = user.LastName;
            _context.Entry(foundUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return foundUser;
        }
    }
}
