using System;
using Ludo.Entities;
using Ludo.Interfaces;

namespace Ludo.Services
{
    public class RatingService : IRatingService
    {
        private Game _game;

        public RatingService(Game game)
        {
            _game = game;
        }

        public void Add(Rating rating)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }
    }
}