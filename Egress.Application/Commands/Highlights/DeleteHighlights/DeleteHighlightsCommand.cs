using MediatR;

namespace Egress.Application.Commands.Highlights.DeleteHighlights;

public class DeleteHighlightsCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}