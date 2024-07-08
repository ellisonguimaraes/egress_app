using AutoMapper;
using Egress.Application.Queries.Responses;
using Egress.Domain.Entities;
using Egress.Domain.Utils;
using Egress.Infra.Data.Repositories.Interfaces;
using MediatR;

namespace Egress.Application.Queries.Person.GetPaginateEgress;

public class GetPaginateEgressQueryHandler : IRequestHandler<GenericGetPaginateQuery<GenericGetPaginateQueryResponse<GetPaginateEgressQueryResponse>>, GenericGetPaginateQueryResponse<GetPaginateEgressQueryResponse>>
{
    #region Constants
    private const string DEFAULT_QUERY = "Person.PersonType = 0";
    #endregion

    private readonly IPersonCourseRepository _personCourseRepository;
    private readonly IMapper _mapper;

    public GetPaginateEgressQueryHandler(IPersonCourseRepository personCourseRepository, IMapper mapper)
    {
        _personCourseRepository = personCourseRepository;
        _mapper = mapper;
    }

    public async Task<GenericGetPaginateQueryResponse<GetPaginateEgressQueryResponse>> Handle(GenericGetPaginateQuery<GenericGetPaginateQueryResponse<GetPaginateEgressQueryResponse>> request, CancellationToken cancellationToken)
    {
        var paginationParameters = new PaginationParameters(request.PageNumber, request.PageSize);

        var orderByProperty = string.IsNullOrWhiteSpace(request.OrderByProperty)? "Id" : request.OrderByProperty;
        var query = string.IsNullOrWhiteSpace(request.Query)? DEFAULT_QUERY : $"{DEFAULT_QUERY} and {request.Query}";

        var personCourses = await _personCourseRepository.GetPaginate(
            paginationParameters, orderByProperty, query);

        var result = new GenericGetPaginateQueryResponse<GetPaginateEgressQueryResponse>(
            personCourses.Select(ConvertAndClearDataEgressDoNotAllowExpose),
            personCourses.CurrentPage,
            personCourses.PageSize,
            personCourses.TotalCount);

        return result;
    }

    /// <summary>
    /// Convert PersonCourse to GetPaginateEgressQueryResponse, and remove Email and Name when ExposeData is false
    /// </summary>
    /// <param name="personCourse">PersonCourse Entity</param>
    /// <returns>GetPaginateEgressQueryResponse</returns>
    private GetPaginateEgressQueryResponse ConvertAndClearDataEgressDoNotAllowExpose(PersonCourse personCourse)
    {
        var egress = _mapper.Map<GetPaginateEgressQueryResponse>(personCourse);

        if (!egress.ExposeData)
        {
            egress.Email = string.Empty;
            egress.Name = string.Empty;
        }
        
        egress.Address = egress.Address is not null && !(bool)egress.Address.IsPublic? default : egress.Address;
        egress.ContinuingEducation = egress.ContinuingEducation is not null && !egress.ContinuingEducation.IsPublic? default : egress.ContinuingEducation;

        if (egress.Employment is not null)
        {
            egress.Employment.SalaryRange = default;
            
            if (!(bool)egress.Employment.IsPublic)
                egress.Employment = null;
        }
        
        return egress;
    }
}