using Egress.Application;
using Egress.Application.Behaviors;
using Egress.Application.Commands.Highlights.AcceptHighlights;
using Egress.Application.Commands.Highlights.DeleteHighlights;
using Egress.Application.Commands.Highlights.RequestForHighlights;
using Egress.Application.Commands.Note.AcceptNote;
using Egress.Application.Commands.Note.CreateNote;
using Egress.Application.Commands.Person.CreateBasicPerson;
using Egress.Application.Commands.Person.CreateBasicPersonBatch;
using Egress.Application.Commands.Person.DeletePerson;
using Egress.Application.Commands.Person.RegisterPerson;
using Egress.Application.Commands.Testimony.AcceptTestimony;
using Egress.Application.Commands.Testimony.DeleteTestimony;
using Egress.Application.Commands.Testimony.RequestForTestimony;
using Egress.Application.Profiles;
using Egress.Application.Queries;
using Egress.Application.Queries.Course.GetAllCourses;
using Egress.Application.Queries.Highlights.GetPaginateHighlights;
using Egress.Application.Queries.Highlights.GetRandomHighlights;
using Egress.Application.Queries.Note.GetPaginateNote;
using Egress.Application.Queries.Person.GetPaginateEgress;
using Egress.Application.Queries.Person.GetPaginatePerson;
using Egress.Application.Queries.Person.GetPersonByDocument;
using Egress.Application.Queries.Person.GetPersonById;
using Egress.Application.Queries.Testimony.GetPaginateTestimony;
using Egress.Application.Queries.Testimony.GetRandomTestimony;
using Egress.Application.Validators;
using Egress.Domain;
using Egress.Domain.Entities;
using Egress.Infra.Data.Repositories;
using Egress.Infra.Data.Repositories.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Egress.Infra.CrossCutting.IoC;

/// <summary>
/// Service collection extensions
/// </summary>
public static class ServiceCollectionExtensions
{
    #region Constants
    private const bool ASSUME_DEFAULT_VERSION_WHEN_UNSPECIFIED = true;
    private const string API_DEFAULT_VERSION_PROPERTY = "ApiDefaultVersion";
    private const bool REPORT_API_VERSIONS = true;
    private const string HEADER_API_VERSION = "X-Version";
    private const string QUERY_STRING_API_VERSION = "api-version";
    private const string MEDIA_TYPE_API_VERSION = "ver";
    private const string FORMAT_API_VERSION = "'v'VVV";
    private const bool SUBSTITUTE_API_VERSION_IN_URL = true;
    private const string DOT = ".";
    #endregion
    
    /// <summary>
    /// Register application services
    /// </summary>
    /// <param name="services">IServiceCollection object</param>
    /// <param name="configuration">Configuration file ~ appsettings</param>
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        // AutoMapper
        services.AddAutoMapper(typeof(PersonProfile));
        services.AddAutoMapper(typeof(TestimonyProfile));
        services.AddAutoMapper(typeof(HighlightsProfile));
        services.AddAutoMapper(typeof(AddressProfile));
        services.AddAutoMapper(typeof(EmploymentProfile));
        services.AddAutoMapper(typeof(PersonCourseProfile));
        services.AddAutoMapper(typeof(ContinuingEducationProfile));
        services.AddAutoMapper(typeof(CourseProfile));
        services.AddAutoMapper(typeof(NoteProfile));

        // Behaviors
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        
        // Commands
        services.AddMediatR(typeof(AcceptTestimonyCommandHandler).Assembly);
        services.AddMediatR(typeof(AcceptHighlightsCommandHandler).Assembly);
        services.AddMediatR(typeof(RequestForHighlightsCommandHandler).Assembly);
        services.AddMediatR(typeof(RequestForTestimonyCommandHandler).Assembly);
        services.AddMediatR(typeof(CreateBasicPersonCommandHandler).Assembly);
        services.AddMediatR(typeof(CreateBasicPersonBatchCommandHandler).Assembly);   
        services.AddMediatR(typeof(UpdateProfileImageCommandHandler).Assembly);      
        services.AddMediatR(typeof(UpdatePersonCommandHandler).Assembly);      
        services.AddMediatR(typeof(DeleteHighlightsCommandHandler).Assembly);      
        services.AddMediatR(typeof(DeleteTestimonyCommandHandler).Assembly);      
        services.AddMediatR(typeof(DeletePersonCommandHandler).Assembly);    
        services.AddMediatR(typeof(CreateNoteCommandHandler).Assembly);    
        services.AddMediatR(typeof(AcceptNoteCommandHandler).Assembly);    
        
