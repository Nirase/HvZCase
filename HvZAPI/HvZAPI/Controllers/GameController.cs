using AutoMapper;
using HvZAPI.Exceptions;
using HvZAPI.Models;
using HvZAPI.Models.DTOs.GameDTOs;
using HvZAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Runtime.InteropServices;

namespace HvZAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;

        public GameController(IGameService gameService, IMapper mapper)
        {
            _gameService = gameService;
            _mapper = mapper;
        }

        /// <summary>
        /// Fetches all games
        /// </summary>
        /// <returns>Enumerable of all games</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDTO>>> GetGames()
        {
            return Ok(_mapper.Map<IEnumerable<GameDTO>>(await _gameService.GetGames()));
        }

        /// <summary>
        /// Fetches all game entities with details
        /// </summary>
        /// <returns>Detailed game entities</returns>
        [HttpGet("withdetails")]
        [Authorize(Roles = "user")]
        public async Task<ActionResult<IEnumerable<DetailedGameDTO>>> GetGamesDetailed()
        {
            return Ok(_mapper.Map<IEnumerable<DetailedGameDTO>>(await _gameService.GetGames()));
        }
        /// <summary>
        /// Fetches a game entity based on id
        /// </summary>
        /// <param name="id">Entity id</param>
        /// <returns>Found game entity</returns>
        [HttpGet("{id}")]
        [Authorize(Roles ="user")]
        public async Task<ActionResult<GameDTO>> GetGameById(int id)
        {
            try
            {
                return Ok(_mapper.Map<GameDTO>(await _gameService.GetGameById(id)));
            }
            catch(GameNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }
        }

        /// <summary>
        /// Fetches a game entity with details based on id
        /// </summary>
        /// <param name="id">Entity id</param>
        /// <returns>Found game entity</returns>
        [HttpGet("{id}/withdetails")]
        [Authorize(Roles = "user")]
        public async Task<ActionResult<DetailedGameDTO>> GetGameWithDetailsById(int id)
        {
            try
            {
                return Ok(_mapper.Map<DetailedGameDTO>(await _gameService.GetGameById(id)));
            }
            catch (GameNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }
        }

        /// <summary>
        /// Creates a new game entity
        /// </summary>
        /// <param name="createGameDTO">Game entity to create</param>
        /// <returns>Fully created game entity</returns>
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Game>> CreateGame(CreateGameDTO createGameDTO)
        {
            var game = _mapper.Map<Game>(createGameDTO);
            await _gameService.CreateGame(game);
            return CreatedAtAction(nameof(GetGameById), new { id = game.Id }, game);
        }

        /// <summary>
        /// Updates a game entity
        /// </summary>
        /// <param name="id">Id of entity to update</param>
        /// <param name="updatedGame">Values to update with</param>
        /// <returns>Complete updated game entity</returns>
        [HttpPatch("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<GameDTO>> UpdateGame(int id, UpdateGameDTO updatedGame)
        {
            if(id != updatedGame.Id)
                return BadRequest();
            var game = _mapper.Map<Game>(updatedGame);
            var result = await _gameService.UpdateGame(game);
            return Ok(_mapper.Map<GameDTO>(result));
        }

        /// <summary>
        /// Deletes an existing game entity and all players and kills included in it 
        /// </summary>
        /// <param name="id">Id of entity to delete</param>
        /// <returns>NoContent or NotFound</returns>
        [HttpDelete]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            try
            {
                await _gameService.DeleteGame(id);
            }
            catch(GameNotFoundException error)
            {
                return NotFound(new ProblemDetails { Detail= error.Message });
            }
            return NoContent();
        }

    }
}
