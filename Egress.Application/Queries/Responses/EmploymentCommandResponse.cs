using Newtonsoft.Json;

namespace Egress.Application.Queries.Responses;

public class EmploymentCommandResponse : BaseCommandResponse
{
    [JsonProperty("role")]
    public string? Role { get; set; }

    [JsonProperty("enterprise")]
    public string? Enterprise { get; set; }

    [JsonProperty("salary_range")]
    public decimal? SalaryRange { get; set; }

    [JsonProperty("is_public_initiative")]
    public bool? IsPublicInitiative { get; set; }
    
    [JsonProperty("is_public")] 
    public bool? IsPublic { get; set; }

    [JsonProperty("start_date")]
    public string? StartDate { get; set; }
}
