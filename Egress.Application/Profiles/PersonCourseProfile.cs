using AutoMapper;
using Egress.Application.Commands.Person.CreateBasicPerson;
using Egress.Domain.Entities;
using Egress.Application.Queries.Person.GetPaginateEgress;

namespace Egress.Application.Profiles;

public class PersonCourseProfile : Profile
{
    public PersonCourseProfile()
    {
        CreateMap<CourseEntryModel, PersonCourse>()
            .ForMember(dest => dest.BeginningSemester, opt => opt.MapFrom(src => src.BeginningSemester))
            .ForMember(dest => dest.FinalSemester, opt => opt.MapFrom(src => src.FinalSemester))
            .ForMember(dest => dest.Mat, opt => opt.MapFrom(src => src.Mat))
            .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level))
            .ForMember(dest => dest.Modality, opt => opt.MapFrom(src => src.Modality))
            .ForMember(dest => dest.CourseId, opt => opt.MapFrom(src => src.CourseId));

        CreateMap<PersonCourse, GetPaginateEgressQueryResponse>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Person.Name))
            .ForMember(dest => dest.ExposeData, opt => opt.MapFrom(src => src.Person.CanExposeData))
            .ForMember(dest => dest.Mat, opt => opt.MapFrom(src => src.Mat))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Person.Id))
            .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level))
            .ForMember(dest => dest.Modality, opt => opt.MapFrom(src => src.Modality))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Person.Email))
            .ForMember(dest => dest.FinalSemester, opt => opt.MapFrom(src => src.FinalSemester))
            .ForMember(dest => dest.Course, opt => opt.MapFrom(src => src.Course.CourseName));
    }
}
