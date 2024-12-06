using AutoMapper;
using Egress.Application.Queries.Responses;
using Egress.Domain.Exceptions;
using Egress.Infra.CrossCutting.Resource;
using Egress.Infra.Data.Repositories.Interfaces;
using MediatR;

namespace Egress.Application.Commands.Note.AcceptNote;

public class AcceptNoteCommandHandler : IRequestHandler<AcceptNoteCommand, NoteCommandResponse>
{
    private readonly IRepository<Domain.Entities.Note> _notesRepository;
    private readonly IMapper _mapper;

    public AcceptNoteCommandHandler(IRepository<Domain.Entities.Note> notesRepository, IMapper mapper)
    {
        _notesRepository = notesRepository;
        _mapper = mapper;
    }
     
    public async Task<NoteCommandResponse> Handle(AcceptNoteCommand request, CancellationToken cancellationToken)
    {
        var note = await _notesRepository.GetByIdAsync(request.Id) ?? throw new BusinessException(string.Format(ErrorCodeResource.NOT_FOUND_ERROR, nameof(Domain.Entities.Note)));

        note.WasAccepted = true;
        
        note = await _notesRepository.UpdateAsync(note);
        
        return _mapper.Map<NoteCommandResponse>(note);
    }
}