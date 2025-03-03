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
        Task<List<SearchLessonResponse>> SearchLesson(SearchLessonRequest req);
        Task<DetailLessonResponse> Detail(DetailLessonRequest req);
    }
}
