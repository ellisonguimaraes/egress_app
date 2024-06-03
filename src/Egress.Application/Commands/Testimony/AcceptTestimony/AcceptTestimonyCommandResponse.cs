using Newtonsoft.Json;

namespace Egress.Application.Commands.Testimony.AcceptTestimony;

public class AcceptTestimonyCommandResponse
{
    [JsonProperty("id")]
    public Guid Id { get; set; }

    [JsonProperty("was_accepted")]
    public bool WasAccepted { get; set; }
}