using AutoMapper;
using Egress.Application.Services;
using Egress.Infra.Data.Repositories.Interfaces;
using MediatR;

namespace Egress.Application.Commands.Highlights.RequestForHighlights;

public class RequestForHighlightsCommandHandler : IRequestHandler<RequestForHighlightsCommand, RequestForHighlightsCommandResponse>
{
    #region Constants
    private const string BASE_PATH_ADVERTISING_IMAGE = "highlights/advertising-image";
    private const string BASE_PATH_VERACITY_FILES = "highlights/veracity-files";
    private const string VERACITY_FILES_SEPARATOR = "|";
    #endregion

    private readonly IRepository<Domain.Entities.Highlights> _highlightsRepository;
    private readonly IMapper _mapper;

    public RequestForHighlightsCommandHandler(IRepository<Domain.Entities.Highlights> highlightsRepository, IMapper mapper)
    {
        _highlightsRepository = highlightsRepository;
        _mapper = mapper;
    }

    public async Task<RequestForHighlightsCommandResponse> Handle(RequestForHighlightsCommand request, CancellationToken cancellationToken)
    {
        var highlights = _mapper.Map<Domain.Entities.Highlights>(request);
        highlights.WasAccepted = false;

        highlights = await _highlightsRepository.CreateAsync(highlights);

        if (request.AdvertisingImage is not null)
            highlights.AdvertisingImageSrc = await FileHelpers.UploadAsync(request.AdvertisingImage, BASE_PATH_ADVERTISING_IMAGE, highlights.Id.ToString());

        if (request.VeracityFiles is not null && request.VeracityFiles.Count > 0)
        {
            var veracityFilesSrc = new List<string>();

            for (var i = 0; i < request.VeracityFiles.Count; i++)
                veracityFilesSrc.Add(await FileHelpers.UploadAsync(request.VeracityFiles[i], $"{BASE_PATH_VERACITY_FILES}/{highlights.Id}", $"{i}"));

            highlights.VeracityFilesSrc = string.Join(VERACITY_FILES_SEPARATOR, veracityFilesSrc);
        }

        highlights = await _highlightsRepository.UpdateAsync(highlights);

        return new RequestForHighlightsCommandResponse { Id = highlights.Id };
    }
}