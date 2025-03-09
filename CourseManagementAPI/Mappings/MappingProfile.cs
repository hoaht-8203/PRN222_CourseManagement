using AutoMapper;
using CourseManagement.Model.DTOs;
using CourseManagement.Model.Model;

namespace CourseManagementAPI.Mappings {
    public class MappingProfile : Profile {
        public MappingProfile() {
            CreateMap<Course, DetailCourseResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.LevelName, opt => opt.MapFrom(src => src.Level.ToString()))
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.CourseType.ToString()))
                .ForMember(dest => dest.LearningOutcomes, opt => opt.Ignore());

            CreateMap<Module, DetailCourseResponse.Module>()
                .ForMember(dest => dest.CourseId, opt => opt.MapFrom(src => src.CourseId.ToString()));

            CreateMap<Lesson, DetailCourseResponse.Lesson>();
        }
    }
}
