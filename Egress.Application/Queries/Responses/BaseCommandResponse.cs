using Newtonsoft.Json;

namespace Egress.Application.Queries.Responses;

public class BaseCommandResponse
{
    [JsonProperty("id")]
    public Guid Id { get; set; }

    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("updated_at")]
    public DateTime UpdatedAt { get; set; }
}
