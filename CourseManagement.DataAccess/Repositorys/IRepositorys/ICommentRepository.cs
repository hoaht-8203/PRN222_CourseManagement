using CourseManagement.Model.DTOs;
using CourseManagement.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.DataAccess.Repositorys.IRepositorys
{
    public interface ICommentRepository
    {
        public Task CreateComment(Comment comment);
        public Task UpdateComment(Comment comment);
        public Task<List<Comment>> GetComments(int lessonId);
    }
}
