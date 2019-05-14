using System;
using System.Collections.Generic;
using Infrastructure.PlayerDetails;

namespace Infrastructure.Tradeing
{
    public class Trade : IEquatable<Trade>
    {
        #region properties
        public ReadOnlyResourceBag From { get; }
        public ReadOnlyResourceBag To { get; }
        #endregion

        public Trade(ResourceBag from, ResourceBag to) : this(from.AsReadOnly(), to.AsReadOnly())
        {
        }

        public Trade(ReadOnlyResourceBag fromR, ReadOnlyResourceBag to)
        {
            From = fromR;
            To = to;
        }

        #region equals
        public override bool Equals(object obj)
        {
            if (obj == null || !GetType().Equals(obj.GetType()))
                return false;

            var other = (Trade)obj;
            return Equals(other);
        }

        public bool Equals(Trade other)
        {
            if (other == null)
                return false;

            return From.Equals(To);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(From, To);
        }

        public static bool operator ==(Trade trade1, Trade trade2)
        {
            return EqualityComparer<Trade>.Default.Equals(trade1, trade2);
        }

        public static bool operator !=(Trade trade1, Trade trade2)
        {
            return !(trade1 == trade2);
        }
        #endregion
    }
}
