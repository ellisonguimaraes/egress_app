namespace Egress.Domain.Entities;

public class Course : BaseEntity
{
    public string CourseName { get; set; }
    
    public string NormalizedCourseName { get; set; }
    
    #region Relationship
    public List<PersonCourse> PersonCourses { get; set; }
    #endregion
}