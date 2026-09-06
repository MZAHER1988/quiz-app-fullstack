using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Models.ViewModels;
using System.Runtime.InteropServices;

namespace QuizApp.Controllers
{
    public class QuizController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        
        public QuizController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var questions = dbContext.Questions.Include(x => x.Options)
                .Select(x => new QuestionItem()
                {
                    Id = x.Id,
                    Text = x.Text,
                    Options = x.Options
                                .Select(y => 
                                new SelectListItem(y.Text, y.Id.ToString())).ToList()

                })
                .ToList();

            return View(new QuizViewModel() { Questions = questions });
        }

        [HttpPost]
        public IActionResult Submit(List<Guid> usersAnswers)
        {
            var questions = dbContext.Questions.ToList();
            var score = 0;
            var totalScore = questions.Count;
            
            for (int i = 0; i < usersAnswers.Count; i++)
            {
                if (questions[i].CorrectOption == usersAnswers[i])
                {
                    score++;
                }
            }
            /*
            // Jämför baserat på om det valda ID:t finns bland frågorna som CorrectOption
            foreach (var answerId in usersAnswers)
            {
                if (questions.Any(q => q.CorrectOption == answerId))
                {
                    score++;
                }
            }
            */

            ViewBag.Score = score;
            ViewBag.TotalScore = totalScore;
            return View("Results");
        }
    }
}
