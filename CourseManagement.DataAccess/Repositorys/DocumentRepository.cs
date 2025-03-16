using CourseManagement.DataAccess.Data;
using CourseManagement.DataAccess.Repositorys.IRepositorys;
using CourseManagement.Model.DTOs;
using CourseManagement.Model.Model;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.DataAccess.Repositorys
{
    public class DocumentRepository : IDocumentRepository {
        private readonly CourseManagementDb _db;

        public DocumentRepository(CourseManagementDb db) {
            _db = db;
        }

        public async Task<DocumentResponse> AddDocument(AddDocumentRequest req) {
            var lesson = await _db.Lessons.FindAsync(req.LessonId);
            if (lesson == null)
                throw new ArgumentException("Lesson not found");

            var document = new Document {
                Name = req.Name,
                Type = req.Type,
                File = req.File,
                FileSize = req.FileSize,
                LessonId = req.LessonId
            };

            await _db.Documents.AddAsync(document);
            await _db.SaveChangesAsync();

            return new DocumentResponse {
                Id = document.Id,
                Name = document.Name,
                Type = document.Type,
                File = document.File,
                FileSize = document.FileSize,
                UploadedAt = document.UploadedAt,
                LessonId = document.LessonId
            };
        }

        public async Task<List<DocumentResponse>> GetDocumentsByLesson(int lessonId) {
            return await _db.Documents
                .Where(d => d.LessonId == lessonId)
                .Select(d => new DocumentResponse {
                    Id = d.Id,
                    Name = d.Name,
                    Type = d.Type,
                    File = d.File,
                    FileSize = d.FileSize,
                    UploadedAt = d.UploadedAt,
                    LessonId = d.LessonId
                })
                .ToListAsync();
        }

        public async Task RemoveDocument(RemoveDocumentRequest req) {
            var document = await _db.Documents.SingleOrDefaultAsync(d => d.LessonId == req.LessonId && d.File == req.File) 
                ?? throw new ArgumentException("Document not found");
            _db.Documents.Remove(document);
            await _db.SaveChangesAsync();
        }
    }
}
