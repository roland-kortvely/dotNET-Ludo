using System;

namespace LudoLibrary.Models
{
    [Serializable]
    public class Comment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
    }
}