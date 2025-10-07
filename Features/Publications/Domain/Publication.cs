using System;

namespace api.Features.Publications.Domain;

public record Publication(
    Guid Id,
    string Title,
    string Content,
    DateTime PublishDate
);