using ClaudeTestRepo.Application.DTOs;
using ClaudeTestRepo.Application.Mappings;
using MediatR;

namespace ClaudeTestRepo.Application.Queries.ListItems;

public record GetListItemByIdQuery(int Id) : IRequest<ListItemDto?>;

public class GetListItemByIdHandler : IRequestHandler<GetListItemByIdQuery, ListItemDto?>
{
    private readonly Interfaces.IListItemRepository _repository;

    public GetListItemByIdHandler(Interfaces.IListItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<ListItemDto?> Handle(GetListItemByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id);
        return item?.ToDto();
    }
}
