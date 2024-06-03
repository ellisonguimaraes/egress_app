using Egress.Domain.Enums;
using Newtonsoft.Json;

namespace Egress.Application.Queries.Responses;

public class CourseCommandResponse : BaseCommandResponse
{
    [JsonProperty("course_name")]
    public string CourseName { get; set; }
    
    [JsonProperty("beginning_semester")]
    public string BeginningSemester { get; set; }

    [JsonProperty("final_semester")]
    public string? FinalSemester { get; set; }

    [JsonProperty("mat")]
    public string Mat { get; set; }

    [JsonProperty("level")]
    public Level Level { get; set; }
    
    [JsonProperty("modality")]
    public Modality Modality { get; set; }
}
