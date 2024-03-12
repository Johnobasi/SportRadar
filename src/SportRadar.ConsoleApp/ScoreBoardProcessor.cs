using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SportRadar.Infrastructure.Contracts;
using SportRadar.Infrastructure.Models;
using SportRadar.Infrastructure.Services;

namespace SportRadar.ConsoleApp
{
    public class ScoreBoardProcessor
    {
        private readonly IScoreboard _scoreboard;
        public ScoreBoardProcessor(IScoreboard scoreboard)
        {
             _scoreboard = scoreboard;
        }
        public void ProcessScores()
        {         
            var match1 = new MatchDto { HomeTeam = "Mexico", AwayTeam = "Canada" };
            var match2 = new MatchDto { HomeTeam = "Spain", AwayTeam = "Brazil" };
            var match3 = new MatchDto { HomeTeam = "Germany", AwayTeam = "France" };
            var match4 = new MatchDto { HomeTeam = "Uruguay", AwayTeam = "Italy" };
            var match5 = new MatchDto { HomeTeam = "Argentina", AwayTeam = "Australia" };
           

            _scoreboard.StartMatch(match1);
            _scoreboard.UpdateScore(new UpdateMatchDto { HomeTeam = "Mexico", AwayTeam = "Canada", HomeTeamScore = 0, AwayTeamScore = 5 });

            _scoreboard.StartMatch(match2);
            _scoreboard.UpdateScore(new UpdateMatchDto { HomeTeam = "Spain", AwayTeam = "Brazil", HomeTeamScore = 10, AwayTeamScore = 2 });

            _scoreboard.StartMatch(match3);
            _scoreboard.UpdateScore(new UpdateMatchDto { HomeTeam = "Germany", AwayTeam = "France", HomeTeamScore = 2, AwayTeamScore = 2 });

            _scoreboard.StartMatch(match4);
            _scoreboard.UpdateScore(new UpdateMatchDto { HomeTeam = "Uruguay", AwayTeam = "Italy", HomeTeamScore = 6, AwayTeamScore = 6 });

            _scoreboard.StartMatch(match5);
            _scoreboard.UpdateScore(new UpdateMatchDto { HomeTeam = "Argentina", AwayTeam = "Australia", HomeTeamScore = 3, AwayTeamScore = 1 });

            var summary = _scoreboard.GetSummary();
            Console.WriteLine("===========================================");
            Console.WriteLine("\nLive Football World Cup Scoreboard Summary:");
            Console.WriteLine("-------------------------------------------------");
            foreach (var match in summary)
            {
                Console.WriteLine($"{match.HomeTeam} {match.HomeTeamScore} - {match.AwayTeamScore} {match.AwayTeam}");
                Console.WriteLine("-------------------------------------------------");
            }

            Console.WriteLine("===========================================");
        }

        static void Main()
        {
            var serviceProvider = new ServiceCollection()
               .AddLogging(configure => configure.AddConsole())
               .AddScoped<ILogger<ScoreboardService>, Logger<ScoreboardService>>()
               .AddScoped<IScoreboard, ScoreboardService>()
               .AddScoped<IFootballMatch, FootballMatchService>()
               .AddScoped<ScoreBoardProcessor>()
               .BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                var processor = scope.ServiceProvider.GetRequiredService<ScoreBoardProcessor>();
                processor.ProcessScores();
            }
        }
    }
}
