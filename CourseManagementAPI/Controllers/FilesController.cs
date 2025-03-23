using CourseManagement.Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase {
        private readonly MinioFileService _fileService;
        private readonly ILogger<FileController> _logger;

        public FileController(MinioFileService fileService, ILogger<FileController> logger) {
            _fileService = fileService;
            _logger = logger;
        }

        [HttpPost("upload")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
    {
            try {
                if (file == null || file.Length == 0)
                    return BadRequest("No file uploaded.");

                // Kiểm tra kích thước file (ví dụ: max 10MB)
                if (file.Length > 10 * 1024 * 1024)
                    return BadRequest("File size exceeds maximum limit (10MB).");

                // Kiểm tra loại file
                var allowedTypes = new[] {
                            "image/jpeg",
                            "image/png",
                            "application/pdf",
                            "application/msword",                     // .doc
                            "application/vnd.openxmlformats-officedocument.wordprocessingml.document", // .docx
                            "application/json",                       // .json
                            "application/xml",                        // .xml
                            "text/xml"                               // .xml (alternative content type)
                        };

                if (!allowedTypes.Contains(file.ContentType.ToLower())) {
                    return BadRequest($"File type not allowed. Allowed types are: JPEG, PNG, PDF, DOC, DOCX, JSON, XML");
                }

                var fileName = await _fileService.UploadFileAsync(file);
                return Ok(new { fileName });
            } catch (Exception ex) {
                _logger.LogError(ex, "Error uploading file");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{fileName}")]
        public async Task<IActionResult> GetFile(string fileName) {
            try {
                var fileBytes = await _fileService.GetFileAsync(fileName);

                // Xác định content type dựa trên phần mở rộng của file
                string contentType = "application/octet-stream";
                if (fileName.EndsWith(".pdf")) contentType = "application/pdf";
                else if (fileName.EndsWith(".jpg") || fileName.EndsWith(".jpeg")) contentType = "image/jpeg";
                else if (fileName.EndsWith(".png")) contentType = "image/png";

                return File(fileBytes, contentType);
            } catch (FileNotFoundException) {
                return NotFound();
            } catch (Exception ex) {
                _logger.LogError(ex, "Error retrieving file");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("url/{fileName}")]
        public async Task<IActionResult> GetPresignedUrl(string fileName) {
            try {
                var url = await _fileService.GetPresignedUrlAsync(fileName);
                return Ok(new { url });
            } catch (Exception ex) {
                _logger.LogError(ex, "Error generating presigned URL");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{fileName}")]
        public async Task<IActionResult> DeleteFile(string fileName) {
            try {
                await _fileService.DeleteFileAsync(fileName);
                return Ok(new { message = "File deleted successfully" });
            } catch (FileNotFoundException) {
                return NotFound();
            } catch (Exception ex) {
                _logger.LogError(ex, "Error deleting file");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
