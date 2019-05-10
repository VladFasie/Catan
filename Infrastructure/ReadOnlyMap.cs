using Infrastructure.Settlements;
using System.Collections.Generic;

namespace Infrastructure
{
    public sealed class ReadOnlyMap
    {
        public MapSize Size { get; }
        public IReadOnlyCollection<Cell> Cells { get; }
        public IReadOnlyCollection<BaseSettlement> Settlements { get; }
        public IReadOnlyCollection<Road> Roads { get; }

        public ReadOnlyMap(MapSize size, IReadOnlyCollection<Cell> cells,
                        IReadOnlyCollection<BaseSettlement> settlements, IReadOnlyCollection<Road> roads)
        {
            Size = size;
            Cells = cells;
            Settlements = settlements;
            Roads = roads;
        }
    }
}
