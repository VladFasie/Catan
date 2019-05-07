using System.Collections.Generic;

namespace Infrastructure
{
    public sealed class Map
    {
        public MapSize Size { get; set; }
        public IReadOnlyCollection<Cell> Cells { get; set; }

        public override string ToString()
        {
            var result = "";

            var i = 0;
            foreach (var cell in Cells)
            {
                result += "[";
                result += i;
                result += ", ";
                result += cell.Type;
                result += ", ";
                result += cell.Number;
                result += "]\n";
                i++;
            }

            return result;
        }
    }
}
