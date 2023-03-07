using HvZAPI.Models;
using HvZAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace HvZAPI.Controllers
{
    [Route("/v1/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;


        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        /// <summary>
        /// Fetches all games
        /// </summary>
        /// <returns>Enumerable of all games</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetGames()
        {
            return Ok(await _gameService.GetGames());
        }

        /// <summary>
        /// Fetches a game entity based on id
        /// </summary>
        /// <param name="id">Entity id</param>
        /// <returns>Found game entity</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGameById(int id)
        {
            try
            {
                return await _gameService.GetGameById(id);
            }
            catch(Exception ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }
        }
    }
}
