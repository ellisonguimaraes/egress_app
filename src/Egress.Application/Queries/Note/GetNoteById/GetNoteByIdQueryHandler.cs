using AutoMapper;
using Egress.Domain.Exceptions;
using Egress.Infra.CrossCutting.Resource;
using Egress.Infra.Data.Repositories.Interfaces;
using MediatR;

namespace Egress.Application.Queries.Note.GetNoteById;

public class GetNoteByIdQueryHandler : IRequestHandler<GetNoteByIdQuery, NoteQueryResponse>
{
    private readonly IRepository<Domain.Entities.Note> _notesRepository;
    private readonly IMapper _mapper;

    public GetNoteByIdQueryHandler(IRepository<Domain.Entities.Note> notesRepository, IMapper mapper)
    {
        _notesRepository = notesRepository;
        _mapper = mapper;
    }
    
    public async Task<NoteQueryResponse> Handle(GetNoteByIdQuery request, CancellationToken cancellationToken)
    {
        var note = await _notesRepository.GetByIdAsync(request.Id) ?? throw new BusinessException(string.Format(ErrorCodeResource.NOT_FOUND_ERROR, nameof(Domain.Entities.Note)));
        return _mapper.Map<NoteQueryResponse>(note);
    }
}