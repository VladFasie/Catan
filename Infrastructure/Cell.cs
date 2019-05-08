namespace Infrastructure
{
    public class Cell
    {
        public ResourceType Type { get; }
        public int Number { get; }

        public Cell(ResourceType type, int number)
        {
            Type = type;
            Number = number;
        }

        public override string ToString()
        {
            return $"[{Type.ToString()} {Number}]";
        }
    }
}
