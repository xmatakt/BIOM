using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.ML;
using Emgu.CV.ML.MlEnum;
using Emgu.CV.Structure;

namespace BcSvmClassificator.Classes
{
    class SVMClassificator
    {
        private Matrix<float> trainDataMatrix;
        private Matrix<int> trainClasses;
        private SVM model;
        private bool isTrained;

        public SVMClassificator()
        {
            
        }

        public SVMClassificator(Matrix<float> trainData, Matrix<int> trainClasses )
        {
            this.trainDataMatrix = trainData;
            this.trainClasses = trainClasses;
        }

        public void TrainClassificator(int kFold)
        {
            model = new SVM();
            var trainingData = new TrainData(trainDataMatrix, DataLayoutType.RowSample, trainClasses);
            isTrained = model.TrainAuto(trainingData, kFold);
        }

        /// <summary>
        /// Cross validacia SVM klasifikatora
        /// Prvky trenovacej a testovacej mnoziny su vyberane nahodne
        /// </summary>
        /// <param name="data">Vstupne data</param>
        /// <param name="xValCount">Stupen crossvalidacie (pocet podmnozin vstupnej mnoziny)</param>
        /// <param name="kFold">Stupen crossvalidacie pre natrenovanie EmguCV SVM modelu</param>
        /// <returns>Chyba klasifikatora</returns>  
        public void ValidateClassificator(DataStorage trainSet, DataStorage testSet, Error error, int kFold, double gamma, bool isRbfKernel)
        {
            //  natrenovanie modelu
            model = new SVM();
            trainDataMatrix = trainSet.ToMatrix();
            trainClasses = trainSet.GetLabelsMatrix();
            //var trainingData = new TrainData(trainDataMatrix, DataLayoutType.RowSample, trainClasses);
            //  http://www.emgu.com/wiki/files/3.0.0/document/html/87eaf2bf-4bb4-74b6-b750-2eaeeb5f3b6c.htm
            try
            {
                model.Gamma = gamma;
                model.SetKernel(isRbfKernel ? SVM.SvmKernelType.Rbf : SVM.SvmKernelType.Chi2);

                //isTrained = model.TrainAuto(trainingData, kFold);
                isTrained = model.Train(trainDataMatrix, DataLayoutType.RowSample, trainClasses);

            }
            catch (Exception e)
            {
                return;
            }

            //  ciastkovy vypocet chyby SVM klasifikatora
            if (!isTrained) return;

            var badClassifiedCounter = 0;
            foreach (var testData in testSet.Items)
            {
                var predictedLabel = Predict(testData);
                if (predictedLabel == null) continue;
                if ((int)predictedLabel != testData.Label)
                    badClassifiedCounter++;
            }

            error.TrainDataCount = trainSet.Items.Count;
            error.ClassificationError += badClassifiedCounter / (double)testSet.Items.Count;
        }

        public float? Predict(DataStorageItem data)
        {
            try
            {
                var matrix = new Matrix<float>(data.Data);
                matrix = matrix.Transpose();
                return model.Predict(matrix);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return null;
            }
        }
    }
}
