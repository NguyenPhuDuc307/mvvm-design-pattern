using AutoMapper;
using CourseManagement.Data.Entities;

namespace CourseManagement.ViewModels.AutoMapper
{

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Course, CourseViewModel>();
            CreateMap<CourseViewModel, Course>();
            CreateMap<CourseRequest, Course>();
        }
    }
}