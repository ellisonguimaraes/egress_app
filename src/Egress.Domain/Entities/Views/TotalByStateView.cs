using System.ComponentModel.DataAnnotations.Schema;

namespace Egress.Domain.Entities.Views;

public sealed class TotalByStateView : GenericView
{
    [Column("state")]
    public string State { get; set; }
    
    [Column("count")]
    public int Count { get; set; }
}