using System.Windows.Forms;
using cs_GameOfLife.Classes;

namespace cs_GameOfLife.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            CellMatrix.Instance.Paint(e);
        }

        private void CycleTimer_Tick(object sender, System.EventArgs e)
        {
            CellMatrix.Instance.Cycle();
            Refresh();
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            CellMatrix.Instance.ChangeStatus(e.Location);
            Refresh();
        }
    }
}
