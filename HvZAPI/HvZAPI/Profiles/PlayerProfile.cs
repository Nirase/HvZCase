using AutoMapper;
using HvZAPI.Models;
using HvZAPI.Models.DTOs.GameDTOs;
using HvZAPI.Models.DTOs.PlayerDTOs;

namespace HvZAPI.Profiles
{
    public class PlayerProfile : Profile
    {

        public PlayerProfile()
        {
            CreateMap<Player, LightweightPlayerDTO>()
                .ForMember(dto => dto.FirstName, options =>
                {
                    options.MapFrom(src => src.User.FirstName);
                })
                .ForMember(dto => dto.LastName, options =>
                {
                    options.MapFrom(src => src.User.LastName);
                })
                .ForMember(dto => dto.URL, options =>
                {
                    options.MapFrom(src => $"api/v1/game/{src.GameId}/player/{src.Id}");
                });
            CreateMap<Player, PlayerDTO>()
                .ForMember(dto => dto.User, options =>
                {
                    options.MapFrom(src => $"api/v1/user/{src.UserId}");
                })
                .ForMember(dto => dto.Game, options =>
                {
                    options.MapFrom(src => $"api/v1/game/{src.GameId}");
                });

            CreateMap<CreatePlayerDTO, Player>();

            CreateMap<UpdatePlayerDTO, Player>();

        }
    }
}
