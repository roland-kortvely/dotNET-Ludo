using System;

namespace LudoLibrary.Models
{
    [Serializable]
    public class Score
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
    }
}