using ClaudeTestRepo.Application.DTOs;
using ClaudeTestRepo.Application.Mappings;
using MediatR;

namespace ClaudeTestRepo.Application.Queries.Lists;

public record GetListByIdQuery(int Id) : IRequest<ListDto?>;

public class GetListByIdHandler : IRequestHandler<GetListByIdQuery, ListDto?>
{
    private readonly Interfaces.IListRepository _repository;

    public GetListByIdHandler(Interfaces.IListRepository repository)
    {
        _repository = repository;
    }

    public async Task<ListDto?> Handle(GetListByIdQuery request, CancellationToken cancellationToken)
    {
        var list = await _repository.GetByIdAsync(request.Id);
        return list?.ToDto();
    }
}
