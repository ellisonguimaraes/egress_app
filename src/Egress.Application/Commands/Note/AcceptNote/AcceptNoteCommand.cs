using Egress.Application.Queries.Responses;
using MediatR;
using Newtonsoft.Json;

namespace Egress.Application.Commands.Note.AcceptNote;

public class AcceptNoteCommand : IRequest<NoteCommandResponse>
{
    [JsonProperty("id")]
    public Guid Id { get; set; }
}