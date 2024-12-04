using Egress.Domain;
using Egress.Domain.Entities;
using Egress.Domain.Entities.Views;
using Egress.Infra.Data.Context.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Egress.Infra.Data.Context;

/// <summary>
/// EF Database Context
/// </summary>
public class ApplicationDbContext : DbContext
{
    private readonly AesSettings _aesSettings;

    #region Entities
    public DbSet<Person> Persons { get; set; }

    public DbSet<Address> Addresses { get; set; }
    
    public DbSet<Course> Courses { get; set; }
    
    public DbSet<Employment> Employments { get; set; }
    
    public DbSet<Highlights> Highlights { get; set; }
    
    public DbSet<PersonCourse> PersonCourses { get; set; }
    
    public DbSet<Testimony> Testimonies { get; set; }
    
    public DbSet<ContinuingEducation> ContinuingEducations { get; set; }
    
    public DbSet<Note> Notes { get; set; }
    #endregion

    #region Views
    public DbSet<TotalByStateView> TotalByStateView { get; set; }
    public DbSet<TotalOutsideBrazil> TotalOutsideBrazil { get; set; }
    public DbSet<TotalWithCertificationView> TotalWithCertificationView { get; set; }
    public DbSet<TotalWithSpecializationView> TotalWithSpecializationView { get; set; }
    public DbSet<ViewWithMasterDegree> ViewWithMasterDegree { get; set; }
    public DbSet<ViewWithDoctorateDegree> ViewWithDoctorateDegree { get; set; }
    public DbSet<AverageSalaryRangeView> AverageSalaryRangeView { get; set; }
    public DbSet<TotalPerRoleView> TotalPerRoleView { get; set; }
    public DbSet<TotalPerInitiativeTypeView> TotalPerInitiativeTypeView { get; set; }
    public DbSet<TotalEgressHighlightsView> TotalEgressHighlightsView { get; set; }
    public DbSet<TotalEgressTestimoniesView> TotalEgressTestimoniesView { get; set; }
    public DbSet<AverageBirthdateView> AverageBirthdateView { get; set; }
    public DbSet<AverageBirthdayToEntryView> AverageBirthdayToEntryView { get; set; }
    public DbSet<AverageBirthdayToExitView> AverageBirthdayToExitView { get; set; }
    #endregion
    
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
        new NoteEntityConfiguration().Configure(modelBuilder.Entity<Note>());

        modelBuilder.Entity<TotalByStateView>().ToView("total_by_state_view").HasNoKey();
        modelBuilder.Entity<TotalOutsideBrazil>().ToView("total_outside_brazil").HasNoKey();
        modelBuilder.Entity<TotalWithCertificationView>().ToView("total_with_certification_view").HasNoKey();
        modelBuilder.Entity<TotalWithSpecializationView>().ToView("total_with_especialization_view").HasNoKey();
        modelBuilder.Entity<ViewWithMasterDegree>().ToView("total_with_master_degree_view").HasNoKey();
        modelBuilder.Entity<ViewWithDoctorateDegree>().ToView("total_with_doctorate_degree_view").HasNoKey();
        modelBuilder.Entity<AverageSalaryRangeView>().ToView("average_salary_range_view").HasNoKey();
        modelBuilder.Entity<TotalPerRoleView>().ToView("total_per_role_view").HasNoKey();
        modelBuilder.Entity<TotalPerInitiativeTypeView>().ToView("total_per_initiative_type_view").HasNoKey();
        modelBuilder.Entity<TotalEgressHighlightsView>().ToView("total_egress_highlights_view").HasNoKey();
        modelBuilder.Entity<TotalEgressTestimoniesView>().ToView("total_egress_testimonies_view").HasNoKey();
        modelBuilder.Entity<AverageBirthdateView>().ToView("average_birthdate_view").HasNoKey();
        modelBuilder.Entity<AverageBirthdayToEntryView>().ToView("average_birthday_to_entry_view").HasNoKey();
        modelBuilder.Entity<AverageBirthdayToExitView>().ToView("average_birthday_to_exit_view").HasNoKey();
        
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        
        base.OnModelCreating(modelBuilder);
    }
}