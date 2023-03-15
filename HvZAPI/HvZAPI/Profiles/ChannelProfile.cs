using AutoMapper;
using HvZAPI.Models;
using HvZAPI.Models.DTOs.ChannelDTOs;

namespace HvZAPI.Profiles
{
    public class ChannelProfile : Profile
    {
        public ChannelProfile() 
        {
            CreateMap<Channel, ChannelDTO>()
                .ForMember(dto => dto.Messages, options =>
                {
                    options.MapFrom(src => src.Messages.Select(x => $"api/v1/{src.GameId}/message/{x.Id}"));
                });
        }
    }
}
