using System.Drawing;
using System.Windows.Forms;

namespace cs_GameOfLife.Classes
{
    public class CellMatrix
    {

        #region Fields

        private Cell[,] _matrix;
        private int _xdim;
        private int _ydim;
        private int _size;

        #endregion

        #region Constructors

        private CellMatrix()
        {
            //Default value 40x40, cell size 20x20
            _size = 10;
            _xdim = 40;
            _ydim = 40;
            InitMatrix();
        }

        #endregion

        #region Methods

        private void InitMatrix()
        {
            _matrix = new Cell[_xdim, _ydim];
            for(var i=0;i<_xdim;i++)
                for(var j=0;j<_ydim;j++)
                    _matrix[i,j] = new Cell(new Rectangle(i*_size,j*_size,_size,_size), false);
        }

        private int LiveNeighborCount(int x, int y)
        {
            var count = 0;

            for(var i=-1;i<2;i++)
                for (var j = -1; j < 2; j++)
                {
                    var xDim = (x + _xdim + i) % _xdim;
                    var yDim = (y + _ydim + j) % _ydim;
                    if (_matrix[xDim,yDim].Status)
                        count++;
                }

            return count;
        }

        public void Paint(PaintEventArgs e)
        {
            var brush = new SolidBrush(Color.Black);
            for (var i = 0; i < _xdim; i++)
                for (var j = 0; j < _ydim; j++)
                    if (_matrix[i, j].Status) 
                        e.Graphics.FillRectangle(brush, _matrix[i,j].Rect);
        }

        public void ChangeStatus(Point pt)
        {
            for (var i = 0; i < _xdim; i++)
                for (var j = 0; j < _ydim; j++)
                    if (_matrix[i, j].Rect.Contains(pt))
                    {
                        _matrix[i, j].Status = true;
                        break;
                    }
        }

        public void Cycle()
        {
            for (var i = 0; i < _xdim; i++)
                for (var j = 0; j < _ydim; j++)
                {
                    var count = LiveNeighborCount(i, j);
                    if (!_matrix[i, j].Status && count == 3)
                        _matrix[i, j].Status = true;
                    else if (count < 2 || count > 3)
                            _matrix[i, j].Status = false;
                }
        }

        #endregion

        #region Singleton

        private static CellMatrix _instance;

        public static CellMatrix Instance => _instance ?? (_instance = new CellMatrix());

        #endregion
    }
}
