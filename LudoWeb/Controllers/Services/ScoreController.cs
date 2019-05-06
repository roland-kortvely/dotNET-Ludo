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
        public ActionResult Post([FromForm] Score score)
        {
            _service.Add(score);
            return Redirect("/Home/Stats");
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Score data)
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