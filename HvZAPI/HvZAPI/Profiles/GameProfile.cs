using AutoMapper;
using HvZAPI.Contexts;
using HvZAPI.Models;
using HvZAPI.Models.DTOs.GameDTOs;
using HvZAPI.Models.DTOs.KillDTOs;
using HvZAPI.Models.DTOs.PlayerDTOs;
using Microsoft.JSInterop.Infrastructure;

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

            CreateMap<Game, DetailedGameDTO>()
                .ForMember(dto => dto.Players, options =>
                {
                    options.MapFrom(src => src.Players.Select(x => new LightweightPlayerDTO
                    {   
                        Id = x.Id,
                        URL = $"api/game/{x.GameId}/player/{x.Id}",
                        FirstName = x.User != null ? x.User.FirstName : "",
                        LastName = x.User != null ? x.User.LastName : "",
                        IsHuman = x.IsHuman
                    }));
                })
                .ForMember(dto => dto.Kills, options =>
                {
                    options.MapFrom(src => src.Kills.Select(x => new LightweightKillDTO
                    {
                        URL = $"api/game/{x.GameId}/kill/{x.Id}",
                        Id= x.Id,
                        KillerId = x.KillerId,
                        TimeOfDeath= x.TimeOfDeath,
                        VictimId = x.VictimId
                    }));
                });
            CreateMap<UpdateGameDTO, Game>()
                .ForMember(game => game.GameState, options => { options.MapFrom(src => src.GameState); });
            CreateMap<CreateGameDTO, Game>()
                .ForMember(game => game.GameState, options => { options.MapFrom(src => "Registration"); });
                
        }
    }
}
