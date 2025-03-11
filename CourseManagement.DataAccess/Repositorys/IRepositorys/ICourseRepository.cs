using CourseManagement.Model.Constant;
using CourseManagement.Model.DTOs;

namespace CourseManagement.DataAccess.Repositorys.IRepositorys {
    public interface ICourseRepository {
        Task CreateCourse(AddCourseRequest request);
        Task UpdateCourse(UpdateCourseRequest request);
        Task RemoveCourse(RemoveCourseRequest request);
        Task UpdateStatus(UpdateStatusRequest request);
        Task EnrollCourse(EnrollCourseRequest request);
        Task<List<SearchCourseResponse>> Search(SearchCourseRequest request);
        Task<DetailCourseResponse> Detail(DetailCourseRequest request);
        Task<LearnCourseResponse> Learn(LearnCourseRequest request, string userEmail);
        Task<PreviewCourseResponse> Preview(DetailCourseRequest request, string userEmail);
    }
}
