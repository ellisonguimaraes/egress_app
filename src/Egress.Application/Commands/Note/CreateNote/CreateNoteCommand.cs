using Egress.Application.Queries.Responses;
using MediatR;
using Newtonsoft.Json;

namespace Egress.Application.Commands.Note.CreateNote;

public class CreateNoteCommand : IRequest<NoteCommandResponse>
{
    [JsonProperty("title")]
    public string Title { get; set; }
    
    [JsonProperty("content")]
    public string Content { get; set; }
    
    [JsonProperty("person_id")]
    public Guid PersonId { get; set; }
}