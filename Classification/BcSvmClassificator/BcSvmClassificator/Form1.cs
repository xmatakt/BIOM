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

            programToolStripMenuItem.Enabled = false;
        }

        private void loadDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //RemoveControls();

                programToolStripMenuItem.Enabled = false;
                dataStorage = new DataStorage(openFileDialog.FileName);
                programToolStripMenuItem.Enabled = true;
                dataLabel.Text = openFileDialog.SafeFileName;
                //AddControls();
            }
        }

        //private void AddControls()
        //{
        //    var tmp = dataStorage.Items.Select(x => x.Label).Distinct().ToArray();
        //    var counter = 0;

        //    foreach (var label in dataStorage.Items.Select(x => x.Label).Distinct())
        //    {
        //        var count = dataStorage.Items.Count(x => x.Label == label);

        //        var numUpDown = new NumericUpDown
        //        {
        //            Size = numericUpDownTemplate.Size,
        //            Location =
        //                new Point(numericUpDownTemplate.Location.X, numericUpDownTemplate.Location.Y + counter * yOffset),
        //            Name = label.ToString(),
        //            Minimum = 2,
        //            Maximum = count,
        //            Increment = 1,
        //            Visible = true,
        //            Value = count,
        //            TextAlign = HorizontalAlignment.Right
        //        };
        //        Controls.Add(numUpDown);
        //        numUpDowns.Add(numUpDown);

        //        var lab = new Label
        //        {
        //            AutoSize = true,
        //            Location = new Point(labelTemplate.Location.X, labelTemplate.Location.Y + counter * yOffset),
        //            Name = "label" + counter,
        //            Text = "Class " + label + ":"
        //        };
        //        Controls.Add(lab);
        //        labels.Add(lab);
        //        counter++;
        //    }
        //}

        //private void RemoveControls()
        //{
        //    for (var i = 0; i < numUpDowns.Count; i++)
        //    {
        //        Controls.Remove(numUpDowns[i]);
        //        Controls.Remove(labels[i]);
        //    }
        //}

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

        private void calculatePrecisionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            progressBar.Maximum = 100;
            svmClassificatorErrors.Clear();
            for (var percentage = 10; percentage <= 100; percentage+=5)
            {
                var countsDictionary = GetCounts(percentage * 0.01);
                GenerateDataSets(countsDictionary);
                var svmClassificator = new SVMClassificator();
                svmClassificatorErrors.Add(svmClassificator.CrossValidateClassificator(trainingSet, 10, 10));
                progressBar.Value = percentage;
            }

            GenerateGraph();
        }
    }
}
