using AutoMapper;
using Egress.Application.Queries.Course.GetAllCourses;
using Egress.Domain.Entities;

namespace Egress.Application.Profiles;

public sealed class CourseProfile : Profile
{
    public CourseProfile()
    {
        CreateMap<Course, GetAllCoursesQueryResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.CourseName))
            .ForMember(dest => dest.NormalizedCourseName, opt => opt.MapFrom(src => src.NormalizedCourseName));
    }
}