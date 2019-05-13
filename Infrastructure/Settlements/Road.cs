using Infrastructure.PlayerDetails;
using System;
using System.Collections.Generic;

namespace Infrastructure.Settlements
{
    public class Road : IEquatable<Road>, IAsset
    {
        public Tuple<int, int> A { get; }
        public Tuple<int, int> B { get; }
        public PlayerColor Color { get; }

        public Road(PlayerColor color, Tuple<int, int> start, Tuple<int, int> stop)
        {
            Color = color;
            A = start;
            B = stop;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !GetType().Equals(obj.GetType()))
                return false;

            var other = (Road)obj;
            return Equals(other);
        }

        public bool Equals(Road other)
        {
            if (other == null)
                return false;

            return (EqualityComparer<Tuple<int, int>>.Default.Equals(A, other.A) &&
                   EqualityComparer<Tuple<int, int>>.Default.Equals(B, other.B)) ||
                   (EqualityComparer<Tuple<int, int>>.Default.Equals(A, other.B) &&
                   EqualityComparer<Tuple<int, int>>.Default.Equals(B, other.A));
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(A, B);
        }

        public static bool operator ==(Road road1, Road road2)
        {
            return EqualityComparer<Road>.Default.Equals(road1, road2);
        }

        public static bool operator !=(Road road1, Road road2)
        {
            return !(road1 == road2);
        }
    }
}
