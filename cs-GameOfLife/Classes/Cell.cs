using System.Drawing;

namespace cs_GameOfLife.Classes
{
    public class Cell
    {

        #region Properties

        public Rectangle Rect { get; set; }
        public bool Status { get; set; }

        #endregion

        #region Constructors

        public Cell()
        {
            Rect = Rectangle.Empty;
            Status = false;
        }

        public Cell(Rectangle rect, bool status)
        {
            Rect = rect;
            Status = status;
        }

        #endregion

    }
}
