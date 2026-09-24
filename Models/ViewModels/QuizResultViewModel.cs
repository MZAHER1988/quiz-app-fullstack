namespace QuizApp.Models.ViewModels
{
    public class QuizResultViewModel
    {
        public int Score { get; set; }
        public int TotalQuestions { get; set; }
        public List<QuestionResultItem> Questions { get; set; } = new();
    }

    public class QuestionResultItem
    {
        public string QuestionText { get; set; } = string.Empty;
        public string UserAnswerText { get; set; } = string.Empty;
        public string CorrectAnswerText { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
    }
}
