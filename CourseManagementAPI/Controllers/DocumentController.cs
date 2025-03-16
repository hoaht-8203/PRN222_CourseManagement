using CourseManagement.DataAccess.Repositorys;
using CourseManagement.Model.Constant;
using CourseManagement.Model.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase {
        private readonly DocumentRepository documentRepository;

        public DocumentController(DocumentRepository documentRepository) {
            this.documentRepository = documentRepository;
        }

        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] AddDocumentRequest req) {
            try {
                var document = await documentRepository.AddDocument(req);
                return Ok(document);
            } catch (ArgumentException ex) {
                return BadRequest(new { Error = ex.Message });
            } catch (Exception) {
                return StatusCode(500, new { Error = "An error occurred while adding the document" });
            }
        }

        [Authorize]
        [HttpGet("get-by-lesson/{lessonId}")]
        public async Task<IActionResult> GetByLesson(int lessonId) {
            try {
                var documents = await documentRepository.GetDocumentsByLesson(lessonId);
                return Ok(documents);
            } catch (Exception) {
                return StatusCode(500, new { Error = "An error occurred while fetching documents" });
            }
        }

        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpPost("remove")]
        public async Task<IActionResult> Remove([FromBody] RemoveDocumentRequest req) {
            try {
                await documentRepository.RemoveDocument(req);
                return Ok(new { Message = "Document removed successfully" });
            } catch (ArgumentException ex) {
                return BadRequest(new { Error = ex.Message });
            } catch (Exception) {
                return StatusCode(500, new { Error = "An error occurred while removing the document" });
            }
        }
    }
}
