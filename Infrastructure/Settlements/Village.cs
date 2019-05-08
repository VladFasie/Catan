using Infrastructure.PlayerDetails;
using System;

namespace Infrastructure.Settlements
{
    public class Village : BaseSettlement
    {
        public override int Points => 1;

        public Village(PlayerColor color, Tuple<int, int> coordinates) : base(color, coordinates)
        {
        }
    }
}
