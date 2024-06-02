using MediatR;

namespace Egress.Application.Commands.Testimony.DeleteTestimony;

public class DeleteTestimonyCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}