using Egress.Application.Services;
using Egress.Infra.Data.Repositories.Interfaces;
using MediatR;

namespace Egress.Application.Commands.Highlights.DeleteHighlights;

public class DeleteHighlightsCommandHandler : IRequestHandler<DeleteHighlightsCommand, bool>
{
    #region Constants
    private const string BASE_PATH_VERACITY_FILES = "highlights/veracity-files";
    #endregion
    
    private readonly IRepository<Domain.Entities.Highlights> _highlightsRepository;

    public DeleteHighlightsCommandHandler(IRepository<Domain.Entities.Highlights> highlightsRepository)
    {
        _highlightsRepository = highlightsRepository;
    }
    
    public async Task<bool> Handle(DeleteHighlightsCommand request, CancellationToken cancellationToken)
    {
        var highlights = await _highlightsRepository.DeleteAsync(request.Id);
        
        if (!string.IsNullOrWhiteSpace(highlights.VeracityFilesSrc))
            FileHelpers.DeleteDirectory($"{BASE_PATH_VERACITY_FILES}/{highlights.Id}");
        
        if (!string.IsNullOrEmpty(highlights.AdvertisingImageSrc))
            FileHelpers.DeleteFile(highlights.AdvertisingImageSrc);
        
        return true;
    }
}