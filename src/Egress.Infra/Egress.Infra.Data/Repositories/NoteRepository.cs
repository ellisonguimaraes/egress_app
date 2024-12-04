using System.Linq.Expressions;
using Egress.Domain.Entities;
using Egress.Domain.Utils;
using Egress.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Egress.Infra.Data.Repositories;

public sealed class NoteRepository : Repository<Note> 
{
    public NoteRepository(ApplicationDbContext context) : base(context)
    {
    }

    public override async Task<Note?> GetByIdAsync(Guid id)
        => await GetIncludingQueryable().SingleOrDefaultAsync(e => e.Id.Equals(id));

    public override Task<PagedList<Note>> GetPaginate(PaginationParameters parameters, string orderByProperty, string? expression = null)
    {
        var queryable = GetIncludingQueryable();
        return BuildPagedList(queryable, parameters, orderByProperty, expression);
    }

    public override Task<PagedList<Note>> GetPaginate<TKey>(PaginationParameters parameters, Expression<Func<Note, TKey>> orderByProperty, Expression<Func<Note, bool>>? expression = null)
    {
        var queryable = GetIncludingQueryable();
        return BuildPagedList(queryable, parameters, orderByProperty, expression);
    }

    /// <summary>
    /// Get queryable with including's
    /// </summary>
    /// <returns>Queryable object</returns>
    private IQueryable<Note> GetIncludingQueryable()
        => DbSet
            .Include(n => n.Person);
}