using CourseManagement.Business.Services;
using CourseManagement.Business.Services.IService;
using CourseManagement.DataAccess.Repositorys.IRepositorys;
using CourseManagement.Model.DTOs;
using CourseManagement.Model.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CourseManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserService _userService;
        public CommentController(ICommentRepository commentRepository, IUserService userService)
        {
            _commentRepository = commentRepository;
            _userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CommentDTO commentDTO)
        {
            var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            var user = await _userService.GetUserByEmail(userEmail.Value);
            var comment = new Comment
            {
                UserId = user.Id.ToString(),
                LessonId = commentDTO.LessonId,
                Content = commentDTO.Content
            };
            try
            {
                await _commentRepository.CreateComment(comment);
                return Ok(new { Message = "Comment created successfully" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Error = "An error occurred while creating the comment" });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetComments(int lessonId)
        {
            try
            {
                var comments = await _commentRepository.GetComments(lessonId);
                return Ok(comments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = $"An error occurred while getting comments: {ex}" });
            }
        }
    }
}
