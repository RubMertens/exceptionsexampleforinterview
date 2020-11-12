using System.Linq;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace GameReport
{
    public class ScoreRepository
    {
        private SqliteConnection _connection;

        public ScoreRepository(SqliteConnection connection)
        {
            _connection = connection;
        }

        public List<Score> ScoreForGame(string gameId)
        {
            string sql = @"
                SELECT pl.Name, COUNT(p.Id) TotalPoints FROM Games g
                INNER JOIN Participants pa ON pa.GameId = g.Id
                INNER JOIN Players pl ON pa.PlayerId = pl.Id
                INNER JOIN Points p ON p.GameId = g.Id AND p.ScoredBy = pa.Id
                WHERE g.Id = " + gameId +
              " GROUP BY pl.Name";

            List<Score> scores = new List<Score>();
            using (var cmd = _connection.CreateCommand())
            {
                cmd.CommandText = sql;
                using (var reader = cmd.ExecuteReader())
                {
                    int nameOrdinal = reader.GetOrdinal("Name");
                    int pointOrdinal = reader.GetOrdinal("TotalPoints");
                    while (reader.Read())
                    {
                        Score score = new Score();
                        scores.Add(score);
                        score.Player = reader.GetString(nameOrdinal);
                        score.Points = reader.GetInt32(pointOrdinal);
                    }
                }

            }
            return scores;
        }
    }

    public class Score
    {
        public string Player { get; set; }
        public int Points { get; set; }
    }


}