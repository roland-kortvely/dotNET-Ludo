using System.Collections.Generic;
using System.Linq;
using Ludo.Boards;
using Ludo.GameModes;
using Ludo.Models;
using Ludo.UserInterfaces;
using LudoLibrary.Database;
using LudoLibrary.Interfaces;
using LudoLibrary.Models;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IService<Room> _serviceRoom;

        public GameController(LudoContext db, IService<User> serviceUser, IService<Room> serviceRoom)
        {
            _db = db;
            _serviceUser = serviceUser;
            _serviceRoom = serviceRoom;
        }

        [HttpGet("build")]
        public Capsule Build()
        {
            //_serviceUser.Add(new User{Name = "Player 1"});
            //_serviceUser.Add(new User{Name = "Player 2"});

            //var users = new List<User> {_serviceUser.Get(1), _serviceUser.Get(2)};
            //_serviceRoom.Add(new Room {Name = "1 vs 1", Users = users});

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

            Game.Instance.Players[1].Status = "Waiting for opponents turn.";

            Capsule.Info("New game initialized");
            Capsule.Set("game", Game.Instance.ToJson());

            return Capsule;
        }

        [HttpGet("sync")]
        public Capsule Info()
        {
            var l = new List<JObject>();

            for (var playerIndex = 0; playerIndex < Game.Instance.Players.Count; playerIndex++)
            {
                var player = Game.Instance.Players[playerIndex];
                var index = playerIndex;
                l.AddRange(player.Figures.Select((figure, figureIndex) => new JObject
                    {
                        ["player"] = index,
                        ["figure"] = figureIndex,
                        ["position"] = figure.AbstractPosition != 0
                            ? figure.AbstractPosition + 1
                            : figure.State == Figure.States.Start
                                ? 0
                                : 1
                    })
                );
            }

            Capsule.Set("game", Game.Instance.ToJson());
            Capsule.Set("sync", JsonConvert.SerializeObject(l));

            return Capsule;
        }

        [HttpPost("move")]
        public Capsule Move([FromBody] JObject data)
        {
            var currentPlayer = int.Parse(data["currentPlayer"].ToString());
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

            if (Game.Instance.CurrentPlayerIndex != currentPlayer)
            {
                Game.Instance.Players[currentPlayer].Status = "It's not your turn.";
                Capsule.Info("It's not your turn.").Fail();
                return Capsule;
            }

            if (Game.Instance.CurrentPlayerIndex != playerIndex)
            {
                Game.Instance.Players[playerIndex].Status = "It's not your turn.";
                Capsule.Info("It's not your turn.").Fail();
                return Capsule;
            }

            var player = Game.Instance.Players[playerIndex];
            var figure = player.Figures[figureIndex];
          
            if (figure.State == Figure.States.Start)
            {
                if (!Game.Instance.PlayerCanStartWithFigure(figure))
                {
                    Game.Instance.CurrentPlayer.Status = Game.Instance.Dice.Value == 6
                        ? "You can't start with this figure."
                        : "You need to roll six.";

                    Capsule.Info("You can't start with this figure").Fail();
                    return Capsule;
                }

                Game.Instance.StartWithFigure(figure);
            }
            else
            {
                /*
                if (!Game.Instance.PlayerCanMove(figure))
                {
                    Game.Instance.CurrentPlayer.Status = "X";
                    Capsule.Info("Unable to move with a figure.").Fail();
                    return Capsule;
                }*/

                if (Game.Instance.FigureKicked(figure))
                {
                    //kicked
                }

                if (!Game.Instance.MovePlayer(figure))
                {
                    Game.Instance.CurrentPlayer.Status = "Unable to move with a figure.";
                    Capsule.Info("Unable to move with a figure.").Fail();
                    return Capsule;
                }
            }

            Game.Instance.CurrentPlayer.Status = "You moved with a figure.";
            
            Game.Instance.NextPlayer();

            Game.Instance.CurrentPlayer.Status = "Roll the dice.";
            
            Capsule.Info("You moved with a figure.");
            return Capsule;
        }

        [HttpPost("roll")]
        public Capsule Roll([FromBody] JObject data)
        {
            var currentPlayer = int.Parse(data["currentPlayer"].ToString());

            if (Game.Instance.CurrentPlayerIndex != currentPlayer)
            {
                Game.Instance.Players[currentPlayer].Status = "It's not your turn.";
                Capsule.Info("It's not your turn.").Fail();
                return Capsule;
            }

            Game.Instance.Roll();

            Game.Instance.CurrentPlayer.Status = "Rolled " + Game.Instance.Dice.Value + ".";

            Capsule.Info("Rolled " + Game.Instance.Dice.Value);
            Capsule.Set("dice", Game.Instance.Dice.Value);

            return Capsule;
        }
    }
}