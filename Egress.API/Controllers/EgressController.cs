using System.Text.Json;
using Egress.API.Models;
using Egress.Application.Queries;
using Egress.Application.Queries.Person.GetPaginateEgress;
using Egress.Application.Queries.Person.GetPersonByDocument;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Egress.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class EgressController : ControllerBase
{
    private readonly IMediator _mediator;

    public EgressController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetByDocumentAsync([FromQuery] GetPersonByDocumentQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(new GenericHttpResponse
        {
            Data = result
        });
    }

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetPaginateEgressAsync(
        [FromQuery(Name = "page_number")] int pageNumber,
        [FromQuery(Name = "page_size")] int pageSize,
        [FromQuery(Name = "query")] string query,
        [FromQuery(Name = "order_by")] string orderByProperty)
    {
        var command = new GenericGetPaginateQuery<GenericGetPaginateQueryResponse<GetPaginateEgressQueryResponse>>(pageNumber, pageSize, query, orderByProperty);

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
}