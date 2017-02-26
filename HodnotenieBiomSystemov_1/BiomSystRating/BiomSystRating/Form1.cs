using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using BiomSystRating.Classes;
using Emgu.CV;
using Emgu.CV.Structure;
using FaceDetectionAndExtraction;
using FaceDetectionAndExtraction.Forms;

namespace BiomSystRating
{
    public partial class Form1 : Form
    {
        private PCAProjection pca;
        private Image<Gray, byte> imageFrame;
        public Form1()
        {
            InitializeComponent();

            var directory = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\Faces"));
            var content = Directory.GetFiles(directory);
            pca = new PCAProjection(content);


            //var form = new FaceDetectionForm();
            //form.Show();

            //var model = new PCAProjection();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            emguImageBox.Image = pca.tmpImage;
            //if (openFileDialog.ShowDialog() == DialogResult.OK)
            //{

            //    Image InputImg = Image.FromFile(openFileDialog.FileName);
            //    imageFrame = new Image<Gray, byte>(new Bitmap(InputImg));
            //    emguImageBox.Image = imageFrame;
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            VectorManilulation.NormalizeImage(imageFrame);
            emguImageBox.Image = imageFrame;
        }
    }
}
