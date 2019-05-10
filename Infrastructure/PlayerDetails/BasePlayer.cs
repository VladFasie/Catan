using Infrastructure.Settlements;
using System;

namespace Infrastructure.PlayerDetails
{
    public abstract class BasePlayer
    {
        public ReadOnlyResourceBag Resources { get; set; }
        public PlayerColor Color { get; }

        public BasePlayer(PlayerColor color)
        {
            Color = color;
        }

        public abstract ReadOnlyResourceBag DropResources(int toDrop);

        public abstract Tuple<Village, Road> PickFirstSettlementAndRoad(ReadOnlyMap map);

        public abstract Tuple<Village, Road> PickSecondSettlementAndRoad(ReadOnlyMap map);
    }
}
