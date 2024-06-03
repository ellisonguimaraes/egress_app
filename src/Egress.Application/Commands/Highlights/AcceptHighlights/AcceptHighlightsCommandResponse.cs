using Newtonsoft.Json;

namespace Egress.Application.Commands.Highlights.AcceptHighlights;

public class AcceptHighlightsCommandResponse
{
    [JsonProperty("id")]
    public Guid Id { get; set; }

    [JsonProperty("was_accepted")]
    public bool WasAccepted { get; set; }
}