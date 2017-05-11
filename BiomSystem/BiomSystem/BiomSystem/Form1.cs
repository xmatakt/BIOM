using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using BiomSystem.Classes;
using Emgu.CV;
using Emgu.CV.Structure;
using ZedGraph;

namespace BiomSystem
{
    public partial class Form1 : Form
    {
        private string folder1 = null;
        private string folder2 = null;
        private int maxComponents = 5;
        private List<double> compResults;

        private readonly List<double> FMR;
        private readonly List<double> FNMR;
        private readonly List<double> TPR;
        private readonly List<double> FPR;
        private readonly List<int> thresholds;

        public Form1()
        {
            compResults = new List<double>();
            FMR = new List<double>();
            FNMR = new List<double>();
            TPR = new List<double>();
            FPR = new List<double>();
            thresholds = new List<int>();

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                folder1 = folderBrowserDialog.SelectedPath;
                var files = Directory.GetFiles(folder1);

                var templateImage = new Image<Gray, byte>(files.First(x => x.Contains("template")));
                var maskImage = new Image<Gray, byte>(files.First(x => x.Contains("mask")));
                xor1ImageBox.Image = IrisComparator.GetXorImage(templateImage, maskImage);
                face1ImageBox.Image = new Image<Gray, byte>(files.First(x => x.Contains("1.png")));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                folder2 = folderBrowserDialog.SelectedPath;
                var files = Directory.GetFiles(folder2);

                var templateImage = new Image<Gray, byte>(files.First(x => x.Contains("template")));
                var maskImage = new Image<Gray, byte>(files.First(x => x.Contains("mask")));
                xor2ImageBox.Image = IrisComparator.GetXorImage(templateImage, maskImage);
                face2ImageBox.Image = new Image<Gray, byte>(files.First(x => x.Contains("1.png")));
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            compResults.Clear();
            var comparingResult = IrisComparator.GetHammingDistance((Image<Gray, byte>)xor1ImageBox.Image, (Image<Gray, byte>)xor2ImageBox.Image);
            hammDistanceLabel.Text = "Hamming distance: " + Math.Round(comparingResult.HammingDistance, 2);
            var euclidDistance = CompareFaces();
            euclidDistanceLabel.Text = "Euclidean distance: " + Math.Round(euclidDistance, 2);
            compResults.Add(euclidDistance);
            compResults.Add(comparingResult.HammingDistance);
            //var result = GetResult();
            var result = euclidDistance + comparingResult.HammingDistance;

            similarityLabel.Text = "Similarity: " + Math.Round(result, 2);
        }

        private double CompareFaces()
        {
            var face1 = GetData(folder1);
            var face2 = GetData(folder2);
            var distance = EuclideanComparator.GetDistance(face1, face2);
            return distance;

        }

        private List<Tuple<string, Image<Gray, byte>>> GetData(string path)
        {
            var label = path[path.Length - 1].ToString();
            var data = new List<Tuple<string, Image<Gray, byte>>>();
            foreach (var file in Directory.GetFiles(path).Where(x => x.Contains(".png")))
            {
                var image = new Image<Gray, byte>(file);
                var meanMatrix = new Mat();
                var eigenVectors = new Mat();
                var projection = new Mat();
                CvInvoke.PCACompute(image, meanMatrix, eigenVectors, maxComponents);
                CvInvoke.PCAProject(image, meanMatrix, eigenVectors, projection);
                var projectedImage = projection.ToImage<Gray, byte>();
                data.Add(new Tuple<string, Image<Gray, byte>>(label, projectedImage));
            }

            return data;
        }

