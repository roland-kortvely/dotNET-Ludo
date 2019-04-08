using System.Collections.Generic;
using Ludo.Entities;
using Ludo.Interfaces;
using Ludo.Services;
using Microsoft.AspNetCore.Mvc;

namespace LudoWeb.Controllers.Services
{
    [Route("api/score")]
    public class ScoreController : Controller
    {
        private readonly IScoreService _service = new ScoreService();

        // GET: api/score
        [HttpGet]
        public IEnumerable<Score> Get()
        {
            return _service.GetTop();
        }

        // GET api/score/{id}
        [HttpGet("{id}")]
        public Score Get(int id)
        {
            return _service.Get(id);
        }

        // POST api/score
        [HttpPost]
        public void Post([FromBody] Score score)
        {
            _service.Add(score);
        }

        // PUT api/score/{id}
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Score data)
        {
            _service.Update(id, data);
        }

        // DELETE api/score/{id}
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _service.Delete(id);
        }
    }
}