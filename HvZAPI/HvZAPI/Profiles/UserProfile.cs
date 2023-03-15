using AutoMapper;
using HvZAPI.Models;
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
        }
    }
}
