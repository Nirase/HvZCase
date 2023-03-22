using AutoMapper;
using HvZAPI.Exceptions;
using HvZAPI.Models;
using HvZAPI.Models.DTOs.GameDTOs;
using HvZAPI.Models.DTOs.KillDTOs;
using HvZAPI.Models.DTOs.PlayerDTOs;
using HvZAPI.Services.Concrete;
using HvZAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IKillService _killService;
        private readonly IMapper _mapper;

        
        public KillController(IKillService KillService, IMapper mapper)
        {
            _killService = KillService;
            _mapper = mapper;
        }

        /// <summary>
        /// Fetches all Kills
        /// </summary>
        /// <returns>Enumerable of all Kills</returns>
        [HttpGet]
        [Authorize(Roles = "user")]
        public async Task<ActionResult<IEnumerable<Kill>>> GetKills(int gameId)
        {
            return Ok(_mapper.Map<IEnumerable<Kill>>(await _killService.GetKills(gameId)));
        }

        /// <summary>
        /// Fetches a Kill entity based on id
        /// </summary>
        /// <param name="id">Entity id</param>
        /// <param name="gameId">Game id</param>
        /// <returns>Found Kill entity</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "user")]
        public async Task<ActionResult<Kill>> GetKillById(int id, int gameId)
        {
            try
            {
                return Ok(_mapper.Map<Kill>(await _killService.GetKillById(id, gameId)));
            }
            catch (KillNotFoundException ex)
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
        /// <param name="kill">Kill entity to create</param>
        /// <param name="gameId">Id of game</param>
        /// <returns>Fully created Kill entity</returns>
        [HttpPost]
        [ActionName(nameof(GetKillById))]
        [Authorize(Roles = "user")]
        public async Task<ActionResult<Kill>> CreateKill(CreateKillDTO kill, int gameId)
        {
            var dto = _mapper.Map<Kill>(kill);
            try
            {
                var created = await _killService.CreateKill(dto, gameId, kill.BiteCode);
                return CreatedAtAction(nameof(GetKillById), new { id = created.Id }, kill);
            }
            catch(PlayerNotFoundException ex)
            {
                return NotFound(new ProblemDetails { Detail = ex.Message });
            }
            catch(InvalidKillException ex)
            {
                return BadRequest(new ProblemDetails { Detail=ex.Message });
            }
        }


        /// <summary>
        /// Deletes a Kill entity
        /// </summary>
        /// <param name="killId">Id of entity to delete</param>
        /// <param name="gameId">Game that kill is in</param>
        [HttpDelete]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteKill(int killId, int gameId)
        {
            try
            {
                await _killService.DeleteKill(killId, gameId);
            }
            catch (KillNotFoundException error)
            {
                return NotFound(new ProblemDetails { Detail = error.Message });
            }
            catch(GameNotFoundException ex)
            {
                return NotFound(new ProblemDetails { Detail = ex.Message });
            }
            catch(PlayerNotFoundException ex)
            {
                return NotFound(new ProblemDetails { Detail = ex.Message});
            }
            return NoContent();
        }

        /// <summary>
        /// Updates a Kill entity
        /// </summary>
        /// <param name="id">Id of entity to update</param>
        /// <param name="updatedKill">Values to update with</param>
        /// <param name="gameId">Game id</param>
        /// <returns>Complete updated Kill entity</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Kill>> UpdateKill(int id, UpdateKillDTO updatedKill, int gameId)
        {
            if (id != updatedKill.Id)
                return BadRequest();
            var kill = _mapper.Map<Kill>(updatedKill);
            try
            {
                var result = await _killService.UpdateKill(kill, gameId);
                return Ok(result);
            }
            catch(KillNotFoundException ex)
            {
                return NotFound(new ProblemDetails { Detail=ex.Message});   
            }
        }
    }
}
