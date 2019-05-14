using System;

namespace Infrastructure.PlayerDetails
{
    public class ReadOnlyResourceBag : IEquatable<ReadOnlyResourceBag>
    {
        public int Total => Grain + Clay + Wood + Ore + Wool;
        public int Grain { get; }
        public int Clay { get; }
        public int Wood { get; }
        public int Ore { get; }
        public int Wool { get; }

        public int this[ResourceType type]
        {
            get
            {
                switch (type)
                {
                    case ResourceType.Clay: return Clay;
                    case ResourceType.Grain: return Grain;
                    case ResourceType.Ore: return Ore;
                    case ResourceType.Wood: return Wood;
                    case ResourceType.Wool: return Wool;
                    default: return 0;
                }
            }
        }
        
        public ReadOnlyResourceBag(ResourceBag resource)
        {
            Grain = resource.Grain;
            Clay = resource.Clay;
            Wood = resource.Wood;
            Ore = resource.Ore;
            Wool = resource.Wool;
        }

        public bool Equals(ReadOnlyResourceBag other)
        {
            if (other == null)
                return false;

            return Clay == other.Clay &&
                Grain == other.Grain &&
                Ore == other.Ore &&
                Wood == other.Wood &&
                Wool == other.Wool;
        }
    }
}
