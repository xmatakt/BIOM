using System.Drawing;
using System.Windows.Forms;
using ZedGraph;

namespace BimSystRating
{
    public partial class GraphWindow : Form
    {
        public GraphWindow()
        {
            InitializeComponent();
        }

        public GraphWindow(PointPairList fmr, PointPairList fnmr)
        {
            InitializeComponent();

            var pane = zedGraphControl.GraphPane;

            pane.Title.Text = "FMR/FNMR graph";
            pane.XAxis.Title.Text = "threshold";
            pane.YAxis.Title.Text = "FMR/FNMR";

            pane.AddCurve("FMR", fmr, Color.Red, SymbolType.Diamond);
            pane.AddCurve("FNMR", fnmr, Color.Blue, SymbolType.Circle);

            zedGraphControl.AxisChange();
        }

        public GraphWindow(PointPairList roc)
        {
            InitializeComponent();

            var pane = zedGraphControl.GraphPane;

            pane.Title.Text = "ROC";
            pane.XAxis.Title.Text = "false positive rate";
            pane.YAxis.Title.Text = "true positive rate";

            var xyPoints = new PointPairList();
            xyPoints.Add(0, 0);
            xyPoints.Add(1, 1);

            pane.AddCurve("ROC", roc, Color.Blue, SymbolType.Circle);
            var xyCurve = pane.AddCurve("x = y", xyPoints, Color.Black, SymbolType.Circle);
            //xyCurve.Line.Style = DashStyle.DashDot;
            //xyCurve.Line.DashOn = 100;
            //xyCurve.Line.DashOff = 100;

            zedGraphControl.AxisChange();
        }
    }
}
