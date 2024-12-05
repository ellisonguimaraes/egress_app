using AutoMapper;
using Egress.Domain.Utils;
using Egress.Infra.Data.Repositories.Interfaces;
using MediatR;

namespace Egress.Application.Queries.Note.GetPaginateNote;

public class GetPaginateNoteQueryHandler : IRequestHandler<GenericGetPaginateQuery<GenericGetPaginateQueryResponse<NoteQueryResponse>>, GenericGetPaginateQueryResponse<NoteQueryResponse>>
{
    #region Constants
    private const string ORDER_BY_PROPERTY_DEFAULT = "CreatedAt";
    #endregion
    
    private readonly IRepository<Domain.Entities.Note> _notesRepository;
    private readonly IMapper _mapper;
    
    public GetPaginateNoteQueryHandler(IRepository<Domain.Entities.Note> notesRepository, IMapper mapper)
    {
        _notesRepository = notesRepository;
        _mapper = mapper;
    }
    
    public async Task<GenericGetPaginateQueryResponse<NoteQueryResponse>> Handle(GenericGetPaginateQuery<GenericGetPaginateQueryResponse<NoteQueryResponse>> request, CancellationToken cancellationToken)
    {
        var paginationParameters = new PaginationParameters(request.PageNumber, request.PageSize);

        var orderByProperty = string.IsNullOrWhiteSpace(request.OrderByProperty)? ORDER_BY_PROPERTY_DEFAULT : request.OrderByProperty;
        var query = request.Query;

        var notes = await _notesRepository.GetPaginate(
            paginationParameters, orderByProperty, query);

        var result = new GenericGetPaginateQueryResponse<NoteQueryResponse>(
            notes.Select(BuildPaginateNoteQueryResponse),
            notes.CurrentPage,
            notes.PageSize,
            notes.TotalCount);

        return result;
    }

    private NoteQueryResponse BuildPaginateNoteQueryResponse(Domain.Entities.Note note)
    {
        var noteQueryResponse = _mapper.Map<NoteQueryResponse>(note);
        
        if (noteQueryResponse.Content.Length > 140)
            noteQueryResponse.Content = noteQueryResponse.Content[..140] + "...";
        
        return noteQueryResponse;
    }
}