namespace SportRadar.Infrastructure.Models
{
    public class UpdateMatchDto
    {
        public int HomeTeamScore { get; set; }
        public int AwayTeamScore{ get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
    }
}
