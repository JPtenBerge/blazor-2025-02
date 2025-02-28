using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoSharingApplication.Auth;
using PhotoSharingApplication.Client.Core.Models;
using PhotoSharingApplication.Core.Interfaces;

namespace PhotoSharingApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController(ICommentsService _commentsService) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
            Comment? comment = await _commentsService.GetCommentByIdAsync(id);

            if (comment is null)
            {
                return NotFound();
            }

            return comment;
        }

        [HttpGet("/api/photos/{photoId}/comments")]
        public async Task<IEnumerable<Comment>> GetCommentsByPhotoId(int photoId)
        {
            return await _commentsService.GetCommentsForPhotoAsync(photoId);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, Comment comment)
        {
            if (id != comment.Id)
            {
                return BadRequest();
            }

            try
            {
                await _commentsService.UpdateCommentAsync(comment);

                return NoContent();
            } catch (UnauthorizedException)
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Comment>> PostComment(Comment comment)
        {
            try
            {
                await _commentsService.AddCommentAsync(comment);
                return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, comment);
            } catch (UnauthorizedException)
            {
                return Unauthorized();
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Comment>> DeleteComment(int id)
        {
            var comment = await _commentsService.GetCommentByIdAsync(id);
            if (comment is null)
            {
                return NotFound();
            }
            try
            {
                await _commentsService.DeleteCommentAsync(id);
                return comment;
            } catch (UnauthorizedException)
            {
                return Unauthorized();
            }
        }
    }
}
