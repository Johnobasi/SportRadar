using Microsoft.Extensions.Logging;
using SportRadar.Infrastructure.Contracts;
using SportRadar.Infrastructure.Models;

namespace SportRadar.Infrastructure.Services
{
    public class ScoreboardService : IScoreboard
    {
        private Dictionary<(string, string), IFootballMatch> _matches;
        private ILogger<ScoreboardService> _logger;
        public ScoreboardService(ILogger<ScoreboardService> logger)
        {
            _matches = new Dictionary<(string, string), IFootballMatch>();
            _logger = logger;
        }

        public void FinishMatch(MatchDto request)
        {
            try
            {
                _logger.LogInformation("Finishing match between {HomeTeam} and {AwayTeam}", request.HomeTeam, request.AwayTeam);
                var matchKey = (request.HomeTeam, request.AwayTeam);
                if (_matches.ContainsKey(matchKey))
                {
                    _matches.Remove(matchKey);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error finishing match between {HomeTeam} and {AwayTeam}", request.HomeTeam, request.AwayTeam);
            }

        }

        public List<IFootballMatch> GetSummary()
        {
            try
            {
                return _matches.Values
                         .OrderByDescending(match => match.TotalScore)
                             .ThenByDescending(match => match.StartTime)
                                 .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting match summary");
            }

            return new List<IFootballMatch>();

        }

        public void StartMatch(MatchDto request)
        {
            try
            {
                var matchKey = (request.HomeTeam, request.AwayTeam);
                if (!_matches.ContainsKey(matchKey))
                {
                    _logger.LogInformation("Starting match between {HomeTeam} and {AwayTeam}", request.HomeTeam, request.AwayTeam);
                    _matches.Add(matchKey, new FootballMatchService(request.HomeTeam, request.AwayTeam));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error starting match between {HomeTeam} and {AwayTeam}", request.HomeTeam, request.AwayTeam);
            }

        }

        public void UpdateScore(UpdateMatchDto updateRequest)
        {
            try
            {
                var matchKey = (updateRequest.HomeTeam, updateRequest.AwayTeam);
                if (_matches.TryGetValue(matchKey, out var match))
                {
                    _logger.LogInformation("Updating score for match between {HomeTeam} and {AwayTeam}", updateRequest.HomeTeam, updateRequest.AwayTeam);
                    match.HomeTeamScore = updateRequest.HomeTeamScore;
                    match.AwayTeamScore = updateRequest.AwayTeamScore;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "Updating score for match between {HomeTeam} and {AwayTeam}", updateRequest.HomeTeam, updateRequest.AwayTeam);
            }
        }
    }
}
