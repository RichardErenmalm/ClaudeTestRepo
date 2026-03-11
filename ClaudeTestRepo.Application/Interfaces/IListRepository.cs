using ClaudeTestRepo.Domain.Models;
using List = ClaudeTestRepo.Domain.Models.List;

namespace ClaudeTestRepo.Application.Interfaces;

public interface IListRepository
{
    Task<IEnumerable<List>> GetAllAsync();
    Task<List?> GetByIdAsync(int id);
    Task<List> CreateAsync(List list);
    Task UpdateAsync(List list);
    Task DeleteAsync(int id);
}
