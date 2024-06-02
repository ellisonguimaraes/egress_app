using Egress.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Egress.Application.Commands.Person.CreateBasicPerson;

public class CreateBasicPersonCommand : GenericCreateBasicPersonCommand
{
    [JsonProperty("course")]
    [BindProperty(Name = "course")]
    public CourseEntryModel? Course { get; set; }
}

public class CourseEntryModel
{
    [JsonProperty("course_id")]
    public Guid CourseId { get; set; }

    [JsonProperty("beginning_semester")]
    public string BeginningSemester { get; set; } = string.Empty;

    [JsonProperty("final_semester")]
    public string FinalSemester { get; set; } = string.Empty;

    [JsonProperty("mat")]
    public string Mat { get; set; }  = string.Empty;

    [JsonProperty("level")]
    public Level Level { get; set; }
    
    [JsonProperty("modality")]
    public Modality Modality { get; set; }
}