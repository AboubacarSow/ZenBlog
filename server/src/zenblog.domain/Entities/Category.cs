namespace zenBlog.domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = default!;
    public virtual IList<Blog> Blogs { get; set; } = [];
}
