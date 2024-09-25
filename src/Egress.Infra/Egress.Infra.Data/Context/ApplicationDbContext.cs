using Egress.Domain;
using Egress.Domain.Entities;
using Egress.Infra.Data.Context.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Egress.Infra.Data.Context;

/// <summary>
/// EF Database Context
/// </summary>
public class ApplicationDbContext : DbContext
{
    private readonly AesSettings _aesSettings;
    
    public DbSet<Person> Persons { get; set; }

    public DbSet<Address> Addresses { get; set; }
    
    public DbSet<Course> Courses { get; set; }
    
    public DbSet<Employment> Employments { get; set; }
    
    public DbSet<Highlights> Highlights { get; set; }
    
    public DbSet<PersonCourse> PersonCourses { get; set; }
    
    public DbSet<Testimony> Testimonies { get; set; }
    
    public DbSet<ContinuingEducation> ContinuingEducations { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, AesSettings aesSettings) : base(options)
    {
        _aesSettings = aesSettings;
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new PersonEntityConfiguration(_aesSettings).Configure(modelBuilder.Entity<Person>());
        new CourseEntityConfiguration().Configure(modelBuilder.Entity<Course>());
        new PersonCourseEntityConfiguration().Configure(modelBuilder.Entity<PersonCourse>());
        new AddressEntityConfiguration().Configure(modelBuilder.Entity<Address>());
        new EmploymentEntityConfiguration().Configure(modelBuilder.Entity<Employment>());
        new HighlightsEntityConfiguration().Configure(modelBuilder.Entity<Highlights>());
        new TestimonyEntityConfiguration().Configure(modelBuilder.Entity<Testimony>());
        new ContinuingEducationConfiguration().Configure(modelBuilder.Entity<ContinuingEducation>());
        
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        
        base.OnModelCreating(modelBuilder);
    }
}