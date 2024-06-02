namespace Egress.Domain.Entities;

public class Address : BaseEntity
{
    public string State { get; set; }

    public string Country { get; set; }

    public bool IsPublic { get; set; }
    
    #region Relationship
    public virtual Person Person { get; set; }
    public Guid PersonId { get; set; }
    #endregion
}