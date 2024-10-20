using System.ComponentModel.DataAnnotations.Schema;

namespace Egress.Domain.Entities.Views;

public sealed class TotalPerRoleView : GenericView
{
    [Column("role")]
    public string Role { get; set; }
    
    [Column("count")]
    public int Count { get; set; }
}