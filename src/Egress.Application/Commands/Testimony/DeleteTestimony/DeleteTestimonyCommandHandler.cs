using Egress.Infra.Data.Repositories.Interfaces;
using MediatR;

namespace Egress.Application.Commands.Testimony.DeleteTestimony;

public class DeleteTestimonyCommandHandler : IRequestHandler<DeleteTestimonyCommand, bool>
{
    private readonly ITestimonyRepository _testimonyRepository;

    public DeleteTestimonyCommandHandler(ITestimonyRepository testimonyRepository)
    {
        _testimonyRepository = testimonyRepository;
    }
    
    public async Task<bool> Handle(DeleteTestimonyCommand request, CancellationToken cancellationToken)
    { 
        await _testimonyRepository.DeleteAsync(request.Id);
        return true;
    }
}