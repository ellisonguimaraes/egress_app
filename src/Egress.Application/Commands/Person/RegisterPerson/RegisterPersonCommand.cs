using Egress.Application.Commands.Person.CreateBasicPerson;
using Newtonsoft.Json;

namespace Egress.Application.Commands.Person.RegisterPerson;

public class RegisterPersonCommand : GenericCreateBasicPersonCommand
{ 
    [JsonProperty("employment")]
    public EmploymentEntryModel Employment { get; set; } = null!;
    
    [JsonProperty("continuing_education")]
    public ContinuingEducationEntryModel ContinuingEducation { get; set; } = null!;
    
    [JsonProperty("address")]
    public AddressEntryModel Address { get; set; } = null!;
}

public class EmploymentEntryModel
{
    [JsonProperty("role")]
    public string Role { get; set; } = string.Empty;

    [JsonProperty("enterprise")]
    public string Enterprise { get; set; } = string.Empty;

    [JsonProperty("salary_range")]
    public decimal? SalaryRange { get; set; }

    [JsonProperty("is_public_initiative")]
    public bool? IsPublicInitiative { get; set; }
    
    [JsonProperty("is_public")]
    public bool? IsPublic { get; set; }

    [JsonProperty("start_date")]
    public DateTime? StartDate { get; set; }

    [JsonProperty("end_date")]
    public DateTime? EndDate { get; set; }
}

public class ContinuingEducationEntryModel
{
    [JsonProperty("is_public")]
    public bool? IsPublic { get; set; }
    
    [JsonProperty("has_certification")]
    public bool? HasCertification { get; set; }
    
    [JsonProperty("has_specialization")]
    public bool? HasSpecialization { get; set; }
    
    [JsonProperty("has_master_degree")]
    public bool? HasMasterDegree { get; set; }
    
    [JsonProperty("has_doctorate_degree")]
    public bool? HasDoctorateDegree { get; set; }
}

public class AddressEntryModel
{
    [JsonProperty("state")]
    public string State { get; set; } = string.Empty;

    [JsonProperty("country")]
    public string Country { get; set; } = string.Empty;
    
    [JsonProperty("is_public")]
    public bool? IsPublic { get; set; }
}