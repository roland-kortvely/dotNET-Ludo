using Ludo.Boards;
using Ludo.Entities;
using NUnit.Framework;

namespace LudoTest
{
    public class PlayerTest
    {
        private Game _game;
        private Player _player;

        [SetUp]
        public void Setup()
        {
            _game = Game.GameInstance();
            _game.Board = new DefaultBoard();

            _game.NewPlayer("A", 'A');

            _player = _game.CurrentPlayer;
        }

        [Test]
        public void TestPlayerDefaults()
        {
            Assert.AreEqual(0, _player.Index);

            Assert.AreEqual("A", _player.Name);
            Assert.AreEqual('A', _player.Symbol);

            Assert.AreEqual(4, _player.Figures.Count);

            Assert.AreEqual(_game.Board.StartPosition(1), _player.StartPosition);
            Assert.AreEqual(_game.Board.FinalPosition(1), _player.FinalPosition);

            _player.Reset();

            Assert.AreEqual(true, _player.FirstMove);
            Assert.AreEqual(false, _player.ExtraMove);

            Assert.AreEqual(true, _player.HasFigureAtStart(0));
            Assert.AreEqual(true, _player.HasFigureAtStart(1));
            Assert.AreEqual(true, _player.HasFigureAtStart(2));
            Assert.AreEqual(true, _player.HasFigureAtStart(3));
        }

        [Test]
        public void TestPlayerCanStart()
        {
            Assert.AreEqual(true, _player.StartWithFigure());
            Assert.AreEqual(Figure.States.Playing, _player.Figures[0].State);
            Assert.AreEqual(true, _player.StartWithFigure());
            Assert.AreEqual(Figure.States.Playing, _player.Figures[1].State);
            Assert.AreEqual(true, _player.StartWithFigure());
            Assert.AreEqual(Figure.States.Playing, _player.Figures[2].State);
            Assert.AreEqual(true, _player.StartWithFigure());
            Assert.AreEqual(Figure.States.Playing, _player.Figures[3].State);
         
            Assert.AreEqual(false, _player.StartWithFigure());
            Assert.AreEqual(false, _player.HasFigureAtStart(0));
            Assert.AreEqual(false, _player.HasFigureAtStart(1));
            Assert.AreEqual(false, _player.HasFigureAtStart(2));
            Assert.AreEqual(false, _player.HasFigureAtStart(3));
        }
    }
}