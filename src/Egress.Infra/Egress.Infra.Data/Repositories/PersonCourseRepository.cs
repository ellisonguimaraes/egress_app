using System.Linq.Expressions;
using Egress.Domain;
using Egress.Domain.Entities;
using Egress.Domain.Enums;
using Egress.Domain.Utils;
using Egress.Infra.Data.Context;
using Egress.Infra.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Egress.Infra.Data.Repositories;

public class PersonCourseRepository : Repository<PersonCourse>, IPersonCourseRepository
{
    public PersonCourseRepository(ApplicationDbContext context) : base(context)
    {
    }

    public Task<PersonCourse?> GetByMatAsync(string mat)
        => Task.FromResult(GetIncludingQueryable()
            .SingleOrDefault(pc => pc.Mat.Equals(mat)));

    public override Task<PagedList<PersonCourse>> GetPaginate<TKey>(
        PaginationParameters parameters,
        Expression<Func<PersonCourse, TKey>> orderByProperty,
        Expression<Func<PersonCourse, bool>>? expression = null)
    {
        var queryable = GetIncludingQueryable();
        return BuildPagedList(queryable, parameters, orderByProperty, expression);
    }

    public override Task<PagedList<PersonCourse>> GetPaginate(
        PaginationParameters parameters,
        string orderByProperty,
        string? expression = null)
    {
        var queryable = GetIncludingQueryable();
        return BuildPagedList(queryable, parameters, orderByProperty, expression);
    }

    public Task<List<CountGroupBy<string, int>>> GetCountEgressPerFinalSemesterAsync()
        => Task.FromResult(
                DbSet
                    .Where(pc => pc.Person.PersonType.Equals(PersonType.EGRESS) && !string.IsNullOrEmpty(pc.FinalSemester))
                    .GroupBy(pc => pc.FinalSemester)
                    .Select(group => new CountGroupBy<string, int>{ Key = group.Key, Value = group.Count() })
                    .ToList());

    /// <summary>
    /// Get queryable with including's
    /// </summary>
    /// <returns>Queryable object</returns>
    private IQueryable<PersonCourse> GetIncludingQueryable()
        => DbSet
            .Include(pc => pc.Course)
            .Include(pc => pc.Person)
            .Include(pc => pc.Person)
                .ThenInclude(p => p.Employment)
            .Include(pc => pc.Person)
                .ThenInclude(p => p.Address)
            .Include(pc => pc.Person)
                .ThenInclude(p => p.PersonCourses)
            .Include(pc => pc.Person)
                .ThenInclude(p => p.Highlights)
            .Include(pc => pc.Person)
                .ThenInclude(p => p.Testimonies)
            .Include(pc => pc.Person)
                .ThenInclude(p => p.ContinuingEducation);
}
