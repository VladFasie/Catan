namespace Infrastructure.PlayerDetails
{
    public class ReadOnlyResourceBag
    {
        public int Total => Grain + Clay + Wood + Ore + Wool;
        public int Grain => _resource.Grain;
        public int Clay => _resource.Clay;
        public int Wood => _resource.Wood;
        public int Ore => _resource.Ore;
        public int Wool => _resource.Wool;

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

        private ResourceBag _resource;
        
        public ReadOnlyResourceBag(ResourceBag resource)
        {
            _resource = resource;
        }

        public override string ToString()
        {
            return _resource.ToString();
        }
    }
}
