using CourseManagement.Model.DTOs;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
namespace CourseManagementAPI.Hubs
{
    public class CommentHub : Hub
    {
        public async Task SendComment(CommentDTO comment)
        {
            try
            {
                await Clients.Group(comment.LessonId.ToString()).SendAsync("ReceiveComment", comment);
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("ReceiveError", ex.Message);
            }
        }
        public async Task JoinLessonGroup(string lessonId)
        {
            await Console.Out.WriteLineAsync("");
            await Groups.AddToGroupAsync(Context.ConnectionId, lessonId);
        }
        public async Task LeaveLessonGroup(string lessonId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, lessonId);
        }

        //for blog
        public async Task SendBlogComment(CommentDTO comment)
        {
            try
            {
                await Clients.Group(comment.BlogId.ToString()).SendAsync("ReceiveBlogComment", comment);
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("ReceiveError", ex.Message);
            }
        }
        public async Task JoinBlogGroup(string blogId)
        {
            await Console.Out.WriteLineAsync("");
            await Groups.AddToGroupAsync(Context.ConnectionId, blogId);
        }
        public async Task LeaveBlogGroup(string blogId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, blogId);
        }
    }
}
