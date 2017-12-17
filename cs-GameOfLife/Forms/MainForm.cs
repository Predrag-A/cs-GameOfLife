using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using cs_GameOfLife.Classes;

namespace cs_GameOfLife.Forms
{
    public partial class MainForm : Form
    {

        #region Constructors

        public MainForm()
        {
            InitializeComponent();
            CellMatrix.Instance.LoadOptions("settings.xml");
            CycleTimer.Interval = CellMatrix.Instance.Options.Speed;
            BackColor = CellMatrix.Instance.Options.BackgroundColor;
            Size = new Size(CellMatrix.Instance.Options.XDim*CellMatrix.Instance.Options.Size, CellMatrix.Instance.Options.YDim*CellMatrix.Instance.Options.Size);
        }

        #endregion

        #region Events

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            CellMatrix.Instance.Paint(e);
        }

        private void CycleTimer_Tick(object sender, System.EventArgs e)
        {
            CellMatrix.Instance.Cycle();
            Invalidate();
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            Invalidate(CellMatrix.Instance.ChangeStatus(e.Location));
        }

        private void MainForm_MouseClick(object sender, MouseEventArgs e)
        {
            Invalidate(CellMatrix.Instance.InvertStatus(e.Location));
        }

        private void pauseToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            CycleTimer.Stop();
        }

        private void continueToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            CycleTimer.Start();
        }

        private void newToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            CellMatrix.Instance.New();
            Invalidate();
        }

        #endregion
    }
}
