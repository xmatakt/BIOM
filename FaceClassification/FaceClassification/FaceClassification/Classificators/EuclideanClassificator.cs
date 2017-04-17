using System;
using System.Collections.Generic;
using Emgu.CV;

namespace FaceClassification.Classificators
{
    class EuclideanClassificator
    {
        private Matrix<float> trainDataMatrix;
        private Matrix<int> trainClasses;
        public List<int> GoodCount;
        public List<int> BadCount;
        private bool isTrained;

        public EuclideanClassificator()
        {
            GoodCount = new List<int>();
            BadCount = new List<int>();
        }

        public EuclideanClassificator(Matrix<float> trainData, Matrix<int> trainClasses)
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
                    var distance = GetEuclideanDistance(vector1, vector2);
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
                    System.Diagnostics.Debug.WriteLine("Euclidean distance: " + (row + 10) + "/" + testData.Rows);
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
                    var distance = GetEuclideanDistance(vector1, vector2);
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

        //TODO: kontorlovat, ci maju vektory rovnake dimenzie
        // - funckia predpoklada, ze vstupne vektory su dimenzie 1 x N
        private double GetEuclideanDistance(Matrix<float> vector1, Matrix<float> vector2)
        {
            var distance = 0d;

            for (var col = 0; col < vector1.Width; col++)
                distance += Math.Pow(vector1[0, col] - vector2[0, col], 2);

            return Math.Sqrt(distance);
        }
    }
}
