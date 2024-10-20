using Egress.API.Models;
using Egress.Domain.Entities.Views;
using Egress.Domain.Exceptions;
using Egress.Infra.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace Egress.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ChartsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ChartsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("views")]
    public async Task<IActionResult> TotalByStateViewAsync([FromHeader(Name = "views")] string views)
    {
        var response = new List<ChartViewsResponse>();

        if (string.IsNullOrEmpty(views)) throw new BusinessException("Header 'views' is required");
        
        var allViews = views.ToLower().Replace(" ", string.Empty).Split(",");

        foreach (var view in allViews)
        {
            switch (view)
            {
                case "total_by_state_view":
                    response.Add(new ChartViewsResponse()
                    {
                        ViewName = view,
                        Data = await _context.TotalByStateView.ToListAsync()
                    });
                    break;
                case "total_outside_brazil":
                    response.Add(new ChartViewsResponse()
                    {
                        ViewName = view,
                        Data = await _context.TotalOutsideBrazil.ToListAsync()
                    });
                    break;
                case "total_with_certification_view":
                    response.Add(new ChartViewsResponse()
                    {
                        ViewName = view,
                        Data = await _context.TotalWithCertificationView.ToListAsync()
                    });
                    break;
                case "total_with_especialization_view":
                    response.Add(new ChartViewsResponse()
                    {
                        ViewName = view,
                        Data = await _context.TotalWithSpecializationView.ToListAsync()
                    });
                    break;
                case "total_with_master_degree_view":
                    response.Add(new ChartViewsResponse()
                    {
                        ViewName = view,
                        Data = await _context.ViewWithMasterDegree.ToListAsync()
                    });
                    break;
                case "total_with_doctorate_degree_view":
                    response.Add(new ChartViewsResponse()
                    {
                        ViewName = view,
                        Data = await _context.ViewWithDoctorateDegree.ToListAsync()
                    });
                    break;
                case "average_salary_range_view":
                    response.Add(new ChartViewsResponse()
                    {
                        ViewName = view,
                        Data = await _context.AverageSalaryRangeView.ToListAsync()
                    });
                    break;
                case "total_per_role_view":
                    response.Add(new ChartViewsResponse()
                    {
                        ViewName = view,
                        Data = await _context.TotalPerRoleView.ToListAsync()
                    });
                    break;
                case "total_per_initiative_type_view":
                    response.Add(new ChartViewsResponse()
                    {
                        ViewName = view,
                        Data = await _context.TotalPerInitiativeTypeView.ToListAsync()
                    });
                    break;
                case "total_egress_highlights_view":
                    response.Add(new ChartViewsResponse()
                    {
                        ViewName = view,
                        Data = await _context.TotalEgressHighlightsView.ToListAsync()
                    });
                    break;
                case "total_egress_testimonies_view":
                    response.Add(new ChartViewsResponse()
                    {
                        ViewName = view,
                        Data = await _context.TotalEgressTestimoniesView.ToListAsync()
                    });
                    break;
                case "average_birthdate_view":
                    response.Add(new ChartViewsResponse()
                    {
                        ViewName = view,
                        Data = await _context.AverageBirthdateView.ToListAsync()
                    });
                    break;
                case "average_birthday_to_entry_view":
                    response.Add(new ChartViewsResponse()
                    {
                        ViewName = view,
                        Data = await _context.AverageBirthdayToEntryView.ToListAsync()
                    });
                    break;
                case "average_birthday_to_exit_view":
                    response.Add(new ChartViewsResponse()
                    {
                        ViewName = view,
                        Data = await _context.AverageBirthdayToExitView.ToListAsync()
                    });
                    break;
            }
        }
        
        return Ok(new GenericHttpResponse {  Data = response });
    }
}

public class ChartViewsResponse
{
    [JsonProperty("name")]
    public string ViewName { get; set; }
    
    [JsonProperty("data")]
    public object Data { get; set; }
}