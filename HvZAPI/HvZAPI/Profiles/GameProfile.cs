using AutoMapper;
using HvZAPI.Contexts;
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
                })
                .ForMember(dto => dto.Kills, options =>
                {
                    options.MapFrom(p => p.Kills.Select(x => $"api/v1/game/{x.GameId}/kill/{x.Id}"));
                });
            CreateMap<UpdateGameDTO, Game>()
                .ForMember(game => game.GameState, options => { options.MapFrom(src => src.GameState); });
            CreateMap<CreateGameDTO, Game>()
                .ForMember(game => game.GameState, options => { options.MapFrom(src => "Registration"); });
                
        }
    }
}
