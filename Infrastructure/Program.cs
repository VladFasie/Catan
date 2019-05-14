using Infrastructure.PlayerDetails;
using Infrastructure.Tradeing;

namespace Infrastructure
{
    class Program
    {
        static void Main(string[] args)
        {
            ITradeSystem tradeSystem = null;
            var blue = new RandomPlayer(PlayerColor.Blue, tradeSystem);
            var green = new RandomPlayer(PlayerColor.Green, tradeSystem);
            var red = new RandomPlayer(PlayerColor.Red, tradeSystem);

            var game = new Game(MapBuilder.GetMap(MapSize.Small));
            game.AddPlayer(blue);
            game.AddPlayer(green);
            game.AddPlayer(red);

            game.InitialSetup();

            game.PlayRound();
            game.PlayRound();
            game.PlayRound();
            game.PlayRound();
        }
    }
}
