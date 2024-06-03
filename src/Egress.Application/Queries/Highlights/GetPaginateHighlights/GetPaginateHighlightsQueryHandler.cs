using AutoMapper;
using Egress.Domain.Utils;
using Egress.Infra.Data.Repositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Egress.Application.Queries.Highlights.GetPaginateHighlights;

public class GetPaginateHighlightsQueryHandler : IRequestHandler<GenericGetPaginateQuery<GenericGetPaginateQueryResponse<GetPaginateHighlightsQueryResponse>>, GenericGetPaginateQueryResponse<GetPaginateHighlightsQueryResponse>>
{
    #region Constants
    private const string ORDER_BY_PROPERTY_DEFAULT = "Id";
    private const string URL_BASE_PROPERTY_NAME = "UrlBase";
    private const string VERACITY_FILES_SEPARATOR = "|";
    #endregion

    private readonly IRepository<Domain.Entities.Highlights> _highlightsRepository;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public GetPaginateHighlightsQueryHandler(IRepository<Domain.Entities.Highlights> highlightsRepository, IMapper mapper, IConfiguration configuration)
    {
        _highlightsRepository = highlightsRepository;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<GenericGetPaginateQueryResponse<GetPaginateHighlightsQueryResponse>> Handle(GenericGetPaginateQuery<GenericGetPaginateQueryResponse<GetPaginateHighlightsQueryResponse>> request, CancellationToken cancellationToken)
    {
        var paginationParameters = new PaginationParameters(request.PageNumber, request.PageSize);

        var orderByProperty = string.IsNullOrWhiteSpace(request.OrderByProperty)? ORDER_BY_PROPERTY_DEFAULT : request.OrderByProperty;
        var query = request.Query;

        var highlights = await _highlightsRepository.GetPaginate(
            paginationParameters, orderByProperty, query);

        var result = new GenericGetPaginateQueryResponse<GetPaginateHighlightsQueryResponse>(
            highlights.Select(BuildHighlightsQueryResponse),
            highlights.CurrentPage,
            highlights.PageSize,
            highlights.TotalCount);

        return result;
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