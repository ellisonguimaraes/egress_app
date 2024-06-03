using AutoMapper;
using Egress.Application.Commands.Person.CreateBasicPerson;
using Egress.Application.Commands.Person.CreateBasicPersonBatch;
using Egress.Application.Commands.Person.RegisterPerson;
using Egress.Application.Queries.Responses;
using Egress.Domain.Entities;

namespace Egress.Application.Profiles;

public class PersonProfile : Profile
{
    public PersonProfile()
    {
        CreateMap<Person, PersonCommandResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.ToString("d")))
            .ForMember(dest => dest.Cpf, opt => opt.MapFrom(src => src.Cpf))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.ExposeData, opt => opt.MapFrom(src => src.CanExposeData))
            .ForMember(dest => dest.CanReceiveMessage, opt => opt.MapFrom(src => src.CanReceiveMessage))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.PerfilImage, opt => opt.MapFrom(src => src.PerfilImageSrc))
            .ForMember(dest => dest.PersonType, opt => opt.MapFrom(src => src.PersonType))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new AddressCommandResponse
            {
                Id = src.Address.Id,
                Country = src.Address.Country,
                State = src.Address.State,
                IsPublic = src.Address.IsPublic,
                CreatedAt = src.Address.CreatedAt,
                UpdatedAt = src.Address.UpdatedAt
            }))
            .ForMember(dest => dest.Employment, opt => opt.MapFrom(src => new EmploymentCommandResponse
            {
                Id = src.Employment.Id,
                CreatedAt = src.Employment.CreatedAt,
                UpdatedAt = src.Employment.UpdatedAt,
                Enterprise = src.Employment.Enterprise,
                IsPublicInitiative = src.Employment.IsPublicInitiative,
                IsPublic = src.Address.IsPublic,
                Role = src.Employment.Role,
                SalaryRange = src.Employment.SalaryRange,
                StartDate = src.Employment.StartDate.ToString("d")
            }))
            .ForMember(dest => dest.Courses, opt => opt.MapFrom(src => src.PersonCourses.Select(pc => new CourseCommandResponse
            {
                Id = pc.Course.Id,
                CourseName = pc.Course.CourseName,
                BeginningSemester = pc.BeginningSemester,
                Mat = pc.Mat,
                Level = pc.Level,
                Modality = pc.Modality,
                FinalSemester = pc.FinalSemester,
                CreatedAt = pc.CreatedAt,
                UpdatedAt = pc.UpdatedAt
            })))
            .ForMember(dest => dest.Highlights, opt => opt.MapFrom(src => src.Highlights.Select(h => new HighlightsCommandResponse
            {
                Id = h.Id,
                CreatedAt = h.CreatedAt,
                UpdatedAt = h.UpdatedAt,
                Description = h.Description,
                WasAccepted = h.WasAccepted,
                Link = h.Link,
                Title = h.Title,
                AdvertisingImageSrc = h.AdvertisingImageSrc,
                VeracityFilesSrc = string.IsNullOrWhiteSpace(h.VeracityFilesSrc)? new List<string>() : h.VeracityFilesSrc.Split("|", StringSplitOptions.None).ToList()
            })))
            .ForMember(dest => dest.Testimonies, opt => opt.MapFrom(src => src.Testimonies.Select(e => new TestimonyCommandResponse
            {
                Id = e.Id,
                CreatedAt = e.CreatedAt,
                UpdatedAt = e.UpdatedAt,
                Content = e.Content,
                WasAccepted = e.WasAccepted
            })))
            .ForMember(dest => dest.ContinuingEducation, opt => opt.MapFrom(src => new ContinuingEducationCommandResponse
            {
                Id = src.ContinuingEducation.Id,
                CreatedAt = src.ContinuingEducation.CreatedAt,
                UpdatedAt = src.ContinuingEducation.UpdatedAt,
                IsPublic = src.ContinuingEducation.IsPublic,
                HasCertification = src.ContinuingEducation.HasCertification,
                HasSpecialization = src.ContinuingEducation.HasSpecialization,
                HasMasterDegree = src.ContinuingEducation.HasMasterDegree,
                HasDoctorateDegree = src.ContinuingEducation.HasDoctorateDegree
            }));

        CreateMap<RegisterPersonCommand, Person>()
            .ForMember(dest => dest.Cpf, opt => opt.MapFrom(src => src.Cpf))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
            .ForMember(dest => dest.CanExposeData, opt => opt.MapFrom(src => src.CanExposeData))
            .ForMember(dest => dest.CanReceiveMessage, opt => opt.MapFrom(src => src.CanReceiveMessage))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.PersonType, opt => opt.MapFrom(src => src.PersonType));

        CreateMap<GenericCreateBasicPersonCommand, Person>()
            .ForMember(dest => dest.Cpf, opt => opt.MapFrom(src => src.Cpf))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
            .ForMember(dest => dest.CanExposeData, opt => opt.MapFrom(src => src.CanExposeData))
            .ForMember(dest => dest.CanReceiveMessage, opt => opt.MapFrom(src => src.CanReceiveMessage))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.PersonType, opt => opt.MapFrom(src => src.PersonType));
        
        CreateMap<CreateBasicPersonCommand, Person>()
            .ForMember(dest => dest.Cpf, opt => opt.MapFrom(src => src.Cpf))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
            .ForMember(dest => dest.CanExposeData, opt => opt.MapFrom(src => src.CanExposeData))
            .ForMember(dest => dest.CanReceiveMessage, opt => opt.MapFrom(src => src.CanReceiveMessage))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.PersonType, opt => opt.MapFrom(src => src.PersonType))
            .ForMember(dest => dest.PersonCourses, opt => opt.MapFrom(src => new List<PersonCourse>()
            {
                new ()
                {
                    FinalSemester = src.Course!.FinalSemester,
                    BeginningSemester = src.Course.BeginningSemester,
                    Mat = src.Course.Mat,
                    Level = src.Course.Level,
                    Modality = src.Course.Modality,
                    CourseId = src.Course.CourseId
                }
            }));

        CreateMap<EgressCSVFile, Person>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Cpf, opt => opt.MapFrom(src => src.Cpf))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.PersonCourses, opt => opt.MapFrom(src =>
                src.CourseName == null?
                    Enumerable.Empty<PersonCourse>().ToList()
                    : new List<PersonCourse>
                    {
                        new ()
                        {
                            Mat = src.Mat,
                            BeginningSemester = src.Ingress,
                            FinalSemester = src.Egress,
                            Level = src.Level,
                            Modality = src.Modality
                        }
                    }
            ));
    }
}
