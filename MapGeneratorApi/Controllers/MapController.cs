using Infrastructure;
using Infrastructure.PlayerDetails;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MapGeneratorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapController : ControllerBase
    {
        private static readonly Game game;
        private static readonly Map map;
        private static Map diffMap;

        static MapController()
        {
            map = MapBuilder.GetMap(MapSize.Small);
            game = new Game(map);

        }

        [HttpGet]
        [Route("generate/small")]
        public IEnumerable<Cell> GenerateSmall()
        {
            return MapBuilder.GetMap(MapSize.Small).Cells;
        }

        //[HttpGet]
        //[Route("generate/small/diff")]
        //public IEnumerable<Cell> GenerateSmallDiff()
        //{
        //    if (diffMap != null)
        //        MapBuilder.IsValid(diffMap);

        //    diffMap = MapBuilder.GetMapNotSameTypeAdjecient(MapSize.Small);
        //    return diffMap.Cells;
        //}

        [HttpGet]
        [Route("generate/big")]
        public IEnumerable<Cell> GenerateBig()
        {
            return MapBuilder.GetMap(MapSize.Big).Cells;
        }

        //[HttpGet]
        //[Route("generate/big/diff")]
        //public IEnumerable<Cell> GenerateBigDiff()
        //{
        //    return MapBuilder.GetMapNotSameTypeAdjecient(MapSize.Big).Cells;
        //}

        [HttpGet]
        [Route("setup")]
        public List<Tuple<int, int>> Setup()
        {
            var result = new List<Tuple<int, int>>();

            if (game.Players.Count != 0)
                return result;

            var p1 = new Player(PlayerColor.Blue);
            var p2 = new Player(PlayerColor.White);
            var p3 = new Player(PlayerColor.Red);

            game.Players.Add(p1);
            game.Players.Add(p2);
            game.Players.Add(p3);
            result.Add(game.PickRandomSettlement(p1));
            result.Add(game.PickRandomSettlement(p2));
            result.Add(game.PickRandomSettlement(p3));
            result.Add(game.PickRandomSettlement(p3));
            result.Add(game.PickRandomSettlement(p2));
            result.Add(game.PickRandomSettlement(p1));

            return result;
        }


        [HttpGet]
        [Route("resources")]
        public IEnumerable<ResourceBag> Resources()
        {
            return game.Players.Select(x => x.Resources);
        }

        [HttpGet]
        [Route("dices")]
        public int RollDices()
        {
            game.RollDicesAndGiveResources();
            var dices = game.DicesHistory.Peek();
            return dices.Item1 + dices.Item2;
        }
    }
}