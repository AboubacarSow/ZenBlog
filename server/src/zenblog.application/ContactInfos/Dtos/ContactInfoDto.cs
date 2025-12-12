namespace zenblog.application.ContactInfos.Dtos;

public record ContactInfoDto
(
    Guid Id,
    string Email,
    string Phone,
    string Address
);
