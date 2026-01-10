using zenblog.application.Blogs.Commands.CreateBlog;

namespace zenblog.application.Blogs.Dtos;

public class BlogAutoMapper: Profile
{
    public BlogAutoMapper()
    {
        CreateMap<Blog, BlogDto>().ReverseMap();
        CreateMap<CreateBlogCommand,Blog >().ReverseMap();
    }
}
