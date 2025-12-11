namespace zenBlog.domain.Entities;

public class Message : BaseEntity
{
    public string Name  { get; set; }= default!;
    public string Email  { get; set; } = default!;
    public string Subject  { get; set; } = default!;
    public string Body  { get; set; } = default!;
    public bool IsRead  { get; set; } = false;
}

public class Comment : BaseEntity
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Content { get; set; } = default!;
    public Guid BlogId { get; set; }
    public Blog Blog { get; set; } = default!;
    public Guid? ParentCommentId { get; set; }
    public Comment? ParentComment { get; set; }
    public ICollection<Comment> Replies { get; set; } = [];
}
