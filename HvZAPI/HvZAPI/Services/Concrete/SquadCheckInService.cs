using HvZAPI.Models;
using HvZAPI.Services.Interfaces;

namespace HvZAPI.Services.Concrete
{
    public class SquadCheckInService : ISquadCheckInService
    {
        public Task<SquadCheckIn> CreateSquadCheckIn()
        {
            throw new NotImplementedException();
        }

        public Task DeleteSquadCheckIn(int SquadCheckInId, int gameId, int squadId)
        {
            throw new NotImplementedException();
        }

        public Task<SquadCheckIn> GetSquadCheckInById(int id, int gameId, int squadId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SquadCheckIn>> GetSquadCheckIns(int gameId, int squadId)
        {
            throw new NotImplementedException();
        }

        public Task<SquadCheckIn> UpdateSquadCheckIn(SquadCheckIn SquadCheckIn, int gameId, int squadId)
        {
            throw new NotImplementedException();
        }
    }
}
