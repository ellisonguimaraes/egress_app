using Egress.Application.Services;
using Egress.Domain.Entities;
using Egress.Domain.Exceptions;
using Egress.Infra.CrossCutting.Resource;
using Egress.Infra.Data.Repositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Egress.Application;

public class UpdateProfileImageCommandHandler : IRequestHandler<UpdateProfileImageCommand, string>
{
    #region Constants
    private const string BASE_PATH_PERFIL_IMAGE = "perfil-images";
    private const string URL_BASE_PROPERTY_NAME = "UrlBase";
    #endregion

    public readonly IPersonRepository _personRepository;

    private readonly IConfiguration _configuration;

    public UpdateProfileImageCommandHandler(IPersonRepository personRepository, IConfiguration configuration)
    {
        _personRepository = personRepository;
        _configuration = configuration;
    }

    public async Task<string> Handle(UpdateProfileImageCommand request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.GetByIdAsync(request.PersonId) ?? throw new BusinessException(string.Format(ErrorCodeResource.NOT_FOUND_ERROR, nameof(Person)));
        
        person.PerfilImageSrc = await FileHelpers.UploadAsync(request.PerfilImage!, BASE_PATH_PERFIL_IMAGE, person.Id.ToString());
        
        await _personRepository.UpdateAsync(person);

        return BuildStaticFileLink(person.PerfilImageSrc);
    }

    /// <summary>
    /// Build static file link
    /// </summary>
    /// <param name="path">Local path (directory)</param>
    /// <returns>Access link</returns>
    private string BuildStaticFileLink(string path)
        => $"{_configuration[URL_BASE_PROPERTY_NAME]}/archives/{path}";
}
