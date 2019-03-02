namespace Ludo
{
    public class DefaultMode : IGameMode
    {
        public  void Start(Game game)
        {
        }

        public void Loop(Game game)
        {
            game.CurrentPlayer.Turn(game);
                
            if (!game.CurrentPlayer.ExtraMove)
            {
                game.NextPlayer();
            }
        }
    }
}