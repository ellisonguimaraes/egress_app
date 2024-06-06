using Egress.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Egress.Infra.Data.Context.Configurations;

/// <summary>
/// Address ef configuration
/// </summary>
public class AddressEntityConfiguration : BaseEntityConfiguration<Address>
{
    #region Constants
    private const string TABLE_NAME = "address";
    private const string STATE_DB_PROPERTY_NAME = "state";
    private const byte STATE_DB_PROPERTY_LENGTH = 45;
    private const string COUNTRY_DB_PROPERTY_NAME = "country";
    private const string IS_PUBLIC_DB_PROPERTY_NAME = "is_public";
    private const byte COUNTRY_DB_PROPERTY_LENGTH = 45;
    private const string PERSON_ID_DB_PROPERTY_NAME = "person_id";
    private const byte PERSON_ID_DB_PROPERTY_LENGTH = 36;
    #endregion

    public override void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable(TABLE_NAME);
        
        builder.Property(e => e.State)
            .HasColumnName(STATE_DB_PROPERTY_NAME)
            .HasMaxLength(STATE_DB_PROPERTY_LENGTH)
            .IsRequired();
        
        builder.Property(e => e.Country)
            .HasColumnName(COUNTRY_DB_PROPERTY_NAME)
            .HasMaxLength(COUNTRY_DB_PROPERTY_LENGTH)
            .IsRequired();
        
        builder.Property(e => e.IsPublic)
            .HasColumnName(IS_PUBLIC_DB_PROPERTY_NAME)
            .IsRequired();
        
        builder.Property(e => e.PersonId)
            .HasColumnName(PERSON_ID_DB_PROPERTY_NAME)
            .HasMaxLength(PERSON_ID_DB_PROPERTY_LENGTH)
            .IsRequired();

        builder.HasIndex(e => e.PersonId)
            .IsUnique();

        builder.HasOne(a => a.Person)
            .WithOne(p => p.Address)
            .HasForeignKey<Address>(a => a.PersonId);
        
        base.Configure(builder);
    }
}