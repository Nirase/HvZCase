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
    public class KillController : ControllerBase
    {
        private readonly IKillService _KillService;
        private readonly IMapper _mapper;

        public KillController(IKillService KillService, IMapper mapper)
        {
            _KillService = KillService;
            _mapper = mapper;
        }

        /// <summary>
        /// Fetches all Kills
        /// </summary>
        /// <returns>Enumerable of all Kills</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kill>>> GetKills(int gameId)
        {
            return Ok(_mapper.Map<IEnumerable<Kill>>(await _KillService.GetKills(gameId)));
        }

        /// <summary>
        /// Fetches a Kill entity based on id
        /// </summary>
        /// <param name="id">Entity id</param>
        /// <returns>Found Kill entity</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Kill>> GetKillById(int id, int gameId)
        {
            try
            {
                return Ok(_mapper.Map<Kill>(await _KillService.GetKillById(id, gameId)));
            }
            catch (Exception ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }
        }


        /// <summary>
        /// Creates a new Kill entity
        /// </summary>
        /// <param name="biteCode">Supplied bite code</param>
        /// <returns>Fully created Kill entity</returns>
        [HttpPost]
        public async Task<ActionResult<Kill>> CreateKill(int killerId, int gameId, string biteCode)
        {
            var kill = await _KillService.CreateKill(killerId, gameId, biteCode);
            return CreatedAtAction(nameof(GetKillById), new { id = kill.Id }, kill);
        }
    }
}
