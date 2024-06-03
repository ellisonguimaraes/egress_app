using Egress.Domain;
using Egress.Domain.Entities;

namespace Egress.Infra.Data.Repositories.Interfaces;

public interface IPersonCourseRepository : IRepository<PersonCourse>
{
    /// <summary>
    /// Get PersonCourse by registration (mat)
    /// </summary>
    /// <param name="mat">Registration (mat)</param>
    /// <returns>PersonCourse or null</returns>
    Task<PersonCourse?> GetByMatAsync(string mat);

    /// <summary>
    /// Get count egress per final semester
    /// </summary>
    /// <returns>List count per year</returns>
    Task<List<CountGroupBy<string, int>>> GetCountEgressPerFinalSemesterAsync();
}
