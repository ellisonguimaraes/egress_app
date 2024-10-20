using System.ComponentModel.DataAnnotations.Schema;

namespace Egress.Domain.Entities.Views;

public sealed class ViewWithDoctorateDegree : GenericView
{
    [Column("count")]
    public int Count { get; set; }
}