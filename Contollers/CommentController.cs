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

        [HttpGet("{id:int}")]
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
        [Route("{StockId:int}")]
        public async Task<IActionResult> PostCommentToStock([FromBody] CreateCommentRequestDto commentDto, [FromRoute] int StockId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = commentDto.DtoToCommentFromCreate(StockId);

            var commentModel = await _repo.CreateCommentAsync(StockId, comment);

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
            var comment = await _repo.DeleteCommentAsync(id);

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

            var comment = await _repo.UpdateCommentAsync(id, commentDto);

            if (comment == null)
            {
                return NotFound("Comment Does Not Exists");
            }

            return Ok(comment.CommentToDto());
        }

    }
}
