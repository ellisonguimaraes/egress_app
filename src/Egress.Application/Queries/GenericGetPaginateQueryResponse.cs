using Egress.Domain.Utils;

namespace Egress.Application.Queries;

public class GenericGetPaginateQueryResponse<TResponse> : PagedList<TResponse>
{
    public GenericGetPaginateQueryResponse(IQueryable<TResponse> source, int pageNumber, int pageSize) : base(source, pageNumber, pageSize)
    {
    }

    public GenericGetPaginateQueryResponse(IEnumerable<TResponse> source, int pageNumber, int pageSize, int totalCount) : base(source, pageNumber, pageSize, totalCount)
    {
    }
}