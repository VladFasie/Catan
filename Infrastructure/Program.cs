using Infrastructure.PlayerDetails;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure
{
    class Program
    {
        static void Main(string[] args)
        {
            var map = MapBuilder.GetMap(MapSize.Small);
            var game = new Game(map);

            game.InitialSetup();
            game.RollDicesAndGiveResources();
        }
    }
}
