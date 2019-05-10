using Infrastructure.Extensions;
using Infrastructure.PlayerDetails;
using Infrastructure.Settlements;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure
{
    public sealed class Game
    {
        public IEnumerable<BasePlayer> Players => _playerDict.Values.Select(x => x.Item1);
        public IReadOnlyList<PlayerColor> AscendingOrder { get; private set; }
        public int NumberOfPlayers => _playerDict.Keys.Count;
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

        private IEnumerable<Tuple<PlayerColor, int>> GenerateOrder(IEnumerable<PlayerColor> colors)
        {
            var numbers = new List<int>();
            for (var i = 0; i < colors.Count(); ++i)
            {
                var dices = RollDices();
                var sum = dices.Item1 + dices.Item2;
                numbers.Add(sum);
            }

            return colors.Zip(numbers, (color, number) => new Tuple<PlayerColor, int>(color, number)).OrderBy(x => x.Item2);
        }

        private IEnumerable<PlayerColor> FindDrawsAndRemoveThem(IEnumerable<Tuple<PlayerColor, int>> order)
        {
            var counter = new int[13];
            for (var i = 2; i <= 12; ++i)
                counter[i] = order.Count(x => x.Item2 == i);

            var result = new List<PlayerColor>();
            for (var i = 2; i <= 12; ++i)
            {
                if (counter[i] == 1)
                {
                    result.Add(order.First(x => x.Item2 == i).Item1);
                    continue;
                }

                if (counter[i] == 0)
                    continue;

                var draws = order.Where(x => x.Item2 == i).Select(x => x.Item1);
                var tempOrder = GenerateOrder(draws);

                foreach (var tmp in FindDrawsAndRemoveThem(tempOrder))
                    result.Add(tmp);
            }

            return result;
        }

        public void InitialSetup()
        {
            AscendingOrder = FindDrawsAndRemoveThem(GenerateOrder(_playerDict.Keys)).ToList().AsReadOnly();

            foreach (var color in AscendingOrder.Reverse())
            {
                var buildings = _playerDict[color].Item1.PickFirstSettlementAndRoad(Map.AsReadOnly());
                InitialBuild(buildings);
            }

            foreach (var color in AscendingOrder)
            {
                var buildings = _playerDict[color].Item1.PickSecondSettlementAndRoad(Map.AsReadOnly());
                InitialBuild(buildings);

                var neighbourCells = CoordinatesHelper.NeighbourCellsCoordinatesForSettlement(buildings.Item1.Coordinates, Map.Size);
                foreach (var cellCoord in neighbourCells)
                {
                    var cell = Map.Cells[CoordinatesHelper.CellIndexByCoordinates(cellCoord, Map.Size)];
                    AddResources(_playerDict[color].Item2, cell, buildings.Item1.Points);
                }
            }
        }

        private void InitialBuild(Tuple<Village, Road> buildings)
        {
            var settlement = buildings.Item1;
            var road = buildings.Item2;

            if (!road.A.Equals(settlement.Coordinates) && !road.B.Equals(settlement.Coordinates))
                throw new Exception("road must be connected with village");

            BuildRoad(road);
            BuildVillage(settlement);
        }

        private void BuildVillage(Village village)
        {
            if (!VillageIsValid(village))
                throw new Exception("road is not good");

            Map.Settlements.Add(village);
        }

        private bool VillageIsValid(Village village)
        {
            // is settlement coord & empty place & no neighbours occupied
            return CoordinatesHelper.SettlementCoordinatesAreValid(village.Coordinates, Map.Size)   &&
                (!Map.Settlements.Select(x => x.Coordinates).Contains(village.Coordinates))         &&
                CoordinatesHelper.NeighbourSettlementsCoordinatesForSettlement(village.Coordinates, Map.Size).
                    All(x => !Map.Settlements.Select(y => y.Coordinates).Contains(x));
        }

        private void BuildRoad(Road road)
        {
            if (!RoadIsValid(road))
                throw new Exception("road is not good");

            Map.Roads.Add(road);
        }

        private bool RoadIsValid(Road road)
        {
            return CoordinatesHelper.SettlementCoordinatesAreValid(road.A, Map.Size) &&
                CoordinatesHelper.SettlementCoordinatesAreValid(road.B, Map.Size) &&
                CoordinatesHelper.NeighbourSettlementsCoordinatesForSettlement(road.A, Map.Size).Contains(road.B);
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

                        AddResources(resources, cell, settlement.Points);
                    }
                }
            }
        }

        private void AddResources(ResourceBag resources, Cell cell, int ammount)
        {
            switch (cell.Type)
            {
                case ResourceType.Wool: resources.Wool += ammount; break;
                case ResourceType.Ore: resources.Ore += ammount; break;
                case ResourceType.Grain: resources.Grain += ammount; break;
                case ResourceType.Wood: resources.Wood += ammount; break;
                case ResourceType.Clay: resources.Clay += ammount; break;
                default: break;
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
