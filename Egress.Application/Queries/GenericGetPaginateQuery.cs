using Egress.Domain.Utils;
using MediatR;

namespace Egress.Application.Queries;

public class GenericGetPaginateQuery<TRequest> : PaginationParameters, IRequest<TRequest>
{
    public string? Query { get; set; }

    public string? OrderByProperty { get; set; }

    public GenericGetPaginateQuery(int pageNumber, int pageSize, string? query, string? orderByProperty) : base(pageNumber, pageSize)
    {
        Query = query;
        OrderByProperty = orderByProperty;
    }
}