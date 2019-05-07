namespace Infrastructure.PlayerDetails
{
    public class ResourceBag
    {
        public int Total
        {
            get
            {
                return Hay + Clay + Wood + Stone + Sheep;
            }
        }
        public int Hay { get; set; }
        public int Clay { get; set; }
        public int Wood { get; set; }
        public int Stone { get; set; }
        public int Sheep { get; set; }
    }
}
