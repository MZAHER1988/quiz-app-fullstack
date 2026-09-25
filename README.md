# QuizApp – Fullstack ASP.NET Core MVC Application

A dynamic and interactive quiz application built with **C#**, **ASP.NET Core MVC**, **Entity Framework Core**, and **SQL Server**. 

> 💡 **Educational Project:** I created this project to learn more about C#, ASP.NET Core, MVC, Entity Framework Core and working with a SQL Server database.

---

## Features

- Random quiz questions
- Multiple choice answers
- Questions and answers stored in SQL Server
- Random order for questions and answers
- Score calculation
- Results page
- Review of answers
- Error handling and logging
- Responsive design
- Anti-forgery protection

---

## Technologies

- C#
- ASP.NET Core MVC
- ASP.NET Core 9
- Entity Framework Core
- SQL Server
- Razor Views
- HTML
- CSS
- JavaScript
- Bootstrap

---

## How it works

1. The user opens the start page.

2. The quiz controller loads questions from SQL Server through Entity Framework Core.

3. A limited number of questions are selected randomly.

4. The answer options for each question are also randomized.

5. The quiz is rendered through a Razor View.

6. The selected answer IDs are submitted to the server.

7. The server loads the corresponding questions from the database.

8. Submitted option IDs are compared with the stored CorrectOption IDs.

9. The score is calculated on the server.

10. The result page shows the score and allows the user to review their answers.

The application uses IDs rather than answer text when submitting and checking answers. This makes the grading independent of the displayed order of the questions and options.

---

## Project structure

The application follows the ASP.NET Core MVC pattern:


```text
QuizApp
│
├── Controllers
│   └── QuizController.cs
│
├── Data
│   └── ApplicationDbContext.cs
│
├── Migrations
│   └── Entity Framework Core migrations
│
├── Models
│   ├── Entities
│   │   ├── Question.cs
│   │   └── Option.cs
│   │
│   └── ViewModels
│       ├── QuizViewModel.cs
│       ├── QuizResultViewModel.cs
│       └── ErrorViewModel.cs
│
├── Views
│   ├── Quiz
│   │   ├── Start.cshtml
│   │   ├── Index.cshtml
│   │   ├── Results.cshtml
│   │   └── Error.cshtml
│   │
│   └── Shared
│       └── _Layout.cshtml
│
├── wwwroot
│   ├── css
│   └── js
│
├── Program.cs
├── QuizApp.csproj
├── appsettings.json
└── appsettings.Development.json
```

---

## Database Structure

The application uses two main tables:

Questions
---------
Id
Text
CorrectOption

Options
-------
Id
Text
QuestionId

Relationship:

Question ─1────────*─ Options

Questions.CorrectOption stores the ID of the correct Option.

---


## What I learned

While working on this project, I have been learning how the different parts of an ASP.NET Core application work together.

I have also worked with MVC, Entity Framework Core, SQL Server, database migrations, validation, error handling, logging and Git/GitHub.

---

## Future improvements

Possible future improvements include:

- Quiz categories

- Difficulty levels

- User accounts

- Saved quiz history

- REST API for the quiz

- React frontend

- Improved accessibility

- More advanced quiz statistics

---

## How to Run Locally

### Requirements

- .NET 9 SDK

- SQL Server or SQL Server Express

- Visual Studio 2022 or another compatible .NET development environment

- Git

### Steps

1. **Clone the repository:**
   git clone https://github.com/MZAHER1988/QuizApp.git
   cd QuizApp

2. **Configure Database Connection:**
   Verify your local connection string in appsettings.json:
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=QuizDb;TrustServerCertificate=True;Trusted_Connection=True"
   }

3. **Apply the database migrations:**
   In Visual Studio's Package Manager Console:

      Update-Database

    This creates/updates the database schema using the Entity Framework Core migrations included in the project.

4. **Run the Application:**
   From Visual Studio, start the project with the HTTPS profile.

    Or from the terminal:

      dotnet run

    The application will start on the local URL shown by ASP.NET Core.

---

## Adding Questions

Questions can be stored directly in SQL Server using the existing database structure.

Each question has:

**One Question record**

**Four Option records**

**One CorrectOption ID pointing to the correct option**

The project does not require a Category column for the current quiz functionality.

---

## 🌐 Live Demo

*(Live demo link will be added upon online deployment)*

---

## 👤 Author

**Zaher Hariri**  
