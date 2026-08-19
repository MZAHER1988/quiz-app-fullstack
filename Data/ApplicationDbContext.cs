using Microsoft.EntityFrameworkCore;
using QuizApp.Models.Entities;

namespace QuizApp.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Question> Questions { get; set; }
    public DbSet<Option> Options { get; set; }
}
