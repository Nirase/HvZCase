using AutoMapper;
using HvZAPI.Models;
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
        /// Fetches a Mission entity based on id
        /// </summary>
        /// <param name="id">Entity id</param>
        /// <returns>Found Mission entity</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Mission>> GetMissionById(int id, int gameId)
        {
            try
            {
                return Ok(_mapper.Map<Mission>(await _missionService.GetMissionById(id, gameId)));
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
