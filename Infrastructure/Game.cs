using Infrastructure.Extensions;
using Infrastructure.PlayerDetails;
using Infrastructure.Settlements;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Infrastructure
{
    public sealed class Game
    {
        private Random _random;
        public IReadOnlyList<Cell> Cells { get; private set; }
        public Stack<Tuple<int, int>> DicesHistory { get; }
        private Dictionary<PlayerColor, Player> _playerDict;
        public IEnumerable<Player> Players => _playerDict.Values;
        private List<BaseSettlement> _settlements;
        public MapSize MapSize { get; private set; }

        public Game(Map map)
        {
            DicesHistory = new Stack<Tuple<int, int>>();
            _random = new Random();
            _playerDict = new Dictionary<PlayerColor, Player>();

            MapSize = map.Size;

            var cells = new List<Cell>();
            foreach (var cell in map.Cells)
                cells.Add(new Cell(cell.Type, cell.Number));
            Cells = cells.AsReadOnly();
        }
        
        public void AddPlayer(Player p)
        {
            _playerDict[p.Color] = p;
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

            for (var i = 0; i < Cells.Count; ++i)
            {
                var cell = Cells[i];
                if (cell.Number != sum)
                    continue;

                var cellCoords = CoordinatesHelper.CellCoordinatesByIndexAndSize(i, MapSize);
                var settlemenstCoord = CoordinatesHelper.NeighbourSettlementsCoordinatesForCell(cellCoords, MapSize);

                foreach (var settlement in _settlements)
                {
                    if (!settlemenstCoord.Contains(settlement.Coordinates))
                        continue;

                    var res = _playerDict[settlement.Color].Resources;
                    var count = settlement.Points;
                    switch (cell.Type)
                    {
                        case ResourceType.Wool: res.Wool += count; break;
                        case ResourceType.Ore: res.Ore += count; break;
                        case ResourceType.Grain: res.Grain += count; break;
                        case ResourceType.Wood: res.Wood += count; break;
                        case ResourceType.Clay: res.Clay += count; break;
                        default: break;
                    }
                }
            }
        }

        private void DropResourcesIfNecesarry(int sum)
        {
            if (sum != 7)
                return;

            foreach (var player in Players)
                player.DropResources();
        }

        private Tuple<int, int> RollDices()
        {
            var dices = _random.RollDices();
            DicesHistory.Push(dices);
            return dices;
        }
    }
}
