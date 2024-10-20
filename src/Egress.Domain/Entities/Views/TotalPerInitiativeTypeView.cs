using System.ComponentModel.DataAnnotations.Schema;

namespace Egress.Domain.Entities.Views;

public sealed class TotalPerInitiativeTypeView : GenericView
{
    [Column("is_public_initiative")]
    public bool IsPublicInitiative { get; set; }
    
    [Column("count")]
    public int Count { get; set; }
}