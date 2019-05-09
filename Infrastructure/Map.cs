using Infrastructure.PlayerDetails;
using Infrastructure.Settlements;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure
{
    public sealed class Map
    {
        public MapSize Size { get; set; }
        public IReadOnlyList<Cell> Cells { get; set; }
        public IReadOnlyList<BaseSettlement> Settlements { get; }
        public IReadOnlyList<Road> Roads { get; }

        public int PointsFor(PlayerColor color)
        {
            return Settlements.Where(settlement => settlement.Color == color).Select(settlement => settlement.Points).Sum();
        }

        public override string ToString()
        {
            var result = "";

            var i = 0;
            foreach (var cell in Cells)
            {
                result += "[";
                result += i;
                result += ", ";
                result += cell.Type;
                result += ", ";
                result += cell.Number;
                result += "]\n";
                i++;
            }

            return result;
        }
    }
}
