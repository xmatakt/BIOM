using System;
using System.Windows.Forms;
using BimSystRating.Classes;

namespace BimSystRating
{
    public partial class Form1 : Form
    {
        private Ratings ratings;
        private TrainingSet trainingSet;
        private TestSet testSet;
        //private 

        public Form1()
        {
            InitializeComponent();
        }

        private void createDataSetsButton_Click(object sender, EventArgs e)
        {
            fmrFnmrgraphsButton.Enabled = false;
            rocGraphButton.Enabled = false;

            ratings = new Ratings(radioButton1.Checked);

            //ratings.GenerateRatingCurves(0, 700, 100, 1);
            //ratings.GenerateRatingCurves(0, 700, 25, 5);

            ratings.GenerateRatingCurves((int) minThresholdNumUpDown.Value, (int) maxThresholdNumUpDown.Value,
                (int) threshStepNumUpDown.Value, (int) xValCountNumUpDown.Value);

            fmrFnmrgraphsButton.Enabled = true;
            rocGraphButton.Enabled = true;
        }

        private void createRecognizerButton_Click(object sender, EventArgs e)
        {
            var form = new GraphWindow(ratings.GenerateFMRGraph(), ratings.GenerateFNMRGraph());
            form.ShowDialog();
        }

        private void rocGraphButton_Click(object sender, EventArgs e)
        {
            var form = new GraphWindow(ratings.GenerateROCGraph());
            form.ShowDialog();
            //ratings.GenerateGraphs();
        }
    }
}
