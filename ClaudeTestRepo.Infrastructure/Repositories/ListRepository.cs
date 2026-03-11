using ClaudeTestRepo.Application.Interfaces;
using ClaudeTestRepo.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using List = ClaudeTestRepo.Domain.Models.List;

namespace ClaudeTestRepo.Infrastructure.Repositories;

public class ListRepository : IListRepository
{
    private readonly AppDbContext _context;

    public ListRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<List>> GetAllAsync()
    {
        return await _context.Lists.Include(l => l.Items).ToListAsync();
    }

    public async Task<List?> GetByIdAsync(int id)
    {
        return await _context.Lists.Include(l => l.Items).FirstOrDefaultAsync(l => l.Id == id);
    }

    public async Task<List> CreateAsync(List list)
    {
        _context.Lists.Add(list);
        await _context.SaveChangesAsync();
        return list;
    }

    public async Task UpdateAsync(List list)
    {
        _context.Lists.Update(list);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var list = await _context.Lists.FindAsync(id);
        if (list != null)
        {
            _context.Lists.Remove(list);
            await _context.SaveChangesAsync();
        }
    }
}
