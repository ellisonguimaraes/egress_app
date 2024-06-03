using Egress.Infra.Data.Repositories.Interfaces;
using MediatR;

namespace Egress.Application.Commands.Person.DeletePerson;

public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, bool>
{
    private readonly IPersonRepository _personRepository;

    public DeletePersonCommandHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }
    
    public async Task<bool> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        await _personRepository.DeleteAsync(request.PersonId);
        return true;
    }
}