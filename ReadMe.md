# Live Football World Cup Scoreboard Documentation
Introduction


Installation

Getting Started
Clone the repository.
Open the solution in Visual Studio.
Run the solution.

###Usage
This project provides a live football World Cup scoreboard library implemented in C#. The library allows users to manage football matches, update scores, finish matches, and retrieve a summary of ongoing matches.

Assumptions and Considerations
1. Dependency Injection:
The solution relies on the Microsoft.Extensions.DependencyInjection library for dependency injection. I implemented dependency injection configuration in the ScoreBoardProcessor class.

2. Logging:
Logging is implemented using Microsoft's ILogger. The provided ScoreboardService logs match-related events, such as starting and finishing matches, as information messages.


3. Data Storage:
The ScoreboardService uses a Dictionary to store football matches in memory. This is a basic in-memory solution. 

4. Unit Tests:
The ScoreBoardTest class contains unit tests using xUnit, following a Test-Driven Development (TDD) approach. I ensured that the test cases cover application's expected behavior and edge cases.

5. Console Output:
The Console.WriteLine statements in the ProcessScores method are used to display the live football World Cup scoreboard summary in the console.

6. SOLID Principles:
a. Single Responsibility Principle: The ScoreboardService and FootballMatchService classes follow the Single Responsibility Principle by focusing on managing football matches and updating scores, respectively.
b. Open/Closed Principle: The solution is open for extension and closed for modification. The ScoreboardService and FootballMatchService classes can be extended to add new functionality without modifying existing code.
c. Liskov Substitution Principle: The solution uses interfaces to ensure that derived classes can be substituted for their base classes.
d. Interface Segregation Principle: The solution uses interfaces to segregate the responsibilities of the ScoreboardService and FootballMatchService classes.
e. Dependency Inversion Principle: The solution uses dependency injection to invert the dependency between the ScoreboardService and FootballMatchService classes and their dependencies.

7. Clean Code:
The solution follows clean code principles, such as meaningful variable names, small methods, and consistent formatting.

8. Clean Architecture:
The solution follows clean architecture principles by separating the application into layers, such as the application core and infrastructure. The application core contains the ScoreboardService and FootballMatchService classes, while the infrastructure contains the Program class and the dependency injection configuration.

9. Error Handling:
The solution uses try-catch blocks to handle exceptions and log error messages using Microsoft's ILogger.
The ScoreboardService logs an error message when an exception occurs while processing scores.

### Usage

#Images

