using System.Collections.Generic;
using LudoLibrary.Interfaces;
using LudoLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace LudoWeb.Controllers.Services
{
    [Route("api/score")]
    public class ScoreController : Controller
    {
        private readonly IScoreService _service;

        public ScoreController(IScoreService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<Score> Get()
        {
            return _service.GetAll();
        }

        [HttpGet("top")]
        public IEnumerable<Score> GetTop()
        {
            return _service.GetTop();
        }

        [HttpGet("{id}")]
        public Score Get(int id)
        {
            return _service.Get(id);
        }

        [HttpPost]
        public void Post([FromBody] Score score)
        {
            _service.Add(score);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Score data)
        {
            _service.Update(id, data);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _service.Delete(id);
        }

        [HttpGet("clear")]
        public void Clear()
        {
            _service.Clear();
        }
    }
}