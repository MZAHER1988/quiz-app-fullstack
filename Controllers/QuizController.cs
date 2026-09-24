using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Models.Entities;
using QuizApp.Models.ViewModels;

namespace QuizApp.Controllers
{
    public class QuizController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<QuizController> logger;

        public QuizController(ApplicationDbContext dbContext, ILogger<QuizController> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;

        }

        [HttpGet]
        public IActionResult Start()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var rnd = new Random();

            // Fetch 10 random questions with their options from database.
            // AsNoTracking used for better performance because this is a read-only data.
            var rawQuestions = await dbContext.Questions
                .AsNoTracking()
                .Include(x => x.Options)
                .OrderBy(x => Guid.NewGuid())
                .Take(10)
                .ToListAsync();

            // Handle error message if there are no data in th db
            if (!rawQuestions.Any())
            {
                var errorModel = new ErrorViewModel
                {
                    ErrorMessage = "No questions available."
                };

                logger.LogWarning("Questions table is empty. No quiz questions were found in the database.");
                return View("ErrorView", errorModel);
            }

            // 2. Map data to ViewModel and shuffle answer options
            var questions = rawQuestions.Select(x => new QuestionItem()
            {
                Id = x.Id,
                Text = x.Text,
                Options = x.Options
                    .OrderBy(_ => rnd.Next())
                    .Select(y => new SelectListItem(y.Text, y.Id.ToString()))
                    .ToList()
            }).ToList();

            return View(new QuizViewModel() { Questions = questions });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(Dictionary<Guid, Guid>? userAnswers)
        {
            if (userAnswers == null || !userAnswers.Any())
            {
                var errorModel = new ErrorViewModel
                {
                    ErrorMessage = "No answers were submitted. Please answer the questions before submitting."
                };
                logger.LogWarning("User submitted the quiz without any answers.");
                return View("ErrorView", errorModel);
            }         
            
            if (userAnswers.Count > 10)
            {
                var errorModel = new ErrorViewModel
                {
                    ErrorMessage = "Too many answers submitted. Please answer only the questions provided."
                };
                logger.LogWarning("User submitted more answers than expected. Expected: 10, Submitted: {Count}", userAnswers.Count);
                return View("ErrorView", errorModel);
            }
            
            var resultModel = new QuizResultViewModel();

            var questionIds = userAnswers.Keys.ToList();

            // Fetch questions and their options for specified IDs without tracking for better performance.
            // AsNoTracking is used because the data is read-only (improves performance).
            var rawQuestions = await dbContext.Questions
                .AsNoTracking()
                .Include(q => q.Options)
                .Where(q => questionIds.Contains(q.Id))
                .ToListAsync();

            if (userAnswers.Count != rawQuestions.Count)
            {
                var errorModel = new ErrorViewModel
                {
                    ErrorMessage = "Some submitted answers do not match any questions in the database. Please check your submission."
                };
                logger.LogWarning("Mismatch between submitted answers and database questions. Submitted: {SubmittedCount}, Found in DB: {DbCount}", userAnswers.Count, rawQuestions.Count);
                return View("ErrorView", errorModel);
            }

            // Sort the questions in exactly the same order as questionIds (the order from the form).
            var questions = questionIds
                .Select(id => rawQuestions.First(q => q.Id == id))
                .ToList();

            foreach (var q in questions)
            {
                userAnswers.TryGetValue(q.Id, out Guid selectedOptionId);

                var userOption = q.Options.FirstOrDefault(o => o.Id == selectedOptionId);
                var correctOption = q.Options.FirstOrDefault(o => o.Id == q.CorrectOption);

                bool isCorrect = userOption != null && q.CorrectOption == selectedOptionId;
                if (isCorrect)
                {
                    resultModel.Score++;
                }

                resultModel.Questions.Add(new QuestionResultItem
                {
                    QuestionText = q.Text,
                    UserAnswerText = userOption != null ? userOption.Text : "No Answer",
                    CorrectAnswerText = correctOption != null ? correctOption.Text : "Unknown",
                    IsCorrect = isCorrect
                });
            }
            resultModel.TotalQuestions = questions.Count;

            return View("Results", resultModel);
        }
    }
}
