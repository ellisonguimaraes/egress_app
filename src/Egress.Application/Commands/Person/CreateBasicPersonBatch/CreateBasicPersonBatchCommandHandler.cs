using AutoMapper;
using Egress.Application.Services;
using Egress.Domain.Entities;
using Egress.Domain.Enums;
using Egress.Domain.Exceptions;
using Egress.Domain.Utils;
using Egress.Infra.CrossCutting.Resource;
using Egress.Infra.Data.Repositories.Interfaces;
using MediatR;

namespace Egress.Application.Commands.Person.CreateBasicPersonBatch;

public class CreateBasicPersonBatchCommandHandler : IRequestHandler<CreateBasicPersonBatchCommand, IEnumerable<CreateBasicPersonBatchCommandResponse>>
{
    #region Constants
    private const string USER_WITH_THIS_CPF_ALREADY_EXISTS = "User with this CPF";
    #endregion

    private readonly IPersonRepository _personRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IMapper _mapper;

    public CreateBasicPersonBatchCommandHandler(IPersonRepository personRepository, ICourseRepository courseRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _courseRepository = courseRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CreateBasicPersonBatchCommandResponse>> Handle(CreateBasicPersonBatchCommand request, CancellationToken cancellationToken)
    {
        var response = new List<CreateBasicPersonBatchCommandResponse>();

        var egressCsvList = CsvUtils.ReadCsvToList<EgressCSVFile>(request.Batch!.OpenReadStream());

        foreach (var egress in egressCsvList)
        {
            var result = new CreateBasicPersonBatchCommandResponse
            {
                Name = egress.Name,
                Cpf = egress.Cpf,
                Successfully = true,
                ErrorMessage = default
            };

            try
            {
                await CreatePersonAsync(egress);
            }
            catch (Exception ex)
            {
                result.Successfully = false;
                result.ErrorMessage = ex.Message;
            }

            response.Add(result);
        }

        return response;
    }

    /// <summary>
    /// Create person by egress file model
    /// </summary>
    /// <param name="egress">Egress file model</param>
    /// <returns>Created person</returns>
    private async Task CreatePersonAsync(EgressCSVFile egress)
    {
        if (await _personRepository.GetByCpfAsync(egress.Cpf) is not null)
            throw new BusinessException(string.Format(ErrorCodeResource.ALREADY_EXISTS, USER_WITH_THIS_CPF_ALREADY_EXISTS, string.Empty));

        var course = await GetCourseByNameAsync(egress.CourseName);

        var person = _mapper.Map<Domain.Entities.Person>(egress);
        
        if (person.PersonCourses.Any())
            person.PersonCourses.First().CourseId = course.Id;
        
        person.CanExposeData = false;
        person.CanReceiveMessage = false;

        await _personRepository.CreateAsync(person);
    }

    /// <summary>
    /// Get course by name
    /// </summary>
    /// <param name="courseName">Course name</param>
    private async Task<Course> GetCourseByNameAsync(string courseName)
    {
        var course = await _courseRepository.GetByNormalizedNameAsync(courseName.NormalizeStringCustom());

        if (course is null)
            throw new BusinessException(string.Format(ErrorCodeResource.NOT_FOUND_ERROR, $"Course name {courseName}"));
        
        return course;
    }
}

