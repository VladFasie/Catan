using Infrastructure.Extensions;
using Infrastructure.PlayerDetails;
using Infrastructure.Settlements;
using System;
using System.Collections.Generic;

namespace Infrastructure
{
    public sealed class Game
    {
        private Random _random;
        // TODO make private
        public readonly List<DetailsCell> _cells;
        public Stack<Tuple<int, int>> DicesHistory { get; }
        public ICollection<Player> Players { get; private set; }

        public Game(Map map)
        {
            DicesHistory = new Stack<Tuple<int, int>>();
            _random = new Random();
            _cells = new List<DetailsCell>();
            Players = new List<Player>();

            foreach (var cell in map.Cells)
            {
                var dc = new DetailsCell
                {
                    Number = cell.Number,
                    Type = cell.Type,
                };
                _cells.Add(dc);
            }

            List<int> l;
            if (map.Size == MapSize.Small)
                l = new List<int> { 3, 4, 5, 4, 3 };
            else //(map.Size == MapSize.Big)
                l = new List<int> { 3, 4, 5, 6, 5, 4, 3 };

            var s = 0;
            for (var j = 0; j < l.Count; ++j)
            {
                var k = l[j];
                for (var i = s; i < s + k; ++i)
                {
                    var leftUpIndex = i - k;
                    var rightUpIndex = i - k + 1;
                    if (j == 0)
                    {
                        leftUpIndex = rightUpIndex = -1;
                    }
                    else if (l[j - 1] < k)
                    {
                        if (i == s)
                            leftUpIndex = -1;
                        else if (i == s + k - 1)
                            rightUpIndex = -1;
                    }
                    if (leftUpIndex != -1)
                        _cells[i].LeftUp = _cells[leftUpIndex];
                    if (rightUpIndex != -1)
                        _cells[i].RightUp = _cells[rightUpIndex];

                    ///
                    var leftDownIndex = i + k;
                    var rightDownIndex = i + k + 1;
                    if (j == l.Count - 1)
                    {
                        leftDownIndex = rightDownIndex = -1;
                    }
                    else if (k > l[j + 1])
                    {
                        leftDownIndex--;
                        rightDownIndex--;
                        if (i == s)
                            leftDownIndex = -1;
                        else if (i == s + k - 1)
                            rightDownIndex = -1;
                    }
                    if (leftDownIndex != -1)
                        _cells[i].LeftDown = _cells[leftDownIndex];
                    if (rightDownIndex != -1)
                        _cells[i].RightDown = _cells[rightDownIndex];

                    // left
                    if (i - 1 >= s)
                        _cells[i].Left = _cells[i - 1];

                    // right
                    if (i + 1 < s + k)
                        _cells[i].Right = _cells[i + 1];
                }

                s += k;
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

        public void RollDicesAndGiveResources()
        {
            var dices = RollDices();
            var sum = dices.Item1 + dices.Item2;

            DropResourcesIfNecesarry(sum);
            GiveResources(sum);

            Console.WriteLine("Dices: " + sum);
        }

        private void GiveResources(int sum)
        {
            if (sum == 7)
                return;

            foreach (var cell in _cells)
            {
                if (cell.Number != sum)
                    continue;

                foreach (var settlement in cell.Settlements)
                {
                    if (settlement == null)
                        continue;

                    var res = settlement.Player.Resources;
                    var count = settlement.Points;
                    switch (cell.Type)
                    {
                        case ResourceType.Wool: res.Sheep += count; break;
                        case ResourceType.Ore: res.Stone += count; break;
                        case ResourceType.Grain: res.Hay += count; break;
                        case ResourceType.Wood: res.Wood += count; break;
                        case ResourceType.Clay: res.Clay += count; break;
                        default: break;
                    }
                }
            }
        }

        public Tuple<int, int> PickRandomSettlement(Player p)
        {
            var settlement = new Village
            {
                Player = p
            };
            p.Settlements.Add(settlement);

            DetailsCell cell;
            int idx;
            do
            {
                cell = _cells.RandomELement();
                idx = _random.Next(0, 6);
            } while (cell.Settlements[idx] != null);
            
            cell.Settlements[idx] = settlement;
            DetailsCell n1, n2;
            switch (idx)
            {
                case 0:
                    {
                        n1 = cell.Left;
                        if (n1 != null)
                            n1.Settlements[2] = settlement;
                        n2 = cell.LeftUp;
                        if (n2 != null)
                            n2.Settlements[4] = settlement;
                        break;
                    }
                case 1:
                    {
                        n1 = cell.LeftUp;
                        if (n1 != null)
                            n1.Settlements[3] = settlement;
                        n2 = cell.RightUp;
                        if (n2 != null)
                            n2.Settlements[5] = settlement;
                        break;
                    }
                case 2:
                    {
                        n1 = cell.RightUp;
                        if (n1 != null)
                            n1.Settlements[4] = settlement;
                        n2 = cell.Right;
                        if (n2 != null)
                            n2.Settlements[0] = settlement;
                        break;
                    }
                case 3:
                    {
                        n1 = cell.Right;
                        if (n1 != null)
                            n1.Settlements[5] = settlement;
                        n2 = cell.RightDown;
                        if (n2 != null)
                            n2.Settlements[1] = settlement;
                        break;
                    }
                case 4:
                    {
                        n1 = cell.RightDown;
                        if (n1 != null)
                            n1.Settlements[0] = settlement;
                        n2 = cell.LeftDown;
                        if (n2 != null)
                            n2.Settlements[2] = settlement;
                        break;
                    }
                case 5:
                    {
                        n1 = cell.LeftDown;
                        if (n1 != null)
                            n1.Settlements[1] = settlement;
                        n2 = cell.Left;
                        if (n2 != null)
                            n2.Settlements[3] = settlement;
                        break;
                    }
            }

            Console.WriteLine(p.Color + " " + _cells.IndexOf(cell) + " " + idx);
            return new Tuple<int, int>(_cells.IndexOf(cell), idx);
        }
    }
}
