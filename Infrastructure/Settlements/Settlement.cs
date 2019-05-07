using Infrastructure.PlayerDetails;

namespace Infrastructure.Settlements
{
    public class Settlement
    {
        public Player Player { get; set; }
        public virtual int Points { get; }
    }
}
