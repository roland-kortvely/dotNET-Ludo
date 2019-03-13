using System;

namespace Ludo.Entities
{
    [Serializable]
    public class Rating
    {
        public int Id { get; set; }
        public int Stars { get; set; }

        public string Content { get; set; }
    }
}