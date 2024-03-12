namespace SportRadar.Infrastructure.Contracts
{
    public interface IFootballMatch
    {
        string HomeTeam { get; set; }
        string AwayTeam { get; set; }
        int HomeTeamScore { get; set; }
        int AwayTeamScore { get; set; }
        DateTime StartTime { get; set; }
        int TotalScore { get; }
    }
}
