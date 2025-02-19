using CourseManagement.Model.Model;
using static CourseManagement.Model.DTOs.ModuleDTO;

namespace CourseManagement.DataAccess.Repositorys.IRepositorys {
    public interface IModuleRepository {
        Task CreateModule(AddModuleRequest req);
        Task UpdateModule(UpdateModuleRequest req);
        Task RemoveModule(RemoveModuleRequest req);
        Task<List<SearchModuleResponse>> SearchModule(SearchModuleRequest req);
        Task<DetailModuleResponse> DetailModule(DetailModuleRequest req);
    }
}
