using System;
using System.Collections.Generic;
using System.Linq;
using LudoLibrary.Database;
using LudoLibrary.Interfaces;
using LudoLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace LudoLibrary.Services
{
    public class RoomService : IService<Room>
    {
        private readonly LudoContext _db;

        public RoomService(LudoContext db)
        {
            _db = db;
        }

        public void Add(Room entry)
        {
            _db.Add(entry);
            _db.SaveChanges();
        }

        public void Update(int id, Room data)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            var entity = _db.Rooms.Find(id);
            if (entity == null) return;

            _db.Rooms.Remove(entity);
            _db.SaveChanges();
        }

        public void Clear()
        {
            _db.Database.ExecuteSqlCommand("DELETE FROM Rooms");
        }

        public Room Get(int id)
        {
            return _db.Rooms.Find(id);
        }

        public IList<Room> GetAll()
        {
            return (from s in _db.Rooms.Include(r => r.Users) select s)
                .ToList();
        }
    }
}