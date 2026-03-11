using ClaudeTestRepo.Application.DTOs;
using MediatR;

namespace ClaudeTestRepo.Application.Queries.ListItems;

public record GetAllListItemsQuery : IRequest<IEnumerable<ListItemDto>>;

public class GetAllListItemsHandler : IRequestHandler<GetAllListItemsQuery, IEnumerable<ListItemDto>>
{
    private readonly Interfaces.IListItemRepository _repository;

    public GetAllListItemsHandler(Interfaces.IListItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ListItemDto>> Handle(GetAllListItemsQuery request, CancellationToken cancellationToken)
    {
        var items = await _repository.GetAllAsync();
        return items.Select(Mappings.ListItemMappings.ToDto);
    }
}
