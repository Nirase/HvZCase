using AutoMapper;
using HvZAPI.Models.DTOs.UserDTOs;
using HvZAPI.Models;
using HvZAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net.Mime;
using HvZAPI.Services.Concrete;
using HvZAPI.Exceptions;

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
        [Authorize(Roles = "user")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            return Ok(_mapper.Map<IEnumerable<UserDTO>>(await _userService.GetUsers()));
        }

        /// <summary>
        /// Fetches all Users
        /// </summary>
        /// <returns>Enumerable of all Users</returns>
        [HttpGet("withdetails")]
        [Authorize(Roles = "user")]
        public async Task<ActionResult<IEnumerable<DetailedUserDTO>>> GetUsersWithDetails()
        {
            return Ok(_mapper.Map<IEnumerable<DetailedUserDTO>>(await _userService.GetUsers()));
        }

        /// <summary>
        /// Fetches a User entity based on id
        /// </summary>
        /// <param name="id">Entity id</param>
        /// <returns>Found User entity</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "user")]
        public async Task<ActionResult<UserDTO>> GetUserById(int id)
        {
            try
            {
                return Ok(_mapper.Map<UserDTO>(await _userService.GetUserById(id)));
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }
        }

        /// <summary>
        /// Fetches a User entity based on id
        /// </summary>
        /// <param name="id">Entity id</param>
        /// <returns>Found User entity</returns>
        [HttpGet("withdetails/{id}")]
        [Authorize(Roles = "user")]
        public async Task<ActionResult<DetailedUserDTO>> GetUserByIdWithDetails(int id)
        {
            try
            {
                return Ok(_mapper.Map<DetailedUserDTO>(await _userService.GetUserById(id)));
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }
        }

        /// <summary>
        /// Fetches a User entity based on keycloakId
        /// </summary>
        /// <param name="id">Keycloak entity id</param>
        /// <returns>Found User entity</returns>
        [HttpGet("/keycloak/{id}")]
        [Authorize(Roles = "user")]
        public async Task<ActionResult<UserDTO>> GetUserByKeycloakId(string id)
        {
            try
            {
                return Ok(_mapper.Map<UserDTO>(await _userService.GetUserByKeycloakId(id)));
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }
        }

        /// <summary>
        /// Creates a new User entity
        /// </summary>
        /// <param name="createUserDTO">User entity to create</param>
        /// <returns>Fully created User entity</returns>
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(CreateUserDTO createUserDTO)
        {
            var user = _mapper.Map<User>(createUserDTO);
            try
            {
                await _userService.AddUser(user);
                return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
            }
            catch(UserAlreadyExistsException ex)
            {
                return Ok();
            }

        }

        /// <summary>
        /// Deletes an existing User entity and all players and kills included in it 
        /// </summary>
        /// <param name="id">Id of entity to delete</param>
        /// <returns>NoContent or NotFound</returns>
        [HttpDelete]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _userService.DeleteUser(id);
            }
            catch (UserNotFoundException error)
            {
                return NotFound(new ProblemDetails { Detail = error.Message });
            }
            return NoContent();
        }

        /// <summary>
        /// Updates a User entity
        /// </summary>
        /// <param name="id">Id of entity to update</param>
        /// <param name="updatedUser">Values to update with</param>
        /// <returns>Complete updated User entity</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<UserDTO>> UpdateUser(int id, UpdateUserDTO updatedUser)
        {
            if (id != updatedUser.Id)
                return BadRequest();
            var User = _mapper.Map<User>(updatedUser);
            var result = await _userService.UpdateUser(User);
            return Ok(_mapper.Map<UserDTO>(result));
        }

    }
}
