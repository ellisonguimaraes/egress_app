using Egress.Domain.Enums;
using Newtonsoft.Json;

namespace Egress.Application.Queries.Responses;

public class PersonCommandResponse : BaseCommandResponse
{
    [JsonProperty("cpf")]
    public string Cpf { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("birth_date")]
    public string? BirthDate { get; set; }

    [JsonProperty("email")]
    public string? Email { get; set; }

    [JsonProperty("phone_number")]
    public string? PhoneNumber { get; set; }

    [JsonProperty("perfil_image")]
    public string? PerfilImage { get; set; }

    [JsonProperty("can_expose_data")]
    public bool? ExposeData { get; set; }
    
    [JsonProperty("can_receive_message")]
    public bool? CanReceiveMessage { get; set; }

    [JsonProperty("person_type")]
    public PersonType? PersonType { get; set; }

    [JsonProperty("address")]
    public AddressCommandResponse Address { get; set; }

    [JsonProperty("employment")]
    public EmploymentCommandResponse Employment { get; set; }
    
    [JsonProperty("continuing_education")]
    public ContinuingEducationCommandResponse ContinuingEducation { get; set; }

    [JsonProperty("courses")]
    public IList<CourseCommandResponse> Courses { get; set; }

    [JsonProperty("highlights")]
    public IList<HighlightsCommandResponse> Highlights { get; set; }

    [JsonProperty("testimonies")]
    public IList<TestimonyCommandResponse> Testimonies { get; set; }
}
