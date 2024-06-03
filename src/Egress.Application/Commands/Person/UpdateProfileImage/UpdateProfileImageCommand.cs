using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Egress.Application;

public class UpdateProfileImageCommand : IRequest<string>
{
    [BindProperty(Name = "person_id")]
    public Guid PersonId { get; set; }

    [BindProperty(Name = "perfil_image")]
    public IFormFile? PerfilImage { get; set; }
}
