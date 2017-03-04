using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Face;
using Emgu.CV.Structure;


namespace BiomSystRating.Classes
{
    class PCAProjection
    {
        private Matrix<double> inputMatrix;         //  matica, kde kazdy stpec predstavuje jeden obrazok
        private Mat mean = new Mat();               // create *empty* mean array so that PCACompute() calculates its own means
        private Matrix<double> eigenvectors;        // eigenvectors
        private double retainedVariance = 0.95;     // Percentage of variance that PCA should retain.
        private Emgu.CV.UI.ImageBox imageBox;
        public Image<Gray, byte> tmpImage;

        public PCAProjection(string[] imagePaths, Emgu.CV.UI.ImageBox imageBox)
        {
            this.imageBox = imageBox;
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
            //inputMatrix = new Matrix<double>(imagePaths.Length, height, 1);                 //  TODO: sirku matice urcit podla velkosti vstupnych obr

            var index = 0;
            foreach (var path in imagePaths)
            {
                var image = Image.FromFile(path);                                           //  TODO: doplnit kontrolu existencie suboru
                var emguImage = new Image<Gray, byte>(new Bitmap(image));

                //TODO: IMA DOCASNE, DAT PREC!
                //if (index == 0)
                //{
                //    imageBox.Image = emguImage; 
                //    imageBox.Refresh();                   
                //}
                //
                    
                var imageVector = VectorManilulation.GetImageVector(emguImage, true);

                for (var row = 0; row < height; row++)
                    inputMatrix[row, index] = imageVector[row, 0];

                index++;
            }

            //TODO:DATDOPICI
            var sb = new StringBuilder();

            sb.Append("{");
            for (var row = 0; row < inputMatrix.Height; row++)
            {
                sb.Append("{");
                for (var col = 0; col < inputMatrix.Width; col++)
                {
                    sb.Append(inputMatrix[row, col]);
                    if (col != inputMatrix.Width - 1)
                        sb.Append(",");
                }
                sb.Append("}");
                if (row != inputMatrix.Height - 1)
                    sb.Append(",");
                sb.AppendLine("");
            }
            sb.Append("}");

            System.IO.File.WriteAllText(@"C:\Users\Timotej\Desktop\mat.txt", sb.ToString());
            //TODO:DATDOPICI
        }

        private void CalculatePCA()
        {
            EigenFaceRecognizer recog = new EigenFaceRecognizer(2, 2);
            var predResult = recog.Predict(inputMatrix);
            //predResult.La


            var tmp = new Mat(1, 1, DepthType.Cv64F, 1).ToImage<Gray, double>();
            tmp.Data[0, 0, 0] = 0.543774509803921;

            var outputArray = new Matrix<double>(inputMatrix.Height, inputMatrix.Height);
            eigenvectors = new Matrix<double>(inputMatrix.Height, inputMatrix.Width);
            var outp = new Mat();
            try
            {
                CvInvoke.PCACompute(inputMatrix, mean, eigenvectors);
                CvInvoke.PCAProject(inputMatrix, mean, eigenvectors, outputArray);
                //TODO: DAT DOPICI
                for (int i = 0; i < 10; i++)
                {
                    tmpImage = VectorManilulation.GetImageFromVector(inputMatrix.GetCol(i), 16, 15);
                    imageBox.Image = tmpImage;
                    imageBox.Refresh();
                    Thread.Sleep(500);

                    tmp = new Mat(1, 1, DepthType.Cv64F, 1).ToImage<Gray, double>();
                    tmp.Data[0, 0, 0] = mean.ToImage<Gray, double>().Data[0, i, 0];

                    //CvInvoke.PCAProject(inputMatrix, mean, eigenvectors, outputArray);
                    //var haha = new Matrix<double>(inputMatrix.Height, inputMatrix.Height);
                    CvInvoke.PCAProject(inputMatrix.GetCol(i), tmp, eigenvectors.GetCol(0), outputArray);
                    //CvInvoke.PCAProject(inputMatrix, mean, eigenvectors, haha);

                    //imageBox.Image = VectorManilulation.GetInvertedImage(inputMatrix.GetCol(i), 16, 15);

                    tmpImage = VectorManilulation.GetImageFromVector(outputArray.GetCol(0), 16, 15);
                    //tmpImage = VectorManilulation.GetImageFromVector(haha.GetCol(i), 16, 15);
                    imageBox.Image = tmpImage;
                    imageBox.Refresh();
                    Thread.Sleep(500);
                }
                //

                //tmpImage = VectorManilulation.GetImageFromVector(outputArray.GetCol(0), 16, 15);

                var sum = 0.0;
                for (int i = 0; i < 432; i++)
                    sum += eigenvectors[188, i] * eigenvectors[189, i];

                sum = Math.Sqrt(sum);
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
