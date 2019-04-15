using System;
using System.Collections.Generic;
using System.Linq;
using LudoLibrary.Database;
using LudoLibrary.Interfaces;
using LudoLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace LudoLibrary.Services
{
    public class CommandService : IService<Command>
    {
        private readonly LudoContext _db;

        public CommandService(LudoContext db)
        {
            _db = db;
        }

        public void Add(Command entry)
        {
            _db.Add(entry);
            _db.SaveChanges();
        }

        public void Update(int id, Command data)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            var entity = _db.Commands.Find(id);
            if (entity == null) return;

            _db.Commands.Remove(entity);
            _db.SaveChanges();
        }

        public void Clear()
        {
            _db.Database.ExecuteSqlCommand("DELETE FROM Commands");
        }

        public Command Get(int id)
        {
            return _db.Commands.Find(id);
        }

        public IList<Command> GetAll()
        {
            return (from s in _db.Commands select s)
                .ToList();
        }
    }
}