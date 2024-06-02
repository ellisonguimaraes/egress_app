using System.Text.Json.Serialization;
using MediatR;

namespace Egress.Application.Commands;

public class GenericAcceptCommand<T> : IRequest<T>
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
}