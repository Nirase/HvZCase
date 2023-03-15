using AutoMapper;
using HvZAPI.Models.DTOs.UserDTOs;
using HvZAPI.Models;
using HvZAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net.Mime;
using HvZAPI.Models.DTOs.UserDTOs;
using HvZAPI.Services.Concrete;

namespace HvZAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        /// <summary>
        /// Fetches all Users
        /// </summary>
        /// <returns>Enumerable of all Users</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            return Ok(_mapper.Map<IEnumerable<UserDTO>>(await _userService.GetUsers()));
        }


    }
}
