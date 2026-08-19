namespace QuizApp.Models.Entities;

public record Option
{
    public Guid Id { get; init; }
    public required string Text { get; init; } // = string.Empty;

    // Relationship to Question
    public Guid QuestionId { get; init; }
}
