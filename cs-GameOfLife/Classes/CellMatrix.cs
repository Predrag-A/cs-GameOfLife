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
        private SolidBrush _cellColor;
        private Pen _backgroundColor;

        #endregion

        #region Constructors

        private CellMatrix()
        {
            //Default value 40x40, cell size 20x20
            _size = 10;
            _xdim = 60;
            _ydim = 60;
            _cellColor = new SolidBrush(Color.White);
            _backgroundColor = new Pen(Color.Black);
            InitMatrix();
        }

        #endregion

        #region Private Methods

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

        #endregion

        #region Public Methods

        public void Paint(PaintEventArgs e)
        {
            for (var i = 0; i < _xdim; i++)
                for (var j = 0; j < _ydim; j++)
                    if (_matrix[i, j].Status)
                    {
                        e.Graphics.FillRectangle(_cellColor, _matrix[i, j].Rect);
                        e.Graphics.DrawRectangle(_backgroundColor, _matrix[i, j].Rect);
                    }
        }

        public Rectangle ChangeStatus(Point pt)
        {
            var i = (int) (pt.X / _size);
            var j = (int) (pt.Y / _size);
            if (i >= _xdim || j >= _ydim)
                return Rectangle.Empty;
            _matrix[i, j].Status = true;
            return _matrix[i, j].Rect;
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
