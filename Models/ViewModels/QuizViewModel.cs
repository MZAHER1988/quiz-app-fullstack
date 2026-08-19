using Microsoft.AspNetCore.Mvc.Rendering;

namespace QuizApp.Models.ViewModels;

public class QuizViewModel
{
    // List of questions with their options for the quiz, to be displayed on the quiz page
    public required List<QuestionItem> Questions { get; set; } = [];
}

// Represents a single question in the quiz along with its possible answer options
public class  QuestionItem
{
    public required Guid Id { get; set; }                   // The unique identifier of the question
    public required string Text { get; set; }               // The text of the question
    public required List<SelectListItem> Options { get; set; }      // The list of possible answer options for the question
}