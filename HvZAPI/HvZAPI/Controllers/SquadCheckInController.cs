using AutoMapper;
using HvZAPI.Exceptions;
using HvZAPI.Models;
using HvZAPI.Models.DTOs.SquadCheckInDTOs;
using HvZAPI.Models.DTOs.SquadDTOs;
using HvZAPI.Services.Concrete;
using HvZAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net.Mime;
using System.Security.Claims;

namespace HvZAPI.Controllers
{
    [Route("api/v1/game/{gameId}/squad/{squadId}/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    public class SquadCheckInController : ControllerBase
    {

        private readonly ISquadCheckInService _squadCheckInService;
        private readonly IMapper _mapper;

        public SquadCheckInController(ISquadCheckInService squadCheckInService, IMapper mapper)
        {
            _squadCheckInService = squadCheckInService;
            _mapper = mapper;
        }

        /// <summary>
        /// Fetches all SquadCheckIns
        /// </summary>
        /// <returns>Enumerable of all SquadCheckIns</returns>
        [HttpGet]
        [Authorize(Roles = "user")]
        public async Task<ActionResult<IEnumerable<SquadCheckInDTO>>> GetSquadCheckIns(int gameId, int squadId)
        {
            var subject = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                return Ok(_mapper.Map<IEnumerable<SquadCheckInDTO>>(await _squadCheckInService.GetSquadCheckIns(gameId, squadId, subject)));
            }
            catch (SubjectDoesNotMatchException ex)
            {
                return Unauthorized(new ProblemDetails { Detail = ex.Message });
            }
        }

        /// <summary>
        /// Fetches a SquadCheckIn entity based on id
        /// </summary>
        /// <param name="id">Entity id</param>
        /// <param name="gameId">Game id</param>
        /// <param name="squadId">Squad id</param>
        /// <returns>Found SquadCheckIn entity</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "user")]

        public async Task<ActionResult<SquadCheckInDTO>> GetSquadCheckInById(int id, int gameId, int squadId)
        {
            var subject = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                return Ok(_mapper.Map<SquadCheckInDTO>(await _squadCheckInService.GetSquadCheckInById(id, gameId, squadId, subject)));
            }
            catch (SquadCheckInNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }
            catch (SubjectDoesNotMatchException ex)
            {
                return Unauthorized(new ProblemDetails { Detail = ex.Message });
            }
        }

        /// <summary>
        /// Creates a new squad checkin marker
        /// </summary>
        /// <param name="gameId">Game id</param>
        /// <param name="createSquadCheckInDTO">Squad check in to create</param>
        /// <returns>Fully created squad check in</returns>
        [HttpPost]
        [ActionName(nameof(GetSquadCheckInById))]
        [Authorize(Roles = "user")]
        public async Task<ActionResult<SquadCheckInDTO>> CreateSquadCheckIn(int gameId, CreateSquadCheckInDTO createSquadCheckInDTO)
        {
            var subject = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var squadCheckIn = _mapper.Map<SquadCheckIn>(createSquadCheckInDTO);
            try
            {
                var created = await _squadCheckInService.CreateSquadCheckIn(squadCheckIn, gameId, createSquadCheckInDTO.SquadId, subject);
                return CreatedAtAction(nameof(GetSquadCheckInById), new { id = created.Id }, _mapper.Map<SquadCheckInDTO>(created));
            }
            catch (SubjectDoesNotMatchException ex)
            {
                return Unauthorized(new ProblemDetails { Detail = ex.Message });
            }
        }

        /// <summary>
        /// Deletes a SquadCheckIn entity
        /// </summary>
        /// <param name="id">Id of entity to delete</param>
        /// <param name="gameId">Game that SquadCheckIn is in</param>
        /// <param name="squadId">Squad id</param>
        [HttpDelete]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> DeleteSquadCheckIn(int id, int gameId, int squadId)
        {
            var subject = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                await _squadCheckInService.DeleteSquadCheckIn(id, gameId, squadId, subject);
            }
            catch (SquadCheckInNotFoundException error)
            {
                return NotFound(new ProblemDetails { Detail = error.Message });
            }
            catch(SubjectDoesNotMatchException ex)
            {
                return Unauthorized(new ProblemDetails { Detail = ex.Message });
            }
            return NoContent();
        }
    }
}
