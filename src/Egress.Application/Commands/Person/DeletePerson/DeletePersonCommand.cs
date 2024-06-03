using MediatR;

namespace Egress.Application.Commands.Person.DeletePerson;

public class DeletePersonCommand : IRequest<bool>
{
    public Guid PersonId { get; set; }
}