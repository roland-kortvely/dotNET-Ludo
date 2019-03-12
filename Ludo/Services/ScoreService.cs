using System;
using System.Collections;
using Ludo.Entities;
using Ludo.Interfaces;

namespace Ludo.Services
{
    public class ScoreService : IScoreService
    {
        private Game _game;

        public ScoreService(Game game)
        {
            _game = game;
        }

        public void Add(Score score)
        {
            throw new NotImplementedException();
        }

        public IList GetTop()
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }
    }
}