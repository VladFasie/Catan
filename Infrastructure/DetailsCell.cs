using Infrastructure.Settlements;
using System.Collections.Generic;

namespace Infrastructure
{
    public class DetailsCell : Cell
    {
        public DetailsCell()
        {
            _neighbourCells = new List<DetailsCell>(6);
            Roads = new List<Road>(6);
            Settlements = new List<Settlement>(6);

            for (var i = 0; i < 6; ++i)
            {
                _neighbourCells.Add(null);
                Roads.Add(null);
                Settlements.Add(null);
            }
        }

        private IList<DetailsCell> _neighbourCells;
        public IList<Road> Roads { get; }
        public IList<Settlement> Settlements { get; }

        public DetailsCell LeftUp
        {
            get => _neighbourCells[0];
            set
            {
                _neighbourCells[0] = value;
                if (value != null)
                    value._neighbourCells[3] = this;
            }
        }
        public DetailsCell RightUp
        {
            get => _neighbourCells[1];
            set
            {
                _neighbourCells[1] = value;
                if (value != null)
                    value._neighbourCells[4] = this;
            }
        }
        public DetailsCell Right
        {
            get => _neighbourCells[2];
            set
            {
                _neighbourCells[2] = value;
                if (value != null)
                    value._neighbourCells[5] = this;
            }
        }
        public DetailsCell RightDown
        {
            get => _neighbourCells[3];
            set
            {
                _neighbourCells[3] = value;
                if (value != null)
                    value._neighbourCells[0] = this;
            }
        }
        public DetailsCell LeftDown
        {
            get => _neighbourCells[4];
            set
            {
                _neighbourCells[4] = value;
                if (value != null)
                    value._neighbourCells[1] = this;
            }
        }
        public DetailsCell Left
        {
            get => _neighbourCells[5];
            set
            {
                _neighbourCells[5] = value;
                if (value != null)
                    value._neighbourCells[2] = this;
            }
        }
    }
}
