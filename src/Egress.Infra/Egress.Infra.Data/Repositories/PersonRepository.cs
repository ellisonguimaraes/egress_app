using System.Linq.Expressions;
using Egress.Domain.Entities;
using Egress.Domain.Utils;
using Egress.Infra.Data.Context;
using Egress.Infra.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Egress.Infra.Data.Repositories;

public class PersonRepository : Repository<Person>, IPersonRepository
{
    public PersonRepository(ApplicationDbContext context) : base(context)
    {
    }

    public Task<Person?> GetByCpfAsync(string cpf)
        => Task.FromResult(GetIncludingQueryable()
            .SingleOrDefault(p => p.Cpf.Equals(cpf)));

    public override Task<PagedList<Person>> GetPaginate<TKey>(
        PaginationParameters parameters,
        Expression<Func<Person, TKey>> orderByProperty,
        Expression<Func<Person, bool>>? expression = null)
    {
        var queryable = GetIncludingQueryable();
        return BuildPagedList(queryable, parameters, orderByProperty, expression);
    }

    public override Task<PagedList<Person>> GetPaginate(
        PaginationParameters parameters,
        string orderByProperty,
        string? expression = null)
    {
        var queryable = GetIncludingQueryable();
        return BuildPagedList(queryable, parameters, orderByProperty, expression);
    }

    public override Task<Person?> GetByIdAsync(Guid id)
        => Task.FromResult(GetIncludingQueryable()
            .SingleOrDefault(p => p.Id.Equals(id)));

    /// <summary>
    /// Get queryable with including's
    /// </summary>
    /// <returns>Queryable object</returns>
    private IQueryable<Person> GetIncludingQueryable()
        => DbSet
            .Include(p => p.Employment)
            .Include(p => p.Address)
            .Include(p => p.PersonCourses)
                .ThenInclude(pc => pc.Course)
            .Include(p => p.Highlights)
            .Include(p => p.Testimonies)
            .Include(p => p.ContinuingEducation);

    public override async Task<Person> CreateAsync(Person entity)
    {
        var utcNow = DateTime.UtcNow;

        entity.CreatedAt = utcNow;
        entity.UpdatedAt = utcNow;

        if (entity.Address is not null)
            entity.Address.CreatedAt = entity.Address.UpdatedAt = utcNow;

        if (entity.Employment is not null)
            entity.Employment.CreatedAt = entity.Employment.UpdatedAt = utcNow;
        
        if (entity.ContinuingEducation is not null)
            entity.ContinuingEducation.CreatedAt = entity.ContinuingEducation.UpdatedAt = utcNow;

        entity.Highlights?.ToList().ForEach(s => {
            s.CreatedAt = s.UpdatedAt = utcNow;
        });

        entity.Testimonies?.ToList().ForEach(s => {
            s.CreatedAt = s.UpdatedAt = utcNow;
        });
        
        entity.PersonCourses?.ToList().ForEach(s => {
            s.CreatedAt = s.UpdatedAt = utcNow;
        });

        DbSet.Add(entity);
        await Context.SaveChangesAsync();

        return entity;
    }

    public override async Task<Person> UpdateAsync(Person entity)
    {
        var person = await GetByIdAsync(entity.Id);
        
        entity.UpdatedAt = DateTime.UtcNow;
        
        Context.Entry(person!).CurrentValues.SetValues(entity);
        Context.Entry(person!).Property(e => e.CreatedAt).IsModified = false;
        Context.Entry(person!).Property(e => e.Cpf).IsModified = false;
        Context.Entry(person!).Property(e => e.PersonType).IsModified = false;

        UpdateNestedObjects(entity, person!);

        await Context.SaveChangesAsync();

        return person!;
    }

    private void UpdateNestedObjects(Person source, Person destination)
    {
        destination.Employment ??= new Employment { CreatedAt = DateTime.UtcNow };
        UpdateProperty(source.Employment, destination.Employment, src => {
            src.Id = destination.Employment!.Id;
            src.PersonId = destination.Id;
            src.UpdatedAt = src.CreatedAt = DateTime.UtcNow;
        });

        destination.Address ??= new Address { CreatedAt = DateTime.UtcNow };
        UpdateProperty(source.Address, destination.Address, src => {
            src.Id = destination.Address!.Id;
            src.PersonId = destination.Id;
            src.UpdatedAt = src.CreatedAt = DateTime.UtcNow;
        });

        destination.ContinuingEducation ??= new ContinuingEducation { CreatedAt = DateTime.UtcNow };
        UpdateProperty(source.ContinuingEducation, destination.ContinuingEducation, src => {
            src.Id = destination.ContinuingEducation!.Id;
            src.PersonId = destination.Id;
            src.UpdatedAt = src.CreatedAt = DateTime.UtcNow;
        });
    }
    
    private void UpdateProperty<T>(T? source, T? destination, Action<T> updateAction) where T : BaseEntity
    {
        if (source is null || destination is null) return;
        
        updateAction(source);
        Context.Entry(destination).CurrentValues.SetValues(source);
        
        Context.Entry(destination).Property(e => e.CreatedAt).IsModified = false;
    }
}
