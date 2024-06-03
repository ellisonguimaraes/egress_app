using Egress.Application.Queries.Responses;
using Newtonsoft.Json;

namespace Egress.Application.Queries.Testimony.GetPaginateTestimony;

public class GetPaginateTestimonyQueryResponse : TestimonyCommandResponse
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("perfil_image_src")]
    public string? PerfilImageSrc { get; set; }

    [JsonProperty("courses")]
    public IEnumerable<string> Courses { get; set; }
}