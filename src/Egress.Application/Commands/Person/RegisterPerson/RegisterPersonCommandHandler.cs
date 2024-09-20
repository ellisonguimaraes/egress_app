using AutoMapper;
using Egress.Domain.Exceptions;
using Egress.Infra.CrossCutting.Resource;
using Egress.Infra.Data.Repositories.Interfaces;
using MediatR;

namespace Egress.Application.Commands.Person.RegisterPerson;

public class RegisterPersonCommandHandler : IRequestHandler<RegisterPersonCommand, GenericCreatePersonCommandResponse>
{
    #region Constants
    private const string USER_WITH_THIS_CPF_ALREADY_EXISTS = "User with this CPF";
    #endregion

    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public RegisterPersonCommandHandler(
        IPersonRepository personRepository,
        IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<GenericCreatePersonCommandResponse> Handle(RegisterPersonCommand request, CancellationToken cancellationToken)
    {
        if (await _personRepository.GetByCpfAsync(request.Cpf) is not null)
            throw new BusinessException(string.Format(ErrorCodeResource.ALREADY_EXISTS, USER_WITH_THIS_CPF_ALREADY_EXISTS, string.Empty));
        
        var person = _mapper.Map<Domain.Entities.Person>(request);
        person = await _personRepository.CreateAsync(person);
        
        return new GenericCreatePersonCommandResponse{
            PersonId = person.Id
        };
    }
}
