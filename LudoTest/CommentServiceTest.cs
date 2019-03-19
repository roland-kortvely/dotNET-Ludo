using Ludo.Entities;
using Ludo.Interfaces;
using Ludo.Services;
using NUnit.Framework;

namespace LudoTest
{
    public class CommentServiceTest
    {
        private ICommentService _service;

        [SetUp]
        public void Setup()
        {
            _service = new CommentService();
            _service.Clear();
        }

        [Test]
        public void TestClearComments()
        {
            _service.Add(new Comment {Name = "A", Content = "AA"});
            _service.Add(new Comment {Name = "B", Content = "BB"});

            _service.Clear();

            Assert.AreEqual(0, _service.GetAll().Count);
        }

        [Test]
        public void TestAddComment()
        {
            _service.Add(new Comment {Name = "A", Content = "AA"});

            Assert.AreEqual(1, _service.GetAll().Count);

            var comment = (Comment) _service.GetAll()[0];
            Assert.AreNotEqual(null, comment.Id);
            Assert.AreEqual("A", comment.Name);
            Assert.AreEqual("AA", comment.Content);
        }

        [Test]
        public void TestNewComment()
        {
            _service.NewComment(null, "A");
            Assert.AreEqual(0, _service.GetAll().Count);
            
            _service.NewComment("A", null);
            Assert.AreEqual(0, _service.GetAll().Count);
            
            _service.NewComment("A", "A");
            Assert.AreEqual(1, _service.GetAll().Count);
        }
        
        [Test]
        public void TestGetAndDelete()
        {
            _service.Add(new Comment {Name = "A", Content = "AA"});
            var comment = (Comment) _service.GetAll()[0];
            
            Assert.AreEqual(null, _service.Get(comment.Id + 1)?.Id);
            Assert.AreEqual(comment.Id, _service.Get(comment.Id)?.Id);
            
            _service.Delete(comment.Id);
            
            Assert.AreEqual(0, _service.GetAll().Count);
        }
    }
}