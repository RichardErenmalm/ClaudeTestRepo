using ClaudeTestRepo.Application.DTOs;
using ClaudeTestRepo.Domain.Models;

namespace ClaudeTestRepo.Application.Mappings;

public static class ListItemMappings
{
    public static ListItemDto ToDto(this ListItem item)
    {
        return new ListItemDto
        {
            Id = item.Id,
            Title = item.Title,
            IsCompleted = item.IsCompleted,
            ListId = item.ListId
        };
    }
}
