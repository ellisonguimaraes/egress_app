using AutoMapper;
using Egress.Application.Queries.Testimony.GetPaginateTestimony;
using Egress.Infra.Data.Repositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Egress.Application.Queries.Testimony.GetRandomTestimony;

public class GetRandomTestimonyQueryHandler : IRequestHandler<GenericGetRandomQuery<IEnumerable<GetPaginateTestimonyQueryResponse>>, IEnumerable<GetPaginateTestimonyQueryResponse>>
{
    #region Constants
    private const string URL_BASE_PROPERTY_NAME = "UrlBase";
    #endregion

    private readonly ITestimonyRepository _testimonyRepository;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public GetRandomTestimonyQueryHandler(ITestimonyRepository testimonyRepository, IMapper mapper, IConfiguration configuration)
    {
        _testimonyRepository = testimonyRepository;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<IEnumerable<GetPaginateTestimonyQueryResponse>> Handle(GenericGetRandomQuery<IEnumerable<GetPaginateTestimonyQueryResponse>> request, CancellationToken cancellationToken)
    {
        var randomTestimonies = await _testimonyRepository.GetRandom(request.Quantity);
        return randomTestimonies.Select(BuildTestimonyQueryResponse);
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