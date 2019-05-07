using Infrastructure.PlayerDetails;
using System;

namespace Infrastructure
{
    class Program
    {
        static void Main(string[] args)
        {
            var map = MapBuilder.GetMap(MapSize.Small);
            Console.WriteLine(map);
            var g = new Game(map);

            var p1 = new Player(PlayerColor.Blue);
            var p2 = new Player(PlayerColor.White);
            var p3 = new Player(PlayerColor.Red);

            g.Players.Add(p1);
            g.Players.Add(p2);
            g.Players.Add(p3);
            g.PickRandomSettlement(p1);
            g.PickRandomSettlement(p2);
            g.PickRandomSettlement(p3);
            g.PickRandomSettlement(p3);
            g.PickRandomSettlement(p2);
            g.PickRandomSettlement(p1);

            char k;
            do
            {
                k = Console.ReadKey().KeyChar;
                g.RollDicesAndGiveResources();

                Console.WriteLine(p1);
                Console.WriteLine(p2);
                Console.WriteLine(p3);
                Console.WriteLine();

            } while (k != 's');
        }
    }
}
