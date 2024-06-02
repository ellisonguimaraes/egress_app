using Egress.Domain;
using Newtonsoft.Json;

namespace Egress.Application;

public class GetCountEgressPerFinalSemesterQueryResponse
{
    [JsonProperty("total")]
    public int? Total { get; set; }
    
    [JsonProperty("egress_per_year")]
    public List<CountGroupBy<string, int>> EgressPerYearList { get; set; } = null!;
}
