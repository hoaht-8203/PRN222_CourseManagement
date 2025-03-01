using CourseManagement.Model.DTOs;

namespace CourseManagement.DataAccess.Repositorys.IRepositorys {
    public interface ICourseRepository {
        Task CreateCourse(AddCourseRequest request);
        Task UpdateCourse(UpdateCourseRequest request);
        Task RemoveCourse(RemoveCourseRequest request);
        Task<List<SearchCourseResponse>> Search(SearchCourseRequest request);
        Task<DetailCourseResponse> Detail(DetailCourseRequest request);
    }
}
