namespace zenblog.application.ContactInfos.Dtos;

public class ContactInfoAutoMapper : Profile
{
    public ContactInfoAutoMapper()
    {
        CreateMap<ContactInfo, ContactInfoDto>().ReverseMap();
    }
}