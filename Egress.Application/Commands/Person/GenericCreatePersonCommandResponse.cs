using Newtonsoft.Json;

namespace Egress.Application.Commands.Person;

public class GenericCreatePersonCommandResponse
{
    [JsonProperty("person_id")]
    public Guid PersonId { get; set; }
}