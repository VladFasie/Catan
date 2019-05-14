using Infrastructure.Commands;
using Infrastructure.Settlements;
using Infrastructure.Tradeing;
using System;
using System.Collections.Generic;

namespace Infrastructure.PlayerDetails
{
    public abstract class BasePlayer
    {
        public event EventHandler<BasePlayer> Action;
        public ReadOnlyResourceBag Resources { get; set; }
        public Map Map { get; set; }
        public PlayerColor Color { get; }
        public ITradeSystem TradeSystem { get; set; }

        public virtual void OnAction(BasePlayer player)
        {
            Action?.Invoke(this, player);
        }

        public BasePlayer(PlayerColor color, ITradeSystem tradeSystem)
        {
            Color = color;
            TradeSystem = tradeSystem;
        }

        public abstract ReadOnlyResourceBag DropResources(int toDrop);

        public abstract Tuple<Village, Road> PickFirstSettlementAndRoad();

        public abstract Tuple<Village, Road> PickSecondSettlementAndRoad();

        public abstract IEnumerable<Command> PlayTurn();

        public abstract bool RespondTo(Trade trade, PlayerColor fromPlayer);

        public abstract Tuple<int, int> MoveThief();
    }
}
