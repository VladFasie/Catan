using Infrastructure.Extensions;
using Infrastructure.PlayerDetails;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure
{
    public sealed class Game
    {
        public IEnumerable<BasePlayer> Players => _playerDict.Values.Select(x => x.Item1);
        public Stack<Tuple<int, int>> DicesHistory { get; }

        public Map Map { get; }
        private Dictionary<PlayerColor, Tuple<BasePlayer, ResourceBag>> _playerDict;
        private Random _random;

        public Game(Map map)
        {
            DicesHistory = new Stack<Tuple<int, int>>();
            _random = new Random();
            _playerDict = new Dictionary<PlayerColor, Tuple<BasePlayer, ResourceBag>>();

            Map = map;
        }

        public void AddPlayer(BasePlayer player)
        {
            var resources = new ResourceBag();
            _playerDict[player.Color] = new Tuple<BasePlayer, ResourceBag>(player, resources);
        }

        public void InitialSetup()
        {
            var order = new List<Tuple<BasePlayer, int>>();
            foreach (var player in Players)
            {
                var dices = RollDices();
                var sum = dices.Item1 + dices.Item2;
                order.Add(new Tuple<BasePlayer, int>(player, sum));
            }

            order.Sort();
            // TODO treat draws

            for (var i = 0; i < order.Count; ++i)
            {
                var player = order[i].Item1;
                player.PickFirstSettlementAndRoad(Map);
            }

            for (var i = order.Count - 1; i >= 0; --i)
            {
                var player = order[i].Item1;
                player.PickSecondSettlementAndRoad(Map);
            }
        }

        public void RollDicesAndGiveResources()
        {
            var dices = RollDices();
            var sum = dices.Item1 + dices.Item2;

            DropResourcesIfNecesarry(sum);
            GiveResources(sum);
        }

        private void GiveResources(int sum)
        {
            if (sum == 7)
                return;

            for (var i = 0; i < Map.Cells.Count; ++i)
            {
                var cell = Map.Cells[i];
                if (cell.Number != sum)
                    continue;

                var cellCoords = CoordinatesHelper.CellCoordinatesByIndexAndSize(i, Map.Size);
                var settlemenstCoord = CoordinatesHelper.NeighbourSettlementsCoordinatesForCell(cellCoords, Map.Size);

                foreach (var player in Players)
                {
                    var resources = _playerDict[player.Color].Item2;
                    foreach (var settlement in Map.Settlements)
                    {
                        if (!settlemenstCoord.Contains(settlement.Coordinates))
                            continue;

                        var count = settlement.Points;
                        switch (cell.Type)
                        {
                            case ResourceType.Wool: resources.Wool += count; break;
                            case ResourceType.Ore: resources.Ore += count; break;
                            case ResourceType.Grain: resources.Grain += count; break;
                            case ResourceType.Wood: resources.Wood += count; break;
                            case ResourceType.Clay: resources.Clay += count; break;
                            default: break;
                        }
                    }
                }
            }
        }

        private void DropResourcesIfNecesarry(int sum)
        {
            if (sum != 7)
                return;

            foreach (var item in _playerDict)
            {
                var resources = item.Value.Item2;
                var player = item.Value.Item1;
                var toDropCount = resources.Total / 2;
                var toDropResources = player.DropResources(toDropCount);

                if (toDropResources.Total != toDropCount)
                    throw new Exception("wrong nunmber of resources dropped");
                if (toDropResources.Grain > resources.Grain ||
                    toDropResources.Clay > resources.Clay ||
                    toDropResources.Wood > resources.Wood ||
                    toDropResources.Wool > resources.Wool ||
                    toDropResources.Ore > resources.Ore)
                    throw new Exception("can't drop more than you have");

                resources.Substract(toDropResources);
            }
        }

        private Tuple<int, int> RollDices()
        {
            var dices = _random.RollDices();
            DicesHistory.Push(dices);
            return dices;
        }
    }
}
