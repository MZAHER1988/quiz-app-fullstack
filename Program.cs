using Microsoft.EntityFrameworkCore;
using QuizApp.Data;

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
    pattern: "{controller=Quiz}/{action=Start}/{id?}")
    .WithStaticAssets();

app.Run();
