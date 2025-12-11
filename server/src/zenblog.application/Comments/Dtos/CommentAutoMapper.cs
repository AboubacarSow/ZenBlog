namespace zenblog.application.Comments.Dtos;

public class CommentAutoMapper : Profile
{
    public CommentAutoMapper()
    {
        CreateMap<Comment, CommentDto>()
            .ForMember(dest => dest.Replies, opt => opt.MapFrom(src => src.Replies))
            .ReverseMap();
    }
}