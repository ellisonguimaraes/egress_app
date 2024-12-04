using AutoMapper;
using Egress.Domain.Utils;
using Egress.Infra.Data.Repositories.Interfaces;
using MediatR;

namespace Egress.Application.Queries.Note.GetPaginateNote;

public class GetPaginateNoteQueryHandler : IRequestHandler<GenericGetPaginateQuery<GenericGetPaginateQueryResponse<GetPaginateNoteQueryResponse>>, GenericGetPaginateQueryResponse<GetPaginateNoteQueryResponse>>
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
    
    public async Task<GenericGetPaginateQueryResponse<GetPaginateNoteQueryResponse>> Handle(GenericGetPaginateQuery<GenericGetPaginateQueryResponse<GetPaginateNoteQueryResponse>> request, CancellationToken cancellationToken)
    {
        var paginationParameters = new PaginationParameters(request.PageNumber, request.PageSize);

        var orderByProperty = string.IsNullOrWhiteSpace(request.OrderByProperty)? ORDER_BY_PROPERTY_DEFAULT : request.OrderByProperty;
        var query = request.Query;

        var notes = await _notesRepository.GetPaginate(
            paginationParameters, orderByProperty, query);

        var result = new GenericGetPaginateQueryResponse<GetPaginateNoteQueryResponse>(
            notes.Select(n => _mapper.Map<GetPaginateNoteQueryResponse>(n)),
            notes.CurrentPage,
            notes.PageSize,
            notes.TotalCount);

        return result;
    }
}