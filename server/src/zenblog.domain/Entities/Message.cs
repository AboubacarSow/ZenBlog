namespace zenBlog.domain.Entities;

public class Message : BaseEntity
{
    public string Name  { get; set; }= default!;
    public string Email  { get; set; } = default!;
    public string Subject  { get; set; } = default!;
    public string Body  { get; set; } = default!;
    public bool IsRead  { get; set; } = false;
}
