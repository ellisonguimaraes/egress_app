namespace Egress.Domain.Entities;

public class Highlights : BaseEntity
{
    public string Title { get; set; }

    public string Description { get; set; }

    public string? Link { get; set; }

    public string? AdvertisingImageSrc { get; set; }

    public string? VeracityFilesSrc { get; set; }
    
    public bool WasAccepted { get; set; }
    
    #region Relationship
    public virtual Person Person { get; set; }
    public Guid PersonId { get; set; }
    #endregion
}