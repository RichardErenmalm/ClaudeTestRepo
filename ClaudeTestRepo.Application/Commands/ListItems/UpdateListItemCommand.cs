using ClaudeTestRepo.Application.DTOs;
using MediatR;

namespace ClaudeTestRepo.Application.Commands.ListItems;

public record UpdateListItemCommand(int Id, UpdateListItemDto Dto) : IRequest<bool>;

public class UpdateListItemHandler : IRequestHandler<UpdateListItemCommand, bool>
{
    private readonly Interfaces.IListItemRepository _repository;

    public UpdateListItemHandler(Interfaces.IListItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateListItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id);
        if (item == null) return false;

        item.Title = request.Dto.Title;
        item.IsCompleted = request.Dto.IsCompleted;
        await _repository.UpdateAsync(item);
        return true;
    }
}
