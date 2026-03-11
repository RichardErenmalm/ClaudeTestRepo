namespace ClaudeTestRepo.Domain.Models;

public class ListItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public int ListId { get; set; }
    public List List { get; set; } = null!;
}
