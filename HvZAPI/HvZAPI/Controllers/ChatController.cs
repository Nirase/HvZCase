using AutoMapper;
using HvZAPI.Exceptions;
using HvZAPI.Models;
using HvZAPI.Models.DTOs.ChatMessageDTOs;
using HvZAPI.Services.Concrete;
using HvZAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PusherServer;
using System.Data;
using System.Net;
using System.Net.Mime;
using System.Security.Claims;
using System.Threading.Channels;

namespace HvZAPI.Controllers
{
    [Route("api/v1/chat")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    public class ChatController : ControllerBase
    {

        private readonly IChatMessageService _chatMessageService;
        private readonly IMapper _mapper;

        public ChatController(IChatMessageService chatMessageService, IMapper mapper)
        {
            _chatMessageService = chatMessageService;
            _mapper = mapper;
        }

        /// <summary>
        /// Fetches all ChatMessage entities in a game
        /// </summary>
        /// <param name="gameId">Game to fetch from</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<ChatMessageDTO>>> GetChatMessages(int gameId)
        {
            return Ok(_mapper.Map<IEnumerable<ChatMessageDTO>>(await _chatMessageService.GetChatMessages(gameId)));
        }

        /// <summary>
        /// Fetches a ChatMessage entity based on id
        /// </summary>
        /// <param name="id">Entity id</param>
        /// <returns>Found ChatMessage entity</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<ChatMessageDTO>> GetChatMessageById(int id, int gameId)
        {
            try
            {
                return Ok(_mapper.Map<ChatMessageDTO>(await _chatMessageService.GetChatMessageById(id, gameId)));
            }
            catch (ChatMessageNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }
        }


        /// <summary>
        /// Deletes a ChatMessage entity
        /// </summary>
        /// <param name="chatMessageId">Id of entity to delete</param>
        /// <param name="gameId">Game that ChatMessage is in</param>
        [HttpDelete]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteChatMessage(int chatMessageId, int gameId)
        {
            try
            {
                await _chatMessageService.DeleteChatMessage(chatMessageId, gameId);
            }
            catch (ChatMessageNotFoundException error)
            {
                return NotFound(new ProblemDetails { Detail = error.Message });
            }
            return NoContent();
        }

        /// <summary>
        /// Creates a chat message entity and relays it to listeners in a game
        /// </summary>
        /// <param name="message">Message to send</param>
        /// <param name="gameId">Game id</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "user")]
        public async Task<ActionResult> CreateMessage(CreateChatMessageDTO message, int gameId)
        {
            if (message.Contents.Length <= 0)
                return BadRequest();
            try
            {
                var created = await _chatMessageService.CreateChatMessage(_mapper.Map<ChatMessage>(message), gameId);
                return CreatedAtAction(nameof(GetChatMessageById), new { id = created.Id }, message);
            }
            catch(PlayerNotFoundException ex)
            {
                return NotFound(new ProblemDetails { Detail = ex.Message });
            }
        }
    }
}
