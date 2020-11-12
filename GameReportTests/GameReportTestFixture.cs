using NUnit.Framework;
using GameReport;
using Microsoft.Data.Sqlite;
using System;

namespace GameReportTests
{
    public class GameReportTestFixture
    {
        private SqliteConnection _connection;

        [SetUp]
        public void Setup()
        {
            _connection = new SqliteConnection("Data Source=:memory:");
            _connection.Open();
            new GamesDatabaseFactory(_connection).GenerateDatabase();
        }

        [TearDown]
        public void Cleanup()
        {
            _connection.Dispose();
        }

        [Test]
        public void CreateReportForGame_PlayerWithoutScore_IsIncludedInReport()
        {
            var ruben = new PlayerBuilder()
                .WithId(1)
                .WithName("Ruben")
                .Build(_connection);
            var benny = new PlayerBuilder()
                .WithId(2)
                .WithName("Benny")
                .Build(_connection);
            var hanjo = new PlayerBuilder()
                .WithId(3)
                .WithName("Hanjo")
                .Build(_connection);
            new GameBuilder()
                .WithId(42)
                .WithParticipant(ruben)
                .WithParticipant(benny)
                .WithParticipant(hanjo)
                .WithPointFor(ruben)
                .WithPointFor(benny)
                .WithPointFor(ruben)
                .Build(_connection);

            var reportGenerator = new ReportGenerator(new ScoreRepository(_connection));

            var output = reportGenerator.CreateReportForGame(42);

            Assert.AreEqual(
            "Score for game 42" + Environment.NewLine +
            "Player: Ruben Score: 2" + Environment.NewLine +
            "Player: Benny Score: 1" + Environment.NewLine +
            "Player: Hanjo Score: 0" + Environment.NewLine, output);
        }
    }
}