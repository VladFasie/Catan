using System.Collections.Generic;
using Infrastructure.Settlements;

namespace Infrastructure.PlayerDetails
{
    public class Player
    {
        public ResourceBag Resources {get;}
        public PlayerColor Color { get; }
        public List<BaseSettlement> Settlements { get; internal set; }

        public Player(PlayerColor color)
        {
            Resources = new ResourceBag();
            Settlements = new List<BaseSettlement>();
            Color = color;
        }

        internal void DropResources()
        {
            // TODO
        }
    }
}
