using Egress.Application.Queries.Responses;
using Newtonsoft.Json;

namespace Egress.Application.Queries.Highlights.GetPaginateHighlights;

public class GetPaginateHighlightsQueryResponse : HighlightsCommandResponse
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("advertising_image_src")]
    public string? AdvertisingImageSrc { get; set; }

    [JsonProperty("veracity_files_src")]
    public IEnumerable<string> VeracityFilesSrc { get; set; }

    [JsonProperty("courses")]
    public IEnumerable<string> Courses { get; set; }

    [JsonProperty("perfil_image_src")]
    public string? PerfilImageSrc { get; set; }
}