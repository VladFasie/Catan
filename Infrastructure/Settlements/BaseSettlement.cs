using Infrastructure.PlayerDetails;
using System;

namespace Infrastructure.Settlements
{
    public abstract class BaseSettlement : IAsset
    {
        public Tuple<int, int> Coordinates { get; }
        public abstract int Points { get; }
        public PlayerColor Color { get; }

        protected BaseSettlement(PlayerColor color, Tuple<int, int> coordinates)
        {
            Color = color;
            Coordinates = coordinates;
        }
    }
}
