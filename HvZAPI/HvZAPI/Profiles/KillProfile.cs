using AutoMapper;
using HvZAPI.Models;
using HvZAPI.Models.DTOs.KillDTOs;

namespace HvZAPI.Profiles
{
    public class KillProfile : Profile
    {
        public KillProfile() 
        {
            CreateMap<UpdateKillDTO, Kill>();
        }
    }
}
