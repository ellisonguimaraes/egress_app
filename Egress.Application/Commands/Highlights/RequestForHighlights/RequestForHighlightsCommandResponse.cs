using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Egress.Application.Commands.Highlights.RequestForHighlights;

public record RequestForHighlightsCommandResponse
{
    [JsonProperty("id")]
    public Guid Id { get; set; }
}