using Ludo.Entities;
using Ludo.Interfaces;
using Ludo.Services;
using NUnit.Framework;

namespace LudoTest
{
    public class ScoreServiceTest
    {
        private IScoreService _service;

        [SetUp]
        public void Setup()
        {
            _service = new ScoreService();
            _service.Clear();
        }

        [Test]
        public void TestClearScores()
        {
            _service.Add(new Score {Name = "A", Points = 10});
            _service.Add(new Score {Name = "B", Points = 20});

            _service.Clear();

            Assert.AreEqual(0, _service.GetAll().Count);
        }

        [Test]
        public void TestAddScore()
        {
            _service.Add(new Score {Name = "A", Points = 10});

            Assert.AreEqual(1, _service.GetAll().Count);

            var score = (Score) _service.GetAll()[0];
            Assert.AreNotEqual(null, score.Id);
            Assert.AreEqual("A", score.Name);
            Assert.AreEqual(10, score.Points);
        }

        [Test]
        public void TestTopScore()
        {
            _service.Add(new Score {Name = "A", Points = 10});
            _service.Add(new Score {Name = "B", Points = 50});
            _service.Add(new Score {Name = "C", Points = 30});

            Assert.AreEqual(3, _service.GetAll().Count);

            var scores = _service.GetTop();
            Assert.AreEqual("B", ((Score) scores[0]).Name);
            Assert.AreEqual("C", ((Score) scores[1]).Name);
            Assert.AreEqual("A", ((Score) scores[2]).Name);
        }

        [Test]
        public void TestIncreaseScore()
        {
            _service.IncreaseScore(null);
            Assert.AreEqual(0, _service.GetAll().Count);

            _service.Add(new Score {Name = "A", Points = 10});

            _service.IncreaseScore("A");
            _service.IncreaseScore("B");

            var scores = _service.GetAll();
            Assert.AreEqual(20, ((Score) scores[0]).Points);
            Assert.AreEqual(10, ((Score) scores[1]).Points);
        }
    }
}