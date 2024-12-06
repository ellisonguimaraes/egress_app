using AutoMapper;
using Egress.Application.Queries.Responses;
using Egress.Domain.Exceptions;
using Egress.Infra.CrossCutting.Resource;
using Egress.Infra.Data.Repositories.Interfaces;
using MediatR;

namespace Egress.Application.Commands.Note.CreateNote;

public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, NoteCommandResponse>
{
    private readonly IRepository<Domain.Entities.Note> _notesRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public CreateNoteCommandHandler(IRepository<Domain.Entities.Note> notesRepository, IPersonRepository personRepository, IMapper mapper)
    {
        _notesRepository = notesRepository;
        _personRepository = personRepository;
        _mapper = mapper;
    }
    
    public async Task<NoteCommandResponse> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.GetByIdAsync(request.PersonId) ?? throw new BusinessException(string.Format(ErrorCodeResource.NOT_FOUND_ERROR, nameof(Domain.Entities.Person)));
        
        var note = _mapper.Map<Domain.Entities.Note>(request);
        
        note.PersonId = person.Id;
        note.WasAccepted = false;

        note = await _notesRepository.CreateAsync(note);
        
        return _mapper.Map<NoteCommandResponse>(note);
    }
}