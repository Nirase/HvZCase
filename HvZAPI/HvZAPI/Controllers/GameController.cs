using AutoMapper;
using HvZAPI.Models;
using HvZAPI.Models.DTOs.GameDTOs;
using HvZAPI.Services.Interfaces;
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
        /// Fetches a game entity based on id
        /// </summary>
        /// <param name="id">Entity id</param>
        /// <returns>Found game entity</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<GameDTO>> GetGameById(int id)
        {
            try
            {
                return Ok(_mapper.Map<GameDTO>(await _gameService.GetGameById(id)));
            }
            catch(Exception ex)
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
        public async Task<ActionResult<Game>> CreateGame(CreateGameDTO createGameDTO)
        {
            var game = _mapper.Map<Game>(createGameDTO);
            await _gameService.CreateGame(game);
            return CreatedAtAction(nameof(GetGameById), new { id = game.Id }, game);
        }
    }
}
