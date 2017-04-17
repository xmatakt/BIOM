using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.ML;
using Emgu.CV.ML.MlEnum;

namespace FaceClassification.Classificators
{
    class SVMClassificator
    {
        private Matrix<float> trainDataMatrix;
        private Matrix<int> trainClasses;
        private SVM model;
        private bool isTrained;
        public List<int> GoodCount;
        public List<int> BadCount;

        public SVMClassificator()
        {
            GoodCount = new List<int>();
            BadCount = new List<int>();
        }

        public SVMClassificator(Matrix<float> trainData, Matrix<int> trainClasses)
        {
            GoodCount = new List<int>();
            BadCount = new List<int>(); 
            trainDataMatrix = trainData;
            this.trainClasses = trainClasses;
        }

        public void SetDatasets(Matrix<float> trainData, Matrix<int> trainClases)
        {
            trainDataMatrix = trainData;
            trainClasses = trainClases;
        }

        public void TrainClassificator()
        {
            model = new SVM();
            try
            {
                // alebo SVM.SvmKernelType.Inter
                model.SetKernel(SVM.SvmKernelType.Linear);
                isTrained = model.Train(trainDataMatrix, DataLayoutType.RowSample, trainClasses);
            }
            catch (Exception ex)
            {
                return;
            }
        }

        public void TestClassificator(Matrix<float> testData, Matrix<int> testLabels)
        {
            if (!isTrained)
                return;

            GoodCount.Add(0);
            BadCount.Add(0);
            for (var row = 0; row < testData.Rows; row++)
            {
                var label = testLabels[row, 0];
                var vector = GetVectorForPrediction(testData, row);
                try
                {
                    var predictedLabel = model.Predict(vector);
                    if (label == (int)predictedLabel)
                        GoodCount[0]++;
                    else
                        BadCount[0]++;
                }
                catch (Exception ex)
                {
                    return;
                }
            }
        }

        public void TestClassificator(Matrix<float> testData, Matrix<int> testLabels, int i)
        {
            if (!isTrained)
                return;
            GoodCount.Add(0);
            BadCount.Add(0);
            for (var row = 0; row < testData.Rows; row++)
            {
                var label = testLabels[row, 0];
                var vector = GetVectorForPrediction(testData, row);
                try
                {
                    var predictedLabel = model.Predict(vector);
                    if (label == (int)predictedLabel)
                        GoodCount[i]++;
                    else
                        BadCount[i]++;
                }
                catch (Exception ex)
                {
                    return;
                }
            }
        }

        private Matrix<float> GetVectorForPrediction(Matrix<float> matrix, int index)
        {
            return matrix.GetRow(index);
        }
    }
}
