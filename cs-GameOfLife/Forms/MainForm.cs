using System.Drawing.Drawing2D;
using System.Windows.Forms;
using cs_GameOfLife.Classes;

namespace cs_GameOfLife.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            if(!CellMatrix.Instance.Options.Load("settings.xml"))
                CellMatrix.Instance.Options.Default();
            CycleTimer.Interval = CellMatrix.Instance.Options.Speed;
        }

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
    }
}
