using Newtonsoft.Json;

namespace Egress.Application.Queries.Responses;

public sealed class ContinuingEducationCommandResponse : BaseCommandResponse
{
    [JsonProperty("is_public")]
    public bool IsPublic { get; set; }
    
    [JsonProperty("has_certification")]
    public bool HasCertification { get; set; }
    
    [JsonProperty("has_specialization")]
    public bool HasSpecialization { get; set; }
    
    [JsonProperty("has_master_degree")]
    public bool HasMasterDegree { get; set; }
    
    [JsonProperty("has_doctorate_degree")]
    public bool HasDoctorateDegree { get; set; }
}