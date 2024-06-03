using AutoMapper;
using Egress.Domain.Utils;
using Egress.Infra.Data.Repositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Egress.Application.Queries.Testimony.GetPaginateTestimony;

public class GetPaginateTestimonyQueryHandler : IRequestHandler<GenericGetPaginateQuery<GenericGetPaginateQueryResponse<GetPaginateTestimonyQueryResponse>>, GenericGetPaginateQueryResponse<GetPaginateTestimonyQueryResponse>>
{
    #region Constants
    private const string ORDER_BY_PROPERTY_DEFAULT = "Id";
    private const string URL_BASE_PROPERTY_NAME = "UrlBase";
    #endregion

    private readonly ITestimonyRepository _testimonyRepository;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public GetPaginateTestimonyQueryHandler(ITestimonyRepository testimonyRepository, IMapper mapper, IConfiguration configuration)
    {
        _testimonyRepository = testimonyRepository;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<GenericGetPaginateQueryResponse<GetPaginateTestimonyQueryResponse>> Handle(GenericGetPaginateQuery<GenericGetPaginateQueryResponse<GetPaginateTestimonyQueryResponse>> request, CancellationToken cancellationToken)
    {
        var paginationParameters = new PaginationParameters(request.PageNumber, request.PageSize);

        var orderByProperty = string.IsNullOrWhiteSpace(request.OrderByProperty)? ORDER_BY_PROPERTY_DEFAULT : request.OrderByProperty;
        var query = request.Query;

        var acceptedTestimonies = await _testimonyRepository.GetPaginate(
            paginationParameters, orderByProperty, query);

        var result = new GenericGetPaginateQueryResponse<GetPaginateTestimonyQueryResponse>(
            acceptedTestimonies.Select(BuildTestimonyQueryResponse),
            acceptedTestimonies.CurrentPage,
            acceptedTestimonies.PageSize,
            acceptedTestimonies.TotalCount);

        return result;
    }

    /// <summary>
    /// Build testimony response model
    /// </summary>
    /// <param name="testimony">Testimony entity</param>
    /// <returns>Testimony response</returns>
    private GetPaginateTestimonyQueryResponse BuildTestimonyQueryResponse(Domain.Entities.Testimony testimony)
    {
        var testimonyResponse = _mapper.Map<GetPaginateTestimonyQueryResponse>(testimony);
        testimonyResponse.PerfilImageSrc = string.IsNullOrWhiteSpace(testimonyResponse.PerfilImageSrc)? default : BuildStaticFileLink(testimonyResponse.PerfilImageSrc);
        return testimonyResponse;
    }

    /// <summary>
    /// Build static file link
    /// </summary>
    /// <param name="path">Local path (directory)</param>
    /// <returns>Access link</returns>
    private string BuildStaticFileLink(string path)
        => $"{_configuration[URL_BASE_PROPERTY_NAME]}/archives/{path}";
}