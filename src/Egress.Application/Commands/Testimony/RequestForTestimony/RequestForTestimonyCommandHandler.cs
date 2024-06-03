using AutoMapper;
using Egress.Infra.Data.Repositories.Interfaces;
using MediatR;

namespace Egress.Application.Commands.Testimony.RequestForTestimony;

public class RequestForTestimonyCommandHandler : IRequestHandler<RequestForTestimonyCommand, RequestForTestimonyCommandResponse>
{
    private readonly ITestimonyRepository _testimonyRepository;
    private readonly IMapper _mapper;

    public RequestForTestimonyCommandHandler(ITestimonyRepository testimonyRepository, IMapper mapper)
    {
        _testimonyRepository = testimonyRepository;
        _mapper = mapper;
    }

    public async Task<RequestForTestimonyCommandResponse> Handle(RequestForTestimonyCommand request, CancellationToken cancellationToken)
    {
        var testimony = _mapper.Map<Domain.Entities.Testimony>(request);
        testimony.WasAccepted = false;

        testimony = await _testimonyRepository.CreateAsync(testimony);

        return new RequestForTestimonyCommandResponse { Id = testimony.Id };
    }
}