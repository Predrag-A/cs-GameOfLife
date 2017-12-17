using System.Drawing;
using System.Windows.Forms;

namespace cs_GameOfLife.Classes
{
    public class CellMatrix
    {

        #region Fields

        private Cell[,] _matrix;

        #endregion

        #region Properties

        public Settings Options { get; }

        #endregion

        #region Constructors

        private CellMatrix()
        {
            Options = new Settings();
            InitMatrix();
        }

        #endregion

        #region Private Methods

        private void InitMatrix()
        {
            _matrix = new Cell[Options.XDim,Options.YDim];
            for(var i=0;i<Options.XDim;i++)
                for(var j=0;j<Options.YDim;j++)
                    _matrix[i,j] = new Cell(new Rectangle(i*Options.Size,j*Options.Size, Options.Size, Options.Size), false);
        }

        private int LiveNeighborCount(int x, int y)
        {
            var count = 0;

            for(var i=-1;i<2;i++)
                for (var j = -1; j < 2; j++)
                {
                    var xDim = (x + Options.XDim + i) % Options.XDim;
                    var yDim = (y + Options.YDim + j) % Options.YDim;
                    if (_matrix[xDim,yDim].Status)
                        count++;
                }

            return count;
        }

        #endregion

        #region Public Methods

        public void Paint(PaintEventArgs e)
        {
            for (var i = 0; i < Options.XDim; i++)
                for (var j = 0; j < Options.YDim; j++)
                    if (_matrix[i, j].Status)
                    {
                        e.Graphics.FillRectangle(Options.CellColor, _matrix[i, j].Rect);
                        e.Graphics.DrawRectangle(Options.BackgroundColor, _matrix[i, j].Rect);
                    }
        }

        public Rectangle ChangeStatus(Point pt)
        {
            var i = (int) (pt.X / Options.Size);
            var j = (int) (pt.Y / Options.Size);
            if (i >= Options.XDim || j >= Options.YDim)
                return Rectangle.Empty;
            _matrix[i, j].Status = true;
            return _matrix[i, j].Rect;
        }

        public void Cycle()
        {
            for (var i = 0; i < Options.XDim; i++)
                for (var j = 0; j < Options.YDim; j++)
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
