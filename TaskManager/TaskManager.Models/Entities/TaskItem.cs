namespace TaskManager.Models.Entities;

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? Deadline { get; set; }

    public int Priority { get; set; }

    public int UserId { get; set; }
    public User? User { get; set; }


    public string? FilePath { get; set; }
}