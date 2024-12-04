using Egress.Application.Queries.Responses;
using Egress.Domain.Enums;
using Newtonsoft.Json;

namespace Egress.Application.Queries.Note.GetPaginateNote;

public class GetPaginateNoteQueryResponse : NoteCommandResponse
{
    [JsonProperty("author")]
    public string Author { get; set; }
    
    [JsonProperty("person_type")]
    public PersonType PersonType { get; set; }
}