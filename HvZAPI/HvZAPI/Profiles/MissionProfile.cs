using AutoMapper;
using HvZAPI.Models;
using HvZAPI.Models.DTOs.MissionDTOs;

namespace HvZAPI.Profiles
{
    public class MissionProfile : Profile
    {

        public MissionProfile()
        {
            CreateMap<Mission, MissionDTO>()
                .ForMember(dto => dto.Game, options =>
                {
                    options.MapFrom(src => $"api/v1/game/{src.GameId}");
                });
        }
    }
}
