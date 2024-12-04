using AutoMapper;
using Egress.Application.Queries.Note.GetPaginateNote;
using Egress.Application.Queries.Responses;
using Egress.Domain.Entities;

namespace Egress.Application.Profiles;

public class NoteProfile : Profile
{
    public NoteProfile()
    {
        CreateMap<Note, NoteCommandResponse>()
            .ForMember(n => n.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(n => n.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(n => n.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
            .ForMember(n => n.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(n => n.Content, opt => opt.MapFrom(src => src.Content))
            .ForMember(n => n.WasAccepted, opt => opt.MapFrom(src => src.WasAccepted));

        CreateMap<Note, GetPaginateNoteQueryResponse>()
            .ForMember(n => n.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(n => n.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(n => n.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
            .ForMember(n => n.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(n => n.Content, opt => opt.MapFrom(src => src.Content))
            .ForMember(n => n.WasAccepted, opt => opt.MapFrom(src => src.WasAccepted))
            .ForMember(n => n.Author, opt => opt.MapFrom(src => src.Person.Name))
            .ForMember(n => n.PersonType, opt => opt.MapFrom(src => src.Person.PersonType)); 
    }
}