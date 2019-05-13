namespace Infrastructure
{
    public class Trade
    {
        public int FromAmmount { get; }
        public ResourceType From { get; }
        public int ToAmmount { get; }
        public ResourceType To { get; }

        public Trade(int fromAmmount, ResourceType from, int toAmmount, ResourceType to)
        {
            From = from;
            FromAmmount = fromAmmount;
            To = to;
            ToAmmount = toAmmount;
        }
    }
}
