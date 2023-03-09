using AutoMapper;
using HvZAPI.Models;
using HvZAPI.Models.DTOs.KillDTOs;

namespace HvZAPI.Profiles
{
    public class KillService : Profile
    {
        public KillService() 
        {
            CreateMap<UpdateKillDTO, Kill>();
        }
    }
}
