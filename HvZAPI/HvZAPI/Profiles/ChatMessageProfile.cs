using AutoMapper;
using HvZAPI.Models;
using HvZAPI.Models.DTOs.ChatMessageDTOs;

namespace HvZAPI.Profiles
{
    public class ChatMessageProfile : Profile
    {

        public ChatMessageProfile() 
        {
            CreateMap<CreateChatMessageDTO, ChatMessage>();
            CreateMap<UpdateChatMessageDTO, ChatMessage>();
            CreateMap<ChatMessage, ChatMessageDTO>()
                .ForMember(dto => dto.Channel, options =>
                {
                    options.MapFrom(src => $"{src.Channel.Name}");
                })
                .ForMember(dto => dto.Sender, options =>
                {
                    options.MapFrom(src => $"api/v1/{src.Player.GameId}/player/{src.PlayerId}");
                });
        }

    }
}
