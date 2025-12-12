namespace zenblog.application.Messages.Dtos;

public record MessageDto(
    Guid Id,
     string Name ,
     string Email  ,
     string Subject ,
     string Body ,
     bool IsRead
);

public class MessageAutoMapper : Profile
{
    public MessageAutoMapper()
    {
        CreateMap<Message, MessageDto>().ReverseMap();
    }
}