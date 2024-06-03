using AutoMapper;
using Egress.Application.Commands.Person;
using Egress.Domain.Entities;
using Egress.Infra.Data.Repositories.Interfaces;
using MediatR;

namespace Egress.Application;

public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, GenericCreatePersonCommandResponse>
{
    private readonly IPersonRepository _personRepository;
    private readonly IRepository<Address> _addressRepository;
    private readonly IRepository<Employment> _employmentRepository;
    private readonly IMapper _mapper;

    public UpdatePersonCommandHandler(
        IPersonRepository personRepository,
        IRepository<Address> addressRepository,
        IRepository<Employment> employmentRepository,
        IMapper mapper)
    {
        _personRepository = personRepository;
        _addressRepository = addressRepository;
        _employmentRepository = employmentRepository;
        _mapper = mapper;
    }
    
    public async Task<GenericCreatePersonCommandResponse> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        var personRequest = _mapper.Map<Person>(request);
        
        var person = await _personRepository.UpdateAsync(personRequest);
        
        return new GenericCreatePersonCommandResponse { PersonId = person.Id };
    }
}
