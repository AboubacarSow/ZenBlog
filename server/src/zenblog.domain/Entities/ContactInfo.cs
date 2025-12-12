namespace zenBlog.domain.Entities;

public class ContactInfo : BaseEntity
{
    public string Address  { get; set; }= default!;
    public string Email  { get; set; } = default!;
    public string Phone  { get; set; } = default!;
}

