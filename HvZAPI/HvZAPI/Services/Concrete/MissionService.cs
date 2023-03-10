﻿using HvZAPI.Contexts;
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

        public Task DeleteMission(int MissionId, int gameId)
        {
            throw new NotImplementedException();
        }

        public async Task<Mission> GetMissionById(int id, int gameId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Mission>> GetMissions(int gameId)
        {
            throw new NotImplementedException();
        }

        public Task<Mission> UpdateMission(Mission Mission, int gameId)
        {
            throw new NotImplementedException();
        }
    }
}
