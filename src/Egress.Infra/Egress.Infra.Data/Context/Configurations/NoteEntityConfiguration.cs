using Egress.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Egress.Infra.Data.Context.Configurations;

public class NoteEntityConfiguration : BaseEntityConfiguration<Note>
{
    #region Constants
    private const string TABLE_NAME = "notes";
    private const string TITLE_DB_PROPERTY_NAME = "title";
    private const int TITLE_DB_PROPERTY_LENGTH = 300;
    private const string CONTENT_DB_PROPERTY_NAME = "content";
    private const string WAS_ACCEPTED_DB_PROPERTY_NAME = "was_accepted";
    private const string PERSON_ID_DB_PROPERTY_NAME = "person_id";
    #endregion
    
    public override void Configure(EntityTypeBuilder<Note> builder)
    {
        builder.ToTable(TABLE_NAME);

        builder.Property(n => n.Title)
            .HasColumnName(TITLE_DB_PROPERTY_NAME)
            .HasMaxLength(TITLE_DB_PROPERTY_LENGTH)
            .IsRequired();
        
        builder.Property(n => n.Content)
            .HasColumnName(CONTENT_DB_PROPERTY_NAME)
            .IsRequired();
        
        builder.Property(n => n.WasAccepted)
            .HasColumnName(WAS_ACCEPTED_DB_PROPERTY_NAME)
            .IsRequired();
        
        builder.Property(n => n.PersonId)
            .HasColumnName(PERSON_ID_DB_PROPERTY_NAME)
            .IsRequired();

        builder.HasOne<Person>(c => c.Person)
            .WithMany(p => p.Notes)
            .HasForeignKey(n => n.PersonId);
        
        base.Configure(builder);
    }
}