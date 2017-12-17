using System.Drawing;
using System.Windows.Forms;

namespace cs_GameOfLife.Classes
{
    public class CellMatrix
    {

        #region Fields

        private Cell[,] _matrix;
        private Cell[,] _previous;

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
            _previous = new Cell[Options.XDim,Options.YDim];
            for(var i=0;i<Options.XDim;i++)
                for (var j = 0; j < Options.YDim; j++)
                {
                    _matrix[i, j] = new Cell(new Rectangle(i * Options.Size, j * Options.Size + 24, Options.Size, Options.Size),
                        false);
                    _previous[i,j] = new Cell(_matrix[i,j].Rect, false);
                }
        }

        private int LiveNeighborCount(int x, int y)
        {
            var count = 0;

            for (var i = -1; i < 2; i++)
            {
                var xDim = (x + Options.XDim + i) % Options.XDim;
                for (var j = -1; j < 2; j++)
                {
                    var yDim = (y + Options.YDim + j) % Options.YDim;
                    if (_previous[xDim, yDim].Status)
                        count++;
                }
            }

            return count - (_previous[x,y].Status ? 1 : 0);
        }

        #endregion

        #region Public Methods

        public void Paint(PaintEventArgs e)
        {
            for (var i = 0; i < Options.XDim; i++)
                for (var j = 0; j < Options.YDim; j++)
                {
                    if (_matrix[i, j].Status)
                        e.Graphics.FillRectangle(Options.CellColor, _matrix[i, j].Rect);
                    e.Graphics.DrawRectangle(Options.BorderColor, _matrix[i, j].Rect);
                }

        }

        public Rectangle ChangeStatus(Point pt)
        {
            var i = (int) (pt.X / Options.Size);
            var j = (int) ((pt.Y-24) / Options.Size);
            if (i >= Options.XDim || j >= Options.YDim)
                return Rectangle.Empty;
            _matrix[i, j].Status = true;
            return _matrix[i, j].Rect;
        }

        public Rectangle InvertStatus(Point pt)
        {
            var i = (int)(pt.X / Options.Size);
            var j = (int)((pt.Y - 24) / Options.Size);
            if (i >= Options.XDim || j >= Options.YDim)
                return Rectangle.Empty;
            _matrix[i, j].Status = !_matrix[i,j].Status;
            return _matrix[i, j].Rect;
        }

        public void Cycle()
        {
            var temp = _previous;
            _previous = _matrix;
            _matrix = temp;

            for (var i = 0; i < Options.XDim; i++)
                for (var j = 0; j < Options.YDim; j++)
                {
                    var count = LiveNeighborCount(i, j);
                    _matrix[i, j].Status = _previous[i, j].Status && (count == 2 || count == 3) ||
                                      !_previous[i, j].Status && count == 3;
                }
        }

        public void New()
        {
            InitMatrix();
        }

        public void LoadOptions(string fileName)
        {
            if(!Options.Load(fileName))
                Options.Default();
            InitMatrix();
        }

        #endregion

        #region Singleton

        private static CellMatrix _instance;

        public static CellMatrix Instance => _instance ?? (_instance = new CellMatrix());

        #endregion
    }
}
