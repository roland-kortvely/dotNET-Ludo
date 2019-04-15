using System.Collections.Generic;
using LudoLibrary.Interfaces;
using LudoLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace LudoWeb.Controllers.Services
{
    [Route("api/room")]
    public class RoomController : Controller
    {
        private readonly IService<Room> _service;

        public RoomController(IService<Room> service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<Room> Get()
        {
            return _service.GetAll();
        }

        [HttpGet("{id}")]
        public Room Get(int id)
        {
            return _service.Get(id);
        }

        [HttpPost]
        public void Post([FromBody] Room entry)
        {
            _service.Add(entry);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Room data)
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