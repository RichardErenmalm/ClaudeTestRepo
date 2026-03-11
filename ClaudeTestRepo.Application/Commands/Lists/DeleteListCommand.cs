using MediatR;

namespace ClaudeTestRepo.Application.Commands.Lists;

public record DeleteListCommand(int Id) : IRequest<bool>;

public class DeleteListHandler : IRequestHandler<DeleteListCommand, bool>
{
    private readonly Interfaces.IListRepository _repository;

    public DeleteListHandler(Interfaces.IListRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteListCommand request, CancellationToken cancellationToken)
    {
        var list = await _repository.GetByIdAsync(request.Id);
        if (list == null) return false;

        await _repository.DeleteAsync(request.Id);
        return true;
    }
}
