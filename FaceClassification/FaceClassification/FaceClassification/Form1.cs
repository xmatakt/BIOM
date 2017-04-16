using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;

namespace FaceClassification
{
    public partial class Form1 : Form
    {
        private const int maxComponents = 20;
        private string selectedFolder = "";
        private bool canContinue;
        private bool debug = true;
        private Dictionary<int, List<Image<Gray, byte>>> originalImages;
        private Dictionary<int, List<Image<Gray, byte>>> pcaImages;
        private DataSet trainingSet;
        private DataSet testSet;
        public Form1()
        {
            trainingSet = new DataSet();
            testSet = new DataSet();

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

            LoadImages();
            CreatePcaImages();
            GenerateDataSets();
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
            foreach (var keyPairValue in pcaImages)
            {
                var label = keyPairValue.Key;
                var images = keyPairValue.Value;
                // trenovacie data budu obsahovat 60% prvkov
                var randomIndexes = GenerateRandomIndexes((int)(images.Count * 0.6), images.Count);
                for (int i = 0; i < images.Count; i++)
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
    }
}
