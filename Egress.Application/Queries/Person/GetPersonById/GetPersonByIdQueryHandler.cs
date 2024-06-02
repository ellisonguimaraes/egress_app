using AutoMapper;
using Egress.Application.Queries.Responses;
using Egress.Domain.Exceptions;
using Egress.Infra.CrossCutting.Resource;
using Egress.Infra.Data.Repositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Egress.Application.Queries.Person.GetPersonById;

public class GetPersonByIdQueryHandler : IRequestHandler<GetPersonByIdQuery, PersonCommandResponse>
{
    #region Constants
    private const string PERSON_NAME = "Person";
    private const string URL_BASE_PROPERTY_NAME = "UrlBase";
    private const string VERACITY_FILES_SEPARATOR = "|";
    #endregion

    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public GetPersonByIdQueryHandler(IPersonRepository personRepository, IMapper mapper, IConfiguration configuration)
    {
        _personRepository = personRepository;
        _mapper = mapper;
        _configuration = configuration;
        _configuration = configuration;
    }

    public async Task<PersonCommandResponse> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.GetByIdAsync(request.PersonId) ?? throw new BusinessException(string.Format(ErrorCodeResource.NOT_FOUND_ERROR, PERSON_NAME));
        return BuildPersonCommandResponse(person);
    }
    
    /// <summary>
    /// Build person response model
    /// </summary>
    /// <param name="person">Person</param>
    /// <returns>PersonCommandResponse</returns>
    private PersonCommandResponse BuildPersonCommandResponse(Domain.Entities.Person person)
    {
        var personCommandResponse = _mapper.Map<PersonCommandResponse>(person);
        
        foreach (var h in personCommandResponse.Highlights)
        {
            h.AdvertisingImageSrc = string.IsNullOrWhiteSpace(h.AdvertisingImageSrc)? default : BuildStaticFileLink(h.AdvertisingImageSrc);
            h.VeracityFilesSrc = h.VeracityFilesSrc?.Select(BuildStaticFileLink);
        }
        
        personCommandResponse.PerfilImage = string.IsNullOrWhiteSpace(personCommandResponse.PerfilImage)? default : BuildStaticFileLink(personCommandResponse.PerfilImage);

        return personCommandResponse;
    }
    
    /// <summary>
    /// Build static file link
    /// </summary>
    /// <param name="path">Local path (directory)</param>
    /// <returns>Access link</returns>
    private string BuildStaticFileLink(string path)
        => $"{_configuration[URL_BASE_PROPERTY_NAME]}/archives/{path}";
}
