using System;

namespace Infrastructure
{
    public class Thief
    {
        public Tuple<int, int> Coordinates { get; }

        public Thief(Tuple<int, int> coordinates)
        {
            Coordinates = coordinates;
        }
    }
}
