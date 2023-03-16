using AutoMapper;
using HvZAPI.Models;
using HvZAPI.Models.DTOs.SquadCheckInDTOs;
using HvZAPI.Services.Concrete;
using HvZAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net.Mime;

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
        public async Task<ActionResult<IEnumerable<SquadCheckInDTO>>> GetSquadCheckIns(int gameId, int squadId)
        {
            return Ok(_mapper.Map<IEnumerable<SquadCheckInDTO>>(await _squadCheckInService.GetSquadCheckIns(gameId, squadId)));
        }

        /// <summary>
        /// Fetches a SquadCheckIn entity based on id
        /// </summary>
        /// <param name="id">Entity id</param>
        /// <returns>Found SquadCheckIn entity</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<SquadCheckInDTO>> GetSquadCheckInById(int id, int gameId, int squadId)
        {
            try
            {
                return Ok(_mapper.Map<SquadCheckInDTO>(await _squadCheckInService.GetSquadCheckInById(id, gameId, squadId)));
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
        /// Deletes a SquadCheckIn entity
        /// </summary>
        /// <param name="id">Id of entity to delete</param>
        /// <param name="gameId">Game that SquadCheckIn is in</param>
        [HttpDelete]
        public async Task<IActionResult> DeleteSquadCheckIn(int id, int gameId, int squadId)
        {
            try
            {
                await _squadCheckInService.DeleteSquadCheckIn(id, gameId, squadId);
            }
            catch (Exception error)
            {
                return NotFound(new ProblemDetails { Detail = error.Message });
            }
            return NoContent();
        }
    }
}
