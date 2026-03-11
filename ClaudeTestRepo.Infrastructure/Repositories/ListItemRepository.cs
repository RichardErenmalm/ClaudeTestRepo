using ClaudeTestRepo.Application.Interfaces;
using ClaudeTestRepo.Domain.Models;
using ClaudeTestRepo.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace ClaudeTestRepo.Infrastructure.Repositories;

public class ListItemRepository : IListItemRepository
{
    private readonly AppDbContext _context;

    public ListItemRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ListItem>> GetAllAsync()
    {
        return await _context.ListItems.ToListAsync();
    }

    public async Task<IEnumerable<ListItem>> GetByListIdAsync(int listId)
    {
        return await _context.ListItems.Where(li => li.ListId == listId).ToListAsync();
    }

    public async Task<ListItem?> GetByIdAsync(int id)
    {
        return await _context.ListItems.FindAsync(id);
    }

    public async Task<ListItem> CreateAsync(ListItem item)
    {
        _context.ListItems.Add(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task UpdateAsync(ListItem item)
    {
        _context.ListItems.Update(item);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var item = await _context.ListItems.FindAsync(id);
        if (item != null)
        {
            _context.ListItems.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
