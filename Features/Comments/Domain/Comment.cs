using System;

namespace api.Features.Comments.Domain;

public record Comment(
    Guid Id,
    Guid PostId,
    string Content,
    DateTime PublishDate
);
