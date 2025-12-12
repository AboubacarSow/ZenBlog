namespace zenBlog.domain.Entities;

public class SocialMedia: BaseEntity
{
    public string Name  { get; set; }= default!;
    public string AddressUrl  { get; set; } = default!;
    public string Icon  { get; set; } = default!;
}

