namespace ClaudeTestRepo.Application.DTOs;

public class ListItemDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public int ListId { get; set; }
}
