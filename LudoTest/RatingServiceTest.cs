using Ludo.Entities;
using Ludo.Interfaces;
using Ludo.Services;
using NUnit.Framework;

namespace LudoTest
{
    public class RatingServiceTest
    {
        private IRatingService _service;

        [SetUp]
        public void Setup()
        {
            _service = new RatingService();
            _service.Clear();
        }

        [Test]
        public void TestClearRatings()
        {
            _service.Add(new Rating {Stars = 2, Content = "A"});
            _service.Add(new Rating {Stars = 4, Content = "B"});

            _service.Clear();

            Assert.AreEqual(0, _service.GetAll().Count);
        }

        [Test]
        public void TestAddRating()
        {
            _service.Add(new Rating {Stars = 2, Content = "A"});

            Assert.AreEqual(1, _service.GetAll().Count);

            var rating = (Rating) _service.GetAll()[0];
            Assert.AreNotEqual(null, rating.Id);
            Assert.AreEqual("A", rating.Content);
            Assert.AreEqual(2, rating.Stars);
        }

        [Test]
        public void TestRate()
        {
            _service.Rate(-1, null);
            Assert.AreEqual(0, _service.GetAll().Count);

            _service.Rate(6, null);
            Assert.AreEqual(0, _service.GetAll().Count);

            _service.Rate(5, null);
            Assert.AreEqual(0, _service.GetAll().Count);

            _service.Rate(5, "A");
            Assert.AreEqual(1, _service.GetAll().Count);
        }

        [Test]
        public void TestAverageRating()
        {
            Assert.AreEqual(0.0f, _service.AverageRating());

            _service.Rate(5, "A");
            Assert.AreEqual(5.0f, _service.AverageRating());

            _service.Rate(1, "B");
            Assert.AreEqual(3.0f, _service.AverageRating());

            _service.Rate(3, "C");
            Assert.AreEqual(3.0f, _service.AverageRating());
        }
    }
}