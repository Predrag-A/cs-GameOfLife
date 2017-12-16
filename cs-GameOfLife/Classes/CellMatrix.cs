namespace cs_GameOfLife.Classes
{
    public class CellMatrix
    {

        #region Fields

        private Cell[,] _matrix;
        private int _xdim;
        private int _ydim;

        #endregion

        #region Constructors

        public CellMatrix()
        {
            //Default value 40x40
            _xdim = 40;
            _ydim = 40;
            InitMatrix();
        }

        public CellMatrix(int xdim, int ydim)
        {
            _xdim = xdim;
            _ydim = ydim;
            InitMatrix();
        }

        #endregion

        #region Methods

        private void InitMatrix()
        {
            _matrix = new Cell[_xdim, _ydim];
            for(var i=0;i<_xdim;i++)
                for(var j=0;j<_ydim;j++)
                    _matrix[i,j] = new Cell();
        }

        #endregion
    }
}
