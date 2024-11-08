using System.Runtime.CompilerServices;
using api.Dtos.CommentDtos;
using api.Extensions;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Contollers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly UserManager<AppUser> _userManager;

        public CommentController(ICommentRepository repo, UserManager<AppUser> userManager)
        {
            _commentRepository = repo;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            var commets = await _commentRepository.GetAllAsync();
            var commentDtos = commets.Select(comment => comment.CommentToDto());
            return Ok(commentDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCommentById([FromRoute] int id)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);

            if (comment == null)
            {
                return NotFound("Comment Not Found");
            }

            return Ok(comment.CommentToDto());
        }

        [HttpPost]
        [Route("{StockId:int}")]
        [Authorize]
        public async Task<IActionResult> PostCommentToStock([FromBody] CreateCommentRequestDto commentDto, [FromRoute] int StockId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string? userName = User.GetUserName();
            AppUser? appUser = await _userManager.FindByNameAsync(userName);

            var comment = commentDto.DtoToCommentFromCreate(StockId);
            comment.AppUserId = appUser?.Id ?? string.Empty;

            var commentModel = await _commentRepository.CreateCommentAsync(StockId, comment);

            if (commentModel == null)
            {
                return BadRequest("Issue with comment");
            }


            if (commentModel == null)
            {
                return BadRequest("Stock doest not exist");
            }

            return CreatedAtAction(nameof(GetCommentById), new { id = commentModel.Id }, comment.CommentToDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteCommmentById([FromRoute] int id)
        {
            var comment = await _commentRepository.DeleteCommentAsync(id);

            if (comment == null)
            {
                return NotFound("Comment Does Not Exists");
            }

            return NoContent();
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateComment([FromRoute] int id, [FromBody] CommentUpdateRequestDto commentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _commentRepository.UpdateCommentAsync(id, commentDto);

            if (comment == null)
            {
                return NotFound("Comment Does Not Exists");
            }

            return Ok(comment.CommentToDto());
        }

    }
}
