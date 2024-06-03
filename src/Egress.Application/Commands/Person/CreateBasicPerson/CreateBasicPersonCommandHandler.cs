using AutoMapper;
using Egress.Domain.Exceptions;
using Egress.Infra.CrossCutting.Resource;
using Egress.Infra.Data.Repositories.Interfaces;
using MediatR;

namespace Egress.Application.Commands.Person.CreateBasicPerson;

public class CreateBasicPersonCommandHandler : IRequestHandler<CreateBasicPersonCommand, GenericCreatePersonCommandResponse>
{
    #region Constants
    private const string USER_WITH_THIS_CPF_ALREADY_EXISTS = "User with this CPF";
    #endregion

    private readonly IPersonRepository _personRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IMapper _mapper;

    public CreateBasicPersonCommandHandler(IPersonRepository personRepository, ICourseRepository courseRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _courseRepository = courseRepository;
        _mapper = mapper;
    }

    public async Task<GenericCreatePersonCommandResponse> Handle(CreateBasicPersonCommand request, CancellationToken cancellationToken)
    {
        if (await _personRepository.GetByCpfAsync(request.Cpf) is not null)
            throw new BusinessException(string.Format(ErrorCodeResource.ALREADY_EXISTS, USER_WITH_THIS_CPF_ALREADY_EXISTS, string.Empty));
    
        if (request.Course is not null)
            await ValidateCourseAsync(request.Course);

        var person = _mapper.Map<Domain.Entities.Person>(request);

        person.CanExposeData = person.CanReceiveMessage = false;

        person = await _personRepository.CreateAsync(person);

        return new GenericCreatePersonCommandResponse
        {
            PersonId = person.Id
        };
    }

    /// <summary>
    /// Validate if course exist
    /// </summary>
    /// <param name="course">Course entry model</param>
    private async Task ValidateCourseAsync(CourseEntryModel course)
    {
        if (await _courseRepository.GetByIdAsync(course.CourseId) is null)
            throw new BusinessException(string.Format(ErrorCodeResource.NOT_FOUND_ERROR, $"Course identify {course.CourseId}"));
    }
}
