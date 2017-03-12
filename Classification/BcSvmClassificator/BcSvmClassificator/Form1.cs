using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BcSvmClassificator.Classes;
using Emgu.CV;
using Emgu.CV.ML;
using Emgu.CV.ML.MlEnum;
using ZedGraph;
using Label = System.Windows.Forms.Label;

namespace BcSvmClassificator
{
    public partial class Form1 : Form
    {
        private DataStorage dataStorage;
        private DataStorage trainingSet;
        private List<Error> svmClassificatorErrors;
        private List<NumericUpDown> numUpDowns;
        private List<Label> labels;
        private int yOffset = 25;

        public Form1()
        {
            svmClassificatorErrors = new List<Error>();
            numUpDowns = new List<NumericUpDown>();
            labels = new List<Label>();
            InitializeComponent();

            calPrecision.Enabled = false;
            trackBar.Value = 10;
        }

        private void loadDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                calPrecision.Enabled = false;
                dataStorage = new DataStorage(openFileDialog.FileName);
                calPrecision.Enabled = true;
                dataLabel.Text = openFileDialog.SafeFileName;
            }
        }

        private Dictionary<int, int> GetCounts()
        {
            var result = new Dictionary<int, int>();

            foreach (var label in dataStorage.Items.Select(x => x.Label).Distinct())
            {
                var numUpDown = numUpDowns.Find(x => x.Name == label.ToString());
                result.Add(label, (int)numUpDown.Value);
            }

            return result;
        }

        /// <summary>
        /// Z kazdej triedy vyberie percentage% prvkov
        /// </summary>
        /// <param name="percentage">Percento prvkov, ktore maju byt z triedy vybrate</param>
        /// <returns></returns>
        private Dictionary<int, int> GetCounts(double percentage)
        {
            var result = new Dictionary<int, int>();

            foreach (var label in dataStorage.Items.Select(x => x.Label).Distinct())
            {
                var numerOfItems = (int) (dataStorage.Items.Count(x => x.Label == label) * percentage);
                result.Add(label, numerOfItems);
            }

            return result;
        }

        private void GenerateDataSets(Dictionary<int, int> counts)
        {

            trainingSet = new DataStorage();

            var row = 0;
            foreach (var pair in counts)
            {
                var counter = 1;
                foreach (var dataRow in dataStorage.Items.Where(x => x.Label == pair.Key))
                {
                    if (counter > pair.Value)
                        break;

                        trainingSet.Items.Add(dataRow);
                        counter++;
                }
            }
        }

        private void GenerateGraph()
        {
            var svm = new PointPairList();

            foreach (var error in svmClassificatorErrors)
            {
                if (error.TrainDataCount != 0)
                    svm.Add(new PointPair(error.TrainDataCount, error.ClassificationError));
            }

            var pane = zedGraphControl.GraphPane;
            pane.CurveList.Clear();

            pane.Title.Text = "Classificators precision graph";
            pane.XAxis.Title.Text = "train data count";
            pane.YAxis.Title.Text = "classification error";

            pane.AddCurve("SVM", svm, Color.Red, SymbolType.Diamond);
            //pane.AddCurve("FNMR", fnmr, Color.Blue, SymbolType.Circle);

            zedGraphControl.AxisChange();
            zedGraphControl.Refresh();
        }

        private void svmButton_Click(object sender, EventArgs e)
        {
            var countsDictionary = GetCounts();
            var trainData = dataStorage.GetTrainigDatamatrix(countsDictionary);
            var trainClasses = dataStorage.GetLabelsMatrix();

            var svmClassificator = new SVMClassificator(trainData, trainClasses);
            svmClassificator.TrainClassificator(10);

            foreach (var dataItem in dataStorage.Items)
            {
                svmClassificator.Predict(dataItem);
            }
        }

        private void rbfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rbfToolStripMenuItem.Checked = !rbfToolStripMenuItem.Checked;
            chi2ToolStripMenuItem.Checked = !rbfToolStripMenuItem.Checked;
        }

        private void chi2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chi2ToolStripMenuItem.Checked = !chi2ToolStripMenuItem.Checked;
            rbfToolStripMenuItem.Checked = !chi2ToolStripMenuItem.Checked;
        }

        private void trackBar_ValueChanged(object sender, EventArgs e)
        {
            trackLabel.Text = (0.001 * trackBar.Value).ToString("0.000");
        }

        private void calPrecision_Click(object sender, EventArgs e)
        {
            progressBar.Maximum = 100;
            svmClassificatorErrors.Clear();
            for (var percentage = 10; percentage <= 100; percentage += 5)
            {
                var countsDictionary = GetCounts(percentage * 0.01);
                GenerateDataSets(countsDictionary);
                var svmClassificator = new SVMClassificator();
                svmClassificatorErrors.Add(svmClassificator.ValidateClassificator(trainingSet, 10, 10,
                    trackBar.Value * 0.001, rbfToolStripMenuItem.Checked));
                progressBar.Value = percentage;
            }

            GenerateGraph();
        }

        private void CrossValidateClassificators()
        {
            for (var x = 0; x < xValCount; x++)
            {
                //  66% povodnych dat bude tvorit trenovacie data, zvysok testovacie
                //var indexes = GenerateRandomIndexes((int)(data.Items.Count * 0.66), data.Items.Count);
                var trainSet = new DataStorage();
                var testSet = new DataStorage();

                foreach (var label in data.Items.Select(l => l.Label).Distinct())
                {
                    var dataWithSameLabel = data.Items.Where(l => l.Label == label).ToList();
                    var indexes = GenerateRandomIndexes((int)(dataWithSameLabel.Count * 0.66), dataWithSameLabel.Count);
                    for (var i = 0; i < dataWithSameLabel.Count; i++)
                    {

                        if (indexes.Contains(i))
                            trainSet.Items.Add(dataWithSameLabel[i]);
                        else
                            testSet.Items.Add(dataWithSameLabel[i]);
                    }
                }
        }
    }
}
