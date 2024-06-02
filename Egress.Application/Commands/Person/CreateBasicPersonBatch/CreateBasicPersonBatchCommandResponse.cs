using Newtonsoft.Json;

namespace Egress.Application.Commands.Person.CreateBasicPersonBatch;

public class CreateBasicPersonBatchCommandResponse
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("cpf")]
    public string Cpf { get; set; }

    [JsonProperty("successfully")]
    public bool Successfully { get; set; }

    [JsonProperty("error_message")]
    public string? ErrorMessage { get; set; }
}
