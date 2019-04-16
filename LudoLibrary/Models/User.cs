using System;
using System.Collections.Generic;

namespace LudoLibrary.Models
{
    [Serializable]
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Command> Commands { get; set; } = new List<Command>();
    }
}