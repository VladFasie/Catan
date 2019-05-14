using System;
using System.Collections.Generic;
using Infrastructure.PlayerDetails;

namespace Infrastructure.Tradeing
{
    public class SimpleTradeSystem : ITradeSystem
    {
        private BasePlayer _player;
        private Trade _lastTrade;
        private Bank _bank;
        private readonly List<PlayerColor> _offerts;
        public static Dictionary<PlayerColor, Tuple<BasePlayer, ResourceBag>> PlayerDict { get; set; }

        public SimpleTradeSystem(BasePlayer player)
        {
            _player = player;
            _bank = Bank.Instance;
        }

        public void AcceptLastTrade(PlayerColor color)
        {
            if (!_offerts.Contains(color))
                throw new Exception("invalid trade");
        }

        public IEnumerable<PlayerColor> AskFor(Trade trade)
        {
            throw new NotImplementedException();
        }

        public void InvalidateLastTrade()
        {
            _lastTrade = null;
            _offerts.Clear();
        }

        public void TradeWithBank(SimpleTrade trade)
        {
            _bank.Change(PlayerDict[_player.Color].Item2, trade);
        }

        public void AcceptLastTrade(Trade trade, PlayerColor color)
        {
            throw new NotImplementedException();
        }
    }
}
