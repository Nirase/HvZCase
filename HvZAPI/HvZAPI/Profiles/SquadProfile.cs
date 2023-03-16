using AutoMapper;
using HvZAPI.Models;
using HvZAPI.Models.DTOs.SquadDTOs;

namespace HvZAPI.Profiles
{
    public class SquadProfile : Profile
    {
        public SquadProfile() 
        {
            CreateMap<Squad, SquadDTO>()
                .ForMember(dto => dto.Players, options =>
                {
                    options.MapFrom(src => src.Players.Select(x => $"api/v1/game/{x.GameId}/player/{x.Id}"));
                })
                .ForMember(dto => dto.SquadCheckIns, options =>
                {
                    options.MapFrom(src => src.SquadCheckIns.Select(x => $"api/v1/game/{x.Squad.GameId}/squad/{x.SquadId}/checkin/{x.Id}"));
                });

            CreateMap<CreateSquadDTO, Squad>();
            CreateMap<UpdateSquadDTO, Squad>();
        }
    }
}
