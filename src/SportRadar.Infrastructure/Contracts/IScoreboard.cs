using SportRadar.Infrastructure.Models;

namespace SportRadar.Infrastructure.Contracts
{
    public interface IScoreboard
    {
        void StartMatch(MatchDto request);
        void UpdateScore(UpdateMatchDto updateRequest);
        void FinishMatch(MatchDto request);
        List<IFootballMatch> GetSummary();
    }
}
