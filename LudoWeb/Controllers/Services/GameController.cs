using Ludo.Boards;
using Ludo.Entities;
using Ludo.GameModes;
using Ludo.UserInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace LudoWeb.Controllers.Services
{
    [Route("api/game")]
    public class GameController : Controller
    {
        private Capsule _capsule = new Capsule {State = true};

        [HttpGet("init")]
        public Capsule Get()
        {
            Game.GameInstance();
            
            Game.Instance.Board = new WebBoard();
            Game.Instance.GameMode = new WebMode();
            Game.Instance.UserInterface = new WebUI();
            
            return new Capsule {State = true, Message = "Game initialized"};
        }

        [HttpGet("roll")]
        public Capsule Roll()
        {
            Game.Instance.Roll();

            _capsule.Message = "Rolled " + Game.Instance.Dice.Value;
            _capsule.Data["dice"] = Game.Instance.Dice.Value;

            return _capsule;
        }
    }
}