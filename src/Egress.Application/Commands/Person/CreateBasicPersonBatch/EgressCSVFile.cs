using CsvHelper.Configuration.Attributes;
using Egress.Domain.Enums;

namespace Egress.Application.Commands.Person.CreateBasicPersonBatch;

public class EgressCSVFile
{
    [Name("matricula")]
    public string Mat { get; set; }

    [Name("curso")]
    public string CourseName { get; set; }

    [Name("nome")]
    public string Name { get; set; }

    [Name("cpf")]
    public string Cpf { get; set; }

    [Name("email")]
    public string Email { get; set; }

    [Name("telefone")]
    public string PhoneNumber { get; set; }

    [Name("ingresso")]
    public string Ingress { get; set; }

    [Name("egresso")]
    public string Egress { get; set; }
    
    [Name("modality")]
    public Modality Modality { get; set; }
    
    [Name("level")]
    public Level Level { get; set; }
}