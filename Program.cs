using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Models.Entities;

// Create Builder to configure and build the web application
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure Entity Framework and SQL Server connection string 
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Register the DbContext with the dependency injection container, using SQL Server , // and the connection string from configuration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Build the application object based on the settings above
var app = builder.Build();

// Seed the database with initial data if there are no questions present
using var scope = app.Services.CreateScope();

// Get the ApplicationDbContext instance from the service provider
var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

if (!dbContext.Questions.Any())
{
    var question1Answer = Guid.NewGuid();
    var question1 = new Question()
    {
        Text = "What is the capital of Sweden?",
        Options = new List<Option>
        {
            new Option()
            {
                Id = question1Answer,
                Text = "Stockholm"
            }
            ,
            new Option()
            {
                Id = Guid.NewGuid(),
                Text = "Paris"
            }
            ,
            new Option()
            {
                Id = Guid.NewGuid(),
                Text = "Madrid"
            }
            ,
            new Option()
            {
                Id = Guid.NewGuid(),
                Text = "Oslo"
            }
        },
        CorrectOption = question1Answer
    };

    var question2Answer = Guid.NewGuid();
    var question2 = new Question()
    {
        Text = "What is the capital of Spain?",
        Options = new List<Option>
        {
            new Option()
            {
                Id = Guid.NewGuid(),
                Text = "Washinton"
            }
            ,
            new Option()
            {
                Id = Guid.NewGuid(),
                Text = "Athena"
            }
            ,
            new Option()
            {
                Id = question2Answer,
                Text = "Madrid"
            }
            ,
            new Option()
            {
                Id = Guid.NewGuid(),
                Text = "Oslo"
            }
        },
        CorrectOption = question2Answer
    };

    var question3Answer = Guid.NewGuid();
    var question3 = new Question()
    {
        Text = "What is the capital of Norway?",
        Options = new List<Option>
        {
            new Option()
            {
                Id = Guid.NewGuid(),
                Text = "Stockholm"
            }
            ,
            new Option()
            {
                Id = Guid.NewGuid(),
                Text = "Copenhagen"
            }
            ,
            new Option()
            {
                Id = Guid.NewGuid(),
                Text = "Helsinki"
            }
            ,
            new Option()
            {
                Id = question3Answer,
                Text = "Oslo"
            }
        },
        CorrectOption = question3Answer
    };

    var question4Answer = Guid.NewGuid();
    var question4 = new Question()
    {
        Text = "What is the capital of Greece?",
        Options = new List<Option>
        {
            new Option()
            {
                Id = Guid.NewGuid(),
                Text = "Copenhagen"
            }
            ,
            new Option()
            {
                Id = Guid.NewGuid(),
                Text = "Helsinki"
            }
            ,
            new Option()
            {
                Id = question4Answer,
                Text = "Athena"
            }
            ,
            new Option()
            {
                Id = Guid.NewGuid(),
                Text = "Oslo"
            }
        },
        CorrectOption = question4Answer
    };

    var question5Answer = Guid.NewGuid();
    var question5 = new Question()
    {
        Text = "What is the capital of Denmark?",
        Options = new List<Option>
        {
            new Option()
            {
                Id = Guid.NewGuid(),
                Text = "Stockholm"
            }
            ,
            new Option()
            {
                Id = question5Answer,
                Text = "Copenhagen"
            }
            ,
            new Option()
            {
                Id = Guid.NewGuid(),
                Text = "Helsinki"
            }
            ,
            new Option()
            {
                Id = Guid.NewGuid(),
                Text = "Oslo"
            }
        },
        CorrectOption = question5Answer
    };

    var question6Answer = Guid.NewGuid();
    var question6 = new Question()
    {
        Text = "What is the capital of Morocco?",
        Options = new List<Option>
        {
            new Option()
            {
                Id = Guid.NewGuid(),
                Text = "Casablanca"
            }
            ,
            new Option()
            {
                Id = Guid.NewGuid(),
                Text = "Marrakech"
            }
            ,
            new Option()
            {
                Id = question6Answer,
                Text = "Rabat"
            }
            ,
            new Option()
            {
                Id = Guid.NewGuid(),
                Text = "Oslo"
            }
        },
        CorrectOption = question6Answer
    };

    var question7Answer = Guid.NewGuid();
    var question7 = new Question()
    {
        Text = "What is the capital of Turkey?",
        Options = new List<Option>
        {
            new Option()
            {
                Id = question7Answer,
                Text = "Ankara"
            }
            ,
            new Option()
            {
                Id = Guid.NewGuid(),
                Text = "Istanbul"
            }
            ,
            new Option()
            {
                Id = Guid.NewGuid(),
                Text = "Izmir"
            }
            ,
            new Option()
            {
                Id = Guid.NewGuid(),
                Text = "Antalya"
            }
        },
        CorrectOption = question7Answer
    };

    dbContext.Questions.AddRange([question1, question2, question3, question4, question5, question6, question7]);
    dbContext.SaveChanges();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();

// Map the default route for the application, specifying the controller and action to use when no specific route is provided. The default controller is "Quiz" and the default action is "Index". The optional "id" parameter can be included in the URL if needed.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Quiz}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
