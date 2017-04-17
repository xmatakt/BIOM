using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;

namespace FaceClassification
{
    class DataSet
    {
        // list dvojic: id triedy - vektor
        public List<Tuple<int, Matrix<float>>> Data { get; set; }

        public DataSet()
        {
            Data = new List<Tuple<int, Matrix<float>>>();
        }

        public void Add(Image<Gray, byte> image, int label)
        {
            Data.Add(new Tuple<int, Matrix<float>>(label, Get1DMatrixFromImage(image)));
        }

        private Matrix<float> Get1DMatrixFromImage(Image<Gray, byte> image)
        {
            var matrix = new Matrix<float>(1, image.Width * image.Height);

            var index = 0;
            for (var row = 0; row < image.Height; row++)
            {
                for (var col = 0; col < image.Width; col++)
                {
                    matrix[0, index++] = image.Data[row, col, 0];
                }
            }

            return matrix;
        }
    }
}
