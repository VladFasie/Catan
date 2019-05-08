using Infrastructure.PlayerDetails;
using System;

namespace Infrastructure.Settlements
{
    public abstract class BaseSettlement
    {
        public PlayerColor Color { get; protected set; }
        public Tuple<int, int> Coordinates { get; protected set; }
        public abstract int Points { get; }

        protected BaseSettlement(PlayerColor color, Tuple<int, int> coordinates)
        {
            Color = color;
            Coordinates = coordinates;
        }
    }
}
