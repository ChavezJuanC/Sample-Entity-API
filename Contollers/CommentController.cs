using api.Interfaces;
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
            return Ok(commets);
        }

    }
}