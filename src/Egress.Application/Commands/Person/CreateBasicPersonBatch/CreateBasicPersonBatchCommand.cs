using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Egress.Application.Commands.Person.CreateBasicPersonBatch;

public class CreateBasicPersonBatchCommand : IRequest<IEnumerable<CreateBasicPersonBatchCommandResponse>>
{
    [BindProperty(Name = "batch")]
    public IFormFile? Batch { get; set; }
}