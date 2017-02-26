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
using FaceDetectionAndExtraction.CascadeClassifiers;

using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;

namespace FaceDetectionAndExtraction.Forms
{
    public partial class FaceDetectionForm : Form
    {
        public FaceDetectionForm()
        {
            InitializeComponent();
        }

        private void FaceDetectionForm_Load(object sender, EventArgs e)
        {
            var classifier = new CascadeClassifier(ClassifierPath.FrontalFaceAltTree());
        }

        private void loadImageButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Image InputImg = Image.FromFile(openFileDialog.FileName);
                Image<Bgr, byte> imageFrame = new Image<Bgr, byte>(new Bitmap(InputImg));
                emguImageBox.Image = imageFrame;

                //FaceDetection.DetectFaces(imageFrame);
                emguImageBox.Image = FaceDetection.GetImageAfterPCA(imageFrame);
                emguImageBox.Refresh();
            }
        }
    }
}
