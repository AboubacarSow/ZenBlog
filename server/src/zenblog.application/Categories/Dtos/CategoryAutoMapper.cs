namespace zenblog.application.Categories.Dtos;

public class CategoryAutoMapper : Profile
{
    public CategoryAutoMapper()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();
    }
}
  