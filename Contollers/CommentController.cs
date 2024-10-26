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
                return NotFound();
            }

            return Ok(comment.CommentToDto());
        }

    }
}