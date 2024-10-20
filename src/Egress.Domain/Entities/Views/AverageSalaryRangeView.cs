using System.ComponentModel.DataAnnotations.Schema;

namespace Egress.Domain.Entities.Views;

public class AverageSalaryRangeView : GenericView
{
    [Column("average")]
    public decimal Average { get; set; }
}