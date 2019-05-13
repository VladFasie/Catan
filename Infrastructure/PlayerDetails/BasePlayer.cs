using Infrastructure.Commands;
using Infrastructure.Settlements;
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
        protected ITradeSystem TradeSystem { get; }

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
    }
}
