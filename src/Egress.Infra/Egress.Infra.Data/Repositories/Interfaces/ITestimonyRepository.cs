using Egress.Domain.Entities;

namespace Egress.Infra.Data.Repositories.Interfaces;

public interface ITestimonyRepository : IRepository<Testimony>
{
    /// <summary>
    /// Get testimony by person identifier
    /// </summary>
    /// <param name="personId">Person identifier (guid)</param>
    /// <returns>Testimony or null</returns>
    Task<Testimony?> GetByPersonId(Guid personId);
}