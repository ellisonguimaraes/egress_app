using AutoMapper;
using Egress.Application.Queries.Responses;
using Egress.Domain.Utils;
using Egress.Infra.Data.Repositories.Interfaces;
using MediatR;

namespace Egress.Application.Queries.Person.GetPaginatePerson;

public class GetPaginatePersonQueryHandler : IRequestHandler<GenericGetPaginateQuery<GenericGetPaginateQueryResponse<PersonCommandResponse>>, GenericGetPaginateQueryResponse<PersonCommandResponse>>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public GetPaginatePersonQueryHandler(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }
    
    public async Task<GenericGetPaginateQueryResponse<PersonCommandResponse>> Handle(GenericGetPaginateQuery<GenericGetPaginateQueryResponse<PersonCommandResponse>> request, CancellationToken cancellationToken)
    {
        var paginationParameters = new PaginationParameters(request.PageNumber, request.PageSize);

        var orderByProperty = string.IsNullOrWhiteSpace(request.OrderByProperty)? "Id" : request.OrderByProperty;
        
        var persons = await _personRepository.GetPaginate(paginationParameters, orderByProperty, request.Query);

        var result = new GenericGetPaginateQueryResponse<PersonCommandResponse>(
            persons.Select(p => _mapper.Map<PersonCommandResponse>(p)),
            persons.CurrentPage,
            persons.PageSize,
            persons.TotalCount);

        return result;
    }
}