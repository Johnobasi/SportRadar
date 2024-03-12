using Moq;
using SportRadar.Infrastructure.Contracts;
using SportRadar.Infrastructure.Services;

namespace SportRadar.Tests
{
    public class ScoreBoardTest
    {
        private readonly Mock<IScoreboard> _mock;
        private readonly IScoreboard _scoreboard;
        public ScoreBoardTest()
        {
                _mock = new Mock<IScoreboard>();
                _scoreboard = (IScoreboard)new ScoreboardService();
        }

        [Fact]
        public void StartMatch_ShouldAddNewMatchToScoreboard()
        {

            _mock.Setup(x => x.StartMatch(It.IsAny<string>(), It.IsAny<string>()));

             _scoreboard.StartMatch("HomeTeam", "AwayTeam");
            _mock.Verify(x => x.StartMatch(It.IsAny<string>(), It.IsAny<string>()), Times.Once);

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

            _scoreboard.StartMatch("TeamA", "TeamB");
            _scoreboard.UpdateScore("TeamA", "TeamB", 2, 1);

            _scoreboard.StartMatch("TeamC", "TeamD");
            _scoreboard.UpdateScore("TeamC", "TeamD", 1, 2);

            // Act
            List<IFootballMatch> matches = _scoreboard.GetSummary();

            // Assert
            Assert.Equal(2, matches.Count);

            Assert.Equal("TeamC", matches[0].HomeTeam);
            Assert.Equal("TeamD", matches[0].AwayTeam);
            Assert.Equal(1, matches[0].HomeTeamScore);
            Assert.Equal(2, matches[0].AwayTeamScore);

            Assert.Equal("TeamA", matches[1].HomeTeam);
            Assert.Equal("TeamB", matches[1].AwayTeam);
            Assert.Equal(2, matches[1].HomeTeamScore);
            Assert.Equal(1, matches[1].AwayTeamScore);
        }

        [Fact]
        public void FinishMatch_ShouldRemoveMatchFromScoreboard()
        {
            // Arrange
            _scoreboard.StartMatch("TeamA", "TeamB");

            // Act
            _scoreboard.FinishMatch("TeamA", "TeamB");

            // Assert
            List<IFootballMatch> matches = _scoreboard.GetSummary();
            Assert.Empty(matches);
        }
    }
}