using api.Dtos.CommentDtos;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Contollers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _repo;
        public CommentController(ICommentRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            var commets = await _repo.GetAllAsync();
            var commentDtos = commets.Select(comment => comment.CommentToDto());
            return Ok(commentDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById([FromRoute] int id)
        {
            var comment = await _repo.GetCommentByIdAsync(id);

            if (comment == null)
            {
                return NotFound("Comment Not Found");
            }

            return Ok(comment.CommentToDto());
        }

        [HttpPost]
        public async Task<IActionResult> PostCommentToStock([FromBody] CreateCommentRequestDto commentDto)
        {
            int StockId = commentDto.StockId;
            var comment = commentDto.DtoToComment();

            var commentModel = await _repo.CreateCommentAsync(StockId, comment);

            if (commentModel == null)
            {
                return BadRequest("Stock doest not exist");
            }

            return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id }, comment.CommentToDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCommmentById([FromRoute] int id)
        {
            var comment = await _repo.DeleteCommentAsync(id);

            if (comment == null)
            {
                return NotFound("Comment Does Not Exists");
            }

            return NoContent();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateComment([FromRoute] int id, CommentUpdateRequestDto commentDto)
        {
            var comment = await _repo.UpdateCommentAsync(id, commentDto);

            if (comment == null)
            {
                return NotFound("Comment Does Not Exists");
            }

            return Ok(comment.CommentToDto());
        }


    }
}
