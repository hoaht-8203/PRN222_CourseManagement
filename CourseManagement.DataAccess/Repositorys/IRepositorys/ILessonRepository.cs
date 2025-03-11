using CourseManagement.Model.DTOs;
using CourseManagement.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CourseManagement.Model.DTOs.ModuleDTO;

namespace CourseManagement.DataAccess.Repositorys.IRepositorys {
    public interface ILessonRepository {
        Task CreateLesson(AddLessonRequest req);
        Task UpdateLesson(UpdateLessonRequest req);
        Task RemoveLesson(RemoveLessonRequest req);
        Task ReorderLessons(ReorderLessonsRequest req);
        Task CompletedLesson(CompletedLessonRequest req, string userEmail);
        Task NotCompletedLesson(NotCompletedLessonRequest req, string userEmail);
        Task<List<SearchLessonResponse>> SearchLesson(SearchLessonRequest req);
        Task<GetLessonsCompletedResponse> GetLessonsCompleted(GetLessonsCompletedRequest req, string userEmail);
        Task<DetailLessonResponse> Detail(DetailLessonRequest req);
    }
}
