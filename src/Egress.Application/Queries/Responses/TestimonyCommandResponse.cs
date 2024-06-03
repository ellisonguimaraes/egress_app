using Newtonsoft.Json;

namespace Egress.Application.Queries.Responses;

public class TestimonyCommandResponse : BaseCommandResponse
{
    [JsonProperty("content")]
    public string Content { get; set; }

    [JsonProperty("was_accepted")]
    public bool WasAccepted { get; set; }
}
