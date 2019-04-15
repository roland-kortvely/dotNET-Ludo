using Ludo.Boards;
using Ludo.Interfaces;
using Ludo.Models;
using NUnit.Framework;

namespace LudoTest
{
    public class BoardTest
    {
        private IBoard _board;
        private Figure _figure;
        private Game _game;

        [SetUp]
        public void Setup()
        {
            _game = Game.GameInstance();
            _board = _game.Board = new DefaultBoard();

            _game.Reset();

            _game.NewPlayer("A", 'A');

            _figure = _game.CurrentPlayer.Figures[0];
        }

        [Test]
        public void TestBoardDefaults()
        {
            for (var i = 0; i < _board.MaxPlayers(); i++) Assert.AreNotEqual(null, _board.Colors(i));
        }

        [Test]
        public void TestPlayerCanStart()
        {
            _game.Dice.Set(1);
            Assert.AreEqual(false, _board.PlayerCanStartWithFigure(_game));

            _game.Dice.Set(6);
            Assert.AreEqual(true, _board.PlayerCanStartWithFigure(_game));
        }

        [Test]
        public void TestPlayerCanMove()
        {
            Assert.AreEqual(Figure.States.Start, _figure.State);

            _game.Dice.Set(1);
            Assert.AreEqual(false, _board.PlayerCanMove(_game, _figure));

            _game.Dice.Set(6);
            Assert.AreEqual(false, _board.PlayerCanMove(_game, _figure));

            Assert.AreEqual(true, _board.PlayerCanStartWithFigure(_game));
            Assert.AreEqual(true, _game.StartWithFigure());

            Assert.AreEqual(false, _board.PlayerCanStartWithFigure(_game));


            _game.Dice.Set(1);
            Assert.AreEqual(true, _board.PlayerCanMove(_game, _figure));

            Assert.AreEqual(Figure.States.Playing, _figure.State);
            Assert.AreEqual(_figure.Player.StartPosition, _figure.Position);

            _game.MovePlayer(_figure);

            Assert.AreEqual(_figure.Player.StartPosition + 1, _figure.Position);
        }
    }
}