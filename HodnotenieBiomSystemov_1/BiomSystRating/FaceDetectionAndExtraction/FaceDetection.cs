using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Cuda;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using FaceDetectionAndExtraction.CascadeClassifiers;

namespace FaceDetectionAndExtraction
{
    public static class FaceDetection
    {
        static CascadeClassifier classifier = new CascadeClassifier(ClassifierPath.FrontalFaceAltDefault());

        /// <summary>
        /// Metoda preberie obrazok a vrati obrazok s detekovanymi tvarami
        /// </summary>
        /// <param name="imageFrame"></param>
        //http://blogs.interknowlogy.com/2013/10/21/face-detection-for-net-using-emgucv/
        public static void DetectFaces(Image<Bgr, byte> imageFrame)
        {
            Image<Gray, byte> grayFrame = imageFrame.Convert<Gray, byte>();

            var faces = classifier.DetectMultiScale(grayFrame, 1.1, 0, Size.Empty, Size.Empty);// new Size(20, 20), new Size(imageFrame.Width, imageFrame.Height));

            if (faces.Length <= 0) return;

            foreach (var face in faces)                 // Draw a colored rectangle on each detected face in image
                imageFrame.Draw(face, new Bgr(Color.Red), 2);
        }

        public static Image<Gray, byte> GetImageAfterPCA(Image<Bgr, byte> imageFrame)
        {
            var grayFrame = imageFrame.Convert<Gray, byte>();
            return ApplyPCA(grayFrame);
        }

        /// <summary>
        /// Analyza hlavnych komponentov vstupneho pola. Metoda vypocita prvych X hl. komponentov a 
        /// aplikuje na vstupne pole dat.
        /// </summary>
        /// <param name="inputArray"></param>
        private static Image<Gray, byte> ApplyPCA(IInputArray inputArray)
        {
            //var eigenvectors = new Mat(inputArray.GetInputArray().GetSize().Width,
            //    inputArray.GetInputArray().GetSize().Height, DepthType.Cv64F, 1);


            var mean = new Mat();           // create *empty* mean array so that PCACompute() calculates its own means
            var eigenvectors = new Mat();   // eigenvectors
            var retainedVariance = 0.95;      // Percentage of variance that PCA should retain.
                                            // Using this parameter will let the PCA decided how many components to retain but it will always keep at least 2.
            //IOutputArray outputArray = new Image<Gray, byte>(inputArray.GetInputArray().GetSize().Width,
            //    inputArray.GetInputArray().GetSize().Height);
            IOutputArray outputArray = new Mat(/*new Size(),*/ );
            IOutputArray outputArray2 = new Mat(/*new Size(),*/ );

            try
            {
                CvInvoke.PCACompute(inputArray, mean, eigenvectors, 7);
                CvInvoke.PCAProject(inputArray, mean, eigenvectors, outputArray);
                CvInvoke.PCABackProject(outputArray.GetOutputArray().GetMat(), mean, eigenvectors, outputArray2);

                System.Diagnostics.Debug.WriteLine(eigenvectors.GetValueRange().Max);
                System.Diagnostics.Debug.WriteLine(eigenvectors.GetValueRange().Min);

                var fero = eigenvectors.GetRow(0);
                var jano = mean.GetOutputArray().GetMat();
                var tmp = outputArray2.GetOutputArray().GetMat().ToImage<Gray, byte>();
                return tmp;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw;
            }
        }
    }
}
