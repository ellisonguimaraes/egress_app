using System.ComponentModel.DataAnnotations.Schema;

namespace Egress.Domain.Entities.Views;

public sealed class TotalEgressTestimoniesView : GenericView
{
    [Column("count")]
    public int Count { get; set; }
}