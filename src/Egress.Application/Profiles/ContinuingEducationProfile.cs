using AutoMapper;
using Egress.Application.Commands.Person.RegisterPerson;
using Egress.Domain.Entities;

namespace Egress.Application.Profiles;

public class ContinuingEducationProfile : Profile
{
    public ContinuingEducationProfile()
    {
        CreateMap<ContinuingEducationEntryModel, ContinuingEducation>()
            .ForMember(dest => dest.IsPublic, opt => opt.MapFrom(src => src.IsPublic))
            .ForMember(dest => dest.HasCertification, opt => opt.MapFrom(src => src.HasCertification))
            .ForMember(dest => dest.HasSpecialization, opt => opt.MapFrom(src => src.HasSpecialization))
            .ForMember(dest => dest.HasMasterDegree, opt => opt.MapFrom(src => src.HasMasterDegree))
            .ForMember(dest => dest.HasDoctorateDegree, opt => opt.MapFrom(src => src.HasDoctorateDegree));
    }
}