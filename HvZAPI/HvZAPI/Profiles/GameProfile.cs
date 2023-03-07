using AutoMapper;
using HvZAPI.Models;
using HvZAPI.Models.DTOs.GameDTOs;

namespace HvZAPI.Profiles
{
    public class GameProfile : Profile
    {
        public GameProfile() 
        {
            CreateMap<Game, GameDTO>()
                .ForMember(dto => dto.Players, options =>
                {
                    options.MapFrom(p => p.Players.Select(x => $"api/v1/player/{x.Id}"));
                });
            CreateMap<CreateGameDTO, Game>();
        }
    }
}
