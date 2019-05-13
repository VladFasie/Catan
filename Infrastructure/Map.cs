using Infrastructure.PlayerDetails;
using Infrastructure.Settlements;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Infrastructure
{
    public sealed class Map
    {
        #region properties
        public MapSize Size { get; set; }
        public IList<Cell> Cells { get; set; }
        public IList<BaseSettlement> Settlements { get; set; }
        public IList<Road> Roads { get; set; }
        #endregion

        public int PointsFor(PlayerColor color)
        {
            return Settlements.Where(settlement => settlement.Color == color).Select(settlement => settlement.Points).Sum();
        }

        public ReadOnlyMap AsReadOnly()
        {
            var cells = new ReadOnlyCollection<Cell>(Cells);
            var settlements = new ReadOnlyCollection<BaseSettlement>(Settlements);
            var roads = new ReadOnlyCollection<Road>(Roads);

            return new ReadOnlyMap(Size, cells, settlements, roads);
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
