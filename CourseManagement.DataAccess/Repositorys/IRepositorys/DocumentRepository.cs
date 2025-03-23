using CourseManagement.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.DataAccess.Repositorys.IRepositorys
{
    public interface IDocumentRepository
    {
        public Task<DocumentResponse> AddDocument(AddDocumentRequest req);
        public Task<List<DocumentResponse>> GetDocumentsByLesson(int lessonId);
        public Task RemoveDocument(RemoveDocumentRequest req);
    }
}
