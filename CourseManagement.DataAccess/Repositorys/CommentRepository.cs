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
            var comments = await _context.Comments.Where(c => c.LessonId == lessonId && !c.isDisabled).Include(c => c.User).ToListAsync();
            return comments;
        }
        public async Task<List<Comment>> GetCommentsByBlog(int blogId)
        {
            var comments = await _context.Comments.Where(c => c.BlogId == blogId && !c.isDisabled).Include(c => c.User).ToListAsync();
            return comments;
        }

        public async Task UpdateComment(Comment comment)
        {
            var commentFromDb = await _context.Comments.FirstOrDefaultAsync(c => c.Id == comment.Id);

            await _context.SaveChangesAsync();

        }
    }
}
