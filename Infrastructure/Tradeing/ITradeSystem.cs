using Infrastructure.PlayerDetails;
using System.Collections.Generic;

namespace Infrastructure.Tradeing
{
    public interface ITradeSystem
    {
        void TradeWithBank(SimpleTrade trade);
        IEnumerable<PlayerColor> AskFor(Trade trade);
        void AcceptLastTrade(Trade trade, PlayerColor color);
        void InvalidateLastTrade();
    }
}