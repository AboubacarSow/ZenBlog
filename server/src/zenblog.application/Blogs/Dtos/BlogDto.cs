namespace zenblog.application.Blogs.Dtos;
public record BlogDto
(
    Guid Id,
    string Title,
    string Content,
    string ImageUrl,
    string CoverImageUrl,
    Guid CategoryId,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
