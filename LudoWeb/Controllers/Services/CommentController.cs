using System.Collections.Generic;
using Ludo.Entities;
using Ludo.Interfaces;
using Ludo.Services;
using Microsoft.AspNetCore.Mvc;

namespace LudoWeb.Controllers.Services
{
    [Route("api/comment")]
    public class CommentController : Controller
    {
        private readonly ICommentService _service = new CommentService();

        // GET: api/comment
        [HttpGet]
        public IEnumerable<Comment> Get()
        {
            return _service.GetAll();
        }

        // GET api/comment/{id}
        [HttpGet("{id}")]
        public Comment Get(int id)
        {
            return _service.Get(id);
        }

        // POST api/comment
        [HttpPost]
        public void Post([FromBody] Comment comment)
        {
            _service.Add(comment);
        }

        // PUT api/comment/{id}
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Comment data)
        {
            _service.Update(id, data);
        }

        // DELETE api/comment/{id}
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _service.Delete(id);
        }
    }
}