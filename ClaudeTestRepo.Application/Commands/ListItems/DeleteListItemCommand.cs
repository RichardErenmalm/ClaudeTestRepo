using MediatR;

namespace ClaudeTestRepo.Application.Commands.ListItems;

public record DeleteListItemCommand(int Id) : IRequest<bool>;

public class DeleteListItemHandler : IRequestHandler<DeleteListItemCommand, bool>
{
    private readonly Interfaces.IListItemRepository _repository;

    public DeleteListItemHandler(Interfaces.IListItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteListItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id);
        if (item == null) return false;

        await _repository.DeleteAsync(request.Id);
        return true;
    }
}
