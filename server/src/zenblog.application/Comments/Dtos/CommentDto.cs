namespace zenblog.application.Comments.Dtos;
public record CommentDto
(
    Guid Id,
    string Name,
    string Email,
    string Content,
    Guid BlogId,
    Guid? ParentCommentId,
    CommentDto[] Replies
);
