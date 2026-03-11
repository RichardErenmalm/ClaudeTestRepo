using ClaudeTestRepo.Application.DTOs;
using MediatR;

namespace ClaudeTestRepo.Application.Queries.Lists;

public record GetAllListsQuery : IRequest<IEnumerable<ListDto>>;

public class GetAllListsHandler : IRequestHandler<GetAllListsQuery, IEnumerable<ListDto>>
{
    private readonly Interfaces.IListRepository _repository;

    public GetAllListsHandler(Interfaces.IListRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ListDto>> Handle(GetAllListsQuery request, CancellationToken cancellationToken)
    {
        var lists = await _repository.GetAllAsync();
        return lists.Select(Mappings.ListMappings.ToDto);
    }
}
