using AutoMapper;
using HvZAPI.Exceptions;
using HvZAPI.Models;
using HvZAPI.Models.DTOs.GameDTOs;
using HvZAPI.Models.DTOs.PlayerDTOs;
using HvZAPI.Services.Concrete;
using HvZAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net.Mime;
using System.Security.Claims;

namespace HvZAPI.Controllers
{
    [Route("api/v1/game/{gameId}/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;
        private readonly IMapper _mapper;

        public PlayerController(IPlayerService playerService, IMapper mapper)
        {
            _playerService = playerService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all players in a game
        /// </summary>
        /// <param name="gameId">Game id</param>
        /// <returns>Enumerable of players</returns>
        [HttpGet]
        [Authorize(Roles = "user")]
        public async Task<ActionResult<IEnumerable<PlayerDTO>>> GetPlayers(int gameId)
        {
            return Ok(_mapper.Map<IEnumerable<PlayerDTO>>(await _playerService.GetPlayers(gameId)));
        }

        /// <summary>
        /// Gets all players in a game
        /// </summary>
        /// <param name="gameId">Game id</param>
        /// <returns>Enumerable of players</returns>
        [HttpGet("withdetails")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<DetailedPlayerDTO>>> GetDetailedPlayers(int gameId)
        {
            return Ok(_mapper.Map<IEnumerable<DetailedPlayerDTO>>(await _playerService.GetPlayers(gameId)));
        }

        /// <summary>
        /// Gets a player entity by id
        /// </summary>
        /// <param name="gameId">Game id</param>
        /// <param name="id">Player id</param>
        /// <returns>Player entity</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "user")]
        public async Task<ActionResult<DetailedPlayerDTO>> GetPlayerById(int gameId, int id)
        {
            var subject = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var roles = User.FindAll(ClaimTypes.Role).ToList();
            try
            {
                return Ok(_mapper.Map<DetailedPlayerDTO>(await _playerService.GetPlayer(gameId, id, subject, roles)));
            }
            catch(PlayerNotFoundException ex)
            {
                return NotFound(new ProblemDetails { Detail = ex.Message });
            }
            catch(SubjectDoesNotMatchException ex)
            {
                return BadRequest(new ProblemDetails { Detail = ex.Message });
            }
        }

        /// <summary>
        /// Creates a new player entity 
        /// </summary>
        /// <param name="gameId">Game to create player in</param>
        /// <param name="createPlayerDTO">Player entity to create</param>
        /// <returns>Fully created player entity</returns>
        [HttpPost]
        [ActionName(nameof(GetPlayerById))]
        [Authorize(Roles = "user")]
        public async Task<ActionResult<PlayerDTO>> CreatePlayer(int gameId, CreatePlayerDTO createPlayerDTO)
        {
            var player = _mapper.Map<Player>(createPlayerDTO);
            try
            {
                await _playerService.AddPlayer(gameId, player);
                return CreatedAtAction(nameof(GetPlayerById), new { id = player.Id }, _mapper.Map<PlayerDTO>(player));
            }
            catch(UserNotFoundException ex)
            {
                return NotFound(new ProblemDetails { Detail = ex.Message });
            }
            catch(PlayerAlreadyInGameException ex)
            {
                return BadRequest(new ProblemDetails { Detail = ex.Message});
            }
        }

        /// <summary>
        /// Updates an existing player
        /// </summary>
        /// <param name="gameId">Game to update in</param>
        /// <param name="updatedPlayer">Updated player</param>
        /// <returns>Fully updated player entity</returns>
        [HttpPatch("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<PlayerDTO>> UpdatePlayer(int gameId, UpdatePlayerDTO updatedPlayer)
        {
            var player = _mapper.Map<Player>(updatedPlayer);
            try
            {
                var result = await _playerService.UpdatePlayer(gameId, player);
                return Ok(_mapper.Map<PlayerDTO>(result));
            }
            catch(PlayerNotFoundException ex)
            {
                return NotFound(new ProblemDetails { Detail=ex.Message});   
            }
            catch(SquadNotFoundException ex)
            {
                return NotFound(new ProblemDetails { Detail = ex.Message});
            }
        }

        /// <summary>
        /// Deletes an existing player
        /// </summary>
        /// <param name="gameId">Game id</param>
        /// <param name="playerId">Player id</param>
        /// <returns>NoContent or NotFound</returns>
        [HttpDelete]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeletePlayer(int gameId, int playerId)
        {
            try
            {
                await _playerService.DeletePlayer(gameId, playerId);
            }
            catch (PlayerNotFoundException error)
            {
                return NotFound(new ProblemDetails { Detail = error.Message });
            }
            catch (GameNotFoundException error)
            {
                return NotFound(new ProblemDetails { Detail = error.Message });
            }
            return NoContent();
        }
    }
}
