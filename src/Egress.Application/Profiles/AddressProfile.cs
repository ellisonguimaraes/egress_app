using AutoMapper;
using Egress.Application.Commands.Person.RegisterPerson;
using Egress.Domain.Entities;

namespace Egress.Application.Profiles;

public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<AddressEntryModel, Address>()
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
            .ForMember(dest => dest.IsPublic, opt => opt.MapFrom(src => src.IsPublic));
    }
}
