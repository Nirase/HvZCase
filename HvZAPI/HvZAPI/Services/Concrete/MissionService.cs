using HvZAPI.Contexts;
using HvZAPI.Models;
using HvZAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HvZAPI.Services.Concrete
{
    public class MissionService : IMissionService
    {
        private readonly HvZDbContext _context;

        public MissionService(HvZDbContext context)
        {
            _context = context;
        }

        public Task<Mission> CreateMission(int MissionerId, int gameId, string biteCode)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteMission(int missionId, int gameId)
        {
            var mission = await GetMissionById(missionId, gameId);
            if (mission is null)
                throw new Exception("Mission not found");
            _context.Missions.Remove(mission);
            await _context.SaveChangesAsync();
        }

        public async Task<Mission> GetMissionById(int id, int gameId)
        {
            var mission = await _context.Missions.Include(x => x.Game).FirstOrDefaultAsync(x => x.Id == id);
            if (mission is null)
                throw new Exception("Mission not found");
            return mission;
        }

        public async Task<IEnumerable<Mission>> GetMissions(int gameId)
        {
            return await _context.Missions.Include(x => x.Game).ToListAsync();
        }

        public async Task<Mission> UpdateMission(Mission mission, int gameId)
        {
            var foundMission = await GetMissionById(mission.Id, gameId);
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
