using System;
using System.Collections.Generic;

namespace LudoLibrary.Models
{
    [Serializable]
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; } = new List<User>();
    }
}