namespace Infrastructure
{
    public class Cell
    {
        public ResourceType Type { get; set; }
        public int Number { get; set; }

        public override string ToString()
        {
            return $"[{Type.ToString()} {Number}]";
        }

        ///* index
        // * 
        // *        1
        // *  0           2
        // *      this
        // *  5           3
        // *        4
        // */
        //public IEnumerable<Cell> Neighbours { get; set; }
    }
}