        private double GetResult()
        {
            var sum = 0d;
            var mean = compResults.Sum() / compResults.Count;
            foreach (var num in compResults)
            {
                sum += Math.Pow(num - mean, 2);
            }
            sum /= compResults.Count;
            var sigma = Math.Sqrt(sum);

            sum = 0d;

            if (sigma == 0.0d)
                return 0d;

            foreach (var num in compResults)
            {
                sum += (num - mean) / sigma;
            }

            return sum;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //var sb = new StringBuilder();
            var data = LoadData();

            for (var threshold = 500; threshold < 2800; threshold += 150)
            {

                int FP, TN, FN;
                var TP = FP = TN = FN = 0;
                int counter = 0;

                foreach (var item1 in data)
                {
                    var indexes = GenerateRandomIndexes(4, item1.Item3.Count - 1);
                    var trainPictures = new List<Image<Gray, byte>>();
                    foreach (var index in indexes)
                        trainPictures.Add(item1.Item3[index]);

                    var trainData = new Tuple<string, Image<Gray, byte>, List<Image<Gray, byte>>>(item1.Item1,
                        item1.Item2, trainPictures);
                    foreach (var item2 in data)
                    {
                        int[] indexes2;
                        var testPictures = new List<Image<Gray, byte>>();
                        if (item1.Item1 == item2.Item1)
                            indexes2 = new[] {0, 1, 2, 3, 4, 5, 6}.Where(x => !indexes.Contains(x)).ToArray();
                        else
                            indexes2 = GenerateRandomIndexes(3, item2.Item3.Count - 1);

                        foreach (var index in indexes2)
                            testPictures.Add(item2.Item3[index]);
                        var testData = new Tuple<string, Image<Gray, byte>, List<Image<Gray, byte>>>(item2.Item1,
                            item2.Item2, testPictures);

                        var hammingDistance =
                            IrisComparator.GetHammingDistance(trainData.Item2, testData.Item2).HammingDistance;
                        var euclideanDistanceResult =
                            EuclideanComparator.GetDistance(
                                new Tuple<string, List<Image<Gray, byte>>>(testData.Item1, testData.Item3),
                                new Tuple<string, List<Image<Gray, byte>>>(trainData.Item1, trainData.Item3));

                        var label = item2.Item1;
                        var euclideanDistance = euclideanDistanceResult.Item2;
                        var predictedLabel = euclideanDistanceResult.Item1;
                        var summedDistance = hammingDistance + euclideanDistance;
                        var isAccepted = (summedDistance <= threshold);

                        //sb.AppendLine(summedDistance.ToString(CultureInfo.InvariantCulture));

                        //  pocet poz. vzoriek, kt. boli klasif. ako pozitivne
                        if ((label == predictedLabel) && isAccepted)
                            TP++;

                        //  pocet negat. vzoriek, kt. boli klasif. ako pozitivne
                        if ((label != predictedLabel) && isAccepted)
                            FP++;

                        //  pocet negat. vzoriek, kt. boli klasif. ako negativne
                        if ((label != predictedLabel) && !isAccepted)
                            TN++;

                        //  pocet poz. vzoriek, kt. boli klasif. ako negativne
                        if ((label == predictedLabel) && !isAccepted)
                            FN++;

                        counter++;
                    }
                }
                //System.Diagnostics.Debug.WriteLine(sb.ToString());

                FMR.Add(FP / (double) counter);
                FNMR.Add(FN / (double) counter);
                TPR.Add(TP / (double) (TP + FN));
                FPR.Add(FP / (double) (FP + TN));
                thresholds.Add(threshold);
            }

            GenerateRocCurve();
        }

        private void GenerateRocCurve()
        {
            var form = new GraphWindow(GenerateROCGraph());
            form.ShowDialog();
        }

        public PointPairList GenerateROCGraph()
        {
            var oldX = 0d;
            var oldY = 0d;
            var result = new PointPairList();

            for (var i = 0; i < thresholds.Count; i++)
            {
                if (i > 0)
                {
                    if (oldX < FPR[i] && oldY <= TPR[i] && TPR[i] >= FPR[i])
                    {
                        result.Add(FPR[i], TPR[i]);
                        oldX = FPR[i];
                        oldY = TPR[i];
                    }
                }
                else
                {
                    result.Add(FPR[i], TPR[i]);
                    oldX = FPR[i];
                    oldY = TPR[i];
                }
            }

            return result;
        }

        private List<Tuple<string, Image<Gray, byte>, List<Image<Gray, byte>>>> LoadData()
        {
            var result = new List<Tuple<string, Image<Gray, byte>, List<Image<Gray, byte>>>>();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (var subFolder in Directory.GetDirectories(folderBrowserDialog.SelectedPath))
                {
                    var label = subFolder[subFolder.Length - 1].ToString();
                    var files = Directory.GetFiles(subFolder);
                    var templateImage = new Image<Gray, byte>(files.First(x => x.Contains("template")));
                    var maskImage = new Image<Gray, byte>(files.First(x => x.Contains("mask")));
                    var xorImage = IrisComparator.GetXorImage(templateImage, maskImage);
                    var images = new List<Image<Gray, byte>>();

                    foreach (var file in files.Where(x => x.Contains(".png")))
                    {
                        var image = new Image<Gray, byte>(file);
                        var meanMatrix = new Mat();
                        var eigenVectors = new Mat();
                        var projection = new Mat();
                        CvInvoke.PCACompute(image, meanMatrix, eigenVectors, maxComponents);
                        CvInvoke.PCAProject(image, meanMatrix, eigenVectors, projection);
                        var projectedImage = projection.ToImage<Gray, byte>();
                        images.Add(projectedImage);
                    }

                    result.Add(new Tuple<string, Image<Gray, byte>, List<Image<Gray, byte>>>(label, xorImage, images));
                }
            }

            return result;
        }

        private int[] GenerateRandomIndexes(int count, int maxIndexCount)
        {
            var result = new List<int>();
            var rand = new Random();

            while (result.Count != count)
            {
                var randIndex = rand.Next(0, maxIndexCount);
                if (!result.Contains(randIndex))
                    result.Add(randIndex);
            }

            return result.ToArray();
        }
    }
}
