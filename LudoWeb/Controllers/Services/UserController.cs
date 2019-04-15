using System.Collections.Generic;
using LudoLibrary.Interfaces;
using LudoLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace LudoWeb.Controllers.Services
{
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly IService<User> _service;

        public UserController(IService<User> service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _service.GetAll();
        }

        [HttpGet("{id}")]
        public User Get(int id)
        {
            return _service.Get(id);
        }

        [HttpPost]
        public void Post([FromBody] User entry)
        {
            _service.Add(entry);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] User data)
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