using HvZAPI.Contexts;
using HvZAPI.Exceptions;
using HvZAPI.Models;
using HvZAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HvZAPI.Services.Concrete
{
    public class MissionService : IMissionService
    {
        private readonly HvZDbContext _context;

        public MissionService(HvZDbContext context)
        {
            _context = context;
        }

        public async Task<Mission> CreateMission(Mission mission)
        {
            _context.Missions.Add(mission);
            await _context.SaveChangesAsync();
            return mission;
        }

        public async Task DeleteMission(int missionId, int gameId)
        {
            var mission = await _context.Missions.Include(x => x.Game).Where(x => x.GameId == gameId).FirstOrDefaultAsync(x => x.Id == missionId);
            if (mission is null)
                throw new MissionNotFoundException($"Mission {missionId} not found");
            _context.Missions.Remove(mission);
            await _context.SaveChangesAsync();
        }

        public async Task<Mission> GetMissionById(int id, int gameId, string subject, List<Claim> roles)
        {
            if (roles.Where(x => x.Value == "admin").FirstOrDefault() != null)
            {
                var adminMission = await _context.Missions.Include(x => x.Game).Where(x => x.GameId == gameId).FirstOrDefaultAsync(x => x.Id == id);
                if (adminMission is null)
                    throw new MissionNotFoundException("Mission not found");
                return adminMission;
            }
            var player = await _context.Players.Include(x => x.User).Where(x => x.User.KeycloakId == subject).FirstOrDefaultAsync();
            if (player is null)
                throw new PlayerNotFoundException("Player not found");

            var mission = await _context.Missions.Include(x => x.Game).Where(x => x.GameId == gameId).FirstOrDefaultAsync(x => x.Id == id);
            if (mission is null)
                throw new MissionNotFoundException("Mission not found");
            if (player.IsHuman && mission.VisibleToHumans)
                return mission;
            if (!player.IsHuman && mission.VisibleToZombies)
                return mission;
            throw new MissionNotVisibleException("This mission is not visible to the subject");
        }

        public async Task<IEnumerable<Mission>> GetMissions(int gameId, string subject, List<Claim> roles)
        {
            if (roles.Where(x => x.Value == "admin").FirstOrDefault() != null)
                return await _context.Missions.Include(x => x.Game).Where(x => x.GameId == gameId).ToListAsync();

            var player = await _context.Players.Include(x => x.User).Where(x => x.User.KeycloakId == subject).FirstOrDefaultAsync();
            if (player is null)
                throw new PlayerNotFoundException("Player not found");
            
            if(player.IsHuman)
                return await _context.Missions.Include(x => x.Game).Where(x => x.GameId == gameId).Where(x => x.VisibleToHumans).ToListAsync();
            return await _context.Missions.Include(x => x.Game).Where(x => x.GameId == gameId).Where(x => x.VisibleToZombies).ToListAsync();
        }

        public async Task<Mission> UpdateMission(Mission mission, int gameId)
        {
            var foundMission = await _context.Missions.Include(x => x.Game).Where(x => x.GameId == gameId).FirstOrDefaultAsync(x => x.Id == mission.Id);
            if (foundMission is null)
                throw new MissionNotFoundException("Mission not found");
            foundMission.StartDate = mission.StartDate;
            foundMission.Name = mission.Name;
            foundMission.Description = mission.Description;
            foundMission.EndDate = mission.EndDate;
            foundMission.GameId = mission.GameId;
            foundMission.VisibleToHumans = mission.VisibleToHumans;
            foundMission.VisibleToZombies = mission.VisibleToZombies;
            await _context.SaveChangesAsync();
            return foundMission;
        }
    }
}
