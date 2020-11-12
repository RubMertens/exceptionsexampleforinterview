using NUnit.Framework;
using GameReport;
using Microsoft.Data.Sqlite;
using System;

namespace GameReportTests
{
    public class GameReportTestFixture
    {
        private SqliteConnection _connection;
        private Player _ruben;
        private Player _benny;
        private GameBuilder _gameBuilder;


        [SetUp]
        public void Setup()
        {
            _connection = new SqliteConnection("Data Source=:memory:");
            _connection.Open();
            new GamesDatabaseFactory(_connection).GenerateDatabase();
            _ruben = new PlayerBuilder()
               .WithId(1)
               .WithName("Ruben")
               .Build(_connection);
            _benny = new PlayerBuilder()
                .WithId(2)
                .WithName("Benny")
                .Build(_connection);
            _gameBuilder = new GameBuilder()
               .WithId(42);
        }

        [TearDown]
        public void Cleanup()
        {
            _connection.Dispose();
        }

        [Test]
        public void CreateReportForGame_NoPlayers_OnlyTitleShown()
        {
            _gameBuilder
                .Build(_connection);

            var reportGenerator = new ReportGenerator(new ScoreRepository(_connection));

            var output = reportGenerator.CreateReportForGame(42);

            Assert.AreEqual(
                "Score for game 42" + Environment.NewLine, output);
        }

        [Test]
        public void CreateReportForGame_WithScores_CalculatesCorrectTotal()
        {
            _gameBuilder
                .WithParticipant(_ruben)
                .WithPointFor(_ruben)
                .WithPointFor(_ruben)
                .Build(_connection);

            var reportGenerator = new ReportGenerator(new ScoreRepository(_connection));

            var output = reportGenerator.CreateReportForGame(42);

            Assert.AreEqual(
                "Score for game 42" + Environment.NewLine +
                "Player: Ruben Score: 2" + Environment.NewLine, output);
        }

        [Test]
        public void CreateReportForGame_MultiplePlayersWithPoints_AssignsScoreToEachPlayer()
        {
            _gameBuilder
               .WithParticipant(_ruben)
               .WithParticipant(_benny)
               .WithPointFor(_ruben)
               .WithPointFor(_benny)
               .WithPointFor(_ruben)
               .Build(_connection);

            var reportGenerator = new ReportGenerator(new ScoreRepository(_connection));

            var output = reportGenerator.CreateReportForGame(42);

            Assert.AreEqual(
            "Score for game 42" + Environment.NewLine +
            "Player: Ruben Score: 2" + Environment.NewLine +
            "Player: Benny Score: 1" + Environment.NewLine, output);
        }

        [Test]
        public void CreateReportForGame_PlayerWithoutScore_IsIncludedInReport()
        {
            var hanjo = new PlayerBuilder()
                .WithId(3)
                .WithName("Hanjo")
                .Build(_connection);
            _gameBuilder
                .WithParticipant(_ruben)
                .WithParticipant(_benny)
                .WithParticipant(hanjo)
                .WithPointFor(_ruben)
                .WithPointFor(_benny)
                .WithPointFor(_ruben)
                .Build(_connection);

            var reportGenerator = new ReportGenerator(new ScoreRepository(_connection));

            var output = reportGenerator.CreateReportForGame(42);

            Assert.AreEqual(
            "Score for game 42" + Environment.NewLine +
            "Player: Ruben Score: 2" + Environment.NewLine +
            "Player: Benny Score: 1" + Environment.NewLine +
            "Player: Hanjo Score: 0" + Environment.NewLine, output);
        }

        [Test]
        public void CreateReportForGame_PlayersWithIdenticalName_CorrectlyAssignScore()
        {
            var otherBenny = new PlayerBuilder()
                .WithId(3)
                .WithName("Benny")
                .Build(_connection);
            _gameBuilder
                .WithParticipant(_benny)
                .WithParticipant(otherBenny)
                .WithPointFor(_benny)
                .WithPointFor(otherBenny)
                .Build(_connection);

            var reportGenerator = new ReportGenerator(new ScoreRepository(_connection));

            var output = reportGenerator.CreateReportForGame(42);

            Assert.AreEqual(
            "Score for game 42" + Environment.NewLine +
            "Player: Benny (2) Score: 2" + Environment.NewLine +
            "Player: Benny (3) Score: 1" + Environment.NewLine, output);
        }
    }
}