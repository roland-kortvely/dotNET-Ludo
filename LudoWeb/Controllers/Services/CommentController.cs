using System.Collections.Generic;
using LudoLibrary.Interfaces;
using LudoLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace LudoWeb.Controllers.Services
{
    [Route("api/comment")]
    public class CommentController : Controller
    {
        private readonly ICommentService _service;

        public CommentController(ICommentService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<Comment> Get()
        {
            return _service.GetAll();
        }

        [HttpGet("{id}")]
        public Comment Get(int id)
        {
            return _service.Get(id);
        }

        [HttpPost]
        public ActionResult Post([FromForm] Comment comment)
        {
            _service.Add(comment);
            return Redirect("/Home/Stats");
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Comment data)
        {
            _service.Update(id, data);
        }

        [HttpPost("{id}")]
        public ActionResult Delete(int id)
        {
            _service.Delete(id);
            return Redirect("/Home/Stats");
        }

        [HttpGet("clear")]
        public void Clear()
        {
            _service.Clear();
        }
    }
}