using Newtonsoft.Json;

namespace Egress.Application.Queries.Responses;

public class NoteCommandResponse : BaseCommandResponse
{
    [JsonProperty("title")]
    public string Title { get; set; }
    
    [JsonProperty("content")]
    public string Content { get; set; }

    [JsonProperty("was_accepted")]
    public bool WasAccepted { get; set; }
}