using MediatR;
using Newtonsoft.Json;

namespace Egress.Application.Queries;

public class GenericGetRandomQuery<TResponse> : IRequest<TResponse>
{
    [JsonProperty("quantity")]
    public int Quantity { get; set; }
}