using Egress.Application.Queries.Responses;
using MediatR;

namespace Egress.Application.Queries.Person.GetPersonById;

public class GetPersonByIdQuery : IRequest<PersonCommandResponse>
{
    public Guid PersonId { get; set; }
}
