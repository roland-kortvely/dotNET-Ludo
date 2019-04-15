using LudoLibrary.Interfaces;
using LudoLibrary.Models;
using LudoLibrary.Services;
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

            var score = _service.GetAll()[0];
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
            Assert.AreEqual("B", scores[0].Name);
            Assert.AreEqual("C", scores[1].Name);
            Assert.AreEqual("A", scores[2].Name);
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
            Assert.AreEqual(20, scores[0].Points);
            Assert.AreEqual(10, scores[1].Points);
        }

        [Test]
        public void TestGetAndDelete()
        {
            _service.Add(new Score {Name = "A", Points = 10});
            var score = _service.GetAll()[0];

            Assert.AreEqual(null, _service.Get(score.Id + 1)?.Id);
            Assert.AreEqual(score.Id, _service.Get(score.Id)?.Id);

            _service.Delete(score.Id);

            Assert.AreEqual(0, _service.GetAll().Count);
        }
    }
}