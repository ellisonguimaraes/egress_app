using System.Text.Json;
using Egress.API.Models;
using Egress.Application.Commands;
using Egress.Application.Commands.Testimony.AcceptTestimony;
using Egress.Application.Commands.Testimony.DeleteTestimony;
using Egress.Application.Commands.Testimony.RequestForTestimony;
using Egress.Application.Queries;
using Egress.Application.Queries.Testimony.GetPaginateTestimony;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Egress.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class TestimonyController : ControllerBase
{
    private readonly IMediator _mediator;

    public TestimonyController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetPaginateAsync(
        [FromQuery(Name = "page_number")] int pageNumber,
        [FromQuery(Name = "page_size")] int pageSize,
        [FromQuery(Name = "query")] string query,
        [FromQuery(Name = "order_by")] string orderByProperty)
    {
        var command = new GenericGetPaginateQuery<GenericGetPaginateQueryResponse<GetPaginateTestimonyQueryResponse>>(pageNumber, pageSize, query, orderByProperty);

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
    [Route("random/{quantity:int}")]
    public async Task<IActionResult> GetRandomAsync([FromRoute] GenericGetRandomQuery<IEnumerable<GetPaginateTestimonyQueryResponse>> query)
    {
        var result = await _mediator.Send(query);

        return Ok(new GenericHttpResponse
        {
            Data = result
        });
    }

    [HttpPut]
    [Route("accept/{id}")]
    public async Task<IActionResult> AcceptAsync([FromRoute] GenericAcceptCommand<AcceptTestimonyCommandResponse> request)
    {
        var result = await _mediator.Send(request);

        return Ok(new GenericHttpResponse
        {
            Data = result
        });
    }

    [HttpPost]
    [Route("request")]
    public async Task<IActionResult> RequestAsync([FromBody] RequestForTestimonyCommand request, [FromHeader(Name = "Person-Id")] Guid PersonId)
    {
        request.PersonId = PersonId;

        var result = await _mediator.Send(request);

        return Ok(new GenericHttpResponse
        {
            Data = result
        });
    }
    
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        await _mediator.Send(new DeleteTestimonyCommand { Id = id });
        return NoContent();
    }
}