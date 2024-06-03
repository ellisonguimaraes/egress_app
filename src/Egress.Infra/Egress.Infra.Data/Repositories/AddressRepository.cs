using Egress.Domain.Entities;
using Egress.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Egress.Infra.Data.Repositories;

public class AddressRepository : Repository<Address>
{
    public AddressRepository(ApplicationDbContext context) : base(context)
    {
    }

    public override async Task<Address> UpdateAsync(Address entity)
    {
        var dbEntity = await DbSet.SingleAsync(e => e.Id.Equals(entity.Id));

        dbEntity.UpdatedAt = DateTime.UtcNow;
        
        Context.Entry(dbEntity).CurrentValues.SetValues(entity);
        Context.Entry(dbEntity).Property(e => e.CreatedAt).IsModified = false;
        Context.Entry(dbEntity).Property(e => e.PersonId).IsModified = false;

        await Context.SaveChangesAsync();

        return dbEntity;
    }
}