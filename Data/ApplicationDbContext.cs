using Microsoft.EntityFrameworkCore;
using QuizApp.Models.Entities;

namespace QuizApp.Data;


public class ApplicationDbContext : DbContext
{
    // Constructor that takes DbContextOptions and passes it to the base DbContext class, allowing configuration of the database context.
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Question> Questions { get; set; }
    public DbSet<Option> Options { get; set; }
}
