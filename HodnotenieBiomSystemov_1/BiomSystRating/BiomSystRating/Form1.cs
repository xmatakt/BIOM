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
using System.Text.RegularExpressions;
using BiomSystRating.Classes;
using Emgu.CV;
using Emgu.CV.Structure;
using FaceDetectionAndExtraction;
using FaceDetectionAndExtraction.Forms;
using ZedGraph;

namespace BiomSystRating
{
    public partial class Form1 : Form
    {
        private PCAProjection pca;
        private Image<Gray, byte> imageFrame;
        private string[] content;
        public Form1()
        {
            InitializeComponent();

            var directory = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\Faces"));

            content = Directory.GetFiles(directory);
            var names = new List<string>();

            foreach (var path in content)
            {
                var personName = Regex.Replace(new FileInfo(path).Name.Replace(".png", ""), @"[0-9]", "");
                var dirName = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\Faces\" + personName));
                
                if (!Directory.Exists(dirName))
                    Directory.CreateDirectory(dirName);

                File.Copy(path, dirName +"\\"+ new FileInfo(path).Name);
                if(!names.Contains(personName))
                    names.Add(personName);
            }
            //pca = new PCAProjection(content, emguImageBox);

            foreach (var path in content)
            {
                var dirName = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\Faces\" + names[0]));    
            }
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
            pca = new PCAProjection(content, emguImageBox);
            //VectorManilulation.NormalizeImage(imageFrame);
            //emguImageBox.Image = imageFrame;
        }
    }
}
