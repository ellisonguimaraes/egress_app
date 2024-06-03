﻿using AutoMapper;
using Egress.Application.Queries.Responses;
using Egress.Domain.Enums;
using Egress.Domain.Exceptions;
using Egress.Infra.CrossCutting.Resource;
using Egress.Infra.Data.Repositories.Interfaces;
using MediatR;

namespace Egress.Application.Queries.Person.GetPersonByDocument;

public class GetPersonByDocumentQueryHandler : IRequestHandler<GetPersonByDocumentQuery, PersonCommandResponse>
{
    #region Constants
    private const string PERSON_NAME = "Person";
    #endregion

    private readonly IPersonRepository _personRepository;
    private readonly IPersonCourseRepository _personCourseRepository;
    private readonly IMapper _mapper;

    public GetPersonByDocumentQueryHandler(IPersonRepository personRepository, IPersonCourseRepository personCourseRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _personCourseRepository = personCourseRepository;
        _mapper = mapper;
    }

    public async Task<PersonCommandResponse> Handle(GetPersonByDocumentQuery request, CancellationToken cancellationToken)
    {
        var person = default(Domain.Entities.Person?);

        if (request.DocumentType.Equals(DocumentType.Registration))
        {
            person = (await _personCourseRepository.GetByMatAsync(request.Document))?.Person;
        }
        else if (request.DocumentType.Equals(DocumentType.Cpf))
        {
            person = await _personRepository.GetByCpfAsync(request.Document);
        }

        if (person is null)
        {
            throw new BusinessException(string.Format(ErrorCodeResource.NOT_FOUND_ERROR, PERSON_NAME));
        }

        return _mapper.Map<PersonCommandResponse>(person);
    }
}
