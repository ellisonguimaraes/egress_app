using MediatR;

namespace Egress.Application.Queries.Note.GetNoteById;

public sealed class GetNoteByIdQuery : IRequest<NoteQueryResponse>
{
    public Guid Id { get; set; }
}