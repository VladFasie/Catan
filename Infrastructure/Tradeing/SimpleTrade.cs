namespace Infrastructure.Tradeing
{
    public class SimpleTrade
    {
        #region properties
        public ResourceType FromResource { get; }
        public ResourceType ToResource { get; }
        public int FromAmmount { get; }
        public int ToAmmount { get; }
        #endregion

        public SimpleTrade(ResourceType fromResource, int fromAmmount, ResourceType toResource, int toAmmount)
        {
            FromResource = fromResource;
            FromAmmount = fromAmmount;
            ToResource = toResource;
            ToAmmount = toAmmount;
        }
    }
}
