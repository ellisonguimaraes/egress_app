using Egress.Domain.Enums;
using MediatR;
using Newtonsoft.Json;

namespace Egress.Application.Commands.Person.CreateBasicPerson;

public class GenericCreateBasicPersonCommand : IRequest<GenericCreatePersonCommandResponse>
{
    [JsonProperty("id")]
    public Guid? Id { get; set; }

    [JsonProperty("cpf")]
    public string Cpf { get; set; }  = string.Empty;

    [JsonProperty("name")]
    public string Name { get; set; }  = string.Empty;

    [JsonProperty("birth_date")]
    public DateTime? BirthDate { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; } = string.Empty;

    [JsonProperty("phone_number")]
    public string PhoneNumber { get; set; } = string.Empty;
    
    [JsonProperty("can_expose_data")]
    public bool? CanExposeData { get; set; }
    
    [JsonProperty("can_receive_message")]
    public bool? CanReceiveMessage { get; set; }

    [JsonProperty("person_type")]
    public PersonType? PersonType { get; set; }
}