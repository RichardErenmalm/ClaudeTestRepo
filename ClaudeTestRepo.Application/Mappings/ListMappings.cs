using ClaudeTestRepo.Application.DTOs;
using List = ClaudeTestRepo.Domain.Models.List;

namespace ClaudeTestRepo.Application.Mappings;

public static class ListMappings
{
    public static ListDto ToDto(this List list)
    {
        return new ListDto
        {
            Id = list.Id,
            Name = list.Name,
            Items = list.Items.Select(i => i.ToDto()).ToList()
        };
    }
}
