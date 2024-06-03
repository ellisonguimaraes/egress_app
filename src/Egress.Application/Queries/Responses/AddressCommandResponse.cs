using Newtonsoft.Json;

namespace Egress.Application.Queries.Responses;

public class AddressCommandResponse : BaseCommandResponse
{
    [JsonProperty("state")]
    public string? State { get; set; }

    [JsonProperty("country")]
    public string? Country { get; set; }
    
    [JsonProperty("is_public")] 
    public bool? IsPublic { get; set; }
}
