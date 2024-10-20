using System.ComponentModel.DataAnnotations.Schema;

namespace Egress.Domain.Entities.Views;

public sealed class AverageBirthdayToEntryView : GenericView
{
    [Column("avg")]
    public decimal Average { get; set; }
}