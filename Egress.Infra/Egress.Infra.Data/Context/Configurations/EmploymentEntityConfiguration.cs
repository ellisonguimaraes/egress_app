using Egress.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Egress.Infra.Data.Context.Configurations;

/// <summary>
/// Employment ef configuration
/// </summary>
public class EmploymentEntityConfiguration : BaseEntityConfiguration<Employment>
{
    #region Constants
    private const string TABLE_NAME = "Employment";
    private const string ROLE_DB_PROPERTY_NAME = "role";
    private const byte ROLE_DB_PROPERTY_LENGTH = 150;
    private const string ENTERPRISE_DB_PROPERTY_NAME = "enterprise";
    private const byte ENTERPRISE_DB_PROPERTY_LENGTH = 80;
    private const string SALARY_RANGE_DB_PROPERTY_NAME = "salary_range";
    private const string INITIATIVE_DB_PROPERTY_NAME = "is_public_initiative";
    private const string IS_PUBLIC_DB_PROPERTY_NAME = "is_public";
    private const string START_DATE_DB_PROPERTY_NAME = "start_date";
    private const string END_DATE_DB_PROPERTY_NAME = "end_date";
    private const string PERSON_ID_DB_PROPERTY_NAME = "person_id";
    private const byte PERSON_ID_DB_PROPERTY_LENGTH = 36;
    #endregion
    
    public override void Configure(EntityTypeBuilder<Employment> builder)
    {
        builder.ToTable(TABLE_NAME);
        
        builder.Property(e => e.Role)
            .HasColumnName(ROLE_DB_PROPERTY_NAME)
            .HasMaxLength(ROLE_DB_PROPERTY_LENGTH)
            .IsRequired();
        
        builder.Property(e => e.Enterprise)
            .HasColumnName(ENTERPRISE_DB_PROPERTY_NAME)
            .HasMaxLength(ENTERPRISE_DB_PROPERTY_LENGTH)
            .IsRequired();

        builder.Property(e => e.SalaryRange)
            .HasColumnName(SALARY_RANGE_DB_PROPERTY_NAME);
        
        builder.Property(e => e.IsPublicInitiative)
            .HasColumnName(INITIATIVE_DB_PROPERTY_NAME)
            .IsRequired();
        
        builder.Property(e => e.IsPublic)
            .HasColumnName(IS_PUBLIC_DB_PROPERTY_NAME)
            .IsRequired();
        
        builder.Property(e => e.StartDate)
            .HasColumnName(START_DATE_DB_PROPERTY_NAME)
            .IsRequired();
        
        builder.Property(e => e.EndDate)
            .HasColumnName(END_DATE_DB_PROPERTY_NAME);

        builder.Property(e => e.PersonId)
            .HasColumnName(PERSON_ID_DB_PROPERTY_NAME)
            .HasMaxLength(PERSON_ID_DB_PROPERTY_LENGTH)
            .IsRequired();
        
        builder.HasIndex(e => e.PersonId)
            .IsUnique();

        builder.HasOne(e => e.Person)
            .WithOne(p => p.Employment)
            .HasForeignKey<Employment>(e => e.PersonId);
        
        base.Configure(builder);
    }
}