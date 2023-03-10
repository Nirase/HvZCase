using AutoMapper;
using HvZAPI.Models;
using HvZAPI.Models.DTOs.MissionDTOs;
using HvZAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace HvZAPI.Controllers
{
    [Route("api/v1/game/{gameId}/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    public class MissionController : ControllerBase
    {
        private readonly IMissionService _missionService;
        private readonly IMapper _mapper;

        public MissionController(IMissionService missionService, IMapper mapper)
        {
            _missionService = missionService;
            _mapper = mapper;
        }

        /// <summary>
        /// Fetches all Missions in game
        /// </summary>
        /// <param name="gameId">Game to get missions from</param>
        /// <returns>Enumerable of all Missions in game</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MissionDTO>>> GetMissions(int gameId)
        {
            return Ok(_mapper.Map<IEnumerable<MissionDTO>>(await _missionService.GetMissions(gameId)));
        }

        /// <summary>
        /// Fetches a Mission entity based on id
        /// </summary>
        /// <param name="id">Entity id</param>
        /// <param name="gameId">Game entity id</param>
        /// <returns>Found Mission entity</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<MissionDTO>> GetMissionById(int id, int gameId)
        {
            try
            {
                return Ok(_mapper.Map<MissionDTO>(await _missionService.GetMissionById(id, gameId)));
            }
            catch (Exception ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }
        }
    }
}
