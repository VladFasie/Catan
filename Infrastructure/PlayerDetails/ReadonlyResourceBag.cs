namespace Infrastructure.PlayerDetails
{
    public class ReadonlyResourceBag
    {
        public int Total => Grain + Clay + Wood + Ore + Wool;
        public int Grain => _resource.Grain;
        public int Clay => _resource.Clay;
        public int Wood => _resource.Wood;
        public int Ore => _resource.Ore;
        public int Wool => _resource.Wool;

        private ResourceBag _resource;

        public ReadonlyResourceBag(ResourceBag resource)
        {
            _resource = resource;
        }

        public override string ToString()
        {
            return _resource.ToString();
        }
    }
}
