using Egress.Application.Queries.Responses;
using Egress.Domain.Enums;
using Newtonsoft.Json;

namespace Egress.Application.Queries.Person.GetPaginateEgress;

public class GetPaginateEgressQueryResponse
{
    [JsonProperty("id")]
    public Guid Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("expose_data")]
    public bool ExposeData { get; set; }

    [JsonProperty("final_semester")]
    public string? FinalSemester { get; set; }

    [JsonProperty("mat")]
    public string Mat { get; set; }

    [JsonProperty("level")]
    public Level Level { get; set; }

    [JsonProperty("modality")]
    public Modality Modality { get; set; }

    [JsonProperty("course")]
    public string Course { get; set; }
    
    [JsonProperty("address")]
    public AddressCommandResponse Address { get; set; }
    
    [JsonProperty("employment")]
    public EmploymentCommandResponse? Employment { get; set; }
    
    [JsonProperty("continuing_education")]
    public ContinuingEducationCommandResponse ContinuingEducation { get; set; }
}