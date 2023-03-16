using AutoMapper;
using HvZAPI.Models;
using HvZAPI.Models.DTOs.SquadCheckInDTOs;

namespace HvZAPI.Profiles
{
    public class SquadCheckInProfile : Profile
    {

        public SquadCheckInProfile() 
        {
            CreateMap<SquadCheckIn, SquadCheckInDTO>()
                .ForMember(dto => dto.Squad, options =>
                {
                    options.MapFrom(src => $"api/v1/game/{src.Squad.GameId}/squad/{src.SquadId}");
                });
        }
    }
}
