using System.Text.Json;
using Egress.API.Models;
using Egress.Application.Commands.Note.AcceptNote;
using Egress.Application.Commands.Note.CreateNote;
using Egress.Application.Queries;
using Egress.Application.Queries.Note;
using Egress.Application.Queries.Note.GetNoteById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Egress.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class NoteController : ControllerBase
{
    private readonly IMediator _mediator;

    public NoteController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetPaginateNoteAsync(
        [FromQuery(Name = "page_number")] int pageNumber,
        [FromQuery(Name = "page_size")] int pageSize,
        [FromQuery(Name = "query")] string query,
        [FromQuery(Name = "order_by")] string orderByProperty)
    {
        var command = new GenericGetPaginateQuery<GenericGetPaginateQueryResponse<NoteQueryResponse>>(pageNumber, pageSize, query, orderByProperty);

        var result = await _mediator.Send(command);

        var metadata = new
        {
            result.TotalCount,
            result.PageSize,
            result.CurrentPage,
            result.HasNext,
            result.HasPrevious,
            result.TotalPages
        };

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));
        
        return Ok(new GenericHttpResponse
        {
            Data = result
        });
    }
    
    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetNoteByIdAsync([FromRoute(Name = "id")] Guid id)
        => await CallCommandHandlerAndBuildResponseAsync(new GetNoteByIdQuery { Id = id });
    
    [HttpPost]
    public async Task<IActionResult> CreateNoteAsync([FromBody] CreateNoteCommand command)
        => await CallCommandHandlerAndBuildResponseAsync(command);
    
    [HttpPut]
    [Route("accept/{id}")]
    public async Task<IActionResult> AcceptNoteAsync([FromRoute] Guid id)
        => await CallCommandHandlerAndBuildResponseAsync(new AcceptNoteCommand { Id = id });
    
    private async Task<IActionResult> CallCommandHandlerAndBuildResponseAsync<T>(T command)
    {
        var result = await _mediator.Send(command!);
        
        return Ok(new GenericHttpResponse{
            Data = result
        });
    }
}