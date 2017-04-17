using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms.VisualStyles;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace FaceClassification.Classificators
{
    class MahalanobisClassificator
    {
        private Matrix<float> trainDataMatrix;
        private Matrix<int> trainClasses;
        private Matrix<float> meanMatrix;
        private Matrix<float> covarianceMatrix;
        private Matrix<float> invertedCovarianceMatrix;

        public List<int> GoodCount;
        public List<int> BadCount;
        private bool isTrained;

        public MahalanobisClassificator()
        {
            GoodCount = new List<int>();
            BadCount = new List<int>();
        }

        public MahalanobisClassificator(Matrix<float> trainData, Matrix<int> trainClasses)
        {
            GoodCount = new List<int>();
            BadCount = new List<int>(); 
            trainDataMatrix = trainData;
            this.trainClasses = trainClasses;
            isTrained = true;
        }

        public void SetDatasets(Matrix<float> trainData, Matrix<int> trainClases)
        {
            trainDataMatrix = trainData;
            trainClasses = trainClases;
            isTrained = true;
        }

        public void TestClassificator(Matrix<float> testData, Matrix<int> testLabels)
        {
            if (!isTrained)
                return;

            GoodCount.Add(0);
            BadCount.Add(0);
            for (var row = 0; row < testData.Rows; row++)
            {
                var minimalDistance = double.MaxValue;
                var label = testLabels[row, 0];
                var predictedLabel = -1;
                var vector1 = testData.GetRow(row);

                //  porovnam s kazdym trenovacim vektorom a najdem minimum + prislusny label
                for (var i = 0; i < trainDataMatrix.Rows; i++)
                {
                    var vector2 = trainDataMatrix.GetRow(i);
                    var distance = GetMahalanobisDistance(vector1, vector2);
                    if (distance < minimalDistance)
                    {
                        minimalDistance = distance;
                        predictedLabel = trainClasses[i, 0];
                    }
                }

                if (label == predictedLabel)
                    GoodCount[0]++;
                else
                    BadCount[0]++;

                if (row % 10 == 0)
                    System.Diagnostics.Debug.WriteLine("Mahalanobis distance: " + (row + 10) + "/" + testData.Rows);
            }
        }

        public void TestClassificator(Matrix<float> testData, Matrix<int> testLabels, int j)
        {
            if (!isTrained)
                return;

            GoodCount.Add(0);
            BadCount.Add(0);
            for (var row = 0; row < testData.Rows; row++)
            {
                var minimalDistance = double.MaxValue;
                var label = testLabels[row, 0];
                var predictedLabel = -1;
                var vector1 = testData.GetRow(row);

                //  porovnam s kazdym trenovacim vektorom a najdem minimum + prislusny label
                for (var i = 0; i < trainDataMatrix.Rows; i++)
                {
                    var vector2 = trainDataMatrix.GetRow(i);
                    var distance = GetMahalanobisDistance(vector1, vector2);
                    if (distance < minimalDistance)
                    {
                        minimalDistance = distance;
                        predictedLabel = trainClasses[i, 0];
                    }
                }

                if (label == predictedLabel)
                    GoodCount[j]++;
                else
                    BadCount[j]++;

                if (row % 10 == 0)
                    System.Diagnostics.Debug.WriteLine("Euclidean distance: " + (row + 10) + "/" + testData.Rows +
                                                       " at iteration " + (j + 1));
            }
        }

        //  predpoklad je, ze vektory vector1 a vector2 su typu 1 x N
        private double GetMahalanobisDistance(Matrix<float> vector1, Matrix<float> vector2)
        {
            try
            {
                if (meanMatrix == null)
                    ComputeNeededMatrices();

                var distance = 0d;
                var diff = VectorsDiff(vector1, vector2);
                for (var i = 0; i < diff.Width; i++)
                {
                    var sum = 0d;
                    for (var j = 0; j < invertedCovarianceMatrix.Height; j++)
                    {
                        sum += diff[0, j] * invertedCovarianceMatrix[j, i];
                    }
                    sum *= diff[0, i];
                    distance += sum;
                }

                if (distance < 0)
                    return double.MaxValue;  

                return Math.Sqrt(distance);
            }
            catch (Exception ex)
            {
                return double.MaxValue;
            }
        }

        private void ComputeNeededMatrices()
        {
            try
            {
                meanMatrix = GetMeanMatrix(trainDataMatrix);
                covarianceMatrix = GetCovarianceMatrix(trainDataMatrix, meanMatrix);
                invertedCovarianceMatrix = new Matrix<float>(covarianceMatrix.Width, covarianceMatrix.Width);
                CvInvoke.Invert(covarianceMatrix, invertedCovarianceMatrix, DecompMethod.Svd);
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private Matrix<float> GetInputMatrix(Matrix<float> vector1, Matrix<float> vector2)
        {
            var result = new Matrix<float>(2, vector1.Cols);
            for (var col = 0; col < vector1.Cols; col++)
            {
                result[0, col] = vector1[0, col];
                result[1, col] = vector2[0, col];
            }

            return result;
        }

        //  vstupna matica je typu M x N, kde:
        //      - N je rozmer nahodneho vektora
        //      - M je pocet pozorovani
        private Matrix<float> GetMeanMatrix(Matrix<float> matrix)
        {
            var meanMatrix = new Matrix<float>(matrix.Width, 1);

            for (var col = 0; col < matrix.Width; col++)
                meanMatrix[col, 0] = GetMeanOfVector(matrix.GetCol(col));

            return meanMatrix;
        }

        //  funkcia predpoklada vektor typu M x 1
        private float GetMeanOfVector(Matrix<float> vector)
        {
            var mean = 0f;
            for (var row = 0; row < vector.Height; row++)
                mean += vector[row, 0];

            return mean /= (vector.Height);
        }

        //  vstupna matica (matrix) musi byt typu M x N, kde:
        //      - N je rozmer nahodneho vektora
        //      - M je pocet pozorovani
        //  meanVector musi byt typu M x 1
        private Matrix<float> GetCovarianceMatrix(Matrix<float> matrix, Matrix<float> meanVector)
        {
            var n = matrix.Height;
            var cov = new Matrix<float>(meanVector.Height, meanVector.Height);

            for (var i = 0; i < n; i++)
            {
                var Xi = matrix.GetRow(i).Transpose();
                var diff = VectorsDiff(Xi, meanVector);

                for (var row = 0; row < cov.Height; row++)
                {
                    for (var col = 0; col < cov.Width; col++)
                    {
                        cov[row, col] += (diff[row, 0] * diff[col, 0]) * (1 / (float) (n - 1));
                        //cov[row, col] /= (n - 1);
                    }
                }
            }

           // MultiplyMatrixByScalar(cov, 1 / (float) (n - 1));
            return cov;
        }

        //  oba vektory musia mat rovanky rozmer
        private Matrix<float> VectorsDiff(Matrix<float> v1, Matrix<float> v2)
        {
            var result = new Matrix<float>(v1.Height, v1.Width);

            for (var row = 0; row < v1.Height; row++)
                for (var col = 0; col < v1.Width; col++)
                    result[row, col] = v1[row, col] - v2[row, col];

            return result;
        }

        private void MultiplyMatrixByScalar(Matrix<float> matrix, float value)
        {
            for (var row = 0; row < matrix.Height; row++)
            for (var col = 0; col < matrix.Width; col++)
                matrix[row, col] *= value;
        }
    }
}
