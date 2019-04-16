using System.Collections.Generic;
using System.Linq;
using Ludo.Boards;
using Ludo.GameModes;
using Ludo.Models;
using Ludo.UserInterfaces;
using LudoLibrary.Database;
using LudoLibrary.Interfaces;
using LudoLibrary.Models;
using LudoLibrary.Services;
using LudoWeb.Controllers.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LudoWeb.Controllers
{
    [Route("api/game")]
    public class GameController : Controller
    {
        private Capsule Capsule { get; } = new Capsule();

        private readonly LudoContext _db;

        private readonly IService<User> _serviceUser;
        private readonly IService<Command> _serviceCommand;

        public GameController(IService<Command> serviceCommand, LudoContext db, IService<User> serviceUser)
        {
            _serviceCommand = serviceCommand;
            _db = db;
            _serviceUser = serviceUser;
        }

        [HttpGet("build")]
        public Capsule Build()
        {
            return Capsule;
        }

        [HttpGet("init")]
        public Capsule Init()
        {
            Game.CreateInstance();

            Game.Instance.Board = new WebBoard();
            Game.Instance.GameMode = new WebMode();
            Game.Instance.UserInterface = new WebUi();

            Game.Instance.Reset();

            Game.Instance.NewPlayer("Player 1", 'A');
            Game.Instance.NewPlayer("Player 2", 'B');

            Capsule.Info("New game initialized");
            Capsule.Set("game", Game.Instance.ToJson());

            _serviceCommand.Clear();

            return Capsule;
        }

        [HttpGet("info")]
        public Capsule Info()
        {
            Game.CreateInstance();

            Capsule.Set("game", Game.Instance.ToJson());

            return Capsule;
        }

        [HttpGet("commands/{id}")]
        public IList<Command> Commands(int id)
        {
            var q = (from s in _db.Commands where s.User.Id == id select s);
            var l = q.ToList();

            foreach (var c in q)
            {
                _db.Commands.Remove(c);
            }
            
            _db.SaveChanges();
            
            return l;
        }

        [HttpPost("move")]
        public Capsule Move([FromBody] JObject data)
        {
            var playerIndex = int.Parse(data["figure"]["playerIndex"].ToString());
            var figureIndex = int.Parse(data["figure"]["pieceIndex"].ToString());
            var position = int.Parse(data["figure"]["pathPointer"].ToString());

            Capsule.Info("I:" + playerIndex + " F:" + figureIndex + " P:" + position);

            if (playerIndex > Game.Instance.Board.MaxPlayers())
            {
                Capsule.Info("Corrupted request.").Fail();
                return Capsule;
            }

            if (figureIndex > Game.Instance.Board.PlayerFigures())
            {
                Capsule.Info("Corrupted request.").Fail();
                return Capsule;
            }

            if (Game.Instance.CurrentPlayerIndex != playerIndex)
            {
                Capsule.Info("It's not your turn.").Fail();
                return Capsule;
            }

            var player = Game.Instance.Players[playerIndex];
            var figure = player.Figures[figureIndex];

            if (Game.Instance.PlayerCanMove(figure))
            {
                Capsule.Info("You can't move with this figure").Fail();
                return Capsule;
            }
            
            var exec = new JObject
            {
                ["command"] = "move",
                ["dice"] = Game.Instance.Dice.Value,
                ["figure"] = figureIndex,
                ["player"] = playerIndex
            };

            var u = _serviceUser.Get(playerIndex == 0 ? 1 : 2);
            _serviceCommand.Add(new Command {Exec = exec.ToString(), User = u});

            Game.Instance.NextPlayer();

            Capsule.Info("You moved with a figure.");
            return Capsule;
        }

        [HttpGet("roll")]
        public Capsule Roll()
        {
            Game.Instance.Roll();

            Capsule.Info("Rolled " + Game.Instance.Dice.Value);
            Capsule.Set("dice", Game.Instance.Dice.Value);

            return Capsule;
        }
    }
}