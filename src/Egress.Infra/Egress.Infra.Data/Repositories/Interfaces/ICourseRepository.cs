using Egress.Domain.Entities;

namespace Egress.Infra.Data.Repositories.Interfaces;

public interface ICourseRepository : IRepository<Course>
{
    /// <summary>
    /// Get course by name
    /// </summary>
    /// <param name="id">Guid identifier</param>
    /// <returns>Entity or null</returns>
    Task<Course?> GetByNormalizedNameAsync(string courseName);
    
    /// <summary>
    /// Get all courses
    /// </summary>
    /// <returns>All courses</returns>
    Task<IEnumerable<Course>> GetAllAsync();
}