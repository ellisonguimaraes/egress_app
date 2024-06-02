using Egress.Domain.Exceptions;
using Egress.Infra.CrossCutting.Resource;
using Egress.Infra.Data.Repositories.Interfaces;
using MediatR;

namespace Egress.Application.Commands.Highlights.AcceptHighlights;

public class AcceptHighlightsCommandHandler : IRequestHandler<GenericAcceptCommand<AcceptHighlightsCommandResponse>, AcceptHighlightsCommandResponse>
{
    private readonly IRepository<Domain.Entities.Highlights> _highlightsRepository;

    public AcceptHighlightsCommandHandler(IRepository<Domain.Entities.Highlights> highlightsRepository)
    {
        _highlightsRepository = highlightsRepository;
    }

    public async Task<AcceptHighlightsCommandResponse> Handle(GenericAcceptCommand<AcceptHighlightsCommandResponse> request, CancellationToken cancellationToken)
    {
        var highlights = await _highlightsRepository.GetByIdAsync(request.Id);

        if (highlights is null)
            throw new BusinessException(string.Format(ErrorCodeResource.NOT_FOUND_ERROR, nameof(Domain.Entities.Highlights)));

        if (highlights.WasAccepted)
            throw new BusinessException(string.Format(ErrorCodeResource.HAS_ALREADY_BEEN_ACCEPTED, nameof(Domain.Entities.Highlights)));

        highlights.WasAccepted = true;

        var result = await _highlightsRepository.UpdateAsync(highlights);

        return new AcceptHighlightsCommandResponse
        {
            Id = result.Id,
            WasAccepted = result.WasAccepted
        };
    }
}