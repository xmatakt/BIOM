using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using Emgu.CV;
using Emgu.CV.Structure;
using FaceClassification.Classificators;
using ZedGraph;

namespace FaceClassification
{
    public partial class Form1 : Form
    {
        private const int maxComponents = 1;
        private string selectedFolder = "";
        private bool canContinue;
        private bool canClassify;
        private bool debug = true;
        private Dictionary<int, List<Image<Gray, byte>>> originalImages;
        private Dictionary<int, List<Image<Gray, byte>>> pcaImages;
        private DataSet trainingSet;
        private DataSet testSet;
        private SVMClassificator svmClassificator = null;
        private EuclideanClassificator euclideanClassificator = null;
        private MahalanobisClassificator mahalanobisClassificator = null;

        private int xvalCount = 2;

        public Form1()
        {
            InitializeComponent();

            if (debug)
            {
                selectedFolder =
                    @"C:\Users\Timotej\Google Drive\Skola\API\semester_2\Biometria\cvicenia\zadanie_4\att_faces";
                selectedFolderLabel.Text =
                    @"C:\Users\Timotej\Google Drive\Skola\API\semester_2\Biometria\cvicenia\zadanie_4\att_faces";
                canContinue = true;
            }
        }

        private void chooseFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                selectedFolder = folderBrowserDialog.SelectedPath;
                selectedFolderLabel.Text = selectedFolder;
                canContinue = true;
            }
        }

        private void pcaComputeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!canContinue)
            {
                MessageBox.Show("You have to choose the folder first (File -> Choose folder)");
                return;
            }

            if (!IsFolderValid())
            {
                MessageBox.Show("Choosen folder is not in the right format!");
                return;
            }

            svmClassificator = null;
            euclideanClassificator = null;
            LoadImages();
            CreatePcaImages();
            GenerateDataSets();
            canClassify = true;
        }

        private bool IsFolderValid()
        {
            var subDirectories = new DirectoryInfo(selectedFolder).GetDirectories();
            if (subDirectories.Length == 0)
                return false;

            foreach (var subDirectory in subDirectories)
            {
                var subdirectoryContent = subDirectory.GetFiles().Where(x => x.Extension == ".png");
                if (!subdirectoryContent.Any())
                    return false;
            }

            return true;
        }

        private void LoadImages()
        {
            originalImages = new Dictionary<int, List<Image<Gray, byte>>>();
            pcaImages = new Dictionary<int, List<Image<Gray, byte>>>();
            var subDirectories = new DirectoryInfo(selectedFolder).GetDirectories();
            var index = 1;
            foreach (var subDirectory in subDirectories)
            {
                var files = subDirectory.GetFiles();
                foreach (var file in files.Where(x => x.Extension == ".png").Select(x => x.FullName))
                {
                    var img = Image.FromFile(file);
                    var image = new Image<Gray, byte>(new Bitmap(img));
                    if (originalImages.ContainsKey(index))
                        originalImages[index].Add(image);
                    else
                    {
                        originalImages.Add(index, new List<Image<Gray, byte>>());
                        originalImages[index].Add(image);
                    }
                }
                index++;
            }
        }

        private void CreatePcaImages()
        {
            foreach (var keyPairValue in originalImages)
            {
                var label = keyPairValue.Key;
                foreach (var image in keyPairValue.Value)
                {
                    var meanMatrix = new Mat();
                    var eigenVectors = new Mat();
                    var projection = new Mat();
                    CvInvoke.PCACompute(image, meanMatrix, eigenVectors, maxComponents);
                    CvInvoke.PCAProject(image, meanMatrix, eigenVectors, projection);
                    var projectedImage = projection.ToImage<Gray, byte>();

                    if (pcaImages.ContainsKey(label))
                        pcaImages[label].Add(projectedImage);
                    else
                    {
                        pcaImages.Add(label, new List<Image<Gray, byte>>());
                        pcaImages[label].Add(projectedImage);
                    }
                    //iba na kontrolu funkcnosti
                    //var backprojectedImage = new Mat();
                    //CvInvoke.PCABackProject(projectedImage, meanMatrix, eigenVectors, backprojectedImage);
                    //projectedImage.ToImage<Gray,byte>().ToBitmap().Save("tmp.png");
                    //backprojectedImage.ToImage<Gray, byte>().ToBitmap().Save("backproject.png");
                    //Emgu.CV.CvInvoke.Mahalanobis(, , );
                    //CvInvoke.CalcCovarMatrix(,,,CovarMethod.Cols,DepthType.Cv64F);
                } 
            }
        }

        private void GenerateDataSets()
        {
            trainingSet = new DataSet();
            testSet = new DataSet();
            foreach (var keyPairValue in pcaImages)
            {
                var label = keyPairValue.Key;
                var images = keyPairValue.Value;
                // trenovacie data budu obsahovat 60% prvkov
                var randomIndexes = GenerateRandomIndexes((int)(images.Count * 0.6), images.Count);
                for (var i = 0; i < images.Count; i++)
                {
                    if (randomIndexes.Contains(i))
                        trainingSet.Add(images[i], label);
                    else
                        testSet.Add(images[i], label);
                }
            }
        }

        private int[] GenerateRandomIndexes(int retArrLength, int maxIndexCount)
        {
            var result = new List<int>();
            var rand = new Random();

            while (result.Count != retArrLength)
            {
                var randIndex = rand.Next(0, maxIndexCount);
                if (!result.Contains(randIndex))
                    result.Add(randIndex);
            }

            return result.ToArray();
        }

        private void GetDataForClassificators(ref Matrix<float> matrix,  ref Matrix<int> labels, DataSet data)
        {
            matrix = new Matrix<float>(data.Data.Count, data.Data[0].Item2.Width);
            labels = new Matrix<int>(data.Data.Count, 1);
            var index = 0;
            foreach (var dataItem in data.Data)
            {
                var label = dataItem.Item1;
                var dataMatrix = dataItem.Item2;

                labels[index, 0] = label;
                for (var col = 0; col < matrix.Width; col++)
                    matrix[index, col] = dataMatrix[0, col];

                index++;
            }
        }

        private void sVMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!canClassify)
            {
                MessageBox.Show("You have to create datasets first (Datasets -> Create)");
                return;
            }

            Matrix<float> trainDataMatrix = null;
            Matrix<int> trainingLabels = null;
            GetDataForClassificators(ref trainDataMatrix, ref trainingLabels, trainingSet);

            Matrix<float> testDataMatrix = null;
            Matrix<int> testLabels = null;
            GetDataForClassificators(ref testDataMatrix, ref testLabels, testSet);

            svmClassificator = new SVMClassificator(trainDataMatrix, trainingLabels);
            svmClassificator.TrainClassificator();
            svmClassificator.TestClassificator(testDataMatrix, testLabels);

            GenerateTotalGraph();
        }

        private void euclideanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!canClassify)
            {
                MessageBox.Show("You have to create datasets first (Datasets -> Create)");
                return;
            }

            Matrix<float> trainDataMatrix = null;
            Matrix<int> trainingLabels = null;
            GetDataForClassificators(ref trainDataMatrix, ref trainingLabels, trainingSet);

            Matrix<float> testDataMatrix = null;
            Matrix<int> testLabels = null;
            GetDataForClassificators(ref testDataMatrix, ref testLabels, testSet);

            euclideanClassificator = new EuclideanClassificator(trainDataMatrix, trainingLabels);
            euclideanClassificator.TestClassificator(testDataMatrix, testLabels);

            GenerateTotalGraph();
        }

        private void mahalanobisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!canClassify)
            {
                MessageBox.Show("You have to create datasets first (Datasets -> Create)");
                return;
            }

            Matrix<float> trainDataMatrix = null;
            Matrix<int> trainingLabels = null;
            GetDataForClassificators(ref trainDataMatrix, ref trainingLabels, trainingSet);

            Matrix<float> testDataMatrix = null;
            Matrix<int> testLabels = null;
            GetDataForClassificators(ref testDataMatrix, ref testLabels, testSet);

            mahalanobisClassificator = new MahalanobisClassificator(trainDataMatrix, trainingLabels);
            mahalanobisClassificator.TestClassificator(testDataMatrix, testLabels);

            GenerateTotalGraph();
        }

        private void sVMCrossvalidation_Click(object sender, EventArgs e)
        {
            if (!canClassify)
            {
                MessageBox.Show("You have to create datasets first (Datasets -> Create)");
                return;
            }

            euclideanClassificator = null;
            mahalanobisClassificator = null;
            svmClassificator = new SVMClassificator();
            for (var i = 0; i < xvalCount; i++)
            {
                GenerateDataSets();

                Matrix<float> trainDataMatrix = null;
                Matrix<int> trainingLabels = null;
                GetDataForClassificators(ref trainDataMatrix, ref trainingLabels, trainingSet);

                Matrix<float> testDataMatrix = null;
                Matrix<int> testLabels = null;
                GetDataForClassificators(ref testDataMatrix, ref testLabels, testSet);

                svmClassificator.SetDatasets(trainDataMatrix, trainingLabels);
                svmClassificator.TrainClassificator();
                svmClassificator.TestClassificator(testDataMatrix, testLabels, i);
            }

            GenerateGraphs();
        }


        private void euclideanCrossValidation_Click(object sender, EventArgs e)
        {
            if (!canClassify)
            {
                MessageBox.Show("You have to create datasets first (Datasets -> Create)");
                return;
            }

            svmClassificator = null;
            mahalanobisClassificator = null;
            euclideanClassificator = new EuclideanClassificator();
            for (var i = 0; i < xvalCount; i++)
            {
                GenerateDataSets();

                Matrix<float> trainDataMatrix = null;
                Matrix<int> trainingLabels = null;
                GetDataForClassificators(ref trainDataMatrix, ref trainingLabels, trainingSet);

                Matrix<float> testDataMatrix = null;
                Matrix<int> testLabels = null;
                GetDataForClassificators(ref testDataMatrix, ref testLabels, testSet);

                euclideanClassificator.SetDatasets(trainDataMatrix, trainingLabels);
                euclideanClassificator.TestClassificator(testDataMatrix, testLabels, i);
            }

            GenerateGraphs();
        }


        private void mahalanobisCrossvalidation_Click(object sender, EventArgs e)
        {
            if (!canClassify)
            {
                MessageBox.Show("You have to create datasets first (Datasets -> Create)");
                return;
            }

            svmClassificator = null;
            euclideanClassificator = null;
            mahalanobisClassificator = new MahalanobisClassificator();
            for (var i = 0; i < xvalCount; i++)
            {
                GenerateDataSets();

                Matrix<float> trainDataMatrix = null;
                Matrix<int> trainingLabels = null;
                GetDataForClassificators(ref trainDataMatrix, ref trainingLabels, trainingSet);

                Matrix<float> testDataMatrix = null;
                Matrix<int> testLabels = null;
                GetDataForClassificators(ref testDataMatrix, ref testLabels, testSet);

                mahalanobisClassificator.SetDatasets(trainDataMatrix, trainingLabels);
                mahalanobisClassificator.TestClassificator(testDataMatrix, testLabels, i);
            }

            GenerateGraphs();
        }

        private void allCrossvalidation_Click(object sender, EventArgs e)
        {
            if (!canClassify)
            {
                MessageBox.Show("You have to create datasets first (Datasets -> Create)");
                return;
            }

            svmClassificator = new SVMClassificator();
            euclideanClassificator = new EuclideanClassificator();
            mahalanobisClassificator = new MahalanobisClassificator();
            for (var i = 0; i < xvalCount; i++)
            {
                GenerateDataSets();

                Matrix<float> trainDataMatrix = null;
                Matrix<int> trainingLabels = null;
                GetDataForClassificators(ref trainDataMatrix, ref trainingLabels, trainingSet);

                Matrix<float> testDataMatrix = null;
                Matrix<int> testLabels = null;
                GetDataForClassificators(ref testDataMatrix, ref testLabels, testSet);

                euclideanClassificator.SetDatasets(trainDataMatrix, trainingLabels);
                euclideanClassificator.TestClassificator(testDataMatrix, testLabels, i);

                svmClassificator.SetDatasets(trainDataMatrix, trainingLabels);
                svmClassificator.TrainClassificator();
                svmClassificator.TestClassificator(testDataMatrix, testLabels, i);

                mahalanobisClassificator.SetDatasets(trainDataMatrix, trainingLabels);
                mahalanobisClassificator.TestClassificator(testDataMatrix, testLabels, i);
            }

            GenerateGraphs();
        }


        private double GetClassificationError(List<int> good, List<int> bad, int index)
        {
            return good[index] / (double) (good[index] + bad[index]);
        }

        private double GetTotalClassificationError(List<int> good, List<int> bad)
        {
            var goodSum = good.Sum();
            var badSum = bad.Sum();
            return goodSum / (double)(goodSum + badSum);
        }

        private void GenerateGraphs()
        {
            GeneratePartialGraph();
            GenerateTotalGraph();
        }

        private void GenerateTotalGraph()
        {
            // get a reference to the GraphPane
            GraphPane myPane = zedGraphControl_total.GraphPane;
            myPane.CurveList.Clear();

            // Set the Titles
            myPane.Title.Text = "Crossvalidation result";
            myPane.XAxis.Title.Text = "classificators";
            myPane.YAxis.Title.Text = "classificator precision";

            // Make up some random data points
            var labels = new[] {""};
            var graphData = new List<double>();
            BarItem myBar;

            if (svmClassificator != null)
            {
                graphData.Add(GetTotalClassificationError(svmClassificator.GoodCount, svmClassificator.BadCount));

                myBar = myPane.AddBar("SVM", null, graphData.ToArray(), Color.Red);
                myBar.Bar.Fill = new Fill(Color.Red, Color.White, Color.Red);
            }

            if (euclideanClassificator != null)
            {
                graphData.Clear();
                graphData.Add(GetTotalClassificationError(euclideanClassificator.GoodCount, euclideanClassificator.BadCount));

                myBar = myPane.AddBar("Euclidean", null, graphData.ToArray(), Color.Blue);
                myBar.Bar.Fill = new Fill(Color.Blue, Color.White, Color.Blue);
            }

            if (mahalanobisClassificator != null)
            {
                graphData.Clear();
                graphData.Add(GetTotalClassificationError(mahalanobisClassificator.GoodCount, mahalanobisClassificator.BadCount));

                myBar = myPane.AddBar("Mahalanobis", null, graphData.ToArray(), Color.Green);
                myBar.Bar.Fill = new Fill(Color.Green, Color.White, Color.Green);
            }

            // Draw the X tics between the labels instead of 
            // at the labels
            myPane.XAxis.MajorTic.IsBetweenLabels = true;
            // Set the XAxis labels
            myPane.XAxis.Scale.TextLabels = labels;
            // Set the XAxis to Text type
            myPane.XAxis.Type = AxisType.Text;

            // Fill the Axis and Pane backgrounds
            myPane.Chart.Fill = new Fill(Color.White,
                  Color.FromArgb(255, 166, 166), 90F);
            myPane.Fill = new Fill(Color.FromArgb(250, 250, 255));

            // Tell ZedGraph to refigure the
            // axes since the data have changed
            zedGraphControl_total.AxisChange();
            zedGraphControl_total.Refresh();
        }

        private void GeneratePartialGraph()
        {
            // get a reference to the GraphPane
            GraphPane myPane = zedGraphControl_partial.GraphPane;
            myPane.CurveList.Clear();

            // Set the Titles
            myPane.Title.Text = "Results across crossvalidation";
            myPane.XAxis.Title.Text = "iteration";
            myPane.YAxis.Title.Text = "classification precision";

            // Make up some random data points
            var labels = new string[xvalCount];
            for (int i = 0; i < xvalCount; i++)
                labels[i] = (i + 1) + " iteration";

            var graphData = new double[xvalCount];
            BarItem myBar;
            if (svmClassificator != null)
            {
                for (var i = 0; i < xvalCount; i++)
                    graphData[i] = GetClassificationError(svmClassificator.GoodCount, svmClassificator.BadCount, i);

                // Generate a red bar with "Curve 1" in the legend
                myBar = myPane.AddBar("SVM", null, graphData, Color.Red);
                myBar.Bar.Fill = new Fill(Color.Red, Color.White, Color.Red);
            }

            if (euclideanClassificator != null)
            {
                graphData = new double[xvalCount];
                for (var i = 0; i < xvalCount; i++)
                    graphData[i] = GetClassificationError(euclideanClassificator.GoodCount, euclideanClassificator.BadCount, i);

                // Generate a red bar with "Curve 1" in the legend
                myBar = myPane.AddBar("Euclidean", null, graphData, Color.Blue);
                myBar.Bar.Fill = new Fill(Color.Blue, Color.White, Color.Blue);
            }

            if (mahalanobisClassificator != null)
            {
                graphData = new double[xvalCount];
                for (var i = 0; i < xvalCount; i++)
                    graphData[i] = GetClassificationError(mahalanobisClassificator.GoodCount, mahalanobisClassificator.BadCount, i);

                // Generate a red bar with "Curve 1" in the legend
                myBar = myPane.AddBar("Mahalanobis", null, graphData, Color.Green);
                myBar.Bar.Fill = new Fill(Color.Green, Color.White, Color.Green);
            }

            // Draw the X tics between the labels instead of 
            // at the labels
            myPane.XAxis.MajorTic.IsBetweenLabels = true;
            // Set the XAxis labels
            myPane.XAxis.Scale.TextLabels = labels;
            // Set the XAxis to Text type
            myPane.XAxis.Type = AxisType.Text;

            // Fill the Axis and Pane backgrounds
            myPane.Chart.Fill = new Fill(Color.White,
                  Color.FromArgb(255, 255, 166), 90F);
            myPane.Fill = new Fill(Color.FromArgb(250, 250, 255));

            // Tell ZedGraph to refigure the
            // axes since the data have changed
            zedGraphControl_partial.AxisChange();
            zedGraphControl_partial.Refresh();
        }

        private void saToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tmp  = new MahalanobisClassificator();
            tmp.tmp();
        }
    }
}
