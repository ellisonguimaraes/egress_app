using Egress.Domain.Enums;

namespace Egress.Domain.Entities;

public class PersonCourse : BaseEntity
{
    public string BeginningSemester { get; set; }

    public string? FinalSemester { get; set; }

    public string Mat { get; set; }

    public Level Level { get; set; }
    
    public Modality Modality { get; set; }
    
    #region Relationship
    public Guid PersonId { get; set; }
    public virtual Person Person { get; set; }
    
    public Guid CourseId { get; set; }
    public virtual Course Course { get; set; }
    #endregion
}