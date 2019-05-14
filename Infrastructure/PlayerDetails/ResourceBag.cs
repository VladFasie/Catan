namespace Infrastructure.PlayerDetails
{
    public class ResourceBag
    {
        public int Total => Grain + Clay + Wood + Ore + Wool;
        public int Grain { get; set; }
        public int Clay { get; set; }
        public int Wood { get; set; }
        public int Ore { get; set; }
        public int Wool { get; set; }

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
            set
            {
                switch (type)
                {
                    case ResourceType.Clay: Clay = value; break;
                    case ResourceType.Grain: Grain = value; break;
                    case ResourceType.Ore: Ore = value; break;
                    case ResourceType.Wood: Wood = value; break;
                    case ResourceType.Wool: Wool = value; break;
                    default: break;
                }
            }
        }

        public ReadOnlyResourceBag AsReadOnly()
        {
            return new ReadOnlyResourceBag(this);
        }

        public override string ToString()
        {
            var result = "[";

            result += "Clay: ";
            result += Clay;
            result += ", ";

            result += "Wood: ";
            result += Wood;
            result += ", ";

            result += "Grain: ";
            result += Grain;
            result += ", ";

            result += "Wool: ";
            result += Wool;
            result += ", ";

            result += "Ore: ";
            result += Ore;

            result += "]";
            return result;
        }

        public void Substract(ReadOnlyResourceBag bag)
        {
            Grain -= bag.Grain;
            Clay -= bag.Clay;
            Wood -= bag.Wood;
            Ore -= bag.Ore;
            Wool -= bag.Wool;
        }
    }
}
