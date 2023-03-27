using AutoMapper;
using HvZAPI.Models;
using HvZAPI.Models.DTOs.ChannelDTOs;
using HvZAPI.Models.DTOs.ChatMessageDTOs;

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
            CreateMap<Channel, DetailedChannelDTO>()
                .ForMember(dto => dto.Messages, options =>
                {
                    options.MapFrom(src => src.Messages.Select(x => new ChatMessageDTO
                    {
                        Id = x.Id,
                        Channel = x.Channel.Name,
                        Sender = x.Player.User.FirstName + " " + x.Player.User.LastName,
                        Contents = x.Contents
                    }));
                });
        }
    }
}
