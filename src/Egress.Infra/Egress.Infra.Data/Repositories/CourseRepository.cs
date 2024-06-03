using Egress.Domain.Entities;
using Egress.Infra.Data.Context;
using Egress.Infra.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Egress.Infra.Data.Repositories;

public class CourseRepository : Repository<Course>, ICourseRepository
{
    public CourseRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Course?> GetByNormalizedNameAsync(string courseName)
        => await DbSet.SingleOrDefaultAsync(c => c.NormalizedCourseName.ToUpper().Equals(courseName.ToUpper()));

    public async Task<IEnumerable<Course>> GetAllAsync()
        => await DbSet.ToListAsync();
}