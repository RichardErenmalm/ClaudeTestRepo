using ClaudeTestRepo.Application.DTOs;
using ClaudeTestRepo.Application.Mappings;
using MediatR;
using List = ClaudeTestRepo.Domain.Models.List;

namespace ClaudeTestRepo.Application.Commands.Lists;

public record CreateListCommand(CreateListDto Dto) : IRequest<ListDto>;

public class CreateListHandler : IRequestHandler<CreateListCommand, ListDto>
{
    private readonly Interfaces.IListRepository _repository;

    public CreateListHandler(Interfaces.IListRepository repository)
    {
        _repository = repository;
    }

    public async Task<ListDto> Handle(CreateListCommand request, CancellationToken cancellationToken)
    {
        var list = new List { Name = request.Dto.Name };
        var created = await _repository.CreateAsync(list);
        return created.ToDto();
    }
}
