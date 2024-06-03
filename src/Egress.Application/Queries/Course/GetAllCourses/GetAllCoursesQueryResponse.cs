using Newtonsoft.Json;

namespace Egress.Application.Queries.Course.GetAllCourses;

public sealed class GetAllCoursesQueryResponse
{
    [JsonProperty("id")]
    public Guid Id { get; set; }
    
    [JsonProperty("course_name")]
    public string CourseName { get; set; }
    
    [JsonProperty("normalized_course_name")]
    public string NormalizedCourseName { get; set; }
}