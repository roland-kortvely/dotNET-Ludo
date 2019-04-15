using System;

namespace LudoLibrary.Models
{
    [Serializable]
    public class Room
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public User[] Users { get; set; }
    }
}