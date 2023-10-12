using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLL.Repositories;
using BLL.Views;
using BLL.Repositories.Interfaces;

namespace ECommAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepo _repo;
        public CommentController(ICommentRepo repo)
        {
            _repo = repo;
        }

        [HttpGet("GetCommentsByItem")]
        public IActionResult GetCommentsByItem(int itemid)
        {
            return Ok(_repo.GetCommentsByItemId(itemid));
        }

        [HttpGet("GetCommentById")]
        public IActionResult GetComment(int commentid)
        {
            var res = _repo.GetCommentsByCommentId(commentid);
            if (res == null) return Ok(new String("Invalid Comment ID"));
            return Ok(res);
        }

        [HttpPost("NewCommentOrReply")]
        public IActionResult PostComment([FromBody] CommentBody body)
        {
            return Ok(_repo.Comment(body));
        }

        [HttpPut("EditComment")]
        public IActionResult EditComment(int commentid, string comment)
        {
            return Ok(_repo.UpdateComment(commentid, comment));
        }

        [HttpDelete("DeleteComment")]
        public IActionResult Delete(int commentid)
        {
            return Ok(_repo.DeleteComment(commentid));
        }
    }
}
