using Egress.Infra.Data.Repositories.Interfaces;
using MediatR;

namespace Egress.Application;

public class GetCountEgressPerFinalSemesterQueryHandler : IRequestHandler<GetCountEgressPerFinalSemesterQuery, GetCountEgressPerFinalSemesterQueryResponse>
{
    #region Constants
    private const string REPLACE_KEY = ".";
    #endregion
    
    private readonly IPersonCourseRepository _personCourseRepository;

    public GetCountEgressPerFinalSemesterQueryHandler(IPersonCourseRepository personCourseRepository)
    {
        _personCourseRepository = personCourseRepository;
    }
    
    public async Task<GetCountEgressPerFinalSemesterQueryResponse> Handle(GetCountEgressPerFinalSemesterQuery request, CancellationToken cancellationToken)
    {
        var groups = await _personCourseRepository.GetCountEgressPerFinalSemesterAsync();
        var orderedGroup = groups.OrderBy(group => int.Parse(group.Key!.Replace(REPLACE_KEY, string.Empty)));
        var total = groups.Sum(g => g.Value);

        return new GetCountEgressPerFinalSemesterQueryResponse
        {
            EgressPerYearList = orderedGroup.ToList(),
            Total = total
        };
    }
}
