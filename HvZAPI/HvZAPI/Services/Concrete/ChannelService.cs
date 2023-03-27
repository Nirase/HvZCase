using HvZAPI.Contexts;
using HvZAPI.Exceptions;
using HvZAPI.Models;
using HvZAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HvZAPI.Services.Concrete
{
    public class ChannelService : IChannelService
    {

        private readonly HvZDbContext _context;
        public ChannelService(HvZDbContext context)
        {
            _context = context;
        }

        public async Task<Channel> CreateChannel(Channel channelToCreate, int gameId)
        {
            var channel = await _context.Channels.Where(c => c.GameId == gameId).FirstOrDefaultAsync(x => x.Name == channelToCreate.Name);
            if (channel != null)
                throw new ChannelAlreadyExistsException($"Channel with name {channel.Name} already exists");
            _context.Channels.Add(channelToCreate);
            await _context.SaveChangesAsync();
            return channelToCreate;
        }

        public async Task DeleteChannel(int channelId, int gameId)
        {
            var channel = await GetChannelById(channelId, gameId);
            if (channel is null)
                throw new ChannelNotFoundException("Channel not found");
            _context.Channels.Remove(channel);
            await _context.SaveChangesAsync();
        }

        public async Task<Channel> GetChannelById(int id, int gameId)
        {
            var channel = await _context.Channels.Include(c => c.Messages).Where(c => c.GameId == gameId).FirstOrDefaultAsync(x => x.Id == id);
            if (channel is null)
                throw new ChannelNotFoundException("Channel not found");
            return channel;
        }

        public async Task<Channel> GetChannelByName(string name, int gameId)
        {
            var channel = await _context.Channels.Include(c => c.Messages).Where(c => c.GameId == gameId).FirstOrDefaultAsync(x => x.Name == name);
            if (channel is null)
                throw new ChannelNotFoundException("Channel not found");
            return channel;
        }

        public async Task<IEnumerable<Channel>> GetChannels(int gameId, string subject)
        {
            var player = await _context.Players.Include(x => x.User).Include(x => x.Squad).Where(x => x.User.KeycloakId == subject).Where(x => x.GameId == gameId).FirstOrDefaultAsync();
            
            if(player is null)
                throw new PlayerNotFoundException("Player not found");

            var squadName = "";
            if(player.Squad != null)
                squadName = player.Squad.Name;
            if(player.IsHuman)
                return await _context.Channels.Include(c => c.Messages).ThenInclude(x => x.Player).ThenInclude(x => x.User).Where(c => c.GameId == gameId).Where(x => x.Name == "Global" || x.Name == "Humans" || x.Name == squadName).ToListAsync();
            return await _context.Channels.Include(c => c.Messages).ThenInclude(x => x.Player).ThenInclude(x => x.User).Where(c => c.GameId == gameId).Where(x => x.Name == "Global" || x.Name == "Zombies").ToListAsync();
        }

        public async Task<Channel> UpdateChannel(Channel channel, int gameId)
        {
            var current = await GetChannelById(channel.Id, gameId);
            if(channel.Name != current.Name)
            {
                var existingByName = await _context.Channels.Where(c => c.Name == channel.Name).FirstOrDefaultAsync();
                if(existingByName != null)
                    throw new Exception($"Channel with name {channel.Name} already exists");
                current.Name = channel.Name;
            }

            return current;
        }
    }
}
