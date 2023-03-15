using HvZAPI.Contexts;
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
                throw new Exception($"Channel with name {channel.Name} already exists");
            _context.Channels.Add(channelToCreate);
            await _context.SaveChangesAsync();
            return channelToCreate;
        }

        public async Task DeleteChannel(int channelId, int gameId)
        {
            var channel = await GetChannelById(channelId, gameId);
            if (channel is null)
                throw new Exception("Channel not found");
            _context.Channels.Remove(channel);
            await _context.SaveChangesAsync();
        }

        public async Task<Channel> GetChannelById(int id, int gameId)
        {
            var channel = await _context.Channels.Include(c => c.Messages).Where(c => c.GameId == gameId).FirstOrDefaultAsync(x => x.Id == id);
            if (channel is null)
                throw new Exception("Channel not found");
            return channel;
        }

        public async Task<Channel> GetChannelByName(string name, int gameId)
        {
            var channel = await _context.Channels.Include(c => c.Messages).Where(c => c.GameId == gameId).FirstOrDefaultAsync(x => x.Name == name);
            if (channel is null)
                throw new Exception("Channel not found");
            return channel;
        }

        public async Task<IEnumerable<Channel>> GetChannels(int gameId)
        {
            return await _context.Channels.Include(c => c.Messages).Where(c => c.GameId == gameId).ToListAsync();
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
