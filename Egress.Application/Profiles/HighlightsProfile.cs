using AutoMapper;
using Egress.Application.Commands.Highlights.RequestForHighlights;
using Egress.Application.Queries.Highlights.GetPaginateHighlights;
using Egress.Domain.Entities;

namespace Egress.Application.Profiles;

public class HighlightsProfile : Profile
{
    public HighlightsProfile()
    {
        CreateMap<Highlights, GetPaginateHighlightsQueryResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Person.Name))
            .ForMember(dest => dest.AdvertisingImageSrc, opt => opt.MapFrom(src => src.AdvertisingImageSrc))
            .ForMember(dest => dest.WasAccepted, opt => opt.MapFrom(src => src.WasAccepted))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
            .ForMember(dest => dest.Courses, opt => opt.MapFrom(src => src.Person.PersonCourses.Select(pc => pc.Course.CourseName)))
            .ForMember(dest => dest.PerfilImageSrc, opt => opt.MapFrom(src => src.Person.PerfilImageSrc))
            .ForMember(dest => dest.VeracityFilesSrc, opt => opt.Ignore());

        CreateMap<RequestForHighlightsCommand, Highlights>()
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Link, opt => opt.MapFrom(src => src.Link))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.PersonId));
    }
}