namespace Egress.Domain.Entities;

public class ContinuingEducation : BaseEntity
{
    public bool IsPublic { get; set; }
    
    public bool HasCertification { get; set; }
    
    public bool HasSpecialization { get; set; }
    
    public bool HasMasterDegree { get; set; }
    
    public bool HasDoctorateDegree { get; set; }
    
    #region Relationship
    public virtual Person Person { get; set; }
    public Guid PersonId { get; set; }
    #endregion
}