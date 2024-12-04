namespace Egress.Domain.Entities;

public class Note : BaseEntity
{
    public string Title { get; set; }
    
    public string Content { get; set; }
    
    public bool WasAccepted { get; set; }
    
    #region Relationship
    public virtual Person Person { get; set; }
    public Guid PersonId { get; set; }
    #endregion
}