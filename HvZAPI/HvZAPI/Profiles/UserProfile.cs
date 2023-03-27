using AutoMapper;
using HvZAPI.Models;
using HvZAPI.Models.DTOs.GameDTOs;
using HvZAPI.Models.DTOs.PlayerDTOs;
using HvZAPI.Models.DTOs.UserDTOs;

namespace HvZAPI.Profiles
{
    public class UserProfile : Profile
    {

        public UserProfile() 
        {
            CreateMap<User, UserDTO>()
                .ForMember(dto => dto.Players, options =>
                {
                    options.MapFrom(src => src.Players.Select(x => $"api/v1/game/{x.GameId}/player/{x.Id}"));
                });
            CreateMap<CreateUserDTO, User>();
            CreateMap<UpdateUserDTO, User>();
            CreateMap<User, DetailedUserDTO>()
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
                });
        }
    }
}
