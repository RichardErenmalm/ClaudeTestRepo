using ClaudeTestRepo.Application.DTOs;
using ClaudeTestRepo.Application.Mappings;
using ClaudeTestRepo.Domain.Models;
using MediatR;

namespace ClaudeTestRepo.Application.Commands.ListItems;

public record CreateListItemCommand(CreateListItemDto Dto) : IRequest<ListItemDto?>;

public class CreateListItemHandler : IRequestHandler<CreateListItemCommand, ListItemDto?>
{
    private readonly Interfaces.IListItemRepository _repository;
    private readonly Interfaces.IListRepository _listRepository;

    public CreateListItemHandler(Interfaces.IListItemRepository repository, Interfaces.IListRepository listRepository)
    {
        _repository = repository;
        _listRepository = listRepository;
    }

    public async Task<ListItemDto?> Handle(CreateListItemCommand request, CancellationToken cancellationToken)
    {
        var list = await _listRepository.GetByIdAsync(request.Dto.ListId);
        if (list == null) return null;

        var item = new ListItem
        {
            Title = request.Dto.Title,
            ListId = request.Dto.ListId
        };
        var created = await _repository.CreateAsync(item);
        return created.ToDto();
    }
}
