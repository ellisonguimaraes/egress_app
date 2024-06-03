using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Egress.Application.Commands.Testimony.RequestForTestimony;

public record RequestForTestimonyCommand : IRequest<RequestForTestimonyCommandResponse>
{
    [BindProperty(Name = "person_id")]
    public Guid PersonId { get; set; }

    [BindProperty(Name = "content")]
    public string Content { get; set; }
}