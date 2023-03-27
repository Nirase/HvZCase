using AutoMapper;
using HvZAPI.Models.DTOs.SquadDTOs;
using HvZAPI.Models;
using HvZAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using HvZAPI.Services.Concrete;
using HvZAPI.Exceptions;
using System.Security.Claims;

namespace HvZAPI.Controllers
{
    [Route("api/v1/game/{gameId}/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    public class SquadController : ControllerBase
    {
        private readonly ISquadService _squadService;
        private readonly IMapper _mapper;


        public SquadController(ISquadService SquadService, IMapper mapper)
        {
            _squadService = SquadService;
            _mapper = mapper;
        }


        /// <summary>
        /// Gets all squads in a game
        /// </summary>
        /// <param name="gameId">Game id</param>
        /// <returns>Found squads</returns>
        [HttpGet]
        [Authorize(Roles = "user")]
        public async Task<ActionResult<IEnumerable<SquadDTO>>> GetSquads(int gameId)
        {
            return Ok(_mapper.Map<IEnumerable<SquadDTO>>(await _squadService.GetSquads(gameId)));
        }

        /// <summary>
        /// Gets all squads in a game with details
        /// </summary>
        /// <param name="gameId">Game id</param>
        /// <returns>Found squads</returns>
        [HttpGet("withdetails")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<DetailedSquadDTO>>> GetSquadsWithDetails(int gameId)
        {
            var squad = await _squadService.GetSquads(gameId);
            return Ok(_mapper.Map<IEnumerable<DetailedSquadDTO>>(squad));
        }

        /// <summary>
        /// Gets a squad based on id
        /// </summary>
        /// <param name="id">Id of squad entity</param>
        /// <param name="gameId">Game id to search within</param>
        /// <returns>Found squad</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "user")]
        public async Task<ActionResult<SquadDTO>> GetSquadById(int id, int gameId)
        {
            var subject = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                return Ok(_mapper.Map<SquadDTO>(await _squadService.GetSquadById(id, gameId, subject)));
            }
            catch(SquadNotFoundException ex)
            {
                return NotFound(new ProblemDetails { Detail = ex.Message });
            }
        }

        /// <summary>
        /// Gets a squad based on id with details
        /// </summary>
        /// <param name="id">Id of squad entity</param>
        /// <param name="gameId">Game id to search within</param>
        /// <returns>Found squad</returns>
        [HttpGet("{id}/withdetails")]
        [Authorize(Roles = "user")]
        public async Task<ActionResult<DetailedSquadDTO>> GetSquadByIdWithDetails(int id, int gameId)
        {
            var subject = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                return Ok(_mapper.Map<DetailedSquadDTO>(await _squadService.GetSquadById(id, gameId, subject)));
            }
            catch (SquadNotFoundException ex)
            {
                return NotFound(new ProblemDetails { Detail = ex.Message });
            }
            catch(SubjectDoesNotMatchException ex)
            {
                return Unauthorized(new ProblemDetails { Detail = ex.Message });
            }
        }


        /// <summary>
        /// Creates a new squad entity
        /// </summary>
        /// <param name="gameId">Game to create squad in</param>
        /// <param name="createSquadDTO">Squad to create</param>
        /// <returns></returns>
        [HttpPost]
        [ActionName(nameof(GetSquadById))]
        [Authorize(Roles = "user")]
        public async Task<ActionResult<SquadDTO>> CreateSquad(int gameId, CreateSquadDTO createSquadDTO)
        {
            var subject = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            var creatorId = createSquadDTO.CreatorId;
            var squad = _mapper.Map<Squad>(createSquadDTO);
            try
            {
                var created = await _squadService.CreateSquad(squad, gameId, creatorId, subject);
                return CreatedAtAction(nameof(GetSquadById), new { id = created.Id }, _mapper.Map<SquadDTO>(created));
            }
            catch(PlayerNotFoundException ex)
            {
                return NotFound(new ProblemDetails { Detail = ex.Message });
            }
            catch (PlayerAlreadyInSquadException ex)
            {
                return BadRequest(new ProblemDetails { Detail = ex.Message });
            }
            catch (SquadNameAlreadyInUseException ex)
            {
                return BadRequest(new ProblemDetails { Detail = ex.Message });
            }
            catch (SubjectDoesNotMatchException ex)
            {
                return Unauthorized(new ProblemDetails { Detail = ex.Message });
            }
        }


        /// <summary>
        /// Makes a player join an existing squad.
        /// </summary>
        /// <param name="gameId">Game squad is in</param>
        /// <param name="squadId">Squad id</param>
        /// <param name="playerId">Player id</param>
        /// <returns></returns>
        [HttpPatch("{squadId}/join")]
        [Authorize(Roles = "user")]
        public async Task<ActionResult<SquadDTO>> JoinSquad(int gameId, int squadId, [FromBody] int playerId)
        {
            var subject = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                var squad = await _squadService.JoinSquad(gameId, squadId, playerId, subject);
                return Ok(_mapper.Map<SquadDTO>(squad));
            }

            catch (PlayerNotFoundException ex)
            {
                return NotFound(new ProblemDetails { Detail = ex.Message });
            }
            catch (PlayerAlreadyInSquadException ex)
            {
                return BadRequest(new ProblemDetails { Detail = ex.Message });
            }
            catch (SquadNameAlreadyInUseException ex)
            {
                return BadRequest(new ProblemDetails { Detail = ex.Message });
            }
            catch (SubjectDoesNotMatchException ex)
            {
                return Unauthorized(new ProblemDetails { Detail = ex.Message });
            }
        }


        /// <summary>
        /// Makes a player leave the squad
        /// </summary>
        /// <param name="gameId">Game squad is in</param>
        /// <param name="squadId">Squad id</param>
        /// <param name="playerId">Player id</param>
        /// <returns></returns>
        [HttpPatch("{squadId}/leave")]
        [Authorize(Roles = "user")]
        public async Task<ActionResult<SquadDTO>> LeaveSquad(int gameId, int squadId, [FromBody] int playerId)
        {
            var subject = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                var squad = await _squadService.LeaveSquad(gameId, squadId, playerId, subject);
                return Ok(_mapper.Map<SquadDTO>(squad));
            }
            catch(PlayerLeavingWrongSquadException ex)
            {
                return BadRequest(new ProblemDetails { Detail = ex.Message });

            }
            catch (PlayerNotFoundException ex)
            {
                return NotFound(new ProblemDetails { Detail = ex.Message });
            }
            catch (PlayerNotInASquadException ex)
            {
                return BadRequest(new ProblemDetails { Detail = ex.Message });
            }
            catch (SquadNameAlreadyInUseException ex)
            {
                return BadRequest(new ProblemDetails { Detail = ex.Message });
            }
            catch(SubjectDoesNotMatchException ex)
            {
                return Unauthorized(new ProblemDetails { Detail = ex.Message });
            }
        }


        /// <summary>
        /// Deletes an existing Squad entity
        /// </summary>
        /// <param name="id">Id of entity to delete</param>
        /// <param name="gameId">Game to delete from</param>
        [HttpDelete]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteSquad(int id, int gameId)
        {
            try
            {
                await _squadService.DeleteSquad(id, gameId);
            }
            catch (SquadNotFoundException error)
            {
                return NotFound(new ProblemDetails { Detail = error.Message });
            }
            return NoContent();
        }

        /// <summary>
        /// Updates a Squad entity
        /// </summary>
        /// <param name="id">Id of entity to update</param>
        /// <param name="updatedSquad">Values to update with</param>
        /// <param name="gameId">Game id</param>
        /// <returns>Complete updated Squad entity</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<SquadDTO>> UpdateSquad(int id, UpdateSquadDTO updatedSquad, int gameId)
        {
            if (id != updatedSquad.Id)
                return BadRequest();
            var squad = _mapper.Map<Squad>(updatedSquad);
            var result = await _squadService.UpdateSquad(squad, gameId);
            return Ok(_mapper.Map<SquadDTO>(result));
        }
    }
}
