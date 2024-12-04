using Egress.Domain.Enums;

namespace Egress.Domain.Entities;

public class Person : BaseEntity
{
    public string Cpf { get; set; }

    public string Name { get; set; }

    public DateTime BirthDate { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }
    
    public string? PerfilImageSrc { get; set; }

    public bool CanExposeData { get; set; }
    
    public bool CanReceiveMessage { get; set; }

    public PersonType PersonType { get; set; }

    #region Relationship
    public virtual Address? Address { get; set; }

    public virtual Employment? Employment { get; set; }
    
    public virtual ContinuingEducation? ContinuingEducation { get; set; }
    
    public IList<PersonCourse> PersonCourses { get; set; }

    public IList<Highlights> Highlights { get; set; }
    
    public IList<Testimony> Testimonies { get; set; }
    
    public IList<Note> Notes { get; set; }
    #endregion
}