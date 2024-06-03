using Egress.Domain.Entities;
using Egress.Domain.Utils;
using Egress.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Egress.Domain.Exceptions;
using Egress.Infra.CrossCutting.Resource;
using Egress.Infra.Data.Repositories.Interfaces;

namespace Egress.Infra.Data.Repositories;

public class TestimonyRepository : Repository<Testimony>, ITestimonyRepository
{
    public TestimonyRepository(ApplicationDbContext context) : base(context)
    {
    }

    public Task<Testimony?> GetByPersonId(Guid personId)
        => Task.FromResult(GetIncludingQueryable()
            .SingleOrDefault(t => t.PersonId.Equals(personId)));

    public override Task<PagedList<Testimony>> GetPaginate(PaginationParameters parameters, string orderByProperty, string? expression = null)
    {
        var queryable = GetIncludingQueryable();
        return BuildPagedList(queryable, parameters, orderByProperty, expression);
    }

    public override Task<PagedList<Testimony>> GetPaginate<TKey>(PaginationParameters parameters, Expression<Func<Testimony, TKey>> orderByProperty, Expression<Func<Testimony, bool>>? expression = null)
    {
        var queryable = GetIncludingQueryable();
        return BuildPagedList(queryable, parameters, orderByProperty, expression);
    }

    public override Task<List<Testimony>> GetRandom(int quantity = 1)
    {
        var count = DbSet.Count(t => t.WasAccepted.Equals(true));

        if (quantity > count)
            throw new BusinessException(ErrorCodeResource.AMOUNT_IS_GREATER_THEN_TOTAL);

        var limitRange = count - quantity;

        if (limitRange <= 0)
            return Task.FromResult(
                GetIncludingQueryable().Where(t => t.WasAccepted.Equals(true))
                    .OrderBy(t => t.Id)
                    .ToList());

        var randomNumber = new Random().Next(0, limitRange + 1);

        return Task.FromResult(GetIncludingQueryable().Where(t => t.WasAccepted.Equals(true))
            .OrderBy(t => t.Id)
            .Skip(randomNumber)
            .Take(quantity)
            .ToList());
    }

    /// <summary>
    /// Get queryable with including's
    /// </summary>
    /// <returns>Queryable object</returns>
    private IQueryable<Testimony> GetIncludingQueryable()
        => DbSet
            .Include(t => t.Person)
                .ThenInclude(p => p.Highlights)
            .Include(t => t.Person)
                .ThenInclude(p => p.Employment)
            .Include(t => t.Person)
                .ThenInclude(p => p.Address)
            .Include(t => t.Person)
                .ThenInclude(p => p.Testimonies)
            .Include(t => t.Person)
                .ThenInclude(p => p.PersonCourses)
                    .ThenInclude(pc => pc.Course);
}