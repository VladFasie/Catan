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

        public void Substract(ReadonlyResourceBag bag)
        {
            Grain -= bag.Grain;
            Clay -= bag.Clay;
            Wood -= bag.Wood;
            Ore -= bag.Ore;
            Wool -= bag.Wool;
        }
    }
}
