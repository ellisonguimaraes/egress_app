using Egress.Domain.Entities;

namespace Egress.Infra.Data.Repositories.Interfaces;

public interface IPersonRepository : IRepository<Person>
{
    /// <summary>
    /// Get person by cpf
    /// </summary>
    /// <param name="cpf">Cpf</param>
    /// <returns>Person or null</returns>
    Task<Person?> GetByCpfAsync(string cpf);
}
