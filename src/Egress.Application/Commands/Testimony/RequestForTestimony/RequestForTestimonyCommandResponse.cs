using System.Text.Json.Serialization;

namespace Egress.Application.Commands.Testimony.RequestForTestimony;

public record RequestForTestimonyCommandResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
}