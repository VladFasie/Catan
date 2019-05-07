using System.Collections.Generic;
using Infrastructure.Settlements;

namespace Infrastructure.PlayerDetails
{
    public class Player
    {
        public ResourceBag Resources {get;}
        public PlayerColor Color { get; }
        public List<Settlement> Settlements { get; internal set; }

        public Player(PlayerColor color)
        {
            Resources = new ResourceBag();
            Settlements = new List<Settlement>();
            Color = color;
        }

        public override string ToString()
        {
            var result = Color.ToString();
            result += ": [";

            result += "Clay: ";
            result += Resources.Clay;
            result += ", ";

            result += "Wood: ";
            result += Resources.Wood;
            result += ", ";

            result += "Hay: ";
            result += Resources.Hay;
            result += ", ";

            result += "Sheep: ";
            result += Resources.Sheep;
            result += ", ";

            result += "Stone: ";
            result += Resources.Stone;

            result += "]";
            return result;
        }

        internal void DropResources()
        {
            // TODO
        }
    }
}
