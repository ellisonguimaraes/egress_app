using System.ComponentModel.DataAnnotations.Schema;

namespace Egress.Domain.Entities.Views;

public sealed class AverageBirthdateView : GenericView
{
    [Column("avg")]
    public decimal Average { get; set; }
}