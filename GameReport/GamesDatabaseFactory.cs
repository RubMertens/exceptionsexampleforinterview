using Microsoft.Data.Sqlite;
using System;

namespace GameReport
{
    public class GamesDatabaseFactory
    {
        private SqliteConnection _connection;

        public GamesDatabaseFactory(SqliteConnection connection)
        {
            _connection = connection;
        }

        public void GenerateDatabase()
        {
            string sql = @"
                CREATE TABLE Games (
                    Id INTEGER PRIMARY KEY
                );
                CREATE TABLE Players (
                    Id INTEGER PRIMARY KEY,
                    Name TEXT NOT NULL
                );
                CREATE TABLE Participants (
                    Id INTEGER PRIMARY KEY,
                    GameId INTEGER NOT NULL,
                    PlayerId INTEGER NOT NULL,
                    UNIQUE (GameId, PlayerId),
                    FOREIGN KEY (GameId)
                        REFERENCES Games (Id)
                            ON DELETE CASCADE,
                    FOREIGN KEY (PlayerId)
                        REFERENCES Players (Id)
                            ON DELETE CASCADE
                );
                CREATE TABLE Points (
                    Id INTEGER PRIMARY KEY,
                    ScoredBy INTEGER NOT NULL,
                    GameId INTEGER NOT NULL,
                    FOREIGN KEY (GameId)
                        REFERENCES Games (Id)
                            ON DELETE CASCADE,
                    FOREIGN KEY (ScoredBy)
                        REFERENCES Participants (Id)
                            ON DELETE CASCADE
                )
            ";

            using (var cmd = _connection.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }

        }
    }
}