using AutoMapper;
using HvZAPI.Models.DTOs.SquadDTOs;
using HvZAPI.Models;
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
        public async Task<ActionResult<IEnumerable<SquadDTO>>> GetSquads(int gameId)
        {
            return Ok(_mapper.Map<IEnumerable<SquadDTO>>(await _squadService.GetSquads(gameId)));
        }
            
    }
}
