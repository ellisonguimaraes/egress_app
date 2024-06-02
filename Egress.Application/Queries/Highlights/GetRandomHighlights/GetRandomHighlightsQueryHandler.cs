using AutoMapper;
using Egress.Application.Queries.Highlights.GetPaginateHighlights;
using Egress.Infra.Data.Repositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Egress.Application.Queries.Highlights.GetRandomHighlights;

public class GetRandomHighlightsQueryHandler : IRequestHandler<GenericGetRandomQuery<IEnumerable<GetPaginateHighlightsQueryResponse>>, IEnumerable<GetPaginateHighlightsQueryResponse>>
{
    #region Constants
    private const string URL_BASE_PROPERTY_NAME = "UrlBase";
    private const string VERACITY_FILES_SEPARATOR = "|";
    #endregion

    private readonly IRepository<Domain.Entities.Highlights> _highlightsRepository;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public GetRandomHighlightsQueryHandler(IRepository<Domain.Entities.Highlights> highlightsRepository, IMapper mapper, IConfiguration configuration)
    {
        _highlightsRepository = highlightsRepository;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<IEnumerable<GetPaginateHighlightsQueryResponse>> Handle(GenericGetRandomQuery<IEnumerable<GetPaginateHighlightsQueryResponse>> request, CancellationToken cancellationToken)
    {
        var randomHighlights = await _highlightsRepository.GetRandom(request.Quantity);
        return randomHighlights.Select(BuildHighlightsQueryResponse);
    }

    /// <summary>
    /// Build highlights response model
    /// </summary>
    /// <param name="highlights">Highlight entity</param>
    /// <returns>Highlights response</returns>
    private GetPaginateHighlightsQueryResponse BuildHighlightsQueryResponse(Domain.Entities.Highlights highlights)
    {
        var highlightsResponse = _mapper.Map<GetPaginateHighlightsQueryResponse>(highlights);
        highlightsResponse.AdvertisingImageSrc = string.IsNullOrWhiteSpace(highlightsResponse.AdvertisingImageSrc)? default : BuildStaticFileLink(highlightsResponse.AdvertisingImageSrc);
        highlightsResponse.PerfilImageSrc = string.IsNullOrWhiteSpace(highlightsResponse.PerfilImageSrc)? default : BuildStaticFileLink(highlightsResponse.PerfilImageSrc);

        if (!string.IsNullOrWhiteSpace(highlights.VeracityFilesSrc))
        {
            var filesDirectory = highlights.VeracityFilesSrc.Split(VERACITY_FILES_SEPARATOR).ToList();
            highlightsResponse.VeracityFilesSrc = filesDirectory.Select(BuildStaticFileLink);
        }

        return highlightsResponse;
    }

    /// <summary>
    /// Build static file link
    /// </summary>
    /// <param name="path">Local path (directory)</param>
    /// <returns>Access link</returns>
    private string BuildStaticFileLink(string path)
        => $"{_configuration[URL_BASE_PROPERTY_NAME]}/archives/{path}";
}