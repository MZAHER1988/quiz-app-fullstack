namespace QuizApp.Models.Entities;

public record Question
{
    public Guid Id { get; init; }
    public required string Text { get; init; } // = string.Empty;

    // Options

    public required List<Option> Options { get; init; } // = new List<Option>();
    public required Guid CorrectOption { get; init; }

    }
