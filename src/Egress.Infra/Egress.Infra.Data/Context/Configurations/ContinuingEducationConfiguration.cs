using Egress.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Egress.Infra.Data.Context.Configurations;

public class ContinuingEducationConfiguration : BaseEntityConfiguration<ContinuingEducation>
{
    #region Constants
    private const string TABLE_NAME = "continuingeducation";
    private const string IS_PUBLIC_DB_PROPERTY_NAME = "is_public";
    private const string HAS_CERTIFICATION_DB_PROPERTY_NAME = "has_certification";
    private const string HAS_SPECIALIZATION_DB_PROPERTY_NAME = "has_specialization";
    private const string HAS_MASTER_DEGREE_DB_PROPERTY_NAME = "has_master_degree";
    private const string HAS_DOCTORATE_DEGREE_DB_PROPERTY_NAME = "has_doctorate_degree";
    private const string PERSON_ID_DB_PROPERTY_NAME = "person_id";
    #endregion
    
    public override void Configure(EntityTypeBuilder<ContinuingEducation> builder)
    {
        builder.ToTable(TABLE_NAME);

        builder.Property(c => c.IsPublic)
            .HasColumnName(IS_PUBLIC_DB_PROPERTY_NAME)
            .IsRequired();
        
        builder.Property(c => c.HasCertification)
            .HasColumnName(HAS_CERTIFICATION_DB_PROPERTY_NAME)
            .IsRequired();
        
        builder.Property(c => c.HasSpecialization)
            .HasColumnName(HAS_SPECIALIZATION_DB_PROPERTY_NAME)
            .IsRequired();
        
        builder.Property(c => c.HasMasterDegree)
            .HasColumnName(HAS_MASTER_DEGREE_DB_PROPERTY_NAME)
            .IsRequired();
        
        builder.Property(c => c.HasDoctorateDegree)
            .HasColumnName(HAS_DOCTORATE_DEGREE_DB_PROPERTY_NAME)
            .IsRequired();
        
        builder.Property(c => c.PersonId)
            .HasColumnName(PERSON_ID_DB_PROPERTY_NAME)
            .IsRequired();

        builder.HasIndex(c => c.PersonId)
            .IsUnique();

        builder.HasOne<Person>(c => c.Person)
            .WithOne(p => p.ContinuingEducation)
            .HasForeignKey<ContinuingEducation>(c => c.PersonId);
        
        base.Configure(builder);
    }
}