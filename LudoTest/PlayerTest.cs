using Ludo.Boards;
using Ludo.Entities;
using Ludo.Interfaces;
using NUnit.Framework;

namespace LudoTest
{
    public class PlayerTest
    {
        private Game _game;
        private IBoard _board;
        private Player _player;
        private Figure _figure;

        [SetUp]
        public void Setup()
        {
            _game = Game.GameInstance();
            _board = _game.Board = new DefaultBoard();

            _game.Reset();

            _game.NewPlayer("A", 'A');

            _player = _game.CurrentPlayer;

            _figure = _game.CurrentPlayer.Figures[0];
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

            Assert.AreEqual(false, _player.Finished());
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

        [Test]
        public void TestMovePossible()
        {
            _game.Dice.Set(1);
            Assert.AreEqual(false, _player.MovePossible(_game));

            _game.Dice.Set(6);
            Assert.AreEqual(true, _player.MovePossible(_game));
        }
    }
}