namespace ClaudeTestRepo.Application.DTOs;

public class ListDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<ListItemDto> Items { get; set; } = new();
}
