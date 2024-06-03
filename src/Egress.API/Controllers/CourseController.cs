using Egress.API.Models;
using Egress.Application.Queries.Course.GetAllCourses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Egress.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class CourseController : ControllerBase
{
    private readonly IMediator _mediator;

    public CourseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var courses = await _mediator.Send(new GetAllCoursesQuery());
        return Ok(new GenericHttpResponse {  Data = courses });
    }
}