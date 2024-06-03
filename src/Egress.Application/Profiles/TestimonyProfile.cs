using AutoMapper;
using Egress.Application.Commands.Testimony.RequestForTestimony;
using Egress.Application.Queries.Responses;
using Egress.Application.Queries.Testimony.GetPaginateTestimony;
using Egress.Domain.Entities;

namespace Egress.Application.Profiles;

public class TestimonyProfile : Profile
{
    public TestimonyProfile()
    {
        CreateMap<Testimony, GetPaginateTestimonyQueryResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Person.Name))
            .ForMember(dest => dest.PerfilImageSrc, opt => opt.MapFrom(src => src.Person.PerfilImageSrc))
            .ForMember(dest => dest.WasAccepted, opt => opt.MapFrom(src => src.WasAccepted))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
            .ForMember(dest => dest.Courses,
                opt => opt.MapFrom(src => src.Person.PersonCourses.Select(pc => pc.Course.CourseName)));

        CreateMap<Testimony, TestimonyCommandResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
            .ForMember(dest => dest.WasAccepted, opt => opt.MapFrom(src => src.WasAccepted))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt));

        CreateMap<RequestForTestimonyCommand, Testimony>()
            .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.PersonId))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content));
    }
}