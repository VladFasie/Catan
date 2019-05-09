using Infrastructure.Settlements;
using System;

namespace Infrastructure.PlayerDetails
{
    public abstract class BasePlayer
    {
        public ReadonlyResourceBag Resources { get; set; }
        public PlayerColor Color { get; }

        public BasePlayer(PlayerColor color)
        {
            Color = color;
        }

        public abstract ReadonlyResourceBag DropResources(int toDrop);

        public abstract Tuple<Village, Road> PickFirstSettlementAndRoad(Map map);

        public abstract Tuple<Village, Road> PickSecondSettlementAndRoad(Map map);
    }
}
