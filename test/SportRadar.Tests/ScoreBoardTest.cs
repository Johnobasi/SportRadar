using Microsoft.Extensions.Logging;
using Moq;
using SportRadar.Infrastructure.Contracts;
using SportRadar.Infrastructure.Models;
using SportRadar.Infrastructure.Services;

namespace SportRadar.Tests
{
    public class ScoreBoardTest
    {
        private readonly Mock<IScoreboard> _mock;
        private readonly Mock<ILogger<ScoreboardService>> _loggerMock;
        private readonly IScoreboard _scoreboard;
        public ScoreBoardTest()
        {
                _mock = new Mock<IScoreboard>();
                 _loggerMock = new Mock<ILogger<ScoreboardService>>();
                _scoreboard = new ScoreboardService(_loggerMock.Object);
        }

        [Fact]
        public void StartMatch_ShouldAddNewMatchToScoreboard()
        {
            var request = new MatchDto
            {
                HomeTeam = "HomeTeam",
                AwayTeam = "AwayTeam"
            };
            _mock.Setup(x => x.StartMatch(request));

             _scoreboard.StartMatch(request);

            List<IFootballMatch> summary = _scoreboard.GetSummary();
            Assert.Single(summary);
            Assert.Equal("HomeTeam", summary[0].HomeTeam);
            Assert.Equal("AwayTeam", summary[0].AwayTeam);
            Assert.Equal(0, summary[0].HomeTeamScore);
            Assert.Equal(0, summary[0].AwayTeamScore);
        }

        [Fact]
        public void GetSummary_ShouldReturnMatchesOrderedByTotalScoreThenStartTime()
        {

            // Arrange
            var request1 = new MatchDto
            {
                HomeTeam = "TeamA",
                AwayTeam = "TeamB"
            };

            var updateDto = new UpdateMatchDto
            {
                HomeTeam = "TeamA",
                AwayTeam = "TeamB",
                HomeTeamScore = 2,
                AwayTeamScore = 1
            };
            _scoreboard.StartMatch(request1);
            _scoreboard.UpdateScore(updateDto);

            var request2 = new MatchDto
            {
                HomeTeam = "TeamC",
                AwayTeam = "TeamD"
            };

            var updateDto2 = new UpdateMatchDto
            {
                HomeTeam = "TeamC",
                AwayTeam = "TeamD",
                HomeTeamScore = 1,
                AwayTeamScore = 2
            };
            _scoreboard.StartMatch(request2);
            _scoreboard.UpdateScore(updateDto2);

            // Act
            List<IFootballMatch> matches = _scoreboard.GetSummary();

            // Assert
            Assert.Equal(2, matches.Count);

            Assert.Equal(request2.HomeTeam, matches[0].HomeTeam);
            Assert.Equal(request2.AwayTeam, matches[0].AwayTeam);
            Assert.Equal(1, matches[0].HomeTeamScore);
            Assert.Equal(2, matches[0].AwayTeamScore);

            Assert.Equal(request1.HomeTeam, matches[1].HomeTeam);
            Assert.Equal(request1.AwayTeam, matches[1].AwayTeam);
            Assert.Equal(2, matches[1].HomeTeamScore);
            Assert.Equal(1, matches[1].AwayTeamScore);
        }

        [Fact]
        public void FinishMatch_ShouldRemoveMatchFromScoreboard()
        {
            // Arrange
            var request = new MatchDto
            {
                HomeTeam = "TeamA",
                AwayTeam = "TeamB"
            };
            _scoreboard.StartMatch(request);

            // Act
            _scoreboard.FinishMatch(request);

            // Assert
            List<IFootballMatch> matches = _scoreboard.GetSummary();
            Assert.Empty(matches);
        }
    }
}