using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using IrisClassification.Classes;
using ZedGraph;

namespace IrisClassification
{
    public partial class Form1 : Form
    {
        private bool isEye1Loaded = false;
        private bool isEye2Loaded = false;
        private List<Image<Gray, byte>> irisData;
       
        public Form1()
        {
            InitializeComponent();

            irisData = new List<Image<Gray, byte>>();
        }

        private void eye1Button_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var eyeFileName = openFileDialog.FileName;
                if (eyeFileName.Contains("template") || eyeFileName.Contains("mask"))
                {
                    MessageBox.Show("You have to choose image of eye!");
                    isEye1Loaded = false;
                    return;
                }

                var maskFileName = eyeFileName.Replace(".bmp","_mask.bmp");
                var templateFileName = eyeFileName.Replace(".bmp", "_template.bmp");

                var image = new Image<Gray, byte>(eyeFileName);
                eye1_imageBox.Image = image;

                image = new Image<Gray, byte>(maskFileName);
                mask1_imageBox.Image = image;

                var image2 = new Image<Gray, byte>(templateFileName);
                template1_imageBox.Image = image2;

                xor1_imageBox.Image = IrisComparator.GetXorImage(image2, image);

                isEye1Loaded = true;

                EnableCompareButton();
            }
        }

        private void eye2Button_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var eyeFileName = openFileDialog.FileName;
                if (eyeFileName.Contains("template") || eyeFileName.Contains("mask"))
                {
                    MessageBox.Show("You have to choose image of eye!");
                    isEye2Loaded = false;
                    return;
                }

                var maskFileName = eyeFileName.Replace(".bmp", "_mask.bmp");
                var templateFileName = eyeFileName.Replace(".bmp", "_template.bmp");

                var image = new Image<Gray, byte>(eyeFileName);
                eye2_imageBox.Image = image;

                image = new Image<Gray, byte>(maskFileName);
                mask2_imageBox.Image = image;

                var image2 = new Image<Gray, byte>(templateFileName);
                template2_imageBox.Image = image2;

                xor2_imageBox.Image = IrisComparator.GetXorImage(image2, image);

                isEye2Loaded = true;

                EnableCompareButton();
            }
        }

        private void EnableCompareButton()
        {
            if (isEye1Loaded && isEye2Loaded)
                compareEyesButton.Enabled = true;
        }

        private void compareEyesButton_Click(object sender, EventArgs e)
        {
            var comparingResult = IrisComparator.GetHammingDistance((Image<Gray, byte>)xor1_imageBox.Image, (Image<Gray, byte>)xor2_imageBox.Image);

            angleLabel.Text = "Rotation angle: " + comparingResult.Angle + "°";
            hammDistanceLabel.Text = "Hamming distance: " + Math.Round(comparingResult.HammingDistance, 2);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Choose the folder containig iris data");

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                var folderName = folderBrowserDialog.SelectedPath;
                LoadIrisData(folderName);
            }
            else return;

            var histogramData = IrisComparator.CreateHistogram(irisData, 100);

            GraphPane pane = zedGraphControl1.GraphPane;
            pane.GraphObjList.Clear();
            PointPairList histList = new PointPairList();

            foreach (var keyPairValue in histogramData)
                histList.Add(keyPairValue.Key.Item1, keyPairValue.Value);

            for (int i = 0; i < histList.Count - 1; i++)
            {
                BoxObj box = new BoxObj(histList[i].X, histList[i].Y, histList[i + 1].X - histList[i].X, histList[i].Y);
                box.IsClippedToChartRect = true;
                box.Fill.Color = Color.FromArgb(255, 255, 0, 0);
                pane.GraphObjList.Add(box);
            }

            pane.XAxis.Scale.Min = 0;
            pane.XAxis.Scale.Max = 1;
            pane.YAxis.Scale.Min = 0;
            pane.YAxis.Scale.Max = histogramData.Values.Max();

            pane.Title.Text = "Histogram";
            pane.XAxis.Title.Text = "Relative Hamming distance";
            pane.YAxis.Title.Text = "Number of occurrences";

            zedGraphControl1.AxisChange();
            zedGraphControl1.Refresh();
        }

        private void LoadIrisData(string selectedPath)
        {
            foreach (var subDirectory in new DirectoryInfo(selectedPath).GetDirectories())
            {
                foreach (var subSubDirectory in new DirectoryInfo(subDirectory.FullName).GetDirectories())
                {
                    var masks = subSubDirectory.GetFiles().Where(x => x.Name.Contains("mask")).Select(x => x.FullName);
                    var templates =
                        subSubDirectory.GetFiles().Where(x => x.Name.Contains("template")).Select(x => x.FullName);

                    foreach (var mask in masks)
                    {
                        var parintgTemplate =
                            templates.First(x => x.Replace("_template", "") == mask.Replace("_mask", ""));
                        var maskImage = new Image<Gray, byte>(mask);
                        var templateImage = new Image<Gray, byte>(parintgTemplate);
                        irisData.Add(IrisComparator.GetXorImage(templateImage, maskImage));
                    }
                }
            }
        }
    }
}
