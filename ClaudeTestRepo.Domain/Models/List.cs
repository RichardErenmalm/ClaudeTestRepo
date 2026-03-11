namespace ClaudeTestRepo.Domain.Models;

public class List
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<ListItem> Items { get; set; } = new List<ListItem>();
}
