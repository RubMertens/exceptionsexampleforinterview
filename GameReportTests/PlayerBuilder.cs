using GameReport;
using Microsoft.Data.Sqlite;

namespace GameReportTests
{
    public class PlayerBuilder
    {
        private int _id;
        private string _name;

        public PlayerBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public PlayerBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public Player Build(SqliteConnection connection)
        {
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO Players (Id, Name) VALUES (@Id, @Name) ";
                cmd.Parameters.AddWithValue("Id", _id);
                cmd.Parameters.AddWithValue("Name", _name);
                cmd.ExecuteNonQuery();
            }
            return new Player
            {
                Id = _id,
                Name = _name
            };
        }

    }
}