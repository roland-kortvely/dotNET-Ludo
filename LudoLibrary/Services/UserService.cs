using System;
using System.Collections.Generic;
using System.Linq;
using LudoLibrary.Database;
using LudoLibrary.Interfaces;
using LudoLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace LudoLibrary.Services
{
    public class UserService : IService<User>
    {
        private readonly LudoContext _db;

        public UserService(LudoContext db)
        {
            _db = db;
        }

        public void Add(User entry)
        {
            _db.Add(entry);
            _db.SaveChanges();
        }

        public void Update(int id, User data)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            var entity = _db.Users.Find(id);
            if (entity == null) return;

            _db.Users.Remove(entity);
            _db.SaveChanges();
        }

        public void Clear()
        {
            _db.Database.ExecuteSqlCommand("DELETE FROM Users");
        }

        public User Get(int id)
        {
            throw new NotImplementedException();
        }

        public IList<User> GetAll()
        {
            return (from s in _db.Users select s)
                .ToList();
        }
    }
}