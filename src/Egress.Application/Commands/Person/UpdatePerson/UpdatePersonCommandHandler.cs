using AutoMapper;
using Egress.Application.Commands.Person;
using Egress.Domain.Entities;
using Egress.Domain.Exceptions;
using Egress.Infra.CrossCutting.Resource;
using Egress.Infra.Data.Repositories.Interfaces;
using MediatR;

namespace Egress.Application;

public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, GenericCreatePersonCommandResponse>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public UpdatePersonCommandHandler(
        IPersonRepository personRepository,
        IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }
    
    public async Task<GenericCreatePersonCommandResponse> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.GetByIdAsync((Guid)request.Id!) ?? throw new BusinessException(string.Format(ErrorCodeResource.NOT_FOUND_ERROR, nameof(Person)));
        
        var personRequest = _mapper.Map<Person>(request);
        personRequest.PerfilImageSrc = person.PerfilImageSrc;
        
        person = await _personRepository.UpdateAsync(personRequest);
        
        return new GenericCreatePersonCommandResponse { PersonId = person.Id };
    }
}
