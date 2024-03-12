using SportRadar.Infrastructure.Contracts;

namespace SportRadar.Infrastructure.Services
{
    public class FootballMatchService : IFootballMatch
    {
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }
        public DateTime StartTime { get; set; }

        public FootballMatchService(string homeTeam, string awayTeam)
        {
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            HomeTeamScore = 0;
            AwayTeamScore = 0;
            StartTime = DateTime.Now;
        }
        public int TotalScore => HomeTeamScore + AwayTeamScore;
    }
}
