using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ludo.Entities;
using Ludo.Interfaces;
using Ludo.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LudoWeb.Controllers
{
    [Route("api/score")]
    public class ScoreController : Controller
    {
        private readonly IScoreService _service = new ScoreService();

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Score> Get()
        {
            return (IEnumerable<Score>) _service.GetTop();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}