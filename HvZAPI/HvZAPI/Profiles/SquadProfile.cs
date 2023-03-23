using AutoMapper;
using HvZAPI.Models;
using HvZAPI.Models.DTOs.PlayerDTOs;
using HvZAPI.Models.DTOs.SquadCheckInDTOs;
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
                    options.MapFrom(src => src.SquadCheckIns.Select(x => $"api/v1/game/{x.Squad.GameId}/squad/{x.SquadId}/squadcheckin/{x.Id}"));
                });
            CreateMap<Squad, DetailedSquadDTO>()
                .ForMember(dto => dto.Players, options =>
                {
                    options.MapFrom(src => src.Players.Select(x =>
                        new LightweightPlayerDTO
                        {
                            FirstName = x.User.FirstName,
                            LastName = x.User.LastName,
                            Id = x.Id,
                            IsHuman = x.IsHuman,
                            URL = $"api/v1/game/{x.GameId}/player/{x.Id}"
                        }
                    ));
                })
                .ForMember(dto => dto.SquadCheckIns, options =>
                {
                    options.MapFrom(src => src.SquadCheckIns.Select(x => 
                    new SquadCheckInDTO
                        {
                            Id = x.Id,
                            StartDate = x.StartDate,
                            EndDate = x.EndDate,
                            Location = x.Location,
                            Squad = $"api/v1/{x.Squad.GameId}/squad/{x.SquadId}"
                        }
                    ));
                });
            CreateMap<CreateSquadDTO, Squad>();
            CreateMap<UpdateSquadDTO, Squad>();
        }
    }
}
