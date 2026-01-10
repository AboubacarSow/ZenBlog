namespace zenBlog.domain.Entities;

public class Blog: BaseEntity
{
    public string Title { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;
    public string CoverImageUrl { get; set; } = default!;
    public string Content { get; set; } = default!;
    public Guid CategoryId { get; set; }
    public virtual Category Category { get; set; } = default!;
    public Guid AuthorId{get;set;}=default!;
    public virtual ApplicationUser Author {get; set;}= default!;
    public virtual ICollection<Comment> Comments { get; set; } =[];

    public Blog() { }
}
