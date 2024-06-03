using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Egress.Application.Commands.Highlights.RequestForHighlights;

public record RequestForHighlightsCommand : IRequest<RequestForHighlightsCommandResponse>
{
    [BindProperty(Name = "person_id")]
    public Guid PersonId { get; set; }

    [BindProperty(Name = "title")]
    public string Title { get; set; }

    [BindProperty(Name = "description")]
    public string Description { get; set; }

    [BindProperty(Name = "link")]
    public string? Link { get; set; }
    
    [BindProperty(Name = "advertising_image")]
    public IFormFile? AdvertisingImage { get; set; }

    [BindProperty(Name = "veracity_files")]
    public List<IFormFile>? VeracityFiles { get; set; }
}