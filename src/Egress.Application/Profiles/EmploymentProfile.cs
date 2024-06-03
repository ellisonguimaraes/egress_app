using AutoMapper;
using Egress.Application.Commands.Person.RegisterPerson;
using Egress.Domain.Entities;

namespace Egress.Application.Profiles;

public class EmploymentProfile : Profile
{
    public EmploymentProfile()
    {
        CreateMap<EmploymentEntryModel, Employment>()
            .ForMember(dest => dest.Enterprise, opt => opt.MapFrom(src => src.Enterprise))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
            .ForMember(dest => dest.SalaryRange, opt => opt.MapFrom(src => src.SalaryRange))
            .ForMember(dest => dest.IsPublicInitiative, opt => opt.MapFrom(src => src.IsPublicInitiative))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
            .ForMember(dest => dest.IsPublic, opt => opt.MapFrom(src => src.IsPublic));
    }
}
