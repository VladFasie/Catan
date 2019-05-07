using Infrastructure.PlayerDetails;
using System;
using System.Collections.Generic;

namespace Infrastructure.Settlements
{
    public class Road : IEquatable<Road>
    {
        public PlayerColor Owner { get; set; }
        public Tuple<int, int> A { get; set; }
        public Tuple<int, int> B { get; set; }

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
