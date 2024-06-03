using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using Egress.Domain.Entities;
using Egress.Domain.Exceptions;
using Egress.Domain.Utils;
using Egress.Infra.CrossCutting.Resource;
using Egress.Infra.Data.Context;
using Egress.Infra.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Egress.Infra.Data.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly ApplicationDbContext Context;
    
    protected readonly DbSet<TEntity> DbSet;

    public Repository(ApplicationDbContext context)
    {
        Context = context;
        DbSet = context.Set<TEntity>();
    }

    public virtual Task<int> Count() => Task.FromResult(DbSet.Count());

    public virtual async Task<TEntity?> GetByIdAsync(Guid id)
        => await DbSet.SingleOrDefaultAsync(e => e.Id.Equals(id));

    public virtual Task<PagedList<TEntity>> GetPaginate(PaginationParameters parameters)
        => Task.FromResult(new PagedList<TEntity>(
                DbSet.OrderBy(i => i.Id),
                parameters.PageNumber,
                parameters.PageSize
            ));

    public virtual Task<PagedList<TEntity>> GetPaginate<TKey>(
        PaginationParameters parameters, 
        Expression<Func<TEntity, TKey>> orderByProperty,
        Expression<Func<TEntity, bool>>? expression = null)
        => BuildPagedList(DbSet, parameters, orderByProperty, expression);

    public virtual Task<PagedList<TEntity>> GetPaginate(
        PaginationParameters parameters,
        string orderByProperty,
        string? expression = null)
        => BuildPagedList(DbSet, parameters, orderByProperty, expression);

    /// <summary>
    /// Build Paged List
    /// </summary>
    /// <param name="queryable">IQueryable entity data</param>
    /// <param name="parameters">Page number and page size</param>
    /// <param name="orderByProperty">Order by property (lambda expression)</param>
    /// <param name="expression">Filter expression (lambda expression)</param>
    /// <returns>Returns paged, filtered and sorted entity</returns>
    protected Task<PagedList<TEntity>> BuildPagedList<TKey>(
        IQueryable<TEntity> queryable,
        PaginationParameters parameters,
        Expression<Func<TEntity, TKey>> orderByProperty,
        Expression<Func<TEntity, bool>>? expression = null)
        => Task.FromResult(new PagedList<TEntity>(
            expression is null
                ? queryable
                    .OrderBy(orderByProperty)
                : queryable
                    .Where(expression)
                    .OrderBy(orderByProperty),
            parameters.PageNumber,
            parameters.PageSize
        ));

    /// <summary>
    /// Build Paged List
    /// </summary>
    /// <param name="queryable">IQueryable entity data</param>
    /// <param name="parameters">Page number and page size</param>
    /// <param name="orderByProperty">Order by property (dynamic LINQ string expression)</param>
    /// <param name="expression">Filter expression (dynamic LINQ string expression)</param>
    /// <returns>Returns paged, filtered and sorted entity</returns>
    protected Task<PagedList<TEntity>> BuildPagedList(
        IQueryable<TEntity> queryable,
        PaginationParameters parameters,
        string orderByProperty,
        string? expression = null)
        => Task.FromResult(new PagedList<TEntity>(
            expression is null
                ? queryable
                    .OrderBy(orderByProperty)
                : queryable
                    .Where(expression)
                    .OrderBy(orderByProperty),
            parameters.PageNumber,
            parameters.PageSize
        ));

    public virtual Task<List<TEntity>> GetRandom(int quantity = 1)
    {
        var count = DbSet.Count();

        if (quantity > count)
            throw new BusinessException(ErrorCodeResource.AMOUNT_IS_GREATER_THEN_TOTAL);

        var limitRange = DbSet.Count() - quantity;

        if (limitRange <= 0)
            return Task.FromResult(
                DbSet
                    .OrderBy(t => t.Id)
                    .ToList());

        var randomNumber = new Random().Next(0, limitRange + 1);

        return Task.FromResult(
                DbSet
                    .OrderBy(t => t.Id)
                    .Skip(randomNumber)
                    .Take(quantity)
                    .ToList());
    }

    public virtual async Task<TEntity> CreateAsync(TEntity entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = entity.CreatedAt;
        
        DbSet.Add(entity);
        await Context.SaveChangesAsync();

        return entity;
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var dbEntity = await DbSet.SingleAsync(e => e.Id.Equals(entity.Id));

        dbEntity.UpdatedAt = DateTime.UtcNow;
        
        Context.Entry(dbEntity).CurrentValues.SetValues(entity);
        Context.Entry(dbEntity).Property(e => e.CreatedAt).IsModified = false;

        await Context.SaveChangesAsync();

        return entity;
    }

    public virtual async Task<TEntity> DeleteAsync(Guid id)
    {
        var data = await GetByIdAsync(id);

        if (data is null)
            throw new BusinessException(string.Format(ErrorCodeResource.NOT_FOUND_ERROR, nameof(TEntity)));

        DbSet.Remove(data);
        await Context.SaveChangesAsync();

        return data;
    }
}