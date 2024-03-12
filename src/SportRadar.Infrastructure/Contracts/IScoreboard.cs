namespace SportRadar.Infrastructure.Contracts
{
    public interface IScoreboard
    {
        void StartMatch(string homeTeam, string awayTeam);
        void UpdateScore(string homeTeam, string awayTeam, int homeTeamScore, int awayTeamScore);
        void FinishMatch(string homeTeam, string awayTeam);
        List<IFootballMatch> GetSummary();
    }
}
