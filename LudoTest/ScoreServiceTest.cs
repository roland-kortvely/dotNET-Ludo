using Ludo.Entities;
using Ludo.Interfaces;
using Ludo.Services;
using NUnit.Framework;

namespace LudoTest
{
    public class Tests
    {
        private IScoreService _scoreService;

        [SetUp]
        public void Setup()
        {
            _scoreService = new ScoreService();
            _scoreService.Clear();
        }

        [Test]
        public void TestClearScores()
        {
            _scoreService.Add(new Score {Name = "A", Points = 10});
            _scoreService.Add(new Score {Name = "B", Points = 20});

            _scoreService.Clear();

            Assert.AreEqual(0, _scoreService.GetAll().Count);
        }

        [Test]
        public void TestAddScore()
        {
            _scoreService.Add(new Score {Name = "A", Points = 10});

            Assert.AreEqual(1, _scoreService.GetAll().Count);

            var score = (Score) _scoreService.GetAll()[0];
            Assert.AreEqual("A", score.Name);
            Assert.AreEqual(10, score.Points);
        }

        [Test]
        public void TestTopScore()
        {
            _scoreService.Add(new Score {Name = "A", Points = 10});
            _scoreService.Add(new Score {Name = "B", Points = 50});
            _scoreService.Add(new Score {Name = "C", Points = 30});

            Assert.AreEqual(3, _scoreService.GetAll().Count);

            var scores = _scoreService.GetTop();
            Assert.AreEqual("B", ((Score) scores[0]).Name);
            Assert.AreEqual("C", ((Score) scores[1]).Name);
            Assert.AreEqual("A", ((Score) scores[2]).Name);
        }

        [Test]
        public void TestIncreaseScore()
        {
            _scoreService.IncreaseScore(null);
            Assert.AreEqual(0, _scoreService.GetAll().Count);

            _scoreService.Add(new Score {Name = "A", Points = 10});

            _scoreService.IncreaseScore("A");
            _scoreService.IncreaseScore("B");

            var scores = _scoreService.GetAll();
            Assert.AreEqual(20, ((Score) scores[0]).Points);
            Assert.AreEqual(10, ((Score) scores[1]).Points);
        }
    }
}