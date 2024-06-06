using Egress.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Egress.Infra.Data.Context.Configurations;

/// <summary>
/// Person Course ef configuration
/// </summary>
public class PersonCourseEntityConfiguration : BaseEntityConfiguration<PersonCourse>
{
    #region PersonCourse
    private const string TABLE_NAME = "personcourse";
    private const string BEGINNING_SEMESTER_DB_PROPERTY_NAME = "beginning_semester";
    private const string FINAL_SEMESTER_DB_PROPERTY_NAME = "final_semester";
    private const string MAT_DB_PROPERTY_NAME = "mat";
    private const byte MAT_DB_PROPERTY_LENGTH = 20;
    private const string LEVEL_DB_PROPERTY_NAME = "level";
    private const string MODALITY_DB_PROPERTY_NAME = "modality";
    private const string PERSON_ID_DB_PROPERTY_NAME = "person_id";
    private const byte PERSON_ID_DB_PROPERTY_LENGTH = 36;
    private const string COURSE_ID_DB_PROPERTY_NAME = "course_id";
    private const byte COURSE_ID_DB_PROPERTY_LENGTH = 36;
    #endregion
    
    public override void Configure(EntityTypeBuilder<PersonCourse> builder)
    {
        builder.ToTable(TABLE_NAME);
        
        builder.Property(e => e.BeginningSemester)
            .HasColumnName(BEGINNING_SEMESTER_DB_PROPERTY_NAME)
            .IsRequired();

        builder.Property(e => e.FinalSemester)
            .HasColumnName(FINAL_SEMESTER_DB_PROPERTY_NAME);
        
        builder.Property(e => e.Mat)
            .HasColumnName(MAT_DB_PROPERTY_NAME)
            .HasMaxLength(MAT_DB_PROPERTY_LENGTH)
            .IsRequired();
        
        builder.Property(e => e.Level)
            .HasColumnName(LEVEL_DB_PROPERTY_NAME)
            .IsRequired();
        
        builder.Property(e => e.Modality)
            .HasColumnName(MODALITY_DB_PROPERTY_NAME)
            .IsRequired();

        builder.Property(e => e.PersonId)
            .HasColumnName(PERSON_ID_DB_PROPERTY_NAME)
            .HasMaxLength(PERSON_ID_DB_PROPERTY_LENGTH)
            .IsRequired();
        
        builder.Property(e => e.CourseId)
            .HasColumnName(COURSE_ID_DB_PROPERTY_NAME)
            .HasMaxLength(COURSE_ID_DB_PROPERTY_LENGTH)
            .IsRequired();

        builder.HasOne(e => e.Person)
            .WithMany(p => p.PersonCourses)
            .HasForeignKey(e => e.PersonId);
        
        builder.HasOne(e => e.Course)
            .WithMany(p => p.PersonCourses)
            .HasForeignKey(e => e.CourseId);
        
        base.Configure(builder);
    }
}