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

namespace LudoWeb.Controllers
{
    [Route("api/game")]
    public class GameController : Controller
    {
        private Capsule Capsule { get; } = new Capsule();

        private readonly LudoContext _context;

        public GameController(LudoContext context)
        {
            _context = context;
        }

        [HttpGet("build")]
        public Capsule Build()
        {
            return Capsule;
        }

        [HttpGet("init")]
        public Capsule Get()
        {
            Game.GameInstance();

            Game.Instance.Board = new WebBoard();
            Game.Instance.GameMode = new WebMode();
            Game.Instance.UserInterface = new WebUi();

            Capsule.Info("New game initialized");
            Capsule.Set("game", Game.Instance.ToJson());

            return Capsule;
        }

        [HttpGet("move")]
        public Capsule Move()
        {
            Capsule.Info("Not implemented exception.").Fail();

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