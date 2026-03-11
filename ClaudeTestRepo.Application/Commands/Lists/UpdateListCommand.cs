using ClaudeTestRepo.Application.DTOs;
using MediatR;

namespace ClaudeTestRepo.Application.Commands.Lists;

public record UpdateListCommand(int Id, UpdateListDto Dto) : IRequest<bool>;

public class UpdateListHandler : IRequestHandler<UpdateListCommand, bool>
{
    private readonly Interfaces.IListRepository _repository;

    public UpdateListHandler(Interfaces.IListRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateListCommand request, CancellationToken cancellationToken)
    {
        var list = await _repository.GetByIdAsync(request.Id);
        if (list == null) return false;

        list.Name = request.Dto.Name;
        await _repository.UpdateAsync(list);
        return true;
    }
}
