using ClaudeTestRepo.Domain.Models;

namespace ClaudeTestRepo.Application.Interfaces;

public interface IListItemRepository
{
    Task<IEnumerable<ListItem>> GetAllAsync();
    Task<IEnumerable<ListItem>> GetByListIdAsync(int listId);
    Task<ListItem?> GetByIdAsync(int id);
    Task<ListItem> CreateAsync(ListItem item);
    Task UpdateAsync(ListItem item);
    Task DeleteAsync(int id);
}
