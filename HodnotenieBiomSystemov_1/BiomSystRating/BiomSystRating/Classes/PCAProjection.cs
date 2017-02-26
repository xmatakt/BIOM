using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace BiomSystRating.Classes
{
    class PCAProjection
    {
        private Matrix<double> inputMatrix;         //  matica, kde kazdy stpec predstavuje jeden obrazok
        private Mat mean = new Mat();                // create *empty* mean array so that PCACompute() calculates its own means
        private Matrix<double> eigenvectors;        // eigenvectors
        private double retainedVariance = 0.95;     // Percentage of variance that PCA should retain.
        public Image<Gray, byte> tmpImage;

        public PCAProjection(string[] imagePaths)
        {
            CreateInputMatrix(imagePaths, 15*16);            
            CalculatePCA();
        }

        /// <summary>
        /// Vytvori vstupnu maticu pre PCA projekciu - jeden stlpec matice = jeden vstupny obrazok
        /// </summary>
        /// <param name="imagePaths">Cesty k obrazkom</param>
        /// <param name="height">Pocet riadkov matice = sirka * vyska obrazku</param>
        private void CreateInputMatrix(string[] imagePaths, int height)
        {
            inputMatrix = new Matrix<double>(height, imagePaths.Length, 1);                 //  TODO: sirku matice urcit podla velkosti vstupnych obr.

            var index = 0;
            foreach (var path in imagePaths)
            {
                var image = Image.FromFile(path);                                           //  TODO: doplnit kontrolu existencie suboru
                var emguImage = new Image<Gray, byte>(new Bitmap(image));
                var imageVector = VectorManilulation.GetImageVector(emguImage, true);

                for (var row = 0; row < height; row++)
                    inputMatrix[row, index] = imageVector[row, 0];

                index++;
            }
        }

        private void CalculatePCA()
        {
            var tmp = new Mat(1, 1, DepthType.Cv64F, 1).ToImage<Gray, double>();
            tmp.Data[0, 0, 0] = 0.543774509803921;

            var outputArray = new Matrix<double>(inputMatrix.Height, inputMatrix.Height);
            eigenvectors = new Matrix<double>(inputMatrix.Height, inputMatrix.Width);
            try
            {
                CvInvoke.PCACompute(inputMatrix, mean, eigenvectors);
                //CvInvoke.PCAProject(inputMatrix.GetCol(0), tmp, eigenvectors.GetCol(0), outputArray);
                CvInvoke.PCAProject(inputMatrix, mean, eigenvectors, outputArray);

                tmpImage = VectorManilulation.GetImageFromVector(outputArray.GetCol(0), 16, 15);
                //var sum = 0.0;
                //for (int i = 0; i < 432; i++)
                //    sum += eigenvectors[110, i] * eigenvectors[110, i];

                //sum = Math.Sqrt(sum);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            //CvInvoke.PCAProject(inputArray, mean, eigenvectors, outputArray);
            //CvInvoke.PCABackProject(outputArray.GetOutputArray().GetMat(), mean, eigenvectors, outputArray2);
        }
    }
}
