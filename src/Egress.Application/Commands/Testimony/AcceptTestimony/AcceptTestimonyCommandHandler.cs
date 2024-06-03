using Egress.Domain.Exceptions;
using Egress.Infra.CrossCutting.Resource;
using Egress.Infra.Data.Repositories.Interfaces;
using MediatR;

namespace Egress.Application.Commands.Testimony.AcceptTestimony;

public class AcceptTestimonyCommandHandler : IRequestHandler<GenericAcceptCommand<AcceptTestimonyCommandResponse>, AcceptTestimonyCommandResponse>
{
    private readonly ITestimonyRepository _testimonyRepository;

    public AcceptTestimonyCommandHandler(ITestimonyRepository testimonyRepository)
    {
        _testimonyRepository = testimonyRepository;
    }

    public async Task<AcceptTestimonyCommandResponse> Handle(GenericAcceptCommand<AcceptTestimonyCommandResponse> request, CancellationToken cancellationToken)
    {
        var testimony = await _testimonyRepository.GetByIdAsync(request.Id);

        if (testimony is null)
            throw new BusinessException(string.Format(ErrorCodeResource.NOT_FOUND_ERROR, nameof(Testimony)));

        if (testimony.WasAccepted)
            throw new BusinessException(string.Format(ErrorCodeResource.HAS_ALREADY_BEEN_ACCEPTED, nameof(Testimony)));

        testimony.WasAccepted = true;

        var result = await _testimonyRepository.UpdateAsync(testimony);

        return new AcceptTestimonyCommandResponse
        {
            Id = result.Id,
            WasAccepted = result.WasAccepted
        };
    }
}