using Infrastructure.PlayerDetails;
using System;

namespace Infrastructure.Settlements
{
    public class City : BaseSettlement
    {
        public override int Points => 2;

        public City(PlayerColor color, Tuple<int, int> coordinates) : base(color, coordinates)
        {
        }
    }
}
