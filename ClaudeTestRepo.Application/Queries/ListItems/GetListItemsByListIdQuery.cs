using ClaudeTestRepo.Application.DTOs;
using MediatR;

namespace ClaudeTestRepo.Application.Queries.ListItems;

public record GetListItemsByListIdQuery(int ListId) : IRequest<IEnumerable<ListItemDto>>;

public class GetListItemsByListIdHandler : IRequestHandler<GetListItemsByListIdQuery, IEnumerable<ListItemDto>>
{
    private readonly Interfaces.IListItemRepository _repository;

    public GetListItemsByListIdHandler(Interfaces.IListItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ListItemDto>> Handle(GetListItemsByListIdQuery request, CancellationToken cancellationToken)
    {
        var items = await _repository.GetByListIdAsync(request.ListId);
        return items.Select(Mappings.ListItemMappings.ToDto);
    }
}
