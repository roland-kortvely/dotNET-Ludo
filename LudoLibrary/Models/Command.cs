using System;

namespace LudoLibrary.Models
{
    [Serializable]
    public class Command
    {
        public int Id { get; set; }
        public string Exec { get; set; }
        public User User { get; set; }
    }
}