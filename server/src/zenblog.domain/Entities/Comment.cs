namespace zenBlog.domain.Entities;

public class Comment : BaseEntity
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Content { get; set; } = default!;
    public Guid BlogId { get; set; }
    public virtual Blog Blog { get; set; } = default!;
    public Guid? ParentCommentId { get; set; }
    public virtual Comment? ParentComment { get; set; }
    public virtual ICollection<Comment> Replies { get; set; } = [];

    public void Edit(Comment comment)
    {
        Name = comment.Name;
        Email = comment.Email;
        Content = comment.Content;
        BlogId = comment.BlogId;
        ParentCommentId = comment.ParentCommentId;
    }
}
