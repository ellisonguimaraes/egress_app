using MediatR;

namespace Egress.Application.Queries.Course.GetAllCourses;

public sealed class GetAllCoursesQuery : IRequest<IList<GetAllCoursesQueryResponse>>
{
}