namespace zenblog.application.SocialMedias.Dtos;

public class SocialMediaAutoMapper : Profile
{
    public SocialMediaAutoMapper()
    {
        CreateMap<SocialMedia, SocialMediaDto>().ReverseMap();
    }
}