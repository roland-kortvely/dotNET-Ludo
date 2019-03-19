using System.Collections.Generic;
using Ludo.Entities;
using Ludo.Interfaces;
using Ludo.Services;
using Microsoft.AspNetCore.Mvc;

namespace LudoWeb.Controllers
{
    [Route("api/rating")]
    public class RatingController : Controller
    {
        private readonly IRatingService _service = new RatingService();

        // GET: api/rating
        [HttpGet]
        public IEnumerable<Rating> Get()
        {
            return (IEnumerable<Rating>) _service.GetAll();
        }

        // GET api/rating/{id}
        [HttpGet("{id}")]
        public Rating Get(int id)
        {
            return _service.Get(id);
        }

        // POST api/rating
        [HttpPost]
        public void Post([FromBody] Rating rating)
        {
            _service.Add(rating);
        }

        // PUT api/rating/{id}
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Rating data)
        {
            _service.Update(id, data);
        }

        // DELETE api/rating/{id}
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _service.Delete(id);
        }
    }
}