namespace zenblog.application.Blogs.Dtos;

public class BlogAutoMapper: Profile
{
    public BlogAutoMapper()
    {
        CreateMap<Blog, BlogDto>().ReverseMap();
    }
}
