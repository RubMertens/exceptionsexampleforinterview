using System.Text;
using System.Linq;

namespace GameReport
{
    public class ReportGenerator
    {
        private ScoreRepository _scoreRepository;

        public ReportGenerator(ScoreRepository scoreRepository)
        {
            _scoreRepository = scoreRepository;
        }

        public string CreateReportForGame(int game)
        {
            var gameId = game.ToString();
            var scores = _scoreRepository.ScoreForGame(gameId);
            StringBuilder reportOutput = new StringBuilder();
            reportOutput.AppendLine($"Score for game {gameId}");
            foreach (var score in scores.OrderByDescending(s => s.Points))
            {
                reportOutput.AppendLine($"Player: {score.Player} Score: {score.Points}");
            }
            return reportOutput.ToString();

        }
    }



}
