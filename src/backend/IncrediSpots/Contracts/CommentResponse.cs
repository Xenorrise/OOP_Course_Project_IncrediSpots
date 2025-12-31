namespace IncrediSpots.API.Contracts;

public record class CommentResponse(int Id, string Text, string? AuthorEmail, DateTime CreatedAt);