        // Queries
        services.AddMediatR(typeof(GetPersonByDocumentQueryHandler).Assembly);
        services.AddMediatR(typeof(GetPaginateEgressQueryHandler).Assembly);
        services.AddMediatR(typeof(GetPaginateTestimonyQueryHandler).Assembly);
        services.AddMediatR(typeof(GetPaginateHighlightsQueryHandler).Assembly);
        services.AddMediatR(typeof(GetRandomTestimonyQueryHandler).Assembly);
        services.AddMediatR(typeof(GetRandomHighlightsQueryHandler).Assembly);
        services.AddMediatR(typeof(GetCountEgressPerFinalSemesterQueryHandler).Assembly);
        services.AddMediatR(typeof(GetPersonByIdQueryHandler).Assembly);
        services.AddMediatR(typeof(GetPaginatePersonQueryHandler).Assembly);
        services.AddMediatR(typeof(GetAllCoursesQueryHandler).Assembly);
        services.AddMediatR(typeof(GetPaginateNoteQueryHandler).Assembly);

        // Repositories
        services.AddScoped<ITestimonyRepository, TestimonyRepository>();
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<IPersonCourseRepository, PersonCourseRepository>();
        services.AddScoped<IRepository<Highlights>, HighlightsRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<IRepository<Address>, AddressRepository>();
        services.AddScoped<IRepository<Employment>, EmploymentRepository>();
        services.AddScoped<IRepository<Note>, NoteRepository>();

        // Validators
        services.AddScoped<IValidator<GenericGetRandomQuery<IEnumerable<GetPaginateTestimonyQueryResponse>>>, GenericGetRandomQueryValidator<IEnumerable<GetPaginateTestimonyQueryResponse>>>();
        services.AddScoped<IValidator<GenericGetRandomQuery<IEnumerable<GetPaginateHighlightsQueryResponse>>>, GenericGetRandomQueryValidator<IEnumerable<GetPaginateHighlightsQueryResponse>>>();
        services.AddScoped<IValidator<RequestForHighlightsCommand>, RequestForHighlightsCommandValidator>();
        services.AddScoped<IValidator<RequestForTestimonyCommand>, RequestForTestimonyCommandValidator>();
        services.AddScoped<IValidator<RegisterPersonCommand>, RegisterPersonCommandValidator>();
        services.AddScoped<IValidator<CreateBasicPersonCommand>, CreateBasicPersonCommandValidator>();
        services.AddScoped<IValidator<CreateBasicPersonBatchCommand>, CreateBasicPersonBatchCommandValidator>();
        services.AddScoped<IValidator<UpdateProfileImageCommand>, UpdateProfileImageCommandValidator>();
        services.AddScoped<IValidator<UpdatePersonCommand>, UpdatePersonCommandValidator>();
        
        // Settings
        var aesSettings = configuration.GetSection(nameof(AesSettings)).Get<AesSettings>();
        services.AddSingleton(aesSettings);
    }
    
    /// <summary>
    /// Register versioning services
    /// </summary>
    /// <param name="services">Service Collection</param>
    /// <param name="configuration">Configuration file ~ appsettings</param>
    public static void AddApiVersioningConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var defaultVersion = configuration[API_DEFAULT_VERSION_PROPERTY]!.Split(DOT);
        var majorVersion = int.Parse(defaultVersion.First());
        var minorVersion = int.Parse(defaultVersion.Last());

        services.AddApiVersioning(options => {
            options.AssumeDefaultVersionWhenUnspecified = ASSUME_DEFAULT_VERSION_WHEN_UNSPECIFIED;
            options.DefaultApiVersion = new ApiVersion(majorVersion, minorVersion);
            options.ReportApiVersions = REPORT_API_VERSIONS;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new QueryStringApiVersionReader(QUERY_STRING_API_VERSION),
                new HeaderApiVersionReader(HEADER_API_VERSION),
                new MediaTypeApiVersionReader(MEDIA_TYPE_API_VERSION)
            );
        });
        
        services.AddVersionedApiExplorer(setup => {
            setup.GroupNameFormat = FORMAT_API_VERSION;
            setup.SubstituteApiVersionInUrl = SUBSTITUTE_API_VERSION_IN_URL;
        });
    }
}