using Newtonsoft.Json;

namespace Egress.Application.Queries.Responses;

public class HighlightsCommandResponse : BaseCommandResponse
{
    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("link")]
    public string? Link { get; set; }

    [JsonProperty("was_accepted")]
    public bool WasAccepted { get; set; }

    [JsonProperty("advertising_image_src")]
    public string? AdvertisingImageSrc { get; set; }

    [JsonProperty("veracity_files_src")]
    public IEnumerable<string>? VeracityFilesSrc { get; set; }
}
