using System.Collections.Generic;
using LudoLibrary.Interfaces;
using LudoLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace LudoWeb.Controllers.Services
{
    [Route("api/rating")]
    public class RatingController : Controller
    {
        private readonly IRatingService _service;

        public RatingController(IRatingService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<Rating> Get()
        {
            return _service.GetAll();
        }

        [HttpGet("average")]
        public float GetAverageRating()
        {
            return _service.AverageRating();
        }
        
        [HttpGet("{id}")]
        public Rating Get(int id)
        {
            return _service.Get(id);
        }

        [HttpPost]
        public ActionResult Post([FromForm] Rating rating)
        {
            _service.Add(rating);
            return Redirect("/Home/Stats");
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Rating data)
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