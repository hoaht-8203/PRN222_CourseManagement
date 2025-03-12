using CourseManagement.DataAccess.Data;
using CourseManagement.DataAccess.Repositorys.IRepositorys;
using CourseManagement.Model.DTOs;
using CourseManagement.Model.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.DataAccess.Repositorys
{
    public class CommentRepository : ICommentRepository
    {
        private readonly CourseManagementDb _context;
        public CommentRepository(CourseManagementDb context)
        {
            _context = context;
        }
        public async Task CreateComment(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

        }

        public async Task<List<Comment>> GetComments(int lessonId)
        {
            var comments = await _context.Comments.Where(c => c.LessonId == lessonId).Include(c => c.User).ToListAsync();
            return comments;
        }

        public Task UpdateComment(Comment comment)
        {
            throw new NotImplementedException();
        }
    }
}
