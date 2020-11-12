using System.Collections.Generic;
using GameReport;
using Microsoft.Data.Sqlite;

namespace GameReportTests
{
    public class GameBuilder
    {
        private int _gameId;
        private HashSet<Player> _players;
        private List<Player> _points;
        public GameBuilder()
        {
            _players = new HashSet<Player>();
            _points = new List<Player>();
        }

        public GameBuilder WithId(int gameId)
        {
            _gameId = gameId;
            return this;
        }

        public GameBuilder WithParticipant(Player player)
        {
            _players.Add(player);
            return this;
        }

        public GameBuilder WithPointFor(Player player)
        {
            _points.Add(player);
            return this;
        }

        public void Build(SqliteConnection connection)
        {
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO Games (Id) VALUES (@Id)";
                cmd.Parameters.AddWithValue("Id", _gameId);
                cmd.ExecuteNonQuery();
            }
            var participantMap = new Dictionary<Player, long>();
            foreach (var participant in _players)
            {
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Participants (GameId, PlayerId) VALUES (@GameId, @PlayerId); 
                                        SELECT last_insert_rowid()";
                    cmd.Parameters.AddWithValue("GameId", _gameId);
                    cmd.Parameters.AddWithValue("PlayerId", participant.Id);
                    participantMap.Add(participant, (long)cmd.ExecuteScalar());
                }
            }
            foreach (var point in _points)
            {
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Points (GameId, ScoredBy) VALUES (@GameId, @ParticipantId)";
                    cmd.Parameters.AddWithValue("GameId", _gameId);
                    cmd.Parameters.AddWithValue("ParticipantId", participantMap[point]);
                    cmd.ExecuteNonQuery();
                }
            }

        }
    }

}