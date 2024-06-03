using System.Text.Json;
using Egress.API.Models;
using Egress.Application;
using Egress.Application.Commands.Person.CreateBasicPerson;
using Egress.Application.Commands.Person.CreateBasicPersonBatch;
using Egress.Application.Commands.Person.DeletePerson;
using Egress.Application.Commands.Person.RegisterPerson;
using Egress.Application.Queries;
using Egress.Application.Queries.Person.GetPersonById;
using Egress.Application.Queries.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Egress.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class PersonController : ControllerBase
{
    private readonly IMediator _mediator;

    public PersonController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterPersonCommand command)
        => await CallCommandHandlerAndBuildResponseAsync(command);

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdatePersonCommand command)
        => await CallCommandHandlerAndBuildResponseAsync(command);

    [HttpPut]
    [Route("profile-image")]
    public async Task<IActionResult> UpdateProfileImageAsync([FromForm] UpdateProfileImageCommand command)
        => await CallCommandHandlerAndBuildResponseAsync(command);

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateBasicPersonCommand command)
        => await CallCommandHandlerAndBuildResponseAsync(command);

    [HttpPost]
    [Route("batch")]
    public async Task<IActionResult> CreateInBatchAsync([FromForm] CreateBasicPersonBatchCommand command)
        => await CallCommandHandlerAndBuildResponseAsync(command);

    [HttpGet]
    [Route("egress-per-year")]
    public async Task<IActionResult> GetCountEgressPerFinalSemesterAsync([FromQuery] GetCountEgressPerFinalSemesterQuery query)
        => await CallCommandHandlerAndBuildResponseAsync(query);

    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
        => await CallCommandHandlerAndBuildResponseAsync(new GetPersonByIdQuery { PersonId = id });

    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<IActionResult> DeletePersonAsync(Guid id)
    {
        var result = await _mediator.Send(new DeletePersonCommand() { PersonId = id });
        return NoContent();
    }
    
    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetPaginateEgressAsync(
        [FromQuery(Name = "page_number")] int pageNumber,
        [FromQuery(Name = "page_size")] int pageSize,
        [FromQuery(Name = "query")] string query,
        [FromQuery(Name = "order_by")] string orderByProperty)
    {
        var command = new GenericGetPaginateQuery<GenericGetPaginateQueryResponse<PersonCommandResponse>>(pageNumber, pageSize, query, orderByProperty);

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

    private async Task<IActionResult> CallCommandHandlerAndBuildResponseAsync<T>(T command)
    {
        var result = await _mediator.Send(command!);
        
        return Ok(new GenericHttpResponse{
            Data = result
        });
    }
}